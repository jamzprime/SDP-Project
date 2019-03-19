using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MainCamera : MonoBehaviour {

	Vector2?[] oldTouchPositions = {
		null,
		null
	};
	Vector2 oldTouchVector;
	private float _sensitivity = 3.0f;
	private float xPan;
	private float zPan;
	private float oldTouchDistance;

    private Vector3 ResetCamera;
    private Vector3 Origin;
    private Vector3 Diference;
    private bool Drag = false;
    void Start()
    {
        ResetCamera = Camera.main.transform.position;
    }

    void FixedUpdate () {
		string currentCameraName = null;
		if (Camera.current != null) {
			currentCameraName = Camera.current.name;
			if (currentCameraName == this.name) {
				if (Input.touchCount == 0) {
					oldTouchPositions [0] = null;
					oldTouchPositions [1] = null;
				} else if (Input.touchCount == 1) {

					if (oldTouchPositions [0] == null || oldTouchPositions [1] != null) {
						oldTouchPositions [0] = Input.GetTouch (0).position;
						oldTouchPositions [1] = null;
					} else {
//				Vector2 newTouchPosition = Input.GetTouch(0).position;
						Vector2 change = (Input.GetTouch (0).deltaPosition) * _sensitivity * Mathf.Deg2Rad;
//				xPan+= change.x;
//				zPan+= change.y;
						xPan = Mathf.Clamp (xPan -= change.x, 35.5f, 44.5f);
						zPan = Mathf.Clamp (zPan -= change.y, -63, -17);
						transform.position = new Vector3 (Mathf.Clamp (xPan, 35.5f, 44.5f), 100, Mathf.Clamp (zPan, -63, -17));
//				transform.Translate (xPan*Time.deltaTime, zPan*Time.deltaTime,0 );
//				oldTouchPositions[0] = newTouchPosition;
					}
				}
			}
		}

		
		//Below will only be used in Unity editor, WILL THROW ERRORS! TODO:REMOVE BELOW WHEN BUILDING!

       if (Input.GetMouseButton(0))
       {
           Diference = (Camera.main.ScreenToWorldPoint(Input.mousePosition)) - Camera.main.transform.position;
           if (Drag == false)
           {
               Drag = true;
               Origin = Camera.main.ScreenToWorldPoint(Input.mousePosition);
           }
       }
       else
       {
           Drag = false;
       }
       if (Drag == true)
       {
           Camera.main.transform.position = Origin - Diference;
       }
       //RESET CAMERA TO STARTING POSITION WITH RIGHT CLICK
       if (Input.GetMouseButton(1))
       {
           Camera.main.transform.position = ResetCamera;
       }
    }

    
   
}
