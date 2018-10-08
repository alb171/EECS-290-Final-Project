using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TornadoRotate : MonoBehaviour {

    public float rotateSpeed;

	void Update () {
        this.transform.Rotate(rotateSpeed * Vector3.up);
	}
}
