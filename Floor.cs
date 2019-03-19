using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

public class Floor : MonoBehaviour {

	void OnMouseUpAsButton()
	{
		if (EventSystem.current.IsPointerOverGameObject() ||
			EventSystem.current.currentSelectedGameObject != null) {
			return;
		}
		BuildManager.instance.ResetPreviousColor ();
		BuildManager.instance.HideBuyMenu ();
	}
}
