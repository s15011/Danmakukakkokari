using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Exit : MonoBehaviour {
	
	// Update is called once per frame
	void Update () {
		if (Application.platform == RuntimePlatform.Android &&
			(Input.GetKey (KeyCode.Home) || Input.GetKey (KeyCode.Escape) || Input.GetKey (KeyCode.Menu))) {
			Application.Quit ();
		}
	}
}
