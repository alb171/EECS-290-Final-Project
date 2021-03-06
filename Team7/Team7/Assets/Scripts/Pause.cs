using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Pause : MonoBehaviour {
	public GameObject pausedUI;

	void Update (){
		if (Input.GetKey(KeyCode.Escape))
		{
			Paused ();
		}
	}

	void Paused(){
		pausedUI.SetActive (true);
		Time.timeScale = 0f;
	}

	public void Resume(){
		pausedUI.SetActive (false);
		Time.timeScale = 1f;
	}

	public void EndGame(){
		Application.Quit ();
	}
}

