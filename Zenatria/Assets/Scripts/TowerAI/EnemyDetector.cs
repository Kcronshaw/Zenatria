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
            
        }
    }

   

    
}
