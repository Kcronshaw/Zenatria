using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{

    public GameObject targetedEnemy = null;
    public EnemyJoe targetedEnemyScript = null;

    public int attackDamage;
    public float nextAttack = 0;
    public float attackSpeed;


    public GameObject attackAnimation;
    public GameObject attackSwing;

    public CircleCollider2D rangeVisual;

    public int currentLevel = 1;



    void Start()
    {
        
    }



    private void Update()
    {


    }



    public void Attack()
    {
        targetedEnemyScript.TakeDamage(attackDamage);
        //Instantiate(attackAnimation);

    }



    public void Upgrade()
    {
        switch (currentLevel)
        {
            case 1:
                attackSpeed -= 0.3f;
                break;
            case 2:
                attackDamage++;
                break;
            case 3:
                // resistance immunity
                break;
            case 4:
                attackSpeed -= 0.3f;
                break;
        }
        
        
        currentLevel ++;

        }




}
