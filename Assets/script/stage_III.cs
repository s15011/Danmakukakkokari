using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class stage_III : MonoBehaviour {
	Common common;

	IEnumerator Start ()
	{
		// Spaceshipコンポーネントを取得
		common = GetComponent<Common> ();

		// startShotがfalseの場合、ここでコルーチンを一時停止
		if (common.startShot == false) {
			yield return new WaitForSeconds(2.0f);
		}

		while (true) {

			// 子要素を全て取得する
			for (int i = 0; i < transform.childCount; i++) {

				Transform shotPosition = transform.GetChild(i);

				// ShotPositionの位置/角度で弾を撃つ
				common.Shot (shotPosition);
			}

			// shotDelay秒待つ
			yield return new WaitForSeconds (common.shotDelay);
		}
	}
}
