using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class SlimeJoe : EnemyJoe
{
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
    private int _numberOfBabies = 5;
    public int numberOfBabies
    {
        get => _numberOfBabies;
        private set => _numberOfBabies = value;
    }


    public override void TakeDamage(int i)
    {
        health = health - i;


        if (health <= 0 && dying == false)
        {

            dying = true;

            SlimeSplit(transform, this);
            spawner.GetComponent<Spawner>().EnemyKilled();
            Destroy(gameObject);

        }
    }

    private static void SlimeSplit(Transform tran, SlimeJoe self)
    {
        for (int i = 0; i < self.numberOfBabies; i++)
        {
            GameObject babyslime = Instantiate(self.babySlime, tran);
            self.spawner.EnemiesAlive++;
            BabySlime babieScripto = babyslime.GetComponent<BabySlime>();
            babieScripto.target = self.target;
            babieScripto.wavepointIndex = self.wavepointIndex;
            self.babySlime.transform.SetParent(null, true);
        }

    }   

    private static void SlimeSplitJoe()
    {

    }

}
