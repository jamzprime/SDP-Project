// Just add this script to your camera. It doesn't need any configuration.

using UnityEngine;
using UnityEngine.EventSystems;

public class TowerCamera : MonoBehaviour {
	Vector2?[] oldTouchPositions = {
		null,
		null
	};
	Vector2 oldTouchVector;
	private float _sensitivity = 8.0f;
	public float xRot;
	public float yRot;
	public Turret currentTurret = null;
	public float yRotTemp;
	private Transform cameraMount;

//	private float oldTouchDistance;

	void Start()
	{
//		xRot = 180-transform.rotation.y;
//		yRot = transform.rotation.x;
	}

	void FixedUpdate() {
		string currentCameraName = null;
		if (Camera.current != null) {
			currentCameraName = Camera.current.name;
			if (currentCameraName == this.name|| currentCameraName == "SceneCamera") {//add into if condition of || currentCameraName == "SceneCamera" for editor use;
			
				if (Input.touchCount == 0) {
//				Debug.Log ("touch count > 0");
					oldTouchPositions [0] = null;
					oldTouchPositions [1] = null;
				} else {
				
					if (!EventSystem.current.IsPointerOverGameObject (Input.GetTouch (0).fingerId)) {
						RotateCamera (0);
					} else {
						RotateCamera (1);
					}
				}
			}
		}
	}


	//Origional Code for rotating camera for TowerCamera, new code have been implemented so that camera follows position of CameraMount
//	void RotateCamera (int touch)
//	{
//		if (oldTouchPositions [0] == null || oldTouchPositions [1] != null) {
//			oldTouchPositions [0] = Input.GetTouch (touch).position;
//			oldTouchPositions [1] = null;
//		} else {
//			//				Vector2 newTouchPosition = Input.GetTouch(touch).position;
//			Vector2 change = (Input.GetTouch (touch).deltaPosition) * _sensitivity * Mathf.Deg2Rad;
//			xRot -= change.x;
//			yRot = Mathf.Clamp (yRot + change.y, -30f, 50f);
//			transform.rotation = Quaternion.Euler (yRot, xRot, 0);
//			//				oldTouchPositions[0] = newTouchPosition;
//			currentTurret.partRotate.transform.rotation = transform.rotation;
//		}
//	}

	void RotateCamera (int touch)
	{
		if (oldTouchPositions [0] == null || oldTouchPositions [1] != null) {
			oldTouchPositions [0] = Input.GetTouch (touch).position;
			oldTouchPositions [1] = null;
		} else {
//			Vector2 newTouchPosition = Input.GetTouch(touch).position;
			Vector2 change = (Input.GetTouch (touch).deltaPosition) * _sensitivity * Mathf.Deg2Rad;
			xRot -= change.x;
			yRot = Mathf.Clamp (yRot + change.y, -20f, 30f);
			transform.rotation = Quaternion.Euler (yRot, xRot, 0);
//			oldTouchPositions[0] = newTouchPosition;
			currentTurret.partRotate.transform.rotation = transform.rotation;
			transform.position = cameraMount.position;
			//Not needed
//			transform.rotation = currentTurret.transform.GetChild (1).GetChild(0).rotation;
		}
	}



	public void SetXYRot()
	{
		xRot = transform.eulerAngles.y;
		yRotTemp = transform.eulerAngles.x;
		if (yRotTemp > 50)
			yRotTemp -= 360;
		yRot = Mathf.Clamp (yRotTemp, -20f, 30f);
	}

	public void SetTurret (Turret turret)
	{
		currentTurret = turret;
		cameraMount = currentTurret.transform.GetChild (1).GetChild (0);
	}
}
