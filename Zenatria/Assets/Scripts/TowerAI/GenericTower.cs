using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class GenericTower : MonoBehaviour
{

    [Header("Generic Tower Variables")]

    [SerializeField]
    [FormerlySerializedAs("targetedEnemy")]
    protected GameObject _targetedEnemy = null;
    public GameObject targetedEnemy
    {
        get => _targetedEnemy;
        set => _targetedEnemy = value;
    }


    [SerializeField]
    [FormerlySerializedAs("targetedEnemyScript")]
    protected EnemyJoe _targetedEnemyScript = null;
    public EnemyJoe targetedEnemyScript
    {
        get => _targetedEnemyScript;
        set => _targetedEnemyScript = value;
    }

    [SerializeField]
    [FormerlySerializedAs("attackDamage")]
    protected int _attackDamage;
    public int attackDamage
    {
        get => _attackDamage;
        set => _attackDamage = value;
    }

    [SerializeField]
    [FormerlySerializedAs("nextAttack")]
    protected float _nextAttack = 0;
    public float nextAttack
    {
        get => _nextAttack;
        set => _nextAttack = value;
    }

    [SerializeField]
    [FormerlySerializedAs("attackSpeed")]
    protected float _attackSpeed = 0;
    public float attackSpeed
    {
        get => _attackSpeed;
        set => _attackSpeed = value;
    }


    [SerializeField]
    [FormerlySerializedAs("targetedEnemy")]
    protected CircleCollider2D _rangeVisual;
    public CircleCollider2D rangeVisual
    {
        get => _rangeVisual;
        set => _rangeVisual = value;
    }


    [SerializeField]
    [FormerlySerializedAs("currentLevel")]
    protected int _currentLevel = 1;
    public int currentLevel
    {
        get => _currentLevel;
        set => _currentLevel = value;
    }



    [SerializeField]
    [FormerlySerializedAs("attackRange")]
    protected float _attackRange = 0;
    public float attackRange
    {
        get => _attackRange;
        set => _attackRange = value;
    }

    [SerializeField]
    [FormerlySerializedAs("distanceToTarget")]
    protected float _distanceToTarget = 0.2f;
    public float distanceToTarget
    {
        get => _distanceToTarget;
        set => _distanceToTarget = value;
    }

    [SerializeField]
    [FormerlySerializedAs("miniSpeed")]
    protected float _miniSpeed = 2;
    public float miniSpeed
    {
        get => _miniSpeed;
        set => _miniSpeed = value;
    }

    [SerializeField]
    [FormerlySerializedAs("miniTower")]
    protected GameObject _miniTower = null;
    public GameObject miniTower
    {
        get => _miniTower;
        set => _miniTower = value;
    }





    [SerializeField] protected float miniRange;
    [SerializeField] protected GameObject enemy;

    virtual public void Attack()
    {

        targetedEnemyScript.TakeDamage(attackDamage);

    }



    virtual public void Upgrade()
    {

        currentLevel++;

    }


    private void Update()
    {


        if (targetedEnemy == null)
        {
            Debug.Log("Detecting");
            DetectTarget();
        }




        if (Vector2.Distance(targetedEnemy.transform.position, transform.position) >= attackRange)
        {
            targetedEnemy = null;
        }


        


    }





    private void DetectTarget()
    {

        Collider2D[] enemies = Physics2D.OverlapCircleAll(this.transform.position, attackRange);

        bool firstItem = true;




        foreach (Collider2D enemyCollider in enemies)
        {


            enemy = enemyCollider.gameObject;

            EnemyJoe enemyScript = enemy.GetComponent<EnemyJoe>();


            if (enemyScript == null)
            {
                continue;
            }


            float distance = Vector2.Distance(enemy.transform.position, this.gameObject.transform.position);

            if (distance >= attackRange)
            {
                continue;
            }

            if (firstItem)
            {
                targetedEnemy = enemy;
                targetedEnemyScript = targetedEnemy.GetComponent<EnemyJoe>();
                firstItem = false;
                continue;
            }


            if (enemyScript.wavepointIndex >= targetedEnemyScript.wavepointIndex)
            {
                if (enemyScript.distanceToTarget <= targetedEnemyScript.distanceToTarget)
                {
                    targetedEnemy = enemy;
                    targetedEnemyScript = targetedEnemy.GetComponent<EnemyJoe>();
                }
            }



            //Debug.Log("wegetthere");
        }

    }

}
