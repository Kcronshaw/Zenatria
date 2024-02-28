using HutongGames.PlayMaker.Actions;
using System.Collections;
using System.Collections.Generic;
using System.Net;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;
using UnityEngine.Serialization;

public class EnemyJoe : MonoBehaviour
{

    [Header("Generic Enemy Variables")]

    [SerializeField]
    [FormerlySerializedAs("enemyType")]
    protected string _enemyType;
    public string enemyType
    {
        get => _enemyType;
        protected set => _enemyType = value;
    }

    [SerializeField]
    [FormerlySerializedAs("speed")]
    protected float _speed = 1f;
    public float speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    /*
    [SerializeField]
    [FormerlySerializedAs("target")]
    protected Transform _target;
    public Transform target
    {
        get => _target;
        set => _target = value;
    }
    */

    [SerializeField]
    [FormerlySerializedAs("wavepointIndex")]
    protected int _wavepointIndex = 0;
    public int wavepointIndex
    {
        get => _wavepointIndex;
        set => _wavepointIndex = value;
    }

    [SerializeField]
    [FormerlySerializedAs("distanceToTarget")]
    protected float _distanceToTarget;
    public float distanceToTarget 
    { 
        get => _distanceToTarget; 
        set => _distanceToTarget = value;
    }

    [SerializeField]
    [FormerlySerializedAs("health")]
    protected int _health;
    public int health
    {
        get => _health;
        set => _health = value;
    }

    [SerializeField]
    [FormerlySerializedAs("damageDealt")]
    protected int _damageDealt = 1;
    public int damageDealt
    {
        get => _damageDealt;
        set => _damageDealt = value;
    }

    [SerializeField]
    [FormerlySerializedAs("spawnerObject")]
    protected GameObject _spawnerObject;
    public GameObject spawnerObject
    {
        get => _spawnerObject;
        set => _spawnerObject = value;
    }

    [SerializeField]
    [FormerlySerializedAs("spawner")]
    protected Spawner _spawner;
    public Spawner spawner
    {
        get => _spawner;
        set => _spawner = value;
    }

    [SerializeField]
    [FormerlySerializedAs("dying")]
    protected bool _dying = false;
    public bool dying
    {
        get => _dying;
        set => _dying = value;
    }

    [SerializeField]
    [FormerlySerializedAs("spriteRenderer")]
    protected SpriteRenderer _spriteRenderer;
    public SpriteRenderer spriteRenderer
    {
        get => _spriteRenderer;
        set => _spriteRenderer = value;
    }

    [SerializeField]
    [FormerlySerializedAs("healthbar")]
    protected Healthbar _healthbar;
    public Healthbar healthbar
    {
        get => _healthbar;
        set => _healthbar = value;
    }






    protected Bridge bridgeOn = null;
    protected PathSegment currentPath;
    public PathSegment CurrentPath
    {
        get => currentPath;
    }


    protected virtual void Start()
    {
        
        transform.position = Waypoints.paths[0].Begin;
        currentPath = Waypoints.paths[0];
        spriteRenderer = GetComponent<SpriteRenderer>();

        spawnerObject = GameObject.FindGameObjectWithTag("Spawner");

        if (spawnerObject != null)
        {
            spawner = spawnerObject.GetComponent<Spawner>();
        }
        else
        {
            Debug.LogError("Spawner object not found.");
        }

    }

    // Update is called once per frame
    protected void FixedUpdate()
    {
        if (currentPath == null)
        {
            Debug.LogError("Path is null.");
            return;
        }
        
        if (!currentPath.Passable(this))
        {
            var altPath = Waypoints.FindAlternative(this, currentPath);
            if (altPath != null)
            {
                transform.position = altPath.Begin;
                currentPath = altPath;
            } else
            {
                transform.position = currentPath.Begin;
                // do something to repair bridge
            }
            return;
        }


        Vector3 dir = currentPath.Direction(); // this is normalized
        transform.Translate(dir * speed * Time.deltaTime);

        distanceToTarget = Vector3.Distance(transform.position, currentPath.End);

        if (distanceToTarget <= 0.1f) // when you reach the end of a given path
        {
            Debug.Log("End of Path reached for index " + wavepointIndex + ".");
            if (wavepointIndex >= Waypoints.paths.Length - 1) // checks if we're at the last path // subtract 1 for each branched path
            {
                Debug.Log("Ready to deal damage at end of final path, index " + wavepointIndex);
                DealDamage();
                if (spawner != null)
                {
                    spawner.EnemiesAlive--;
                }
                if (gameObject != null)
                {
                    Destroy(gameObject);
                }
                else
                {
                    Debug.LogError("Attempting to destroy a null object.");
                }
                return;
            } else
            {
                
                wavepointIndex++;
                Debug.Log("Final path not reached, next segment will be " + wavepointIndex + ".");


                currentPath = Waypoints.paths[wavepointIndex];
            }

            

        }
    }

    public virtual void TakeDamage(int i)
    {
        health -= i;


        if (health <= 0 && dying == false)
        {

            dying = true;

            Debug.Log("running");
            spawner.GetComponent<Spawner>().EnemyKilled();
            Destroy(this.gameObject);
        

        }
    }

    public virtual void DealDamage()
    {
        TowerBuilder.instance.takeDamage(damageDealt);
    }


    

}
