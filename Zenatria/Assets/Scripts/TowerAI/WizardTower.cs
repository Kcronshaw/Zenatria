using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WizardTower : GenericTower
{
    [SerializeField]
    [FormerlySerializedAs("targetedEnemy")]
    protected GameObject _projectile = null;
    public GameObject projectile
    {
        get => _projectile;
        set => _projectile = value;
    }





    public void FixedUpdate()
    {
        if (nextAttack <= Time.time && targetedEnemy != null)
        {
            nextAttack = Time.time + attackSpeed;
            Debug.Log("ShadowWizardMoneyGang");
            Attack();
        }
    }


    override public void Attack()
    {
        
        GameObject fireball = Instantiate(projectile, this.transform);
        Rigidbody2D rbTemp = fireball.GetComponent<Rigidbody2D>();
        ProjectileScript projScript = fireball.GetComponent<ProjectileScript>();
        


        var dir = targetedEnemy.transform.position - fireball.transform.position;
        rbTemp.velocity = projScript.projectileSpeed * dir.normalized;


    }
}
