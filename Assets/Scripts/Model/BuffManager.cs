using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;


public class BuffManager
{
    List<Buff> buffs;

    Role self;

    public BuffManager(Role role)
    {
        this.self = role;
        buffs = new List<Buff>();



        putCardBuffed += new Buff1().Process;
        putCardBuffed += new Buff2().Process;

    }


    public delegate void BuffEventHandle(System.Object sender, BuffEventArgs e);
    public event BuffEventHandle putCardBuffed;

    public class BuffEventArgs : EventArgs
    {
        public int layer;
        public BuffEventArgs(int layer)
        {
            this.layer = layer;
        }

    }

    public virtual void OnCheckPutCard()
    {
        if (putCardBuffed != null)
        {
            putCardBuffed(self, new BuffEventArgs(0));
        }
        
    }


    public void BuffReduceLayer()
    {
        foreach (Buff buff in buffs)
        {
            buff.Layer--;
            if (buff.Active == false)
            {
                putCardBuffed -= buff.ReProcess;
                buffs.Remove(buff);
            }
        }
    }

    public void AddBuff(BuffType buffType, Buff buff)
    {
        buffs.Add(buff);

        switch (buffType)
        {
            case BuffType.PutCard:
                putCardBuffed += buff.Process;
                break;



        }


        
        
    }




}
