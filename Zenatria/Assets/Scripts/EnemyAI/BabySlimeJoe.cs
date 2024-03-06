using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BabySlimeJoe : SlimeJoe
{
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
    private EnemyJoe _parentSlimeScript;
    public EnemyJoe parentSlimeScript
    {
        get => _parentSlimeScript;
        set => _parentSlimeScript = value;
    }

    protected override void Start()
    {
        parentSlime = this.gameObject.transform.parent.gameObject;
        transform.position = parentSlime.transform.position;
        parentSlimeScript = parentSlime.GetComponent<EnemyJoe>(); // Depending on what the parent slime's script name ends up being, this needs to change
        currentPath = parentSlimeScript.CurrentPath;
        Debug.Log(parentSlimeScript.CurrentPath);
        wavepointIndex = parentSlimeScript.wavepointIndex;
        StartCoroutine(ParentDeletor());
        //I genuinely dont know why i need to wait here but i fucking do deal with it

        spawnerObject = GameObject.FindGameObjectWithTag("Spawner");
        spawner = spawnerObject.GetComponent<Spawner>();
    }

    // Has access to Enemy's FixedUpdate() method

    public override void TakeDamage(int i)
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

    public IEnumerator ParentDeletor()
    {
        Debug.Log("IEnumeratoring BITCH!!!!!!!!!!!");
        yield return new WaitForSeconds(0.01f);
        transform.parent = null;

    }

    // Has access to Enemy's DealDamage() method


}
