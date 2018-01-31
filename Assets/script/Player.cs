using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Player : MonoBehaviour {

	//Commonのコンポーネント
	Common common;
	ex ex;

	//プレイヤーの場所
	private Vector3 playerPos;

	//タッチした場所
	private Vector3 mousePos;

	private bool stoptime;

	private Button retry;
	private Button choice;


	// Startメソッドをコルーチンとして呼び出す
	IEnumerator Start ()
	{

		retry = GameObject.Find ("_retry").GetComponent<Button> ();
		retry.gameObject.SetActive (false);

		choice = GameObject.Find ("_choice").GetComponent<Button> ();
		choice.gameObject.SetActive (false);

		common = GetComponent<Common> ();
		ex = GetComponent<ex> ();

		// startShotがfalseの場合、ここでコルーチンを一時停止
		if (common.startShot == false) {
			yield return new WaitForSeconds(2.0f);
		}

		while (true) {
			// 弾をプレイヤーと同じ位置/角度で作成
			common.Shot(transform);
			// 0.05秒待つ
			yield return new WaitForSeconds (common.shotDelay);
		}
	}
	
	// Update is called once per frame
	void Update () {

		if (Input.GetMouseButtonDown (0)) {
			playerPos = this.transform.position;
			mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
		}

		if (Input.GetMouseButton (0)) {

			Vector3 prePos = this.transform.position;
			Vector3 diff = Camera.main.ScreenToWorldPoint (Input.mousePosition) - mousePos;

			//タッチ対応デバイス向け、1本目の指にのみ反応
			if (Input.touchSupported) {
				diff = Camera.main.ScreenToWorldPoint (Input.GetTouch (0).position) - mousePos;
			}

			diff.z = 0.0f;
			this.transform.position = playerPos + diff;

		}

		if (Input.GetMouseButtonUp (0)) {
			playerPos = Vector3.zero;
			mousePos = Vector3.zero;
		}
	
		Clamp ();


	}
	void Clamp ()
	{
		// 画面左下のワールド座標をビューポートから取得
		Vector2 min = Camera.main.ViewportToWorldPoint(new Vector2(0, 0));

		// 画面右上のワールド座標をビューポートから取得
		Vector2 max = Camera.main.ViewportToWorldPoint(new Vector2(1, 1));

		// プレイヤーの座標を取得
		Vector2 pos = transform.position;

		// プレイヤーの位置が画面内に収まるように制限をかける
		pos.x = Mathf.Clamp (pos.x, min.x, max.x);
		pos.y = Mathf.Clamp (pos.y, min.y + 0.75f, max.y);

		// 制限をかけた値をプレイヤーの位置とする
		transform.position = pos;
	}
	void OnTriggerEnter2D (Collider2D c){

		// レイヤー名を取得
		string layerName = LayerMask.LayerToName(c.gameObject.layer);

		// レイヤー名がBullet (Enemy)の時は弾を削除
		if( layerName == "Bullet(Enemy)"){
			// 弾の削除
			Destroy(c.gameObject);
			this.gameObject.SetActive(false);
		}

		// レイヤー名がBullet (Enemy)またはEnemyの場合は爆発
		if (layerName == "Bullet(Enemy)" || layerName == "Enemy") {
			// 爆発する
			ex.Explosion ();

			this.gameObject.SetActive(false);
			retry.gameObject.SetActive (true);
			choice.gameObject.SetActive (true);
			// プレイヤーを削除
//			Destroy (gameObject);

		}
	}


}
