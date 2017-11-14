using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;
using System.Linq;


namespace Puzzle.Lasers {

	public class laserSystem : MonoBehaviour {

		static public laserSystem instance;

		[SerializeField, HideInInspector]
		public bool infProt = true;
		[SerializeField, HideInInspector]
		public bool controlSwitch = true;
		[SerializeField, HideInInspector]
		public bool mode2D = false;
		[SerializeField, HideInInspector]
		public float hit2DDistance = 0.01f;

		private Vector3 forwardV = Vector3.forward;

		[SerializeField, HideInInspector]
		public float rayL = 30;
		[SerializeField, HideInInspector]
		public bool intersect = true;
		[SerializeField, HideInInspector]
		public bool reflect = true;
		[SerializeField, HideInInspector]
		public bool reflectLRemainder = true;
		[SerializeField, HideInInspector]
		public bool reflectStCap = true;
		[SerializeField, HideInInspector]
		public bool updateLrPos = true;
		[SerializeField, HideInInspector]
		public bool endCapB = true;
		[SerializeField, HideInInspector]
		public bool startCapB = true;
		[SerializeField, HideInInspector]
		public bool hitObjB = true;
		[SerializeField, HideInInspector]
		public bool hitRefl = true;
		[SerializeField, HideInInspector]
		public bool canHit = true;
		[SerializeField, HideInInspector]
		public GameObject laserObj;
		[SerializeField, HideInInspector]
		public GameObject intSctObj;
		[SerializeField, HideInInspector]
		public GameObject hitObj;
		[SerializeField, HideInInspector]
		public GameObject startCapObj;
		[SerializeField, HideInInspector]
		public GameObject endCapObj;
		[SerializeField, HideInInspector]
		public bool onSys = true;
		[SerializeField, HideInInspector]
		public LayerMask hitLayerM;
		[SerializeField, HideInInspector]
		public float minIntDist = 0.05f;
		[SerializeField, HideInInspector]
		public Material[] allowedMat;
		[SerializeField, HideInInspector]
		public GameObject[] allowedMatObj;
		[SerializeField, HideInInspector]
		public Material[] passMat;
        [SerializeField, HideInInspector]
        public GameObject[] allowedTagObj;
		[SerializeField, HideInInspector]
		public bool lasLineREff = true;
		[SerializeField, HideInInspector]
		public float reflInc = 1;
		[SerializeField, HideInInspector]
		public float[] reflMatInc;
		[SerializeField, HideInInspector]
		public float[] pasMatInc;
        [SerializeField, HideInInspector]
        public string[] reflTag;
        [SerializeField, HideInInspector]
        public float[] reflTagInc;
        [SerializeField, HideInInspector]
        public string[] pasTag;
        [SerializeField, HideInInspector]
        public float[] pasTagInc;
        [SerializeField, HideInInspector]
        public int hitScanMode = 0;
		[SerializeField, HideInInspector]
		public float lrWidth = 0.2f;
		[SerializeField, HideInInspector]
		public float desSpObj = 5;
		[SerializeField, HideInInspector]
		public bool disalbePartObj = true;
    
		[SerializeField, HideInInspector]
		public int infLM = 0;
		[SerializeField, HideInInspector]
		public int infLI = 0;

		private List<lFxObj> fxObjs = new List<lFxObj>();
		private List<GameObject> fxObjsGo = new List<GameObject>();
		private List<GameObject> fxObjsGo2 = new List<GameObject>();
		private Dictionary<GameObject, lFxObj> lasObjsD = new Dictionary<GameObject, lFxObj>();
		private Dictionary<GameObject, lFxObj> fxObjsD = new Dictionary<GameObject, lFxObj>();
		private List<GameObject> delFlag = new List<GameObject>();

		private RaycastHit hit;
		private Vector3 fwd2;
		private laser[] lSources;
		private List<laser> lSources2 = new List<laser>();
		private List<laser> lSources3 = new List<laser>();
		private laserStObj[] laserSt = new laserStObj[0];
		private Vector3 overRideV3;
		private LineRenderer lrO;
		private Vector3 t1;
		private Vector3 t2;
		private bool para1;
		private bool exclGO;
		private Vector3 drawGiz;
		private bool isHit;

		private GameObject intersectGO;
		private float tDist;

		[SerializeField, HideInInspector]
		public bool enablePool;

		private List<laserStObj> lSrt = new List<laserStObj>();

		private int i = 0;
		private float[] tInt;
		private int iM1 = 0;
		private int i2 = 0;
		private int iM = 0;
		private laser tL1;
		private laser tL2;
		private laserStObj l1;
		private laserStObj l2 = null;
		private laserStObj l3 = null;
		private List<laserStObj> lStack = new List<laserStObj>();
		private int lC;
		private bool rayPass;
		private int rayPassI;
		private bool reflMatT;
		private bool goMat;
		private Vector3 tv3;
		private bool bar = false;
		private destroyFX tDfx;


		float a;
		float b;
		float e;
		float d;
		Vector3 r;
		float c;
		float f;
		float s;
		float t;
		Vector3 lineVec;
		Vector3 pointVec;
		float dot;

		Quaternion q1;

		int maxI = 0;

		[SerializeField, HideInInspector]
		public float nextSc = 5;
		private float nextScT = 0;

		bool onDel = false;

		private void Awake() {
			instance = this;
			nextScT = Time.time + nextSc;
		}

		// Use this for initialization
		void Start() {
		}

		// Update is called once per frame
		void FixedUpdate() {
			if (laserObj != null) {
				if (laserObj.GetComponent<laser>()) {
					if (onSys) {
						if (enablePool && Pool.instance == null) {
							Debug.Log("Laser System: No gameobject pool available, disabling pool");
							enablePool = false;
						}
						
						{
							forwardV = Vector3.forward;
						}
						updateLSources();
						loadAll();
						scanHits();
						scanLines();
						sortAll();
						findShrt();
						if (onDel != onSys) {
							onDel = onSys;
						}
                    }
				} 
		}
            }

		private void Update() {
			if (laserObj != null) {
				if (laserObj.GetComponent<laser>()) {
					if (onSys) {
						delFlagged();
						updateLineR();
						if (Time.time > nextScT) {
							checkFxObjs();
						}
						if (onDel != onSys) {
							onDel = onSys;
						}
					} else {
						if (onDel != onSys) {
							onDel = onSys;
							reset();
							Invoke("delFlagged", 0.1f);
						}
					}
				}
			}

		}
		void delFlagged() {
			for (i = 0; i < delFlag.Count; i++) {
				if (delFlag[i]) {
					destroyThis(delFlag[i], desSpObj);
				}
			}
			delFlag.Clear();
		}

		public void addLas(laser las) {
			lSources3.Add(las);
		}

		public void destL(GameObject go) {
			resetLfxobj(getlFxObj(go));
			lSources3.Remove(go.GetComponent<laser>());
		}

		public void sDestL(GameObject go) {
			removelFxObjCh(getlFxObj(go));
			lSources3.Remove(go.GetComponent<laser>());
		}

        void updateLSources() {
            if (enablePool) {
                lSources2 = lSources3;
                maxI = lSources2.Count;
                if (maxI < 0) {
                    maxI = 0;
                }
                if (laserSt.Length < maxI) {
                    laserSt = new laserStObj[maxI];
                    for (i = 0; i < lSrt.Count; i++) {
                        laserSt[i] = lSrt[i];
                    }
                }
            } else {
                lSources = FindObjectsOfType(typeof(laser)) as laser[];
                lSources2 = lSources.ToList();
                maxI = lSources2.Count;
                if (maxI < 0) {
                    maxI = 0;
                }
                laserSt = new laserStObj[maxI];
            }
		}

		public class lFxObj {

			public GameObject go = null;
			public GameObject sc = null;
			public GameObject hic = null;
			public GameObject ec = null;
			public int type = 0;
			public bool inf = false;
			public GameObject cL = null;
			public laserStObj lS = null;
			public bool hit = false;

			public lFxObj lFX2 = null;

			public lFxObj() {
			}

		}

		public class laserStObj {
			public laser scriptL;
			public Vector3 intPos = Vector3.zero;
			public int intGoI = -1;
			public GameObject intSctGo = null;
			public float intSctDist = 0;
			public int myInt = -1;

			public GameObject gameObjectSt;

			public GameObject hitGO = null;
			public Vector3 hitA = Vector3.zero;
			public Vector3 hitNorm = Vector3.zero;
			public float hitDist = 0;
			public int shrtA = -1;
			public Vector3[] intSctA;
			public laserStObj[] hitOrder;
			public float[] hitInts;

			public Vector3 fwd = Vector3.zero;
			public Vector3 sPos = Vector3.zero;

			public float length;
			public float sLength;
			public bool intersectMe;
			public bool reflectMe;
			public bool hitReflectMe;
			public bool hitMe;
			public bool reflLenRemainder;
			public LayerMask hitLayerMe;
			public bool hitReflMatMe;
			public Material[] allowedMatsMe;
			public Material[] passedMatsMe;
            public string[] reflTagMe;
            public float[] reflTagIncMe;
            public string[] pasTagMe;
            public float[] pasTagIncMe;
            public GameObject[] allowedTagObj;
            public int hitScanModeMe = 0;
            public bool canRefl;
			public bool lasLineREffMe = true;
			public float reflIncMe = 2;
			public float chargeIncMe = 2;
			public float[] reflMatIncMeA;
			public GameObject[] allowedMatsObjMe;
			public float reflMatIncMe = 1f;
			public float[] pasMatIncMeA;
			public float pasMatIncMe = 1f;
			public float lrWidthMe = 0.2f;
			public bool blockTypeMe = false;
			public float blockThresholdMe = -1;
			public bool on = true;

			public GameObject hitGOM = null;
            public Material hitMat = null;

            public string hitTag = "";

            public GameObject laserObj = null;
			public GameObject intSctObj = null;
			public GameObject hitObj = null;
			public GameObject startCapObj = null;
			public GameObject endCapObj = null;

			public bool hasHitR = false;
			public bool hasInt = false;
			public laserStObj lasSt = null;

			public bool infL = false;
			public bool isChild;
			public string groupId = "";

			public List<laserStObj> lOS = new List<laserStObj>();

			public bool firstRun = false;

			public float curPower = 1;

			public int charged = 0;
			public bool inCol = false;

			public laserStObj() {

			}

			public void reset() {
				scriptL = null;
				intPos = Vector3.zero;
				intGoI = -1;
				intSctGo = null;
				intSctDist = 0;
				myInt = -1;

				gameObjectSt = null;

				hitGO = null;
				hitA = Vector3.zero;
				hitNorm = Vector3.zero;
				hitDist = 0;
				shrtA = -1;
				intSctA = null;
				//hitOrder.Clear();
				hitInts = null;

				fwd = Vector3.zero;
				sPos = Vector3.zero;

				length = 0;
				sLength = 0;
				intersectMe = false;
				reflectMe = false;
				hitReflectMe = false;
				hitMe = false;
				reflLenRemainder = false;
				hitLayerMe = 0;
				hitReflMatMe = false;
				allowedMatsMe = null;
				passedMatsMe = null;
				canRefl = false;
				lasLineREffMe = true;
				reflIncMe = 2;
				reflMatIncMeA = null;
				allowedMatsObjMe = null;
				reflMatIncMe = 1f;
				pasMatIncMeA = null;
				pasMatIncMe = 1f;
				lrWidthMe = 0.2f;
				blockTypeMe = false;
				blockThresholdMe = -1;
				on = true;

				hitGOM = null;
				hitMat = null;

                hitTag = "";
				laserObj = null;
				intSctObj = null;
				hitObj = null;
				startCapObj = null;
				endCapObj = null;
				hasHitR = false;
				hasInt = false;
				lasSt = null;
				infL = false;
				isChild = false;
				groupId = "";
				lOS.Clear();
				firstRun = false;
				inCol = false;
			}

			public void init(float rayL, bool intersectMeV, bool reflectMeV, bool hitReflectMeV, bool hitMeV, bool reflRemV, LayerMask hitLayerMeV, bool lasLineREffMeV, float reflIncMeV, float lrWidthMeV, GameObject gO,
				GameObject laserObjMeV, GameObject intSctObjMeV, GameObject hitObjMeV, GameObject startCapObjMeV,
				GameObject endCapObjMeV, GameObject[] allowedMatsObjMeV,
                string[] reflTagMeV,
                float[] reflTagIncMeV,
                GameObject[] allowedTagObjMeV,
                int hitScanModeMeV) {
				gameObjectSt = gO;
				length = rayL;
				sLength = length;
				intersectMe = intersectMeV;
				reflectMe = reflectMeV;
				hitReflectMe = hitReflectMeV;
				hitMe = hitMeV;
				reflLenRemainder = reflRemV;
				hitLayerMe = hitLayerMeV;

				lasLineREffMe = lasLineREffMeV;
				reflIncMe = reflIncMeV;
				lrWidthMe = lrWidthMeV;
				laserObj = laserObjMeV;
				intSctObj = intSctObjMeV;
				hitObj = hitObjMeV;
				startCapObj = startCapObjMeV;
				endCapObj = endCapObjMeV;
                reflTagMe = reflTagMeV;
                reflTagIncMe = reflTagIncMeV;
                allowedTagObj = allowedTagObjMeV;
                hitScanModeMe = hitScanModeMeV;
				if (gameObjectSt != null) {
					if (scriptL = gameObjectSt.GetComponent<laser>()) {
						isChild = scriptL.isChild;
						if (!scriptL.useGlobal) {
                            if (scriptL.localLength) {
								length = scriptL.length;
							}
							if (scriptL.localIntersect) {
								intersectMe = scriptL.intersectE;
							}
							if (scriptL.localIntersectReflect) {
								reflectMe = scriptL.reflectE;
							}
							if (scriptL.localHitReflect) {
								hitReflectMe = scriptL.hitReflectE;
							}
							if (scriptL.localHit) {
								hitMe = scriptL.hitE;
							}
							if (scriptL.localIntersectRemain) {
								reflLenRemainder = scriptL.reflLenRemE;
							}
							if (scriptL.localHitMask) {
								hitLayerMe = scriptL.hitLayerM;
							}						
							if (scriptL.localLineRenderer) {
								lasLineREffMe = scriptL.lasLineREff;
							}
							if (scriptL.localReflectionPowerInc) {
								reflIncMe = scriptL.reflInc;
							}
							if (scriptL.localLineRendererWidth) {
								lrWidthMe = scriptL.lrWidth;
							}
							if (scriptL.localLaserObj) {
								laserObj = scriptL.laserObj;
							}
							if (scriptL.localIntSctObj) {
								intSctObj = scriptL.intSctObj;
							}
							if (scriptL.localHitObj) {
								hitObj = scriptL.hitObj;
							}
							if (scriptL.localStartCapObj) {
								startCapObj = scriptL.startCapObj;
							}
							if (scriptL.localEndCapObj) {
								endCapObj = scriptL.endCapObj;
							}
						}

						firstRun = scriptL.firstRun;
						on = scriptL.onM;
						blockTypeMe = scriptL.blockType;
						blockThresholdMe = scriptL.blockThreshold;
						groupId = scriptL.groupId;
						if (!isChild) {
							groupId = gameObjectSt.GetInstanceID().ToString();
							scriptL.origin1 = gameObjectSt;
							scriptL.origin2 = gameObjectSt;
							scriptL.groupId = groupId;
						}
						if (scriptL.useRem && scriptL.isChild) {
							sLength = scriptL.remLength;
						} else {
							sLength = length;
						}
					} else {
						Debug.Log("Laser class has no laser script attached, using global values");
					}
				} else {
					Debug.Log("Laser class has no laser script attached, using global values");
				}
				intSctDist = length;
				canRefl = false;
			}

			public void updateScript() {
				if (gameObjectSt != null) {
					if (scriptL = gameObjectSt.GetComponent<laser>()) {
						scriptL.intPos = intPos;
						scriptL.sPos = sPos;
						scriptL.intGoI = intGoI;
						scriptL.intSctGo = intSctGo;
						scriptL.gameObjectSt = gameObjectSt;
						scriptL.hitGO = hitGO;
						scriptL.hitA = hitA;
						scriptL.hitNorm = hitNorm;
						scriptL.fwd = fwd;
						scriptL.shrtA = shrtA;
						scriptL.intSctA = intSctA;
						if (hitDist <= intSctDist) {
							scriptL.hitDist = hitDist;
						} else {
							scriptL.hitDist = intSctDist;
						}
						scriptL.curPower = curPower;
						scriptL.infL = infL;
						scriptL.hasHit = hasHitR;
						scriptL.hasInt = hasInt;
					} else {
                        if (laserSystem.instance.enablePool) {
                            Debug.Log("Laser class has no laser script attached, skipping update");
                        }
					}
				} else {
                    if (laserSystem.instance.enablePool) {
                        Debug.Log("Laser class has no gameobject attached, skipping update");
                    }
				}
			}
		}

		public int PointOnWhichSideOfLineSegment(Vector3 linePoint1, Vector3 linePoint2, Vector3 point) {
			lineVec = linePoint2 - linePoint1;
			pointVec = point - linePoint1;
			dot = Vector3.Dot(pointVec, lineVec);
			if (dot > 0) {
				if (pointVec.magnitude <= lineVec.magnitude) {
					return 0;
				} else {
					return 2;
				}
			} else {
				return 1;
			}
		}
	void loadAll() {
			for (i = 0; i < maxI; i++) {
				//create all classes and variables
				tL1 = lSources2[i];
				if (laserSt[i] == null) {
					laserSt[i] = new laserStObj();
				} else {
					laserSt[i].reset();
				}
				laserSt[i].init(rayL, intersect, reflect, hitRefl, canHit, reflectLRemainder, hitLayerM, lasLineREff, reflInc, lrWidth, tL1.gameObject,
				laserObj, intSctObj, hitObj, startCapObj, endCapObj, allowedMatObj,
                reflTag, reflTagInc, allowedTagObj, hitScanMode);
			}
		}
        void doHitT(string tag) {
            reflMatT = false;
            for (i2 = 0; i2 < l1.reflTagMe.Length; i2++) {
                if (tag == l1.reflTagMe[i2]) {
                    if (l1.reflTagIncMe.Length >= i2) {
                        l1.reflMatIncMe = l1.reflTagIncMe[i2];
                        l1.hitTag = l1.reflTagMe[i2];
                    }
                    reflMatT = true;
                }
            }
        }
        void scanHits() {
			for (i = 0; i < maxI; i++) {
				tL1 = lSources2[i];
				l1 = laserSt[i];
				l1.myInt = i;
				l1.hitGOM = l1.hitObj;
				l1.intSctA = new Vector3[maxI];
				l1.hitInts = new float[maxI];
				l1.hitOrder = new laserStObj[maxI];
                if (l1.on && l1.scriptL.curPower > 0) {
                    l1.sPos = tL1.transform.position;
                    fwd2 = lSources2[i].transform.TransformPoint(forwardV * l1.sLength);
                    l1.fwd = fwd2;
                    l1.intPos = fwd2;
                    l1.hitA = fwd2;
                    l1.hitNorm = Vector3.zero;
                    l1.hitDist = l1.sLength;
                    rayPass = true;
                    rayPassI = 0;
                    if (l1.hitMe) {
                        if (!mode2D) {
                            if (Physics.Linecast(lSources2[i].transform.position, fwd2, out hit, l1.hitLayerMe)) {
                                if (l1.hitScanModeMe == 1) {
                                    while (rayPass == true) {
                                        if (rayPassI > 0) {
                                            Physics.Linecast(hit.point, fwd2, out hit, l1.hitLayerMe);
                                        }
                                        reflMatT = false;
                                        rayPass = false;
                                        if (hit.collider) {                                       
                                            if (!rayPass) {
                                                l1.hitA = hit.point;
                                                l1.hitNorm = hit.normal;
                                                l1.hitDist = Vector3.Distance(tL1.transform.position, hit.point);
                                                l1.hitGO = hit.collider.gameObject;
                                            }
                                            rayPassI++;
                                        }
                                    }
                                } else {                                 
                                    doHitT(hit.collider.tag);
                                    l1.hitA = hit.point;
                                    l1.hitNorm = hit.normal;
                                    l1.hitDist = Vector3.Distance(tL1.transform.position, hit.point);
                                    l1.hitGO = hit.collider.gameObject;
                                    if (l1.hitReflectMe) {
                                        l1.canRefl = reflMatT;
                                    }
                                }
                            }
                        
                            }
                        }
                    }
                }
			}
		void scanLines() {
			for (i = 0; i < maxI; i++) {
				l1 = laserSt[i];
				tL1 = lSources2[i];
				fwd2 = tL1.transform.TransformPoint(forwardV * l1.sLength);
				for (i2 = 0; i2 < maxI; i2++) {
					l1.intSctA[i2] = Vector3.zero;
					l1.hitInts[i2] = l1.sLength;
					l1.hitOrder[i2] = laserSt[i2];
					if (l1.on && !l1.inCol && laserSt[i2].on && l1.scriptL.curPower > 0 && laserSt[i2].scriptL.curPower > 0) {
						tL2 = laserSt[i2].gameObjectSt.GetComponent<laser>();
						if (tL1.parent1 != laserSt[i2].gameObjectSt && tL1.parent2 != laserSt[i2].gameObjectSt) {
							if (tL2.parent1 != tL1.gameObjectSt && tL2.parent2 != tL1.gameObjectSt) {
								bar = false;
								if (lSources2[i2].gameObject != null && l1 != null) {
									if (infProt) {
										if (l1.scriptL.groupId.Contains(laserSt[i2].scriptL.groupId) || laserSt[i2].scriptL.groupId.Contains(l1.scriptL.groupId)) {
										}
									}
                                }
							}
						}
					}
				}
			}
		}
										
		void sortAll() {
			for (i = 0; i < maxI; i++) {
				l1 = laserSt[i];
				tInt = (float[])l1.hitInts.Clone();
				Array.Sort(tInt, l1.hitOrder);
				tInt = null;
			}
			lSrt.Clear();
			lSrt.AddRange(laserSt);
		}
		void updateLineR() {
			l1 = null;
			l2 = null;
			for (i = 0; i < maxI; i++) {
                if (laserSt[i] != null) {
                    l1 = laserSt[i];
                    if (l1.gameObjectSt) {
                        l1.intSctDist = Vector3.Distance(l1.gameObjectSt.transform.position, l1.intPos);
                    }
                }
			}
			l1 = null;
			for (i = 0; i < maxI; i++) {
				l2 = null;
                if (laserSt[i] != null) {
                    l1 = laserSt[i];
                    if (l1.intGoI != -1) {
                        if (laserSt[l1.intGoI] != null) {
                            l2 = laserSt[l1.intGoI];
                        }
                    }
                    if (l1.gameObjectSt) {
                        createIntSect(l1, l2);
                    }
                }
			}
			for (i = 0; i < maxI; i++) {
				l1 = laserSt[i];
                if (laserSt[i] != null) {
                    if (l1.lasLineREffMe) {
                        if (l1.gameObjectSt) {
                            updateLrW(l1.gameObjectSt, l1.lrWidthMe, l1.scriptL.curPower);
                        }
                    }
                    if (updateLrPos) {
                        if (!l1.firstRun) {
                            tv3 = l1.intPos;
                        } else {
                            if (l1.gameObjectSt) {
                                tv3 = l1.gameObjectSt.transform.position;
                            }
                            l1.scriptL.firstRun = false;
                        }
                        if (l1.gameObjectSt) {
                            setLR(tv3, l1.gameObjectSt);
                        }
                    }
                    l1.updateScript();
                }
			}
		}
		void checkFxObjs() {
			for (i = 0; i < fxObjs.Count; i++) {
				bar = false;
				if (!fxObjs[i].go || !fxObjs[i].go.activeSelf) {
					bar = true;
				}
				if (bar) {
					removelFxObjCh(fxObjs[i]);
				}
			}
			fxObjsGo2 = fxObjsGo;
			for (i = 0; i < fxObjsGo.Count; i++) {
				if (fxObjsGo[i] && fxObjsGo[i].activeSelf) {
                    if (fxObjsD.ContainsKey(fxObjsGo[i])) {
                        if (fxObjsD[fxObjsGo[i]].hic != fxObjsGo[i]
                            &&
                            fxObjsD[fxObjsGo[i]].ec != fxObjsGo[i]
                            &&
                            fxObjsD[fxObjsGo[i]].sc != fxObjsGo[i]
                            &&
                            fxObjsD[fxObjsGo[i]].cL != fxObjsGo[i]) {
                            destroyThis(fxObjsGo[i], desSpObj);
                            fxObjsD.Remove(fxObjsGo[i]);
                            fxObjsGo2.RemoveAt(i);
                        }
                    }
				} else {
					fxObjsGo2.RemoveAt(i);
				}
			}
			fxObjsGo = fxObjsGo2;
		}
		public void reset() {
			for (i = 0; i < fxObjs.Count; i++) {
				resetLfxobj(fxObjs[i]);
			}
			lSources3.Clear();
			updateLSources();
			checkFxObjs();
		}
		void findShrt() {
			if (laserSt.Length > 0) {
				lStack.AddRange(laserSt);
				l2 = null;
				l3 = null;
				lC = 0;
				i = 0;
				i2 = 0;
				for (i = 0; i < lStack.Count; i++) {
					l1 = lStack[i];
					if (!l1.inCol) {
						l1.intPos = l1.fwd;
					} else {
						l1.intPos = l1.gameObjectSt.transform.position;
						l1.hitA = l1.intPos;
					}
					l1.intGoI = -1;
					l1.lOS.AddRange(l1.hitOrder);

				}
			while (lStack.Count > 0 && lStack.Count * lStack.Count > lC) {
					l1 = lStack[0];
					lStack.Remove(l1);
					if (l1.on) {
						for (i = 0; i < l1.lOS.Count; i++) {
							l2 = l1.lOS[i];
							if (!l2.intersectMe || l2.lOS.Count <= 0 || !lStack.Contains(l2) || l1.sLength == l1.hitInts[l2.myInt] || l1.intSctA[l2.myInt] == Vector3.zero) {
								l1.lOS.Remove(l2);
							}
						}
						if (l1.lOS.Count > 0 && l1.intersectMe) {
							l2 = l1.lOS[0];
							if (l1.hitDist < l1.hitInts[l2.myInt]) {
								l1.intPos = l1.hitA;
								l1.intGoI = -1;
								lC = 0;
								l1.hasHitR = true;
							} else {
								if (l2.blockTypeMe == l1.blockTypeMe && l2.intersectMe && l2.lOS.Count > 0 && l1.myInt < l2.intSctA.Length && l2.myInt < l1.intSctA.Length && l1.myInt < l2.hitInts.Length) {
									if (l2.lOS[0] == l1 && l2.hitDist > l2.hitInts[l1.myInt] && l1.intSctA[l2.myInt] == l2.intSctA[l1.myInt] && l1.intSctA[l2.myInt] != Vector3.zero) {
										l1.intGoI = l2.myInt;
										l1.intSctGo = l2.gameObjectSt;
										l1.intSctDist = l2.hitInts[l2.myInt];
										l1.intPos = l1.intSctA[l1.intGoI];
										l2.intPos = l1.intPos;
										l2.intSctGo = l1.gameObjectSt;
										l2.intSctDist = l1.hitInts[l2.myInt];
										l2.intGoI = l1.myInt;
										l1.lasSt = l2;
										l2.lasSt = l1;
										l1.hasInt = true;
										l2.hasInt = true;
										lStack.Remove(l2);
										lC = 0;
									}
								} else if (l2.blockTypeMe) {
									l1.intGoI = l2.myInt;
									l1.intPos = l1.intSctA[l1.intGoI];
									l1.lasSt = l2;
									l1.hasInt = true;
									lC = 0;
								}
								if (!l1.hasInt) {
									lC++;
									lStack.Add(l1);
								}
							}
						} else if (l1.intPos != l1.hitA) {
							l1.intPos = l1.hitA;
							l1.intGoI = -1;
							lC = 0;
							l1.hasHitR = true;
						}
					} else {
						l1.intPos = l1.gameObjectSt.transform.position;
						l1.intGoI = -1;
					}
				}
				if (infLM != 3) {
					for (i = 0; i < lStack.Count; i++) {
						l1 = lStack[i];
						if (l1.intersectMe && !l1.blockTypeMe && !l1.inCol) {
							for (i2 = 0; i2 < l1.lOS.Count; i2++) {
								l2 = l1.lOS[i2];
								if (!lStack.Contains(l2)) {
									l1.lOS.Remove(l2);
								}
							}
							switch (infLM) {
								case 0:
									if (l1.scriptL.lastFI) {
										i2 = 0;
									} else {
										i2 = l1.lOS.Count - 1;
									}
									l1.scriptL.lastFI = !l1.scriptL.lastFI;
									break;

								case 1:
									i2 = infLI;
									break;

								case 2:
									i2 = l1.lOS.Count - 1;
									break;
							}
							if (i2 < 0) {
								i2 = 0;
							}
							if (i2 > l1.lOS.Count - 1) {
								i2 = l1.lOS.Count - 1;
							}
							if (l1.lOS.Count > 0) {
								l2 = l1.lOS[i2];
									if (l1.intSctA[l2.myInt] != Vector3.zero) {
										l1.intGoI = l2.myInt;
										l1.intPos = l1.intSctA[l1.intGoI];
									}
									l1.infL = true;
									l1.hasHitR = false;
									l1.lasSt = l2;
									l1.hasInt = true;
								}
							}
						}
					}
				}
			
			lStack.Clear();
		}
		void setLR(Vector3 lrPos, GameObject currentGO) {
			lrO = currentGO.GetComponent<LineRenderer>();
			lrO.enabled = true;
			lrO.SetPosition(1, lrPos);
			lrO.SetPosition(0, currentGO.transform.position);
		}

		void updateLrW(GameObject go, float widthS, float power) {
			float w = power * widthS;
			LineRenderer lr = null;
			if (lr = go.GetComponent<LineRenderer>()) {
#if UNITY_4 || UNITY_5_5_OR_NEWER
				lr.widthMultiplier = w;
#else
				lr.SetWidth(w, w);
#endif
			}
		}
		lFxObj addlFxObj(bool infL, laserStObj obj1) {
			lFxObj tFO = new lFxObj();
			fxObjs.Add(tFO);
			tFO.go = obj1.gameObjectSt;
			tFO.inf = infL;
			lasObjsD[obj1.gameObjectSt] = tFO;
			tFO.lS = obj1;
			return tFO;
		}
		public lFxObj getlFxObj(GameObject l1) {
			if (l1 != null) {
				if (lasObjsD.ContainsKey(l1)) {
					return lasObjsD[l1];
				}
			}
			return null;
		}
		public void removelFxObj(lFxObj tFO) {
			if (tFO != null) {
				destroyThis(tFO.go, 0);
				removelFxObj(getlFxObj(tFO.cL));
				tFO.cL = null;
				//destroyThis(tFO.cL, 0);
				destroyThis(tFO.hic, desSpObj);
				tFO.hic = null;
				destroyThis(tFO.sc, desSpObj);
				tFO.sc = null;
				destroyThis(tFO.ec, desSpObj);
				tFO.ec = null;
				if (tFO.lFX2 != null) {
					tFO.lFX2.lFX2 = null;
				}
				tFO.lFX2 = null;
				tFO.hit = false;
				fxObjs.Remove(tFO);
				lasObjsD.Remove(tFO.go);
			}
		}
		void removelFxObjCh(lFxObj tFO) {
			if (tFO != null) {
				//Destroy(tFO.go);
				removelFxObj(getlFxObj(tFO.cL));
				tFO.cL = null;
				//destroyThis(tFO.cL, 0);
				destroyThis(tFO.hic, desSpObj);
				tFO.hic = null;
				destroyThis(tFO.sc, desSpObj);
				tFO.sc = null;
				destroyThis(tFO.ec, desSpObj);
				tFO.ec = null;
				if (tFO.lFX2 != null) {
					tFO.lFX2.lFX2 = null;
				}
				tFO.lFX2 = null;
				tFO.hit = false;
				fxObjs.Remove(tFO);
				lasObjsD.Remove(tFO.go);
			}
		}
		void resetLfxobj(lFxObj tFO) {
			if (tFO != null) {
				destroyThis(tFO.go, 0);
				removelFxObj(getlFxObj(tFO.cL));
				tFO.cL = null;
				destroyThis(tFO.hic, 0);
				tFO.hic = null;
				destroyThis(tFO.sc, 0);
				tFO.sc = null;
				destroyThis(tFO.ec, 0);
				tFO.ec = null;
				if (tFO.lFX2 != null) {
					tFO.lFX2.lFX2 = null;
				}
				tFO.lFX2 = null;
				tFO.hit = false;
				fxObjs.Remove(tFO);
				lasObjsD.Remove(tFO.go);
			}
		}
		GameObject createGO(GameObject go, Vector3 pos, Quaternion rot, lFxObj tFO) {
			GameObject tGo;
			if (enablePool) {
				tGo = Pool.instance.GetObjectForType(go.name);
				tGo.transform.position = pos;
				tGo.transform.rotation = rot;
			} else {
				tGo = Instantiate(go, pos, rot) as GameObject;
			}
			if (!fxObjsD.ContainsKey(tGo) && tFO != null) {
				fxObjsD.Add(tGo, tFO);
				fxObjsGo.Add(tGo);
			} else {
				fxObjsD[tGo] = tFO;
			}
			return tGo;
		}
		void createIntSect(laserStObj obj1, laserStObj obj2) {
			bool canGo = true;

			Quaternion rotV;
			Quaternion normV;
			Vector3 plusPosL = Vector3.zero;

			obj2 = obj1.lasSt;

			lFxObj tFO = getlFxObj(obj1.gameObjectSt);
			lFxObj tFO2 = null;

			if (obj2 != null) {
				tFO2 = getlFxObj(obj2.gameObjectSt);
				if (tFO2 == null) {
					tFO2 = addlFxObj(false, obj2);
				}
				if (!tFO2.inf && obj2.infL) {
					tFO2.inf = true;
					if (tFO2.cL != null || tFO2.hic != null) {
						destroyThis(tFO2.hic, desSpObj);
						tFO2.hic = null;
						removelFxObj(getlFxObj(tFO2.cL));
						tFO2.cL = null;
					}
				} else {
					tFO2.inf = false;
				}
			}
			laser tLas;
			if (tFO == null) {
				tFO = addlFxObj(false, obj1);
			}

			if (enablePool) {
				if (tFO.cL) {
					if (!tFO.cL.activeSelf) {
						tFO.cL = null;
					}
				}
				if (tFO.hic) {
					if (!tFO.hic.activeSelf) {
						tFO.hic = null;
					}
				}
				if (tFO.ec) {
					if (!tFO.ec.activeSelf) {
						tFO.ec = null;
					}
				}
				if (tFO.sc) {
					if (!tFO.sc.activeSelf) {
						tFO.sc = null;
					}
				}
			}
			if (!tFO.inf && obj1.infL) {
				tFO.inf = true;
				if (tFO.cL != null || tFO.hic != null) {
					destroyThis(tFO.hic, desSpObj);
					tFO.hic = null;
					removelFxObj(getlFxObj(tFO.cL));
					tFO.cL = null;
				}
			} else {
				tFO.inf = false;
			}
			if (obj1.on || !obj1.firstRun) {
				if (obj1.isChild && (!obj1.scriptL.parent1 || !obj1.scriptL.parent2)) {
					canGo = false;
				} else if (obj1.isChild) {
					if (!obj1.scriptL.parent1.activeSelf) {
						canGo = false;
					}
					if (!obj1.scriptL.parent2.activeSelf) {
						canGo = false;
					}
					if (tL1 = obj1.scriptL.parent1.GetComponent<laser>()) {
						if (tL1.infL) {
							canGo = false;
						}
					}
					if (tL1 = obj1.scriptL.parent2.GetComponent<laser>()) {
						if (tL1.infL) {
							canGo = false;
						}
					}
					if (getlFxObj(obj1.scriptL.parent1) != null) {
						if (getlFxObj(obj1.scriptL.parent1).cL) {
							if (getlFxObj(obj1.scriptL.parent1).cL != obj1.gameObjectSt) {
								canGo = false;
							}
						}
					} else {
						canGo = false;
					}
					if (getlFxObj(obj1.scriptL.parent2) != null) {
						if (getlFxObj(obj1.scriptL.parent2).cL) {
							if (getlFxObj(obj1.scriptL.parent2).cL != obj1.gameObjectSt) {
								canGo = false;
							}
						}
					} else {
						canGo = false;
					}
				}
				if (!canGo) {
					removelFxObj(tFO);
				} else {
					if (startCapB && obj1.startCapObj != null && !obj1.isChild && obj1.on) {
						if (tFO.sc == null) {
							tFO.sc = createGO(obj1.startCapObj, obj1.sPos, obj1.gameObjectSt.transform.rotation, tFO) as GameObject;
						} else {
							tFO.sc.transform.position = obj1.sPos;
							tFO.sc.transform.rotation = obj1.gameObjectSt.transform.rotation;
						}
					} else {
						destroyThis(tFO.sc, desSpObj);
						tFO.sc = null;
					}
					if (endCapB && obj1.endCapObj != null && !obj1.hasHitR && !obj1.hasInt && obj1.on) {
						if (tFO.ec == null) {
							tFO.ec = createGO(obj1.endCapObj, obj1.fwd, obj1.gameObjectSt.transform.rotation, tFO) as GameObject;
						} else {
							tFO.ec.transform.position = obj1.fwd;
							tFO.ec.transform.rotation = obj1.gameObjectSt.transform.rotation;
						}
					} else {
						destroyThis(tFO.ec, desSpObj);
						tFO.ec = null;
					}
					if (obj1.laserObj != null) {
						if (obj2 != null && obj2.gameObjectSt != null && intersect && obj1.intSctObj != null && obj1.laserObj != null && !obj1.infL && !obj2.infL && !obj1.blockTypeMe && !obj2.blockTypeMe && obj1.on) {
							plusPosL = obj1.gameObjectSt.transform.TransformDirection(forwardV) + obj2.gameObjectSt.transform.TransformDirection(forwardV);
							plusPosL /= 2;
							if (!mode2D) {
								rotV = Quaternion.LookRotation(plusPosL);
							} else {
								plusPosL.z = 0;
								float rot_z = Mathf.Atan2(plusPosL.y, plusPosL.x) * Mathf.Rad2Deg;
								rotV = Quaternion.Euler(0f, 0f, rot_z - 90);
							}
							if (tFO.lFX2 == null) {
								tFO.lFX2 = tFO2;
							}
							if (tFO.hit == true || tFO.lFX2 != tFO2) {
								tFO.lFX2 = tFO2;
								tFO.lFX2.lFX2 = tFO;
								if (tFO.hic != null) {
									destroyThis(tFO.hic, desSpObj);
									tFO.hic = null;
								}
								if (tFO.cL != null) {
									removelFxObj(getlFxObj(tFO.cL));
									tFO.cL = null;
								}
							}
							tFO.hit = false;
							if (tFO2.hit == true) {
								if (tFO2.hic != null) {
									destroyThis(tFO2.hic, desSpObj);
									tFO2.hic = null;
								}
								if (tFO2.cL != null) {
									removelFxObj(getlFxObj(tFO2.cL));
									tFO2.cL = null;
								}
							}
							if (tFO.hic == null && reflectStCap) {
								tFO.hic = createGO(obj1.intSctObj, obj1.intPos, obj1.intSctObj.transform.rotation, tFO) as GameObject;
								if (tFO2 != null) {
									tFO2.hic = tFO.hic;
								}
							} else if (tFO.hic != null) {
								tFO.hic.transform.position = obj1.intPos;
							} else if (tFO.hic != null && !reflectStCap) {
								destroyThis(tFO.hic, desSpObj);
								tFO.hic = null;
							}
							if (obj1.reflectMe && obj2.reflectMe) {
								if (tFO.cL == null) {
									tFO.cL = createGO(obj1.laserObj, obj1.intPos, rotV, tFO) as GameObject;
									if (tLas = tFO.cL.GetComponent<laser>()) {
										addLas(tLas);
										if (controlSwitch) {
											tLas.onM = true;
										}
										tLas.isChild = true;
										tLas.parent1 = obj1.gameObjectSt;
										tLas.parent2 = obj2.gameObjectSt;
                                        tLas.groupId = obj1.groupId + obj2.groupId;
										tLas.powerStart = obj1.reflIncMe * (obj1.curPower + obj2.curPower);
										updateLrW(tFO.cL, obj1.lrWidthMe, tLas.powerStart);
										if (obj1.reflLenRemainder) {
											tLas.useRem = true;
                                            tLas.remLength = (obj1.sLength - obj1.intSctDist) + (obj2.sLength - obj2.intSctDist);
                                        } else {
											tLas.useRem = false;
										}
									}
									if (tFO2 != null) {
										if (tFO2.cL != tFO.cL && tFO2.cL != null) {
											removelFxObj(getlFxObj(tFO2.cL));
                                        }
										if (tFO2.hic != tFO.hic && tFO2.hic != null) {
											destroyThis(tFO2.hic, desSpObj);
										}
										tFO2.cL = tFO.cL;
										tFO2.hic = tFO.hic;
									}
								} else {
									if (tLas = tFO.cL.GetComponent<laser>()) {
										tLas.powerStart = obj1.reflIncMe * (obj1.curPower + obj2.curPower);
										if (obj1.reflLenRemainder) {
											tLas.useRem = true;
                                            tLas.remLength = (obj1.sLength - obj1.intSctDist) + (obj2.sLength - obj2.intSctDist);
                                        } else {
											tLas.useRem = false;
										}
									}
									tFO.cL.transform.rotation = rotV;
									tFO.cL.transform.position = obj1.intPos;
								}
							} else if (tFO.cL != null && (!obj1.reflectMe || !obj2.reflectMe)) {
								removelFxObj(getlFxObj(tFO.cL));
								tFO.cL = null;
                            }
                        } else if (obj1.hasHitR && !obj1.hasInt && !obj1.infL && obj1.on) {
							plusPosL = Vector3.Reflect((obj1.hitA - obj1.gameObjectSt.transform.position).normalized, obj1.hitNorm);
							if (!mode2D) {
								rotV = Quaternion.LookRotation(plusPosL);
							} else {
								float angle = Mathf.Atan2(-plusPosL.x, plusPosL.y) * Mathf.Rad2Deg;
								rotV = Quaternion.AngleAxis(angle, Vector3.forward);
							}
							normV = Quaternion.identity;
							if (obj1.hitNorm != Vector3.zero) {
								normV = Quaternion.LookRotation(obj1.hitNorm);
							}
							if (obj1.scriptL.hitMat != obj1.hitMat) {
								if (tFO.hic != null) {
									destroyThis(tFO.hic, desSpObj);
									tFO.hic = null;
								}
								if (tFO.cL != null && !obj1.canRefl) {
									removelFxObj(getlFxObj(tFO.cL));
									tFO.cL = null;
								}
								obj1.scriptL.hitMat = obj1.hitMat;
                            }
                            if (obj1.scriptL.hitTag != obj1.hitTag) {
                                if (tFO.hic != null) {
                                    destroyThis(tFO.hic, desSpObj);
                                    tFO.hic = null;
                                }
                                if (tFO.cL != null && !obj1.canRefl) {
                                    removelFxObj(getlFxObj(tFO.cL));
                                    tFO.cL = null;
                                }
                                obj1.scriptL.hitTag = obj1.hitTag;
                            }
                            if (tFO.lFX2 != null) {
								if (tFO.hic != null) {
									destroyThis(tFO.hic, desSpObj);
									tFO.hic = null;
								}
								if (tFO.cL != null) {
									removelFxObj(getlFxObj(tFO.cL));
									tFO.cL = null;
								}
								removelFxObj(getlFxObj(tFO.lFX2.cL));
								tFO.lFX2.cL = null;
								destroyThis(tFO.lFX2.hic, desSpObj);
								tFO.lFX2.hic = null;
								tFO.lFX2.lFX2 = null;
								tFO.lFX2 = null;
							}
							if (tFO.hic == null && hitObjB) {
								tFO.hic = createGO(obj1.hitGOM, obj1.hitA + (obj1.hitNorm * 0.01f), normV, tFO) as GameObject;
							} else if (tFO.hic != null) {
								tFO.hic.transform.position = obj1.hitA + (obj1.hitNorm * 0.01f);
								tFO.hic.transform.rotation = normV;
							} else if (tFO.hic != null) {
								destroyThis(tFO.hic, desSpObj);
								tFO.hic = null;
							}
							if (tFO.cL == null && obj1.canRefl) {
								tFO.cL = createGO(obj1.laserObj, obj1.hitA, rotV, tFO) as GameObject;
								if (tLas = tFO.cL.GetComponent<laser>()) {
									addLas(tLas);
									if (controlSwitch) {
										tLas.onM = true;
									}
									tLas.isChild = true;
									tLas.parent1 = obj1.gameObjectSt;
									tLas.parent2 = obj1.gameObjectSt;
                                    tLas.groupId = obj1.groupId;
									tLas.powerStart = obj1.curPower * obj1.reflMatIncMe;
									updateLrW(tFO.cL, obj1.lrWidthMe, tLas.powerStart);
									if (obj1.reflLenRemainder) {
										tLas.useRem = true;
										tLas.remLength = obj1.sLength - obj1.intSctDist;
									} else {
										tLas.useRem = false;
									}
								}
							} else if (tFO.cL != null) {
								if (tLas = tFO.cL.GetComponent<laser>()) {
									if (obj1.reflLenRemainder) {
										tLas.useRem = true;
										tLas.remLength = obj1.sLength - obj1.intSctDist;
									} else {
										tLas.useRem = false;
									}
									tLas.powerStart = obj1.curPower * obj1.reflMatIncMe;
								}
								tFO.cL.transform.rotation = rotV;
								tFO.cL.transform.position = obj1.hitA;
                            } else if (tFO.cL != null && !obj1.canRefl) {
								removelFxObj(getlFxObj(tFO.cL));
								tFO.cL = null;
							}
							tFO.hit = true;
                        } else {
							destroyThis(tFO.hic, desSpObj);
							tFO.hic = null;
							removelFxObj(getlFxObj(tFO.cL));
							tFO.cL = null;
							tFO.lFX2 = null;
                        }
                        obj1.gameObjectSt.GetComponent<laser>().directChild = tFO.cL;
                    }
				}
			} else {
				if (tFO.lFX2 != null) {
					removelFxObjCh(tFO.lFX2);
				}
				tFO.lFX2 = null;
				removelFxObjCh(tFO);
			}

		}
		private void destroyThis(GameObject goDest, float time) {
			if (goDest) {
				if (disalbePartObj) {
					destroyFX destFX;
					if (destFX = goDest.GetComponent<destroyFX>()) {
						if (destFX.tempP != null) {
							destFX.DestroyFX();
						} else {
							delFlag.Add(goDest);
						}
					}
				}
				laser tLas;
				if (tLas = goDest.GetComponent<laser>()) {
					tLas.destroyLas();
				}
			}
		}
	}
}