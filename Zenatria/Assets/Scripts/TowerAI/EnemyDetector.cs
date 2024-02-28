using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyDetector : MonoBehaviour
{
    public float range;
    //public GameObject parentTower;

    [SerializeField] GameObject parentTower;
    GenericTower genericTower;
    public GameObject enemy;


    void Start()
    {
        parentTower = transform.parent.gameObject;
        genericTower = parentTower.GetComponent<GenericTower>();   
    }

    // Update is called once per frame
    void Update()
    {


        if (genericTower.targetedEnemy != null && Time.time >= genericTower.nextAttack)
        {
            Debug.Log("maybe attacking of somethign");
            genericTower.Attack();
            genericTower.nextAttack = Time.time + genericTower.attackSpeed;
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

            Debug.Log(enemyScript);

            if(enemyScript == null)
            {
                Debug.Log("gaming??????");
                continue;
            }


            float distance = Vector3.Distance(enemy.transform.position, this.gameObject.transform.position);

            if (distance >= range)
            {
                Debug.Log(distance);
                continue;
            }

            if (firstItem)
            {
                fighterScript.targetedEnemy = enemy;
                fighterScript.targetedEnemyScript = fighterScript.targetedEnemy.GetComponent<EnemyJoe>();
                firstItem = false;
                continue;
            }


            if (enemyScript.wavepointIndex >= genericTower.targetedEnemyScript.wavepointIndex)
            {
                if (enemyScript.distanceToTarget <= genericTower.targetedEnemyScript.distanceToTarget)
                {
                    fighterScript.targetedEnemy = enemy;
                    fighterScript.targetedEnemyScript = fighterScript.targetedEnemy.GetComponent<EnemyJoe>();
                }
            }



            //Debug.Log("wegetthere");
        }

        //Debug.Log("done " + targetedEnemy.transform.position.x);
    }
}
