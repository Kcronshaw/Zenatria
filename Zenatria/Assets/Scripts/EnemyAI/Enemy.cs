using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{

    [Header("Generic Enemy Variables")]

    public string enemyType;

    public float speed = 1f;

    public Transform target;
    public int wavepointIndex = 0;

    public float distanceToTarget;

    public int health = 3;
    public int damageDealt = 1;

    public Spawner spawner;

    public bool dying = false;

    public SpriteRenderer spriteRenderer;


    [Header("Slime Enemy Variables")]
    public GameObject babySlime;
    public int numberOfBabies;
    public GameObject parentSlime;
    public Enemy parentSlimeScript;


    Animator animator;
    Rigidbody2D rb;


    public GameObject spawnerObject;

    void Start()
    {

        if(enemyType == "Baby Slime")
        {
            parentSlime = this.gameObject.transform.parent.gameObject;
            transform.position = parentSlime.transform.position;
            parentSlimeScript = parentSlime.GetComponent<Enemy>();
            target = parentSlimeScript.target;
            Debug.Log(parentSlimeScript.target);
            wavepointIndex = parentSlimeScript.wavepointIndex;
            StartCoroutine(ParentDeletor()); 
            //I genuinely dont know why i need to wait here but i fucking do deal with it;
        }
        else
        {
            target = Waypoints.points[0];
            transform.position = target.position;
            spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        }


        spawnerObject = GameObject.FindGameObjectWithTag("Spawner");
        spawner = spawnerObject.GetComponent<Spawner>();
        animator = GetComponent<Animator>();
        rb = GetComponent<Rigidbody2D>();

    }

    
    void FixedUpdate()
    {
        

        Vector3 dir = target.position - transform.position;
        dir = dir.normalized;
        transform.Translate(dir * speed * Time.deltaTime);

        distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= 0.1f)
        {

            if (wavepointIndex >= Waypoints.points.Length)
            {
                DealDamage();
                spawner.EnemiesAlive --;
                Destroy(gameObject);
                return;
            }

            wavepointIndex++;
            target = Waypoints.points[wavepointIndex];

        }

        Debug.Log(dir);

        if (dir.y >= 0.8) //north
        {
            animator.SetInteger("NESW", 1);
        }
        else if (dir.x >= 0.8) //east
        {
            animator.SetInteger("NESW", 2);
        }
        else if (dir.y <= -0.8) //south
        {
            animator.SetInteger("NESW", 3);
        }
        else if (dir.x <= -0.8) //west
        {
            animator.SetInteger("NESW", 4);
        }

        


    }

    public void TakeDamage(int i)
    {
        health = health - i;


        if (health <= 0 && dying == false)
        {

            dying = true;

            if(enemyType == "Slime")
            {
                StartCoroutine(SlimeSplit());
            }
            else
            {
                Debug.Log("running");
                spawner.GetComponent<Spawner>().EnemyKilled();
                Destroy(this.gameObject);
            }
            
        }
    }

    public void DealDamage()
    {
        TowerBuilder.instance.takeDamage(damageDealt);
    }


    IEnumerator SlimeSplit()
    {
        spriteRenderer.enabled = false;
        for (int i = 0; i < numberOfBabies; i++)
        {
            yield return new WaitForSeconds(0.1f);
            GameObject babyslime = Instantiate(babySlime, this.transform);
            spawner.EnemiesAlive++;
            
        }
        
        yield return new WaitForSeconds(0.1f);
        spawner.GetComponent<Spawner>().EnemyKilled();
        Destroy(gameObject);

    }

    IEnumerator ParentDeletor()
    {
        Debug.Log("IEnumeratoring BITCH!!!!!!!!!!!");
        yield return new WaitForSeconds(0.01f);
        this.gameObject.transform.parent = null;

    }

}
