using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Waypoints : MonoBehaviour
{

    public static Transform[] waypoints;

    //public static Vector2[] paths; // research if elements should be linkedlists since they need to connect

    public static Transform[] points;

    public static Bridge[] bridges;



    private void Awake()
    {
        /*
         * WIP
         * I am trying to make each path be an array of zeroes (like [0, 0, 0, 0]) where each 0 represents a normalized vector of whatever the path is
         * and the movement function takes that vector and moves the creature accordingly. If another value is present other than 0, that represents a path object
         * like 1 for bridge, 2 for broken bridge, etc.
         * potentially slow tower if they make a zone could have a code for a slow zone
         * perhaps creatures should track where they are in the array so that stuff like slime splitting can burst around itself
         */
        waypoints = new Transform[transform.childCount];
        for (int i = 0; i < waypoints.Length; i++)
        {
            waypoints[i] = transform.GetChild(i);
        }
        Bridge b = new Bridge(new Vector3(0, 1), new Vector3(0, 2));
        bridges.Append(b);


        /*
        for (int i = 0; i < waypoints.Length-1; i++)
        {
            paths[i] = waypoints[i + 1] - waypoints[i];        }
        */
        points = waypoints;
        
    }



}
