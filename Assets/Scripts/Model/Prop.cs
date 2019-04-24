using UnityEngine;
using System.Collections;

public enum PropType
{
    Active,
    Passive,
}


public enum PropColor
{
    Red,
    Green,
    Gray,
    Purpures,
    Yellow,
}

public class Prop
{
    PropType propType;
    PropColor propColor;

    int cd;
    public int CD
    {
        get
        {
            return cd;
        }
        set
        {
            cd = value - 1 > 0 ? value - 1 : 0;
        }
    }
    public bool Active
    {
        get
        {
            return cd > 0;
        }
    }


    public void Process(Role role)
    {
        //do something
    }
}
