using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{

    public GameObject targetedEnemy = null;
    public Enemy targetedEnemyScript = null;

    public int attackDamage;
    public float nextAttack = 0;
    public float attackSpeed;


    public GameObject attackAnimation;
    public GameObject attackSwing;

    public CircleCollider2D rangeVisual;


    void Start()
    {
        
    }



    private void Update()
    {



    }



    public void Attack()
    {
        Debug.Log("running");
        targetedEnemyScript.TakeDamage(attackDamage);
        //Instantiate(attackAnimation);

    }



}
