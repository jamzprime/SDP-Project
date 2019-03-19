using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

/*
 * The class control the turret building
 * get the selected tile position
 * build the turret on the selected tile
 * */
public class BuildManager : MonoBehaviour {

	public GameObject TileUI;
	public GameObject UpgradeUI;
	public Text sellValueText;
	private GameObject ActiveUI;
	private Tile selectedTile;
	private Tile previousTile;
	public static BuildManager instance;
	private Vector3 UIOffset = new Vector3(0,99,0);
	StatsManager statsManager;

	public List<Image> Images;

    [Header("Turret Set")]
	public Turret basicTurrentPrefab;
	public Turret missileLauncherPrefab;
	public Turret slowTurretPrefab;
	public Turret railCannonPrefab;

	private Turret turretToBuild;
	void Start()
	{
		statsManager = StatsManager.instance;
	}
    // Use this for initialization
    void Awake () {
		if (instance == null)
			instance = this;
		ActiveUI = TileUI;
		TileUI.SetActive(false);
		UpgradeUI.SetActive (false);

		foreach (Image item in Images) {
			item.alphaHitTestMinimumThreshold = 0.10f;
		}
	}

    //	void FixedUpdate()
    //	{
    //		
    //	}

    /*
     * select one tile
     * change the tile color
     * open the shop button
     * */
	public void SelectedTile(Tile tile)
	{
	    //If select same tile
		if (previousTile != null)
		{
			previousTile.ResetColor();//reset tile color
		}

        //if select other tile
		if (previousTile == tile)
		{
			ResetPreviousColor();//reset prvious tile color
			HideBuyMenu();
			//Added below code to RestPreviousColor()
//			previousTile = null;
			return;
		}

        //After select a new tile
		selectedTile = tile;
		previousTile = selectedTile;
		selectedTile.ChangeColor();//change tile color
		ShowBuyMenu(selectedTile);//activie the shop button
	}

    /*
     * reset the turret color to default
     * */
	public void ResetPreviousColor()
	{
		if (previousTile == null)
			return;
		previousTile.ResetColor();
		previousTile = null;
	}

    /*
     * activity shop button
     * @param tile
     * */
    public void ShowBuyMenu(Tile tile)
	{
		ActiveUI.SetActive (false);
		if (tile.GetTurret () != null) {
			ActiveUI = UpgradeUI;
			Turret turret = tile.GetTurret ();
			sellValueText.text = turret.GetSellPrice ().ToString ();
		} else
			ActiveUI = TileUI;
		ActiveUI.transform.position = tile.transform.position + UIOffset;
		ActiveUI.SetActive(true);
	}

	public void HideBuyMenu()
	{
		ActiveUI.SetActive(false);
	}

    

    public Turret GetTurretToBuild()
    {
        return turretToBuild;
    }

    /*
     * set the turret and build the turret
     * @param turret
     * */
	public void SetTurretToBuild(Turret turret)
    {
        
        turretToBuild = turret;

        //build the turret
		Turret builtTurret = Instantiate(turretToBuild, selectedTile.transform.position + selectedTile.positionOffeset, selectedTile.transform.rotation) as Turret;

        //record the turret built on tile
        selectedTile.SetTurret(builtTurret);
        HideBuyMenu();
		ResetPreviousColor();
		statsManager.UpdateTurretsPlacedCount();
    }

	public Tile GetSelectedTile()
	{
		return selectedTile;
	}

}
