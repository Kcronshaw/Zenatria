using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RangeIndicator : MonoBehaviour
{
    public GameObject mainTower;
    EnemyDetector towerScript;
    void Start()
    {
        towerScript = mainTower.GetComponent<EnemyDetector>();
        this.gameObject.transform.localScale = new Vector3(2 * towerScript.range, 2 * towerScript.range);
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
