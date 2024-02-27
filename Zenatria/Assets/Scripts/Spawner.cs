using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Serialization;

public class Spawner : MonoBehaviour
{

    public int EnemiesAlive = 0;
    public Transform spawnPoint;
    [SerializeField] Text enemiesAliveText;

    [SerializeField] int waveIndex;



    [System.Serializable]
    public struct BigWave { public List<Wave> miniWave; }
    [SerializeField]
    public BigWave[] waves;

    private void Update()
    {
        enemiesAliveText.text = EnemiesAlive.ToString();

        if (EnemiesAlive > 0)
        {
            return;
        }
        //if (Input.GetKeyDown("space"))
        //{
            //StartCoroutine(SpawnWave());
        //}

    }
    public void SpawnWaves()
    {
        StartCoroutine(SpawnWave());
    }


    IEnumerator SpawnWave()
    {
        
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
        enemyUnit.GetComponent<Enemy>().spawner = this;
    }

    public void EnemyKilled()
    {
        EnemiesAlive--;

    }

}
