using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SlimeSpawner : MonoBehaviour
{
    public PathSegment currentPath;
    public int wavepointIndex;
    public int numberOfBabies;


    [SerializeField] GameObject babySlime;

    [SerializeField]
    [FormerlySerializedAs("spawner")]
    protected Spawner _spawner;
    public Spawner spawner
    {
        get => _spawner;
        set => _spawner = value;
    }

    [SerializeField]
    [FormerlySerializedAs("spawnerObject")]
    protected GameObject _spawnerObject;
    public GameObject spawnerObject
    {
        get => _spawnerObject;
        set => _spawnerObject = value;
    }


    void Start()
    {
        transform.parent = null;



        spawnerObject = GameObject.FindGameObjectWithTag("Spawner");

        if (spawnerObject != null)
        {
            spawner = spawnerObject.GetComponent<Spawner>();
        }
        else
        {
            Debug.LogError("Spawner object not found.");
        }





        StartCoroutine(SpawnTime());

            
    }

    public IEnumerator SpawnTime()
    { 

        for (int i = 0; i < numberOfBabies; i++)
        {
            GameObject babyslime = Instantiate(babySlime, this.transform);
            spawner.EnemiesAlive++;
            BabySlimeJoe babieScripto = babyslime.GetComponent<BabySlimeJoe>();
            babieScripto.currentPath = currentPath;
            babieScripto.wavepointIndex = wavepointIndex;
            babySlime.transform.SetParent(null, true);



            yield return new WaitForSeconds(0.2f);
        }
    }
}
