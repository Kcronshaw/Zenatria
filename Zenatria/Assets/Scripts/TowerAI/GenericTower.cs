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
    protected Enemy _targetedEnemyScript = null;
    public Enemy targetedEnemyScript
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


    public void Attack()
    {
        targetedEnemyScript.TakeDamage(attackDamage);

    }



    public void Upgrade()
    {

        currentLevel++;

    }

}
