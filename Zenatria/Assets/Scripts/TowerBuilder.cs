using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TowerBuilder : MonoBehaviour
{
    public static TowerBuilder instance;

    public int money = 50;
    public Text moneyText;

    public bool holdingTower = false;
    public int health = 100;
    public Text healthText;


    public GameObject fakeTowerPrefab;

    private void Awake()
    {
        //lets other scripts refernce this without needing to hard code it, or use inspector
        if (instance == null) //in case 2 tower builders are put in somehow
        {
            instance = this;
        }

    }


    

    private void Start()
    {
        
    }

    public void takeDamage(int damage)
    {
        health -= damage;
        healthText.text = "x" + health.ToString();
        if (health <= 0)
        {
            Debug.Log("KILL YOURSELF NOW!!!");
        }
    }

    public void createTower(int towerToBuild)
    {
        if (holdingTower == false)
        {

            switch (towerToBuild)
            {
                case 1:
                    Instantiate(fakeTowerPrefab);
                    break;
                default:
                    break;
            }

            holdingTower = true;


        }

        
    }

    public void CostUpdate()
    {
        moneyText.text = money.ToString();
    }

}
