using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BabySlime : MonoBehaviour
{
    public float speed = 1f;

    public Transform target;
    public int wavepointIndex = 0;

    public float distanceToTarget;

    public int health = 3;
    public int damageDealt = 1;

    public Spawner spawner;


    [Header("Parent Slime Stuff")]

    public Enemy parentSlimeScript;

    void Start()
    {
        Debug.Log("spawning");
        transform.position = new Vector3(transform.parent.transform.position.x, transform.parent.transform.position.y);
        parentSlimeScript = this.GetComponentInParent<Enemy>();
        target = parentSlimeScript.target;
        wavepointIndex = parentSlimeScript.wavepointIndex;
        this.gameObject.transform.parent = null;

    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Debug.Log(target);
        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);

        distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= 0.1f)
        {

            if (wavepointIndex >= Waypoints.points.Length)
            {
                DealDamage();
                spawner.EnemiesAlive--;
                Destroy(gameObject);
                return;
            }

            wavepointIndex++;
            target = Waypoints.points[wavepointIndex];

        }


    }

    public void TakeDamage(int i)
    {
        health = health - i;


        if (health <= 0)
        {
            spawner.GetComponent<Spawner>().EnemyKilled();
            Debug.Log("calling");
            Destroy(gameObject);
        }
    }

    public void DealDamage()
    {
        TowerBuilder.instance.takeDamage(damageDealt);
    }
}
