using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TowerBuilder : MonoBehaviour
{
    public static TowerBuilder instance;

    public int money = 50;
    private void Awake()
    {
        //lets other scripts refernce this without needing to hard code it, or use inspector
        if (instance == null) //in case 2 tower builders are put in somehow
        {
            instance = this;
        }

    }


    public GameObject normalTowerPrefab;

    private void Start()
    {
        
    }


    public void createTower(int towerToBuild)
    {
        switch (towerToBuild)
        {
            case 1:
                Instantiate(normalTowerPrefab);
                break;
            default:
                break;
        }
    }

}
