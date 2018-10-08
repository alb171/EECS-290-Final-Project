using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NextLevel : MonoBehaviour {


	public string winScreen;

	// Use this for initialization
	void Start () {
	}

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
		SceneManager.LoadScene(winScreen);
        }
    }

}
