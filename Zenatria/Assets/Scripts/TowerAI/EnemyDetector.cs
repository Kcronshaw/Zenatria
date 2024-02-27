using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public float range;
    //public GameObject parentTower;

    private GameObject parentTower;
    Fighter fighterScript;
    public GameObject enemy;


    void Start()
    {
        parentTower = transform.parent.gameObject;
        fighterScript = parentTower.GetComponent<Fighter>();   
    }

    // Update is called once per frame
    void Update()
    {


        if (fighterScript.targetedEnemy != null && Time.time >= fighterScript.nextAttack)
        {
            fighterScript.Attack();
            fighterScript.nextAttack = Time.time + fighterScript.attackSpeed;
        }

        /*

        if (fighterScript.targetedEnemy == null)
        {
            DetectTarget();
        }
         
        */

    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 6)
        {
            DetectTarget();
        }
    }

   

    private void DetectTarget()
    {

        Collider2D[] enemies = Physics2D.OverlapCircleAll(this.transform.position, range);

        bool firstItem = true;




        foreach (Collider2D enemyCollider in enemies)
        {
            enemy = enemyCollider.gameObject;
            
            EnemyJoe enemyScript = enemy.GetComponent<EnemyJoe>();

            if(enemyScript == null)
            {
                continue;
            }


            float distance = Vector3.Distance(enemy.transform.position, this.transform.position);

            if (distance >= range)
            {
                continue;
            }

            if (firstItem)
            {
                fighterScript.targetedEnemy = enemy;
                fighterScript.targetedEnemyScript = fighterScript.targetedEnemy.GetComponent<EnemyJoe>();
                firstItem = false;
                continue;
            }


            if (enemyScript.wavepointIndex >= fighterScript.targetedEnemyScript.wavepointIndex)
            {
                if (enemyScript.distanceToTarget <= fighterScript.targetedEnemyScript.distanceToTarget)
                {
                    fighterScript.targetedEnemy = enemy;
                    fighterScript.targetedEnemyScript = fighterScript.targetedEnemy.GetComponent<EnemyJoe>();
                }
            }
        }

        //Debug.Log("done " + targetedEnemy.transform.position.x);
    }
}
