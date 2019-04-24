using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class BuffManager
{
    List<Buff> buffs;

    Role role;

    public BuffManager(Role role)
    {
        this.role = role;
        buffs = new List<Buff>();
    }


    public void BuffReduceLayer()
    {
        foreach (Buff buff in buffs)
        {
            buff.Layer--;
            if (buff.Active == false)
            {
                buff.ReProcess(role);
                buffs.Remove(buff);
            }
        }
    }

    public void BuffProcess()
    {
        foreach (Buff buff in buffs)
        {
            buff.Process(role);
        }
    }


}
