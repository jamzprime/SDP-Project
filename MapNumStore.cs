using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapNumStore : MonoBehaviour {

	public static int selectedMap;

	void Awake()
	{
		DontDestroyOnLoad (this);
	}
}