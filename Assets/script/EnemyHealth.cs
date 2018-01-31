using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class EnemyHealth : MonoBehaviour {

	//Commonのコンポーネント
	Common common;
	ex ex;

	//敵のHP
	public int enemyHP;
	private Slider slider;



//	private GameObject oya;

	private Button retry;
	private Button choice;


	// Use this for initialization
	void Start () {
		// （ポイント）GameObject.Find ("○○")の使い方を覚えよう。名前でオブジェクトを指定できる。
		slider = GameObject.Find ("EnemyHPSlider").GetComponent<Slider> ();

		// スライダーの最大値の設定
		slider.maxValue = enemyHP;

		// スライダーの現在値の設定
		slider.value = enemyHP;

		retry = GameObject.Find ("retry").GetComponent<Button> ();
		retry.gameObject.SetActive (false);

		choice = GameObject.Find ("choice").GetComponent<Button> ();
		choice.gameObject.SetActive (false);

		ex = GetComponent<ex> ();

	}
	
	// Update is called once per frame
	void Update () {
	}

	void OnTriggerEnter2D (Collider2D c)
	{

		if (c.gameObject.CompareTag ("PlayerBullet")) {
			enemyHP -= 1;
			slider.value = enemyHP;
			Destroy (c.gameObject);

			if (enemyHP == 0) {
				ex.Explosion ();
				this.gameObject.SetActive(false);
				retry.gameObject.SetActive (true);
				choice.gameObject.SetActive (true);
			}
		}

	}


}
