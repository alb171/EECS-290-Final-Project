using System.Collections;
using UnityEngine;

public class SideScrollCamera : MonoBehaviour {
	private Transform character;
	private Vector3 camera;


	void Awake (){
		character = GameObject.Find("Character").transform;
		//camera = character.position - transform.position; 
	}


	void Update (){
		transform.position = new Vector3(character.position.x,0f,-100);
	}
}


