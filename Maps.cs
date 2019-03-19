using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Maps : MonoBehaviour {

//	public static Maps instance;
    
	private static List<Vector3> map1 = new List<Vector3> {   //Map 1
		new Vector3 (6, 0, 0),
		new Vector3 (6, 0, -4),
		new Vector3 (2, 0, -4),
		new Vector3 (2, 0, -14),
		new Vector3 (14, 0, -14),
		new Vector3 (14, 0, -11),
		new Vector3 (5, 0, -11),
		new Vector3 (5, 0, -7),
		new Vector3 (14, 0, -7),
		new Vector3 (14, 0, -4),
		new Vector3 (10, 0, -4),
		new Vector3 (10, 0, 0)
	};

	private static List<Vector3> map2 = new List<Vector3>{    //Map 2
		new Vector3 (7, 0, 0),
		new Vector3 (7, 0, -2),
		new Vector3 (2, 0, -2),
		new Vector3 (2, 0, -6),
		new Vector3 (10, 0, -6),
		new Vector3 (10, 0, -10),
		new Vector3 (6, 0, -10),
		new Vector3 (6, 0, -6),
		new Vector3 (10, 0, -6),
		new Vector3 (10, 0, -10),
		new Vector3 (14, 0, -10),
		new Vector3 (14, 0, -13),
		new Vector3 (9, 0, -13),
		new Vector3 (9, 0, -16)
	};

	private static List<Vector3> map3 = new List<Vector3> {
		new Vector3 (0, 0, -15),
		new Vector3 (6, 0, -15),
		new Vector3 (6, 0, -12),
		new Vector3 (4, 0, -12),
		new Vector3 (4, 0, -11),
		new Vector3 (3, 0, -11),
		new Vector3 (3, 0, -9),
		new Vector3 (2, 0, -9),
		new Vector3 (2, 0, -6),
		new Vector3 (3, 0, -6),
		new Vector3 (3, 0, -4),
		new Vector3 (4, 0, -4),
		new Vector3 (4, 0, -3),
		new Vector3 (6, 0, -3),
		new Vector3 (6, 0, -2),
		new Vector3 (10, 0, -2),
		new Vector3 (10, 0, -3),
		new Vector3 (12, 0, -3),
		new Vector3 (12, 0, -4),
		new Vector3 (13, 0, -4),
		new Vector3 (13, 0, -6),
		new Vector3 (14, 0, -6),
		new Vector3 (14, 0, -9),
		new Vector3 (13, 0, -9),
		new Vector3 (13, 0, -11),
		new Vector3 (12, 0, -11),
		new Vector3 (12, 0, -12),
		new Vector3 (10, 0, -12),
		new Vector3 (10, 0, -15),
		new Vector3 (16, 0, -15)
	};

	public static List<List<Vector3>> maps = new List<List<Vector3>>{map1,map2,map3};
	public static List<int> maxWaves = new List<int>{20,20,20};
//
//	void Awake()
//	{
//		if (instance != null)
//			return;
//		instance = this;
//	}
//
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
