using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BigSlime : MonoBehaviour
{
    
    public float speed = 1f;

    private Transform target;
    public int wavepointIndex = 0;

    public float distanceToTarget;

    public int health = 3;
    public int damageDealt = 1;

    public Spawner spawner;




    public GameObject babySlime;
    public int numberOfBabies;

    void Start()
    {
        target = Waypoints.points[0];
        transform.position = target.position;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
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


    public void SlimeSplit()
    {
        for (int i = 0; i < numberOfBabies; i++)
        {
            GameObject babyslime = Instantiate(babySlime, this.transform);
            spawner.EnemiesAlive++;
            BabySlime babieScripto = babyslime.GetComponent<BabySlime>();
            babieScripto.target = target;
            babieScripto.wavepointIndex = wavepointIndex;
        }
        
    }
}
