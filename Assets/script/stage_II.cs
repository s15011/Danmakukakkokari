using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_II: MonoBehaviour {

	//Commonのコンポーネント
	Common common;
	int bullet_num;

	public GameObject enemyFireMissilePrefab;
	public int wayNumber;


	// Use this for initialization
	void Start () {
		for (int i = 0; i < wayNumber; i++) {

			// ★改良
			// 小数の場合「f」を必ずつけること。fを忘れるとエラーになります。（ポイント）
			GameObject enemyFireMissile = (GameObject)Instantiate (enemyFireMissilePrefab, transform.position, Quaternion.Euler (0, 0, 7.5f - (7.5f * wayNumber) + (15 * i)));
			enemyFireMissile.transform.SetParent (this.gameObject.transform);
		}
		StartCoroutine ("ShotCoroutine");
	}

	public IEnumerator ShotCoroutine(){
		//Commonのコンポーネントを取得
		common = GetComponent<Common> ();

		// startShotがfalseの場合、ここでコルーチンを一時停止
		if (common.startShot == false) {
			yield return new WaitForSeconds(2.0f);
		}

		while (true) {
			// 子要素を全て取得する
			//			for (int i = 0; i < transform.childCount; i++) {

			Transform ShotPosition = transform.GetChild (0);

			// ShotPositionの位置/角度で弾を撃つ
			common.Shot (ShotPosition);
			bullet_num += 1;


			if (bullet_num % 15 == 0) {
				yield return new WaitForSeconds (1.0f);

				//				for (int i = 0; i < wayNumber; i++) {
				//
				//					// ★改良
				//					// 小数の場合「f」を必ずつけること。fを忘れるとエラーになります。（ポイント）
				//					GameObject enemyFireMissile = (GameObject)Instantiate (enemyFireMissilePrefab, transform.position, Quaternion.Euler (0, 0, 7.5f - (7.5f * wayNumber) + (15 * i)));
				//					enemyFireMissile.transform.SetParent (this.gameObject.transform);
			}
			//			}

			//			}

			// shotDelay秒待つ
			yield return new WaitForSeconds (common.shotDelay);
		}
	}
}
