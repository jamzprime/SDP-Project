using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{

    public static Transform[] points;

    public void getPoints()
    {
       // Debug.Log(MapGenerator.instance.Waypoints.transform.childCount + "sdfsdfsdf");

        points = new Transform[MapGenerator.instance.Waypoints.transform.childCount];

        for (int i = 0; i < points.Length; i++)
        {
            points[i] = MapGenerator.instance.Waypoints.transform.GetChild(i);
        }

      //  Debug.Log(points.Length + 100);
    }
}