using System.Collections;
using System.Collections.Generic;
using UnityEngine.EventSystems;
using UnityEngine;

/*
 * set up the tile
 * */
public class Tile : MonoBehaviour
{
	BuildManager buildManager;
	private Color startColor;
	private Renderer rend;
	public Turret turret;
    public Vector3 positionOffeset;

    public void ChangeColor()
	{
		rend.material.color = Color.green;//change tile color
	}

	public void ResetColor()
	{
		rend.material.color = startColor;//reset tile color
	}

	void Start()
	{
		rend = gameObject.GetComponent<Renderer>();//get tile renderer
		startColor = rend.material.color;//get initial color
		buildManager = BuildManager.instance;
	}

	void OnMouseUpAsButton()
	{
        //		if (Camera.current.name == "Tower Camera") {
        //			return;
        //		}
		if (EventSystem.current.IsPointerOverGameObject() ||
			EventSystem.current.currentSelectedGameObject != null) {
			return;
		}
//        if (turret != null)
//		{
//			Debug.Log("Have turret");
//			return;
//		}

		buildManager.SelectedTile(this);
        //towerSelectMenu.enabled = true;
        //towerSelectMenu.transform.position = transform.position +new Vector3 (0,0,4);
       
    }

  
	public Turret GetTurret()
    {
        return turret;//return turret information
    }

	public void SetTurret(Turret turret)
    {
		//set the current turret
        this.turret = turret;
		if(turret != null)
			turret.SetTile (this);
    }
}
