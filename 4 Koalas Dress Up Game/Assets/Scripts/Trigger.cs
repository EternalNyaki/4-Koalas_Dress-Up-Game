using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//Trigger class
//Represents a boolean trigger that automatically resets itself whenever the value is checked
//You can pretty much just treat this as a boolean in most circumstances, as it can be implicitly converted
//both to and from a boolean value
public class Trigger
{
    //The boolean value of the trigger
    private bool _value;

    public static implicit operator bool(Trigger t)
    {
        //Reset the trigger whenever the value is checked
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