using HutongGames.PlayMaker.Actions;
using System.Collections;
using System.Collections.Generic;
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

    [SerializeField]
    [FormerlySerializedAs("target")]
    protected Transform _target;
    public Transform target
    {
        get => _target;
        set => _target = value;
    }

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

    
    

    protected virtual void Start()
    {
        target = Waypoints.points[0];
        transform.position = target.position;
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
        if (target == null)
        {
            Debug.LogError("Target is null.");
            return;
        }

        Vector3 dir = target.position - transform.position;
        transform.Translate(dir.normalized * speed * Time.deltaTime);

        distanceToTarget = Vector3.Distance(transform.position, target.position);

        if (distanceToTarget <= 0.1f)
        {

            if (wavepointIndex >= Waypoints.points.Length)
            {
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
            }

            wavepointIndex++;
            target = Waypoints.points[wavepointIndex];

        }
    }

    public virtual void TakeDamage(int i)
    {
        health = health - i;


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
