using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LookAt : MonoBehaviour {

	private GameObject target;

	void Start () {
		// 名前でオブジェクトを特定するので一言一句合致させること（ポイント）
		target = GameObject.Find ("nor_Player");

	}

	void Update () {
		// 「LookAtメソッド」の活用（ポイント）
		if (target != null) {
			Vector3 diff = (this.target.transform.position - this.transform.position).normalized;
			this.transform.rotation = Quaternion.FromToRotation (Vector3.up, diff);
		}
	}
}
