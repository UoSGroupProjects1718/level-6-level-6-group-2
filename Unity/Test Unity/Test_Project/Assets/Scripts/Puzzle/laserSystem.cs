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
		public bool debugM = false;
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
		public int debugI = -1;
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
		public bool hitReflMat = true;
		[SerializeField, HideInInspector]
		public bool passReflMat = true;
		[SerializeField, HideInInspector]
		public bool lasLineREff = true;
		[SerializeField, HideInInspector]
		public float powerStart = 1;
		[SerializeField, HideInInspector]
		public float reflInc = 1;
		[SerializeField, HideInInspector]
		public float chargeInc = 2;
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
		public float powerCap = 0;
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

		private bool intersectS = false;
		private bool reflectS = false;
		private bool updateLrPosS = false;
		private bool endCapBS = false;
		private bool startCapBS = false;
		private bool hitObjBS = false;
		private bool hitReflS = false;
		private bool reflectStCapS = true;
		private bool reflectLRemainderS = true;
		private bool hitReflMatS = true;
		private bool passReflMatS = true;

		private bool hasChanged = false;

		private RaycastHit hit;
		private RaycastHit2D[] hit2D;
		private Vector3 fwd2;
		private laser[] lSources;
		private List<laser> lSources2 = new List<laser>();
		private List<laser> lSources3 = new List<laser>();
		private laserStObj[] laserSt = new laserStObj[0];
		private Vector3 overRideV3;
		private int foundInt = -1;
		private LineRenderer lrO;
		private Vector3 t1;
		private Vector3 t2;
		private bool para1;
		private float hitDist = 0;
		private bool exclGO;
		private Vector3 drawGiz;
		private bool isHit;

		private GameObject intersectGO;
		private int powerLvl = 1;
		private float tDist;
		private float tDist2;

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
		private bool canGo;
		private bool rayPass;
		private int rayPassI;
		private bool reflMatT;
		private Vector3 fwd4;
		private bool goMat;
		private Vector3 tv3;
		private float fl = 0;
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
		MeshCollider meshT = null;
		MeshRenderer rendF = null;
		SpriteRenderer rendS = null;
		Material[] tMat;

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
						hasChanged = false;
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
				} else {
					Debug.Log("Laser System: Laser objct requires laser script, system aborting");
				}
			} else {
				Debug.Log("Laser System: No laser object assigned, system aborting");
			}
		}

		private void Update() {
			if (laserObj != null) {
				if (laserObj.GetComponent<laser>()) {
					if (onSys) {
						delFlagged();
						hasChanged = false;
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
			//public SortedList<float, laserStObj> hitOrder = new SortedList<float, laserStObj>();
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
			public bool passReflMatMe;
			public Material[] passedMatsMe;
            public string[] reflTagMe;
            public float[] reflTagIncMe;
            public string[] pasTagMe;
            public float[] pasTagIncMe;
            public GameObject[] allowedTagObj;
            public int hitScanModeMe = 0;
            public bool canRefl;
			public bool lasLineREffMe = true;
			public float powerStartMe = 1;
			public float reflIncMe = 2;
			public float chargeIncMe = 2;
			public float[] reflMatIncMeA;
			public GameObject[] allowedMatsObjMe;
			public float reflMatIncMe = 1f;
			public float[] pasMatIncMeA;
			public float pasMatIncMe = 1f;
			public float powerCapMe = 0;
			public float lrWidthMe = 0.2f;
			public bool blockTypeMe = false;
			public float blockThresholdMe = -1;
			public bool chargeTypeMe = false;
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
				passReflMatMe = false;
				passedMatsMe = null;
				canRefl = false;
				lasLineREffMe = true;
				powerStartMe = 1;
				reflIncMe = 2;
				chargeIncMe = 2;
				reflMatIncMeA = null;
				allowedMatsObjMe = null;
				reflMatIncMe = 1f;
				pasMatIncMeA = null;
				pasMatIncMe = 1f;
				powerCapMe = 0;
				lrWidthMe = 0.2f;
				blockTypeMe = false;
				blockThresholdMe = -1;
				chargeTypeMe = false;
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

				curPower = 1;

				charged = 0;
				inCol = false;
			}

			public void init(float rayL, bool intersectMeV, bool reflectMeV, bool hitReflectMeV, bool hitMeV, bool reflRemV, LayerMask hitLayerMeV, bool hitReflMatMeV, Material[] allowedMatsMeV, bool passReflMatMeV, Material[] passedMatsMeV, bool lasLineREffMeV, float powerStartMeV, float reflIncMeV, float chargeIncMeV, float[] reflMatIncMeV, float[] pasMatIncMeV, float powerCapMeV, float lrWidthMeV, GameObject gO,
				GameObject laserObjMeV, GameObject intSctObjMeV, GameObject hitObjMeV, GameObject startCapObjMeV,
				GameObject endCapObjMeV, GameObject[] allowedMatsObjMeV,
                string[] reflTagMeV,
                float[] reflTagIncMeV,
                //string[] pasTagMeV,
                //float[] pasTagIncMeV,
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
				hitReflMatMe = hitReflMatMeV;
				allowedMatsMe = allowedMatsMeV;
				passReflMatMe = passReflMatMeV;
				passedMatsMe = passedMatsMeV;
				lasLineREffMe = lasLineREffMeV;
				reflIncMe = reflIncMeV;
				chargeIncMe = chargeIncMeV;
				reflMatIncMeA = reflMatIncMeV;
				pasMatIncMeA = pasMatIncMeV;
				lrWidthMe = lrWidthMeV;
				powerCapMe = powerCapMeV;
				laserObj = laserObjMeV;
				intSctObj = intSctObjMeV;
				hitObj = hitObjMeV;
				startCapObj = startCapObjMeV;
				endCapObj = endCapObjMeV;
				allowedMatsObjMe = allowedMatsObjMeV;
                reflTagMe = reflTagMeV;
                reflTagIncMe = reflTagIncMeV;
                allowedTagObj = allowedTagObjMeV;
                //pasTagMe = pasTagMeV;
                //pasTagIncMe = pasTagIncMeV;
                hitScanModeMe = hitScanModeMeV;
				if (gameObjectSt != null) {
					if (scriptL = gameObjectSt.GetComponent<laser>()) {
						isChild = scriptL.isChild;
						if (!isChild) {
							powerStartMe = powerStartMeV;
						} else {
							powerStartMe = scriptL.powerStart;
						}
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
							if (scriptL.localHitReflectMat) {
                                hitScanModeMe = scriptL.hitScanMode;
                                hitReflMatMe = scriptL.hitRefMatE;
								allowedMatsObjMe = scriptL.reflMatObj;
								allowedMatsMe = scriptL.reflMatsE;
                                reflTagMe = scriptL.reflTag;
                                reflTagIncMe = scriptL.reflTagInc;
                                allowedTagObj = scriptL.allowedTagObj;
                            }
							if (scriptL.localPassthroughMat) {
								passReflMatMe = scriptL.passRefMatE;
								passedMatsMe = scriptL.passedMatsE;
							}
							if (scriptL.localLineRenderer) {
								lasLineREffMe = scriptL.lasLineREff;
							}
							if (scriptL.localStartPower) {
								powerStartMe = scriptL.powerStart;
							}
							if (scriptL.localReflectionPowerInc) {
								reflIncMe = scriptL.reflInc;
							}
							if (scriptL.localChargePowerInc) {
								chargeIncMe = scriptL.chargeInc;
							}
							reflMatIncMeA = scriptL.reflMatInc;
							pasMatIncMeA = scriptL.pasMatInc;
							if (scriptL.localPowerCap) {
								powerCapMe = scriptL.powerCap;
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
						curPower = powerStartMe;
						if (powerCapMe > 0) {
							if (curPower > powerCapMe) {
								curPower = powerCapMe;
							}
						}
						firstRun = scriptL.firstRun;
						on = scriptL.onM;
						blockTypeMe = scriptL.blockType;
						blockThresholdMe = scriptL.blockThreshold;
						chargeTypeMe = scriptL.chargeType;
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

		public bool ClosestPointsOnTwoLines(out Vector3 closestPointLine1, out Vector3 closestPointLine2, Vector3 linePoint1, Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2, laser go2, laser go1, float len1, float len2) {
			closestPointLine1 = Vector3.zero;
			closestPointLine2 = Vector3.zero;

			a = Vector3.Dot(lineVec1, lineVec1);
			b = Vector3.Dot(lineVec1, lineVec2);
			e = Vector3.Dot(lineVec2, lineVec2);

			d = a * e - b * b;

			//lines are not parallel
			if (d != 0.0f) {

				r = linePoint1 - linePoint2;
				c = Vector3.Dot(lineVec1, r);
				f = Vector3.Dot(lineVec2, r);

				s = (b * f - c * e) / d;
				t = (a * f - c * b) / d;

				closestPointLine1 = linePoint1 + lineVec1 * s;
				closestPointLine2 = linePoint2 + lineVec2 * t;
				tv3 = go2.transform.TransformPoint(forwardV * len2);
				if (PointOnWhichSideOfLineSegment(go1.transform.position, go1.transform.TransformPoint(forwardV * len1), closestPointLine1) == 0 && PointOnWhichSideOfLineSegment(linePoint2, tv3, closestPointLine2) == 0) {
					return true;
				}
				return false;
			} else {
				return false;
			}
		}

		public bool LineLineIntersection(out Vector3 intersection, Vector3 linePoint1, Vector3 lineVec1, Vector3 linePoint2, Vector3 lineVec2, Vector3 transformP1, Vector3 transformP2) {

			linePoint1.z = 0;
			linePoint2.z = 0;
			lineVec1.z = 0;
			lineVec2.z = 0;

			Vector3 lineVec3 = linePoint2 - linePoint1;
			Vector3 crossVec1and2 = Vector3.Cross(lineVec1, lineVec2);
			Vector3 crossVec3and2 = Vector3.Cross(lineVec3, lineVec2);

			float planarFactor = Vector3.Dot(lineVec3, crossVec1and2);

			//is coplanar, and not parrallel
			if (Mathf.Abs(planarFactor) < 0.0001f && crossVec1and2.sqrMagnitude > 0.0001f) {
				float s = Vector3.Dot(crossVec3and2, crossVec1and2) / crossVec1and2.sqrMagnitude;
				intersection = linePoint1 + (lineVec1 * s);
				if (PointOnWhichSideOfLineSegment(linePoint1, transformP1, intersection) == 0 && PointOnWhichSideOfLineSegment(linePoint2, transformP2, intersection) == 0) {
					return true;
				} else {
					return false;
				}
			} else {
				intersection = Vector3.zero;
				return false;
			}
		}

		void loadAll() {
			for (i = 0; i < maxI; i++) {
				fwd4 = lSources2[i].transform.TransformPoint(forwardV * hit2DDistance);
				//create all classes and variables
				tL1 = lSources2[i];
				if (laserSt[i] == null) {
					laserSt[i] = new laserStObj();
				} else {
					laserSt[i].reset();
				}
				laserSt[i].init(rayL, intersect, reflect, hitRefl, canHit, reflectLRemainder, hitLayerM, hitReflMat, allowedMat, passReflMat, passMat, lasLineREff, powerStart, reflInc, chargeInc, reflMatInc, pasMatInc, powerCap, lrWidth, tL1.gameObject,
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
			//start scan of all laser objects, if rayhit
			for (i = 0; i < maxI; i++) {
				fwd4 = lSources2[i].transform.TransformPoint(forwardV * hit2DDistance);
				//create all classes and variables
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
                    tMat = new Material[0];
                    //if able to raycast
                    if (l1.hitMe) {
                        //do raycast 3D
                        if (!mode2D) {
                            if (Physics.Linecast(lSources2[i].transform.position, fwd2, out hit, l1.hitLayerMe)) {
                                if (l1.hitScanModeMe == 1) {
                                    while (rayPass == true) {
                                        //change lincecast if not on first scan
                                        if (rayPassI > 0) {
                                            Physics.Linecast(hit.point, fwd2, out hit, l1.hitLayerMe);
                                        }
                                        reflMatT = false;
                                        //turn off the loop
                                        rayPass = false;
                                        //check if hit
                                        if (hit.collider) {
                                            //check if material properties are true for laser
                                            if ((l1.hitReflMatMe && l1.hitReflectMe) || l1.passReflMatMe) {

                                            }

                                            if (l1.hitReflectMe && !l1.hitReflMatMe) {
                                                reflMatT = true;
                                            }

                                            //if no passthrough (and hit), log hit 3D
                                            if (!rayPass) {
                                                l1.hitA = hit.point;
                                                l1.hitNorm = hit.normal;
                                                l1.hitDist = Vector3.Distance(tL1.transform.position, hit.point);
                                                l1.hitGO = hit.collider.gameObject;
                                                if (l1.hitReflectMe) {
                                                    l1.canRefl = reflMatT;
                                                }
                                            }
                                            rayPassI++;
                                        }
                                    }
                                } else {
                                    reflMatT = false;
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
                        } else {
                            hit2D = Physics2D.LinecastAll(fwd4, fwd2, l1.hitLayerMe);
                            if (hit2D.Length > 0) {
                                reflMatT = false;
                                //turn off the loop
                                goMat = false;
                                //check if hit
                                if (hit2D[0].collider) {
                                    if (l1.hitReflMatMe && l1.hitReflectMe) {
                                        if (l1.hitScanModeMe == 1) {
                                        

                                            doHitT(hit2D[0].collider.tag);

                                        }
                                    } else {
                                        reflMatT = false;
                                    }
                                    //if no passthrough (and hit), log hit 3D
                                    q1 = Quaternion.AngleAxis(180, Vector3.forward);
                                    if (!hit2D[0].collider.OverlapPoint(fwd4)) {
                                        l1.hitA = hit2D[0].point;
                                        l1.hitNorm = hit2D[0].normal;
                                        l1.hitDist = Vector2.Distance(tL1.transform.position, hit2D[0].point);
                                        l1.hitGO = hit2D[0].collider.gameObject;
                                        if (l1.hitReflectMe) {
                                            l1.canRefl = reflMatT;
                                        }
                                    } else {
                                        l1.inCol = true;
                                    }
                                }
                            } else {
                                l1.canRefl = l1.hitReflectMe;
                            }
                        }
                    }
                }
			}
		}

		void scanLines() {
			//scann all line intersections and log
			//start with base laser
			for (i = 0; i < maxI; i++) {
				l1 = laserSt[i];
				tL1 = lSources2[i];
				fwd2 = tL1.transform.TransformPoint(forwardV * l1.sLength);
				tDist2 = l1.hitDist;
				//cross reference with other lasers
				for (i2 = 0; i2 < maxI; i2++) {
					canGo = true;
					//if both on, continue
					l1.intSctA[i2] = Vector3.zero;
					l1.hitInts[i2] = l1.sLength;
					l1.hitOrder[i2] = laserSt[i2];
					if (l1.on && !l1.inCol && laserSt[i2].on && l1.scriptL.curPower > 0 && laserSt[i2].scriptL.curPower > 0) {
						tL2 = laserSt[i2].gameObjectSt.GetComponent<laser>();
						//if not parent or child, continue
						if (tL1.parent1 != laserSt[i2].gameObjectSt && tL1.parent2 != laserSt[i2].gameObjectSt) {
							if (tL2.parent1 != tL1.gameObjectSt && tL2.parent2 != tL1.gameObjectSt) {
								bar = false;
								if (lSources2[i2].gameObject != null && l1 != null) {
									if (infProt) {
										if (l1.scriptL.groupId.Contains(laserSt[i2].scriptL.groupId) || laserSt[i2].scriptL.groupId.Contains(l1.scriptL.groupId)) {
											canGo = false;
										}
									}
									if (canGo) {
										//check if not parent or child
										l1.intSctA[i2] = fwd2;
										//check not scanning self
										if (lSources2[i2].gameObject != tL1.gameObject) {
											//get closest point between lines, check intersecting via minIntDist
											if (!mode2D) {
												para1 = ClosestPointsOnTwoLines(out t1, out t2, tL1.transform.position, tL1.transform.TransformDirection(forwardV), lSources2[i2].transform.position, lSources2[i2].transform.TransformDirection(forwardV), lSources2[i2], tL1, l1.hitDist, laserSt[i2].hitDist);
												fl = Vector3.Distance(t1, t2);
											} else {
												para1 = LineLineIntersection(out t1, tL1.transform.position, tL1.transform.TransformDirection(forwardV),
													lSources2[i2].transform.position, lSources2[i2].transform.TransformDirection(forwardV),
													tL1.transform.TransformPoint(forwardV * l1.hitDist),
													lSources2[i2].transform.TransformPoint(forwardV * laserSt[i2].hitDist)
													);
												t2 = t1;
												fl = 0;
											}
											if (para1 && fl < minIntDist) {
												if (!mode2D) {
													tv3 = t2 + (t1 - t2) / 2;
												} else {
													tv3 = t1;
												}
												tDist = Vector3.Distance(tL1.transform.position, tv3);
												l1.hitInts[i2] = tDist;
												//log if able to charge
												if (l1.chargeTypeMe && (!l1.intersectMe || l1.blockTypeMe)) {
													laserSt[i2].charged++;
												}
												//check if can intersect, log if true
												if (l1.intersectMe && laserSt[i2].intersectMe) {
													if (tDist < tDist2) {
														bar = true;
													}
												}
												if (bar) {
													tDist2 = tDist;
													l1.shrtA = i2;
													l1.intPos = tv3;
													l1.intGoI = i2;
												}
												//log intersection
												l1.intSctA[i2] = tv3;
											}
										}
									}
								}
							}
						}
					}
					//if (!l1.hitOrder.ContainsKey(l1.hitInts[i2])) {
					//l1.hitOrder.Add(l1.hitInts[i2], laserSt[i2]);
					//}
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
					//for (i2 = 0; i2 < l1.hitOrder.Values.Count; i2++) {
					//l1.lOS.Add(l1.hitOrder.Values[i2]);
					//}
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
										if (l1.chargeTypeMe) {
											l2.charged++;
										}
										if (l2.chargeTypeMe) {
											l1.charged++;
										}
									}
								} else if (l2.blockTypeMe) {
									l1.intGoI = l2.myInt;
									l1.intPos = l1.intSctA[l1.intGoI];
									l1.lasSt = l2;
									l1.hasInt = true;
									lC = 0;
									if (l2.chargeTypeMe) {
										l1.charged++;
									}
									if (l1.chargeTypeMe) {
										l2.charged++;
									}
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
								if (l2.intersectMe) {
									if (l1.chargeTypeMe) {
										l2.charged++;
									}
									if (l2.chargeTypeMe) {
										l1.charged++;
									}
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

		void setCharge(laserStObj obj1) {
			obj1.curPower = obj1.curPower * (obj1.charged * obj1.chargeIncMe);
			if (obj1.powerCapMe > 0) {
				if (obj1.curPower > obj1.powerCapMe) {
					obj1.curPower = obj1.powerCapMe;
				}
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