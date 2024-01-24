using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Waypoints : MonoBehaviour
{

    public static (float X, float Y)[] waypoints;

    public static Vector2[] paths;



    private void Awake()
    {
        waypoints = new (float X, float Y)[transform.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            var temp = transform.GetChild(i);
            waypoints[i] = (temp.position.x, temp.position.y);
        }

        for (int i = 0;i < waypoints.Length-1; i++)
        {
            waypoints[i+1] - waypoints[i]
        }
    }



}
