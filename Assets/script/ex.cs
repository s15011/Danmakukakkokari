using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ex : MonoBehaviour {

	// Use this for initialization
	public GameObject explosion;

	// 爆発の作成
	public void Explosion ()
	{
		Instantiate (explosion, transform.position, transform.rotation);
	}
}
