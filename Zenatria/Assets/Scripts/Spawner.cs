using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;
using UnityEditor.Callbacks;

public class Spawner : MonoBehaviour
{

    public int EnemiesAlive = 0;
    public Transform spawnPoint;
    [SerializeField] Text enemiesAliveText;

    [SerializeField] int waveIndex;
    [SerializeField] bool tutorial;

    private bool runBefore = false;



    [System.Serializable]
    public struct BigWave 
    {
        public List<Wave> miniWave;
        public int cashGen;
    }


    [SerializeField]
    public BigWave[] waves;

    private void Update()
    {
        //enemiesAliveText.text = EnemiesAlive.ToString();

        // Debug.Log("Running spawner update");
        if (EnemiesAlive > 0)
        {
            return;
        }

        if (Input.GetKeyDown("space"))
        {
            StartCoroutine(SpawnWave());
        }


        if (EnemiesAlive <= 0 && runBefore == false)
        {
            runBefore = true;
            TowerBuilder.instance.money += waves[waveIndex].cashGen;
        }

        

    }



    IEnumerator SpawnWave()
    {

        runBefore = false;
        
        Debug.Log(waveIndex);

        for(int i = 0; i <= waves[waveIndex].miniWave.Count - 1; i++ )
        {
            EnemiesAlive += waves[waveIndex].miniWave[i].count;

            for (int n = 0; n < waves[waveIndex].miniWave[i].count; n++)
            {
                SpawnEnemy(waves[waveIndex].miniWave[i].enemy);

                yield return new WaitForSeconds(waves[waveIndex].miniWave[i].rate);
            }
        }

            


        waveIndex++;

    }

    void SpawnEnemy(GameObject enemy)
    {
        GameObject enemyUnit = Instantiate(enemy, transform, transform);
        enemyUnit.GetComponent<EnemyJoe>().spawner = this;
    }

    public void EnemyKilled()
    {
        EnemiesAlive--;

    }

}
