using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class back : MonoBehaviour {


	public int currentScene;
	// Use this for initialization
	void Start () {

	}
	
	// Update is called once per frame
	void Update () {
		if (Application.platform == RuntimePlatform.Android &&
		    (Input.GetKey (KeyCode.Home) || Input.GetKey (KeyCode.Escape) || Input.GetKey (KeyCode.Menu))) {
//			Application.Quit ();
			SceneManager.LoadScene (currentScene);
		}
	}
}
