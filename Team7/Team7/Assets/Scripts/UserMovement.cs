using System.Collections;
using UnityEngine;

public class UserMovement : MonoBehaviour {
	[SerializeField] private GameObject _cloud; 
	[SerializeField] private GameObject _cloud2;
	public float speed = 10;

    // Update the movement of the User Object based on the key presses
    void Update() {
        //Debug.Log ("Cloud is: " + _cloud.transform.position.x);
        _cloud.transform.Translate(Input.GetAxisRaw("Horizontal") * speed * Time.deltaTime, 0, 0);
        _cloud2.transform.Translate(Input.GetAxisRaw("Horizontal") * -1 * speed * Time.deltaTime, 0, 0);
        _cloud.transform.localPosition = new Vector3(Mathf.Clamp(_cloud.transform.localPosition.x, -26f, 26f), _cloud.transform.localPosition.y, _cloud.transform.localPosition.z);
        _cloud2.transform.localPosition = new Vector3(Mathf.Clamp(_cloud2.transform.localPosition.x, -26f, 26f), _cloud2.transform.localPosition.y, _cloud2.transform.localPosition.z);
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
        if (Input.GetKey(KeyCode.LeftArrow) || Input.GetKey(KeyCode.RightArrow) || Input.GetKey(KeyCode.A) || Input.GetKey(KeyCode.D)) {
			speed =10;
			//_cloud.transform.Translate(Input.GetAxis("Horizontal")*speed*Time.deltaTime, 0, 0);
			//_cloud2.transform.Translate(Input.GetAxis("Horizontal")*-1*speed*Time.deltaTime, 0, 0);
		}
		else if (Input.GetKeyUp (KeyCode.LeftArrow) || Input.GetKeyUp (KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.A) || Input.GetKeyUp(KeyCode.D)) {
			speed = 0;
			//_cloud.transform.Translate(Input.GetAxis("Horizontal")*speed*Time.deltaTime, 0, 0);
			//_cloud2.transform.Translate(Input.GetAxis("Horizontal")*-1*speed*Time.deltaTime, 0, 0);
		}

	}
}
