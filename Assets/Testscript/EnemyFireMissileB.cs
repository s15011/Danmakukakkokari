using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyFireMissileB : MonoBehaviour {

	public GameObject enemyMissilePrefab;
	public float missileSpeed;
	private int timeCount = 0;

	void Update () {

		timeCount += 1;

		// 発射間隔を短くする。
		// 「%」と「==」の意味を復習しましょう！（ポイント）
		if (timeCount % 5 == 0) {
			GameObject enemyMissile = Instantiate (enemyMissilePrefab, transform.position, Quaternion.identity) as GameObject;
			Rigidbody2D enemyMissileRb = enemyMissile.GetComponent<Rigidbody2D> ();
			enemyMissileRb.AddForce (transform.up.normalized * missileSpeed);

			// 10秒後に敵のミサイルを削除する。
			Destroy (enemyMissile, 10.0f);
		}
	}
}