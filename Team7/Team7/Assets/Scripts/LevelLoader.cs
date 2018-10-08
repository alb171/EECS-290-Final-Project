using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelLoader : MonoBehaviour {

	//public string nextLevel;
	public string nextScene;

	// Use this for initialization
	void Start () {
		StartCoroutine ("LevelUp");
	}

	// Update is called once per frame
	void Update () {
	}
		

	IEnumerator LevelUp(){
			yield return new WaitForSeconds (2.5f);
			SceneManager.LoadScene (nextScene);
	}
}
