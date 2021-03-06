using System.Collections;
using UnityEngine;

public class SunCloudMovement : MonoBehaviour {
	[SerializeField] private GameObject _cloud; 
	[SerializeField] private GameObject _cloud2;
	int speed = 10;

	// Used for initialization
	void Start () {
	}

	// Update the movement of the User Object based on the key presses
	void Update () {
		//Debug.Log ("Cloud is: " + _cloud.transform.position.x);
		_cloud.transform.Translate(0, Input.GetAxis("Vertical")*speed*Time.deltaTime, 0);
		_cloud2.transform.Translate(0, Input.GetAxis("Vertical")*-1*speed*Time.deltaTime, 0);
        _cloud.transform.position = new Vector3(_cloud.transform.position.x, Mathf.Clamp(_cloud.transform.position.y, -10f, 13f), _cloud.transform.position.z);
        _cloud2.transform.position = new Vector3(_cloud2.transform.position.x, Mathf.Clamp(_cloud2.transform.position.y, -10f, 13f), _cloud2.transform.position.z);
        /*if (_cloud.transform.position.x >= 7){ //|| _cloud.transform.position.x <= -60) {
			speed--;
		}
		else if (_cloud.transform.position.x <= -59){ //|| _cloud.transform.position.x <= -60) {
			speed--;
		}
		else if (_cloud.transform.position.x <= -55){ //|| _cloud.transform.position.x <= -60) {
				speed = 0;
			if (Input.GetKeyDown (KeyCode.RightArrow)) {
				speed = 10;
				_cloud.transform.Translate(Input.GetAxis("Horizontal")*speed*Time.deltaTime, 0, 0);
				_cloud2.transform.Translate(Input.GetAxis("Horizontal")*-1*speed*Time.deltaTime, 0, 0);
			}
		}*/
        if (Input.GetKeyDown (KeyCode.UpArrow) || Input.GetKeyDown (KeyCode.DownArrow) || Input.GetKey(KeyCode.W) || Input.GetKey(KeyCode.S)) {
			speed =10;
			//_cloud.transform.Translate(Input.GetAxis("Horizontal")*speed*Time.deltaTime, 0, 0);
			//_cloud2.transform.Translate(Input.GetAxis("Horizontal")*-1*speed*Time.deltaTime, 0, 0);
		}
		else if (Input.GetKeyUp (KeyCode.UpArrow) || Input.GetKeyUp (KeyCode.DownArrow) || Input.GetKeyUp(KeyCode.W) || Input.GetKeyUp(KeyCode.S)) {
			speed = 0;
			//_cloud.transform.Translate(Input.GetAxis("Horizontal")*speed*Time.deltaTime, 0, 0);
			//_cloud2.transform.Translate(Input.GetAxis("Horizontal")*-1*speed*Time.deltaTime, 0, 0);
		}

	}
}
