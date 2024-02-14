using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Healthbar : MonoBehaviour
{
    [SerializeField] Image healthbar;
    [SerializeField] int health, healthMax;
    [SerializeField] Enemy enemy;


    private void Start()
    {
        enemy.health = healthMax;
    }

    public void ChangeHealth(int currentHealth)
    {
        health = currentHealth;
        healthbar.transform.localScale = new Vector2((currentHealth / healthMax) * 4, 1);
    }


}
