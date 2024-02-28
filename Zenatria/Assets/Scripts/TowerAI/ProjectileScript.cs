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

    // Update is called once per frame
    void Update()
    {
        
    }
}
