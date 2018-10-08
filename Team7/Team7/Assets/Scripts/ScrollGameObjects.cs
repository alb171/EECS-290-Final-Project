using System.Collections;
using UnityEngine;

public class ScrollGameObjects : MonoBehaviour {
	private Transform character;
	private Vector3 userObject;


	void Awake (){
		character = GameObject.Find("Character").transform; 
	}


	void Update (){
		transform.position = new Vector3(character.position.x,-7,11);
	}
}
