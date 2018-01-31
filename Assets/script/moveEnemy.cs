using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class moveEnemy : MonoBehaviour {



	// Use this for initialization

//	public Vector3 pos;


	IEnumerator Start(){

		while (true) {
			Vector3 v = new Vector3 (transform.position.x, transform.position.y - 0.08f, transform.position.z);
			if (v.y > 3.0f) {
				transform.position = v;
			} else {
				//				Debug.Log ("hogehoge");
				yield break;
			}
			yield return null;
		}
	}
}
