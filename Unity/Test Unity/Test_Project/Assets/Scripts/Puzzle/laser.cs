using UnityEngine;
using System.Collections;
using System.Collections.Generic;


namespace Puzzle.Lasers {

	[System.Serializable]
	public class laser : MonoBehaviour {

     
        [SerializeField, HideInInspector]
		public bool isChild = false;
		[SerializeField, HideInInspector]
		public string groupId = "";
        
		//public bool debugMe = true;

		[SerializeField, HideInInspector]
		public bool onM = true;

		[SerializeField, HideInInspector]
		public LayerMask hitLayerM;

		[SerializeField, HideInInspector]
		public bool useGlobal = true;
		[SerializeField, HideInInspector]
		public float length = 30;
		[SerializeField, HideInInspector]
		public bool intersectE = true;
		[SerializeField, HideInInspector]
		public bool reflectE = true;
		[SerializeField, HideInInspector]
		public bool hitReflectE = true;
		[SerializeField, HideInInspector]
		public bool hitE = true;
		[SerializeField, HideInInspector]
		public bool reflLenRemE = true;
		[SerializeField, HideInInspector]
		public bool hitRefMatE = true;
		[SerializeField, HideInInspector]
		public bool passRefMatE = true;
		[Header("Passthrough materials")]
		[SerializeField, HideInInspector]
		[Tooltip("Enable pass-through materials? (local)")]
		public Material[] passedMatsE;
		[Header("Reflection materials")]
		[SerializeField, HideInInspector]
		[Tooltip("Enable reflection materials? (local)")]
		public Material[] reflMatsE;
		[SerializeField, HideInInspector]
		public GameObject[] reflMatObj;
        [SerializeField, HideInInspector]
        public string[] reflTag;
        [SerializeField, HideInInspector]
        public float[] reflTagInc;
        [SerializeField, HideInInspector]
        public GameObject[] allowedTagObj;
        [SerializeField, HideInInspector]
        public int hitScanMode = 0;
        [SerializeField, HideInInspector]
		public bool lasLineREff = true;
		[SerializeField, HideInInspector]
		public float powerStart = 1;
		[SerializeField, HideInInspector]
		public float reflInc = 2;
		[SerializeField, HideInInspector]
		public float chargeInc = 2;
		[Header("Reflection materials power increase")]
		[SerializeField, HideInInspector]
		[Tooltip("Multiply the reflected laser by this number, a reflected laser starting at 2 with this value at 3 will finish at 6. Each array index corresponds to the relevant index in the reflected material array (local)")]
		public float[] reflMatInc;
		[Header("Passthrough materials power increase")]
		[SerializeField, HideInInspector]
		[Tooltip("Multiply the passthrough laser by this number, a passthrough laser starting at 2 with this value at 3 will finish at 6. Each array index corresponds to the relevant index in the passthrough material array (local)")]
		public float[] pasMatInc;
		[SerializeField, HideInInspector]
		public float powerCap = 0;
		[SerializeField, HideInInspector]
		public float lrWidth = 0.2f;

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
		public bool localLength = true;
		[SerializeField, HideInInspector]
		public bool localIntersect = true;
		[SerializeField, HideInInspector]
		public bool localIntersectReflect = true;
		[SerializeField, HideInInspector]
		public bool localIntersectRemain = true;
		[SerializeField, HideInInspector]
		public bool localHit = true;
		[SerializeField, HideInInspector]
		public bool localHitMask = true;
		[SerializeField, HideInInspector]
		public bool localHitReflect = true;
		[SerializeField, HideInInspector]
		public bool localHitReflectMat = true;
		[SerializeField, HideInInspector]
		public bool localPassthroughMat = true;
		[SerializeField, HideInInspector]
		public bool localLineRenderer = true;
		[SerializeField, HideInInspector]
		public bool localLineRendererWidth = true;
		[SerializeField, HideInInspector]
		public bool localStartPower = true;
		[SerializeField, HideInInspector]
		public bool localPowerCap = true;
		[SerializeField, HideInInspector]
		public bool localReflectionPowerInc = true;
		[SerializeField, HideInInspector]
		public bool localChargePowerInc = true;
		[SerializeField, HideInInspector]
		public bool localLaserObj = true;
		[SerializeField, HideInInspector]
		public bool localIntSctObj = true;
		[SerializeField, HideInInspector]
		public bool localHitObj = true;
		[SerializeField, HideInInspector]
		public bool localStartCapObj = true;
		[SerializeField, HideInInspector]
		public bool localEndCapObj = true;

        [SerializeField, HideInInspector]
        public bool localreflMatObj = true;
        [SerializeField, HideInInspector]
        public bool localreflTag = true;
        [SerializeField, HideInInspector]
        public bool localreflTagInc = true;
        [SerializeField, HideInInspector]
        public bool localallowedTagObj = true;
        [SerializeField, HideInInspector]
        public bool localhitScanMode = true;

        [SerializeField, HideInInspector]
		public bool blockType = false;
		[SerializeField, HideInInspector]
		public float blockThreshold = -1;
		[SerializeField, HideInInspector]
		public bool chargeType = false;

		[SerializeField, HideInInspector]
		public bool useRem = false;
		[SerializeField, HideInInspector]
		public float remLength = 0;

		[SerializeField, HideInInspector]
		public Vector3 intPos;
		[SerializeField, HideInInspector]
		public int intGoI;
		[SerializeField, HideInInspector]
		public GameObject intSctGo;
		[SerializeField, HideInInspector]
		public GameObject gameObjectSt;
		[SerializeField, HideInInspector]
		public GameObject hitGO;
		[SerializeField, HideInInspector]
		public Vector3 hitA;
		[SerializeField, HideInInspector]
		public Vector3 hitNorm;
		[SerializeField, HideInInspector]
		public float hitDist;
		[SerializeField, HideInInspector]
		public int shrtA;
		[SerializeField, HideInInspector]
		public Vector3[] intSctA;
		[SerializeField, HideInInspector]
		public Vector3 fwd;
		[SerializeField, HideInInspector]
		public Vector3 sPos;
		[SerializeField, HideInInspector]
		public GameObject parent1;
		[SerializeField, HideInInspector]
		public GameObject parent2;
		[SerializeField, HideInInspector]
		public GameObject origin1;
		[SerializeField, HideInInspector]
		public GameObject origin2;
		[SerializeField, HideInInspector]
		public float curPower = 1;
		[SerializeField, HideInInspector]
		public Material hitMat;

		[SerializeField, HideInInspector]
		public bool lastFI = false;
		[SerializeField, HideInInspector]
		public bool firstRun = true;
		[SerializeField, HideInInspector]
		public bool infL = false;
		[SerializeField, HideInInspector]
		public bool hasInt = false;
		[SerializeField, HideInInspector]
		public bool hasHit = false;

        [SerializeField, HideInInspector]
        public GameObject directChild = null;

		[SerializeField, HideInInspector]
		public LineRenderer lr = null;

		private bool dest = false;
		private float dT = 0;
		private bool p = false;
		private bool c = false;

        [SerializeField, HideInInspector]
        public string hitTag = "";

		// Use this for initialization
		void Start() {
			lr = GetComponent<LineRenderer>();
			if (!laserSystem.instance.enablePool || !isChild) {
				laserSystem.instance.addLas(this);
			}
		}

		private void OnDestroy() {
			laserSystem.instance.destL(gameObject);
		}

		// Update is called once per frame
		void Update() {
			if (!onM) {
				firstRun = true;
			}
			if (dest) {
				if (Time.time > dT) {
					resetVals();
					laserSystem.instance.sDestL(gameObject);
					Pool.instance.PoolObject(gameObject);
				}
			}
		}

        private void FixedUpdate() {
            if (isChild && gameObject.activeSelf) {
                if (parent1 && parent2) {
                    if ((parent1.GetComponent<laser>().directChild != gameObject || !parent1.activeSelf) && (parent2.GetComponent<laser>().directChild != gameObject || !parent2.activeSelf)) {
                        laserSystem.instance.removelFxObj(laserSystem.instance.getlFxObj(gameObject));
                    }
                }
            }
        }

        public void destroyLas() {
            if (laserSystem.instance) {
                p = laserSystem.instance.enablePool;
                if (p) {
                    dest = true;
                    dT = Time.time + 0.01f;
                    c = laserSystem.instance.controlSwitch;
                    if (c) {
                        onM = false;
                    }
                    if (lr == null) {
                        lr = GetComponent<LineRenderer>();
                    }
                    lr.SetPosition(0, Vector3.zero);
                    lr.SetPosition(1, Vector3.zero);
                    gameObject.name = gameObject.name.Replace("(Clone)", "");
                } else {
                    Destroy(gameObject);
                }
            }
		}

		public void resetVals() {
			remLength = 0;
			intPos = Vector3.zero;
			intGoI = -1;
			intSctGo = null;
			gameObjectSt = null;
			hitGO = null;
			hitA = Vector3.zero;
			hitNorm = Vector3.zero;
			hitDist = -1;
			shrtA = -1;
			intSctA = null;
			fwd = Vector3.zero;
			sPos = Vector3.zero;
			parent1 = null;
			parent2 = null;
			origin1 = null;
			origin2 = null;
			curPower = 1;
			hitMat = null;
            hitTag = "";
            lastFI = false;
			firstRun = true;
			infL = false;
			hasInt = false;
			hasHit = false;
#if UNITY_4 || UNITY_5_5_OR_NEWER
		lr.widthMultiplier = 1;
#else
			lr.SetWidth(1,1);
#endif
			dest = false;
			dT = 0;
			p = false;
			c = false;
            directChild = null;
		}
	}
}