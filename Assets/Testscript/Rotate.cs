using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Rotate : MonoBehaviour {

	public Vector3 pos;
//	Vector3 tmp = GameObject.Find("spiralbullet").transform.position;

	void Start () {
		StartCoroutine ("shotCoroutine");
	}

	public IEnumerator shotCoroutine(){
		while (true) {
//			Vector3 tmp = GameObject.Find("spiral\bbullet").transform.position;
//			transform.Rotate (new Vector3 (pos.x, pos.y, pos.z) * Time.deltaTime);
			transform.Rotate (new Vector3 (pos.x, pos.y, pos.z) * Time.deltaTime);
//			if(tmp.z % 100 == 0){
//				transform.Rotate (new Vector3 (pos.x, pos.y, pos.z * -1) * Time.deltaTime);
				yield return new WaitForSeconds (0.03f);
//			}

		}

	}
}