using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RotatePortal : MonoBehaviour {


	// Update is called once per frame
	void FixedUpdate () {
		transform.Rotate (0,5,0*Time.deltaTime);
	}
}
