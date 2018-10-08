using System.Collections;
using UnityEngine;

public class SideScroll : MonoBehaviour {
	private Transform character;
	public GameObject pausedUI;


	void Awake (){
		character = GameObject.Find("Character").transform;
	}


	void Update (){
		transform.position = new Vector3 (character.position.x, 1f, -100);
	}

}


