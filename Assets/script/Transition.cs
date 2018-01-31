using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Transition : MonoBehaviour {

	GameObject button;
	public int currentSceneIndex;

	public void StageSelect(){
		SceneManager.LoadScene ("stage");
	}

	public void stage_I(){
		SceneManager.LoadScene ("stage1");
	}

	public void stage_II(){
		SceneManager.LoadScene ("stage2");
	}

	public void stage_III(){
		SceneManager.LoadScene ("stage3");
	}

	public void choice(){
		SceneManager.LoadScene (1);
	}

	public void reload() {
		SceneManager.LoadScene (currentSceneIndex);
	}

	public void exit(){
		Application.Quit ();
	}


//
//	public void stage_III(){
//		SceneManager.LoadScene ("stage3");
//	}
//
//	public void stage_IV(){
//		SceneManager.LoadScene ("stage");
//	}
//
//	// Use this for initialization
//	void Start () {
//		button = GameObject.Find ("Canvas/Button");
//	}
//	
//	// Update is called once per frame
//	void Update () {
//		button.gameObject.SetActive(false);
//		button.SetActive(false);
//	}
}
