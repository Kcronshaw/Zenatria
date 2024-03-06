using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PathSegment
{
    protected Vector3 _begin;
    public Vector3 Begin
    {
        get => _begin;
    }
    protected Vector3 _end;
    public Vector3 End
    {
        get => _end;
    }
    protected PathSegment _next;


    public PathSegment(Vector3 beg, Vector3 end)
    {
        _begin = beg;
        _end = end;
    }

    public bool AtBegin(Vector3 pos)
    {
        return Vector3.Distance(pos, _begin) <= 0.1f;
    }

    public bool AtEnd(Vector3 pos)
    {
        return Vector3.Distance(pos, _end) <= 0.1f;
    }

    public Vector3 Direction()
    {
        return (_end - _begin).normalized;
    }

    public virtual bool Passable(EnemyJoe enemy)
    {
        return true;
    }
}
