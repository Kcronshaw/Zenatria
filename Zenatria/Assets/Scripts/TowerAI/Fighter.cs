using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{

    public GameObject targetedEnemy = null;
    Enemy targetedEnemyScript = null;

    public int attackDamage;
    public float nextAttack = 0;
    public float attackSpeed;

    public float range;

    public GameObject attackAnimation;
    public GameObject attackSwing;


    void Start()
    {
        
    }



    private void Update()
    {



        if(targetedEnemy != null && Time.time >= nextAttack)
        {
            Attack();
            nextAttack = Time.time + attackSpeed;
        }

        if(targetedEnemy == null)
        {
            DetectTarget();
        }

    }



    private void Attack()
    {
        Debug.Log("running");
        targetedEnemyScript.TakeDamage(attackDamage);
        //Instantiate(attackAnimation);

    }


    private void OnCollisionEnter2D(Collision2D collision)
    {
        DetectTarget();
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        DetectTarget();
    }

    private void DetectTarget()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Enemy");

        bool firstItem = true;

        foreach (GameObject enemy in enemies)
        {
            Enemy enemyScript = enemy.GetComponent<Enemy>();


            float distance = Vector3.Distance(enemy.transform.position, this.transform.position);

            if(distance >= range)
            {
                continue;
            }

            if (firstItem)
            {
                targetedEnemy = enemy;
                targetedEnemyScript = targetedEnemy.GetComponent<Enemy>();
                firstItem = false;
                continue;
            }

            Debug.Log(targetedEnemy.transform.position);

            if (enemyScript.wavepointIndex >= targetedEnemyScript.wavepointIndex)
            {
                if(enemyScript.distanceToTarget <= targetedEnemyScript.distanceToTarget)
                {
                    targetedEnemy = enemy;
                    targetedEnemyScript = targetedEnemy.GetComponent<Enemy>();
                }
            }
        }
       
        //Debug.Log("done " + targetedEnemy.transform.position.x);
    }

}
