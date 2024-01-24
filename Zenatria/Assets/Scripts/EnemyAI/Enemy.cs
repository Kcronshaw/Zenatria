using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class Enemy : MonoBehaviour
{

    [Header("Generic Enemy Variables")]

    [SerializeField]
    [FormerlySerializedAs("enemyType")]
    private string _enemyType;
    public string enemyType
    {
        get { return _enemyType; }
        protected set { _enemyType = value; }
    }

    [SerializeField]
    [FormerlySerializedAs("speed")]
    private float _speed = 1f;
    public float speed
    {
        get { return _speed; }
        set { _speed = value; }
    }

    [SerializeField]
    [FormerlySerializedAs("target")]
    private Transform _target;
    public Transform target
    {
        get { return _target; }
        set { _target = value; }
    }

    [SerializeField]
    [FormerlySerializedAs("wavepointIndex")]
    private int _wavepointIndex = 0;
    public int wavepointIndex
    {
        get => _wavepointIndex;
        set => _wavepointIndex = value;
    }

    [SerializeField]
    [FormerlySerializedAs("distanceToTarget")]
    private float _distanceToTarget;
    public float distanceToTarget
    {
        get => _distanceToTarget;
        private set => _distanceToTarget = value;
    }

    [SerializeField]
    [FormerlySerializedAs("health")]
    private int _health;
    public int health
    {
        get => _health;
        set => _health = value;
    }

    [SerializeField]
    [FormerlySerializedAs("damageDealt")]
    private int _damageDealt = 1;
    public int damageDealt
    {
        get => _damageDealt;
        set => _damageDealt = value;
    }

    [SerializeField]
    [FormerlySerializedAs("spawner")]
    private Spawner _spawner;
    public Spawner spawner
    {
        get => _spawner;
        set => _spawner = value;
    }

    [SerializeField]
    [FormerlySerializedAs("dying")]
    private bool _dying = false;
    public bool dying
    {
        get => _dying;
        set => _dying = value;
    }

    [SerializeField]
    [FormerlySerializedAs("spriteRenderer")]
    private SpriteRenderer _spriteRenderer;
    public SpriteRenderer spriteRenderer
    {
        get => _spriteRenderer;
        set => _spriteRenderer = value;
    }



    [Header("Slime Enemy Variables")]

    [SerializeField]
    [FormerlySerializedAs("babySlime")]
    private GameObject _babySlime;
    public GameObject babySlime
    {
        get => _babySlime;
        set => _babySlime = value;
    }

    [SerializeField]
    [FormerlySerializedAs("numberOfBabies")]
    private int _numberOfBabies;
    public int numberOfBabies
    {
        get => _numberOfBabies;
        set => _numberOfBabies = value;
    }

    [SerializeField]
    [FormerlySerializedAs("parentSlime")]
    private GameObject _parentSlime;
    public GameObject parentSlime
    {
        get => _parentSlime;
        set => _parentSlime = value;
    }

    [SerializeField]
    [FormerlySerializedAs("parentSlimeScript")]
    private Enemy _parentSlimeScript;
    public Enemy parentSlimeScript
    {
        get => _parentSlimeScript;
        set => _parentSlimeScript = value;
    }

    [SerializeField]
    [FormerlySerializedAs("spawnerObject")]
    private GameObject _spawnerObject;
    public GameObject spawnerObject
    {
        get => _spawnerObject;
        set => _spawnerObject = value;
    }

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
            //I genuinely dont know why i need to wait here but i fucking do deal with it
        }
        else
        {
            target = Waypoints.points[0];
            transform.position = target.position;
            spriteRenderer = this.gameObject.GetComponent<SpriteRenderer>();
        }


        spawnerObject = GameObject.FindGameObjectWithTag("Spawner");
        spawner = spawnerObject.GetComponent<Spawner>();

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
                spawner.EnemiesAlive --;
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
