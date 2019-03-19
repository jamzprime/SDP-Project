using System.Collections;
using System.Collections.Generic;
using UnityEngine;

/*
 * set up the map
 * */
public class MapGenerator : MonoBehaviour {

	public static MapGenerator instance;

    //set GameObject and Transfrom for the objects

	public GameObject tile;
	public GameObject waypoint;
	public GameObject portal;
	public Transform Tiles;
	public Transform Waypoints;
	public Transform Portals; 

	private int MapSize = 17;               // the size of map is 17
	private Vector3 startLocation = new Vector3 (0, 0.75f, 0);
	private Vector3 waypointY = new Vector3 (0, 1, 0);
	private Vector3 xAxisChange = new Vector3 (5, 0, 0);
	private Vector3 zAxisChange = new Vector3 (0, 0, 5);

	public List<Vector3> waypoints;
	private List<Vector3> storedPath;

    /*
     *  set up map 1
     * */
//	private List<Vector3> map1 = new List<Vector3> {
//		new Vector3 (6, 0, 0),
//		new Vector3 (6, 0, -4),
//		new Vector3 (2, 0, -4),
//		new Vector3 (2, 0, -13),
//		new Vector3 (13, 0, -13),
//		new Vector3 (13, 0, -10),
//		new Vector3 (5, 0, -10),
//		new Vector3 (5, 0, -7),
//		new Vector3 (13, 0, -7),
//		new Vector3 (13, 0, -4),
//		new Vector3 (9, 0, -4),
//		new Vector3 (9, 0, -0)
//	};
//
//	private List<List<Vector3>> maps = new List<List<Vector3>>();
//
	void Awake()
	{
		if (instance != null) {
			return;
		}
//		BuildMapsList ();

        instance = this;
	}

//	private void BuildMapsList()
//	{
//		maps.Add (map1);
//	}
//
    /*
     * clean the map
     * */
	public void ClearMap ()
	{
		foreach (Transform child in Tiles) {
			Destroy (child.gameObject);
		}

		foreach (Transform child in Waypoints) {
			Destroy (child.gameObject);
		}

		foreach (Transform child in Portals) {
			Destroy (child.gameObject);
		}

		waypoints = null;
		storedPath = null;
	}
      
    //Generate Map
	public void GenerateMap (int mapNumber)
	{
		waypoints = new List<Vector3> ();
		for (int index = 0; index < Maps.maps[mapNumber].Count; index++) {
			waypoints.Add (Maps.maps[mapNumber][index] * 5 + waypointY);
		}

		GeneratePath (Maps.maps[mapNumber]);

		AddWaypoints (waypoints);

		AddPortals (Maps.maps[mapNumber]);

		for (int z = 0; z < MapSize; z++) {
			for (int x = 0; x < MapSize; x++) {
				if (!storedPath.Contains (new Vector3 (x, 0, -z))) {
					GameObject tileTemp = Instantiate (tile, (startLocation + (xAxisChange * x) - (zAxisChange * z)), transform.rotation, Tiles);
					tileTemp.name = string.Format ("Tile {0:0},{1:0}", tileTemp.transform.position.x, tileTemp.transform.position.z);
				}
			}
		}

        //Debug.Log(MapGenerator.instance.Waypoints.transform.childCount);
	}

    /*
     * add start point
     * add end point
     * */

	private void AddPortals(List<Vector3> _map)
	{
		GameObject portalStart = Instantiate (portal, new Vector3(_map[0].x*5, 0.5f, _map[0].z*5), transform.rotation, Portals);
		portalStart.name = string.Format("Start Portal {0:0},{1:0}",portalStart.transform.position.x,portalStart.transform.position.y);
		GameObject portalEnd = Instantiate (portal, new Vector3(_map[_map.Count-1].x*5, 0.5f, _map[_map.Count-1].z*5), transform.rotation, Portals);
		portalEnd.name = string.Format("End Portal {0:0},{1:0}",portalEnd.transform.position.x,portalEnd.transform.position.y);
	}

    /*
     * add way point
     * */
	private void AddWaypoints (List<Vector3> waypoints)
	{
		for (int index = 0; index < waypoints.Count; index++) {
			GameObject wp = Instantiate (waypoint, waypoints [index], transform.rotation, Waypoints);
			wp.name = string.Format ("Waypoint {0:0}: {1:0},{2:0}", index, wp.transform.position.x, wp.transform.position.z);
		}

        //Debug.Log(MapGenerator.instance.Waypoints.transform.childCount);
	}

    //generate path based on the waypoints

	private void GeneratePath (List<Vector3> _waypoints)
	{
		storedPath = new List<Vector3> ();
		for (int index = 0; index < _waypoints.Count; index++) {
			storedPath.Add (_waypoints [index]);
//			Debug.Log(" Waypoint Node added "+storedPath[storedPath.Count-1]);
			int xDiff;
			int zDiff;
			if (index < _waypoints.Count - 1) {
				xDiff = (int)_waypoints [index].x - (int)_waypoints [index + 1].x;
				zDiff = (int)_waypoints [index].z - (int)_waypoints [index + 1].z;

				if (xDiff == 0) {
					do {
						if (zDiff > 0) {
							storedPath.Add (storedPath [storedPath.Count - 1] - zAxisChange / 5);
							zDiff--;
						} else if (zDiff < 0) {
							storedPath.Add (storedPath [storedPath.Count - 1] + zAxisChange / 5);
							zDiff++;
						}
//						Debug.Log(zDiff+" Path added "+storedPath[storedPath.Count-1]);
					} while (zDiff != 0);
				} else if (zDiff == 0) {
					do {
						if (xDiff > 0) {
							storedPath.Add (storedPath [storedPath.Count - 1] - xAxisChange / 5);
							xDiff--;
						} else if (xDiff < 0) {
							storedPath.Add (storedPath [storedPath.Count - 1] + xAxisChange / 5);
							xDiff++;
						}
//						Debug.Log(xDiff+" Path added "+storedPath[storedPath.Count-1]);
					} while (xDiff != 0);
				}
			}
		}
	}
}