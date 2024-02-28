using System.Collections;
using System.Collections.Generic;
using System.Transactions;
using UnityEngine;

public class Bridge : PathSegment
{
    private bool _broken = false;



    public Bridge(Vector3 beg, Vector3 end) : base(beg, end)
    {
        _broken = false;
    }

    public override bool Passable(EnemyJoe enemy)
    {
        if (enemy == null) // change null to heavy enemy type that breaks bridges
        {
            Broken = false;
            return false;
        }
        return !_broken;
    }

    public bool Broken
    {
        get => _broken;
        set => _broken = value;
    }
}
