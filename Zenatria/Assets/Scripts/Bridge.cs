using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bridge
{
    private Vector3 _begin;
    private Vector3 _end;
    private bool _broken = false;


    public Bridge(Vector3 beg, Vector3 end)
    {
        _begin = beg;
        _end = end;
        _broken = false;
    }

    public bool atBegin(Vector3 pos)
    {
        return Vector3.Distance(pos, _begin) <= 0.1f;
    }

    public bool atEnd(Vector3 pos)
    {
        return Vector3.Distance(pos, _end) <= 0.1f;
    }


    public bool Broken
    {
        get => _broken;
        set => _broken = value;
    }
}
