using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class ProjectileScript : MonoBehaviour
{
    [SerializeField]
    [FormerlySerializedAs("projectileSpeed")]
    protected float _projectileSpeed = 0;
    public float projectileSpeed
    {
        get => _projectileSpeed;
        set => _projectileSpeed = value;
    }

    [SerializeField] GenericTower genTower;

    private void Start()
    {
        genTower = GetComponentInParent<GenericTower>();
    }
    
    private void FixedUpdate()
    {

        var step = projectileSpeed * Time.deltaTime;

        transform.position = Vector3.MoveTowards(transform.position, genTower.targetedEnemy.transform.position, step);

        if (Vector2.Distance(transform.position, genTower.targetedEnemy.transform.position) <= 0.2)
        {
            genTower.targetedEnemyScript.TakeDamage(genTower.attackDamage);
            Destroy(this.gameObject);
        }



    }
}
