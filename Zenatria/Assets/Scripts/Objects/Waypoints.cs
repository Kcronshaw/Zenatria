using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Waypoints : MonoBehaviour
{

    [SerializeField]
    public static PathSegment[] paths;

    private void Awake()
    {
        Console.WriteLine("Waypoints.cs Awake() running");
        paths = new PathSegment[transform.childCount-1];
        for (int i = 0; i < paths.Length; i++)
        {
            Transform currentStart = transform.GetChild(i);
            Transform currentDestination = transform.GetChild(i+1);

            switch (currentStart.parent.tag)
            {
                case "Bridge":
                    Console.WriteLine("bridge detected at: " + currentStart.position.ToString());
                    paths[i] = new Bridge(currentStart.position, currentDestination.position);
                    break;
                default:
                    paths[i] = new PlainPath(currentStart.position, currentDestination.position);
                    break;
            }

           
        }
        
    }

    public static PathSegment FindAlternative(EnemyJoe enemy, PathSegment currentPath)
    {
        for (int i = 0; i < paths.Length; i++)
        {
            if (paths[i] != currentPath && 
                paths[i].Begin == currentPath.Begin &&
                paths[i].Passable(enemy))
            {
                return paths[i];
            }
        }

        return null;
    }



}
