using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuSelection : MonoBehaviour {

	public string firstLevel;

	// Use this for initialization
	void Start () {
		
	}

	public void StartGame(){
		SceneManager.LoadScene(firstLevel);
	}

	public void Settings(){
		SceneManager.LoadScene(firstLevel);
	}

	public void EndGame(){
		Application.Quit ();
	}

}
