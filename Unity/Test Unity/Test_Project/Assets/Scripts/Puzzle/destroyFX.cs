using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Puzzle.Lasers;

namespace Puzzle.Lasers {

	public class destroyFX : MonoBehaviour {

		public GameObject lightObj;

		[SerializeField, HideInInspector]
		public ParticleSystem tempP;
		private ParticleSystem tempP2;

		private bool dest = false;
		private float dT = 0;
		private bool p = false;


		// Use this for initialization
		void Start() {
			tempP = GetComponent<ParticleSystem>();
		}

		// Update is called once per frame
		void Update() {
			if (dest) {
				if (Time.time > dT) {
					if (!p) {
						Destroy(gameObject);
					} else {
						dT = 0;
						p = false;
						dest = false;
						startFX();
						Pool.instance.PoolObject(gameObject);
					}
				}
			}
		}

		public void startFX() {
			if (tempP) {
				tempP.enableEmission = true;
				for (int i = 0; i < transform.childCount; i++) {
					if (tempP2 = transform.GetChild(i).GetComponent<ParticleSystem>()) {
						tempP2.enableEmission = true;
					}
				}
			}
            if (lightObj) {
                lightObj.SetActive(true);
            }
		}

		public void DestroyFX() {
			if (!tempP) {
				tempP = GetComponent<ParticleSystem>();
			}
			if (tempP) {
				tempP.enableEmission = false;
				for (int i = 0; i < transform.childCount; i++) {
					if (tempP2 = transform.GetChild(i).GetComponent<ParticleSystem>()) {
						tempP2.enableEmission = false;
					}
				}
			}
			gameObject.name = gameObject.name.Replace("(Clone)", "");
            if (lightObj) {
                lightObj.SetActive(false);
            }
			dest = true;
			dT = Time.time + laserSystem.instance.desSpObj;
			p = laserSystem.instance.enablePool;
		}
	}
	
}