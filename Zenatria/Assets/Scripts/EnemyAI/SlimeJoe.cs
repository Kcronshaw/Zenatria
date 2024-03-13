using HutongGames.PlayMaker.Actions;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.Serialization;

public class SlimeJoe : EnemyJoe
{
    [Header("Slime Enemy Variables")]

    [SerializeField]
    [FormerlySerializedAs("babySlime")]
    private GameObject _slimeSpawner;
    public GameObject slimeSpawner
    {
        get => _slimeSpawner;
        set => _slimeSpawner = value;
    }

    [SerializeField]
    [FormerlySerializedAs("numberOfBabies")]
    private int _numberOfBabies = 5;
    public int numberOfBabies
    {
        get => _numberOfBabies;
        private set => _numberOfBabies = value;
    }


    public override void TakeDamage(int i)
    {
        health = health - i;

        healthbar.ChangeHealth(health);

        if (health <= 0 && dying == false)
        {

            dying = true;

            spawner.GetComponent<Spawner>().EnemyKilled();

            SlimeSplit(this.gameObject.transform, this);

            StartCoroutine(NotDeadYetButSoon());
        }
    }

    private static void SlimeSplit(Transform tran, SlimeJoe self)
    {

        GameObject slimespawner = Instantiate(self.slimeSpawner, self.transform.position, Quaternion.identity);
        SlimeSpawner spawner = slimespawner.GetComponent<SlimeSpawner>();
        spawner.currentPath = self.currentPath;
        spawner.wavepointIndex = self.wavepointIndex;
        spawner.numberOfBabies = self.numberOfBabies;
        self.spawner.transform.SetParent(null, true);

        

    }   

    public IEnumerator NotDeadYetButSoon()
    {
        yield return new WaitForSeconds(0.05f);
        Destroy(this.gameObject);
    }

}
