using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Healthbar : MonoBehaviour
{
    [SerializeField] GameObject healthbarScaler;
    [SerializeField] float healthMax;
    [SerializeField] EnemyJoe enemy;


    private void Start()
    {
        enemy = this.GetComponentInParent<EnemyJoe>();
        healthMax = enemy.health;
    }

    public void ChangeHealth(int currentHealth)
    {
        Debug.Log(currentHealth / healthMax);
        healthbarScaler.transform.localScale = new Vector2((currentHealth / healthMax), 1);
    }


}
