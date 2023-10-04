using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{

    public int EnemiesAlive = 0;
    public Transform spawnPoint;

    private int waveIndex;
    public Wave[] waves;

    private void Update()
    {

        if (EnemiesAlive > 0)
        {
            return;
        }

        if (Input.GetKeyDown("space"))
        {
            StartCoroutine(SpawnWave());
        }

    }



    IEnumerator SpawnWave()
    {
        waveIndex++;
        Debug.Log(waveIndex);

        for (int i = 0; i < waveIndex; i++)
        {

            EnemiesAlive = waves[i].count;

            for (int n = 0; n < waves[i].count; n++)
            {
                SpawnEnemy(waves[i].enemy);

                yield return new WaitForSeconds(waves[i].rate);
            }

        }


    }

    void SpawnEnemy(GameObject enemy)
    {
        GameObject enemyUnit = Instantiate(enemy, transform, transform);
        enemyUnit.GetComponent<Enemy>().spawner = this;
    }

    public void EnemyKilled()
    {
        EnemiesAlive--;
        if (EnemiesAlive <= 0)
        {
            TowerBuilder.instance.money += waves[waveIndex - 1].cashGen;
        }

    }

}
