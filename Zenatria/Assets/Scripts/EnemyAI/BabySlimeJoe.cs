using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class BabySlimeJoe : SlimeJoe
{



    protected override void Start()
    {
        Debug.Log("im in a superposition of dead and alive!!!!!!");
        StartCoroutine(ParentDeletor());
        //I genuinely dont know why i need to wait here but i fucking do deal with it

        spawnerObject = GameObject.FindGameObjectWithTag("Spawner");
        spawner = spawnerObject.GetComponent<Spawner>();

        animator = this.gameObject.GetComponent<Animator>();

    }

    // Has access to Enemy's FixedUpdate() method


    public IEnumerator ParentDeletor()
    {
        yield return new WaitForSeconds(0.01f);
        transform.parent = null;

    }

    // Has access to Enemy's DealDamage() method
    public override void TakeDamage(int i)
    {
        health -= i;
        healthbar.ChangeHealth(health);

        if (health <= 0 && dying == false)
        {
            DeathMoment();
        }
    }

}
