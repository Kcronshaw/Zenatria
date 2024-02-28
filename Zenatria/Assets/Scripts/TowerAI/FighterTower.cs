using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FighterTower : GenericTower
{
    void Start()
    {

    }



    private void FixedUpdate()
    {
        if (targetedEnemy != null)
        {
            var step = miniSpeed * Time.deltaTime;

            distanceToTarget = Vector3.Distance(miniTower.transform.position, targetedEnemy.transform.position);
            if (distanceToTarget >= miniRange)
            {
                Debug.Log("blud is runnin");
                miniTower.transform.position = Vector3.MoveTowards(miniTower.transform.position, targetedEnemy.transform.position, step);
            }
            else if (Time.time >= nextAttack)
            {
                Attack();
                nextAttack = Time.time + attackSpeed;
            }
        }
    }





    override public void Upgrade()
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


        currentLevel++;

    }


}
