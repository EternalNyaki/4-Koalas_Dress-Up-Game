using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Trigger
{
    private bool _value;

    public static implicit operator bool(Trigger t)
    {
        bool output = t._value;
        t._value = false;
        return output;
    }
    public static implicit operator Trigger(bool b)
    {
        Trigger t = new Trigger();
        t._value = b;
        return t;
    }
}