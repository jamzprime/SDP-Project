using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
    * the class would be awaked when the user click the turret shop button.
    * get the building turret message and send to build manager.
    * */
public class ShopTurret : MonoBehaviour {

	BuildManager buildManager;
	StatsManager statsManager;
	[Header("For TileUI and TurretUI")]
    public int standardTurretCost;
	public int missileLauncherCost;
    public int slowTurretCost;
	public int railCannonCost;

	[Header("For For TurretUI ONLY!")]
	public Camera topCam;
	public Camera towerCam;
	public TowerCamera towerCamera;
	public GameObject blocker;
	public Canvas towerCanvas;
	public Turret currentTurret;

    private void Start()
    {
        buildManager = BuildManager.instance;
		statsManager = StatsManager.instance;
    }


    /*
     * if user click basic turret button
     * the method would check player gold and send the turret information to build manager
     * */
    public void PurchaseStandardTurret()
	{
//		standardTurretCost = 20; //set the turret price

        //if user have enough money
		if (statsManager.currentGoldCount >= standardTurretCost)
		{ 
            //Debug.Log("standard turret purchase");
			buildManager.SetTurretToBuild(buildManager.basicTurrentPrefab);//sent the turret information to build manager
			statsManager.currentGoldCount -= standardTurretCost;//minus the turret money from player gold
			statsManager.UpdateGoldSCount(standardTurretCost);
        }
    }

    /*
     * if user click missile launcher button
     * the method would check player gold and send the turret information to build manager
     * */
    public void PurchaseMissileLauncher()
    {
//		missileLauncherCost = 100;// set up turret price

        //if have enough money
		if (statsManager.currentGoldCount >= missileLauncherCost)
        {
            //Debug.Log("standard turret purchase");
            buildManager.SetTurretToBuild(buildManager.missileLauncherPrefab);//sent the turret information to build manager
			statsManager.currentGoldCount -= missileLauncherCost;//minus the turret money from player gold
			statsManager.UpdateGoldSCount(missileLauncherCost);

        }
    }

    public void PurchaseSlowTurret()
    {
//        slowTurretCost = 50;// set up turret price

        //if have enough money
		if (statsManager.currentGoldCount >= slowTurretCost)
        {
            //Debug.Log("standard turret purchase");
            buildManager.SetTurretToBuild(buildManager.slowTurretPrefab);//sent the turret information to build manager
			statsManager.currentGoldCount -= slowTurretCost;//minus the turret money from player gold
			statsManager.UpdateGoldSCount(slowTurretCost);

        }
    }

	public void PurchaseRailCannon()
	{
		//        slowTurretCost = 50;// set up turret price

		//if have enough money
		if (statsManager.currentGoldCount >= railCannonCost)
		{
			//Debug.Log("standard turret purchase");
			buildManager.SetTurretToBuild(buildManager.railCannonPrefab);//sent the turret information to build manager
			statsManager.currentGoldCount -= railCannonCost;//minus the turret money from player gold
			statsManager.UpdateGoldSCount(railCannonCost);

		}
	}

	public void SellTurret()
	{
		Tile currentTile = buildManager.GetSelectedTile ();
		Turret turret = currentTile.GetTurret ();
		string name = turret.name;
		if(name.Contains("Basic"))
			statsManager.currentGoldCount += (int)(standardTurretCost*0.5f);
		else if(name.Contains("Missile"))
			statsManager.currentGoldCount += (int)(missileLauncherCost*0.5f);
		else if(name.Contains("Slow"))
			statsManager.currentGoldCount += (int)(slowTurretCost*0.5f);
		
		currentTile.SetTurret (null);
		buildManager.HideBuyMenu();
		buildManager.ResetPreviousColor();
		turret.SellTurret ();
	}

	public void TowerCamera()
	{
		topCam.enabled = false;
		currentTurret = buildManager.GetSelectedTile ().GetTurret ();
		towerCamera.SetTurret (currentTurret);
		currentTurret.ActivateControl ();
		towerCam.transform.position = currentTurret.transform.GetChild (1).GetChild(0).position;
		towerCam.transform.rotation = currentTurret.transform.GetChild (1).GetChild(0).rotation;
		towerCamera.SetXYRot ();
		buildManager.HideBuyMenu();
		buildManager.ResetPreviousColor();
		blocker.SetActive (true);
		towerCam.enabled = true;
		towerCanvas.enabled = true;
	}

	public void TopCamera()
	{
		currentTurret.ResetControl ();
		towerCam.enabled = false;
		towerCanvas.enabled = false;
		topCam.enabled = true;
		blocker.SetActive (false);
	}

	public void TurretFire()
	{
		if (currentTurret.canShoot) {
			currentTurret.Shoot ();
			currentTurret.shootCooldown = 1 / (currentTurret.fireRate * 2);
		}
	}
}
