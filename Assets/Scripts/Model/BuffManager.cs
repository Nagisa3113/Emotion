﻿using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class BuffManager
{
    [SerializeField]
    List<Buff> buffs;
    public List<Buff> Buffs
    {
        get
        {
            return buffs;
        }
        set
        {
            buffs = value;
        }
    }

    Role self;

    public View view;

    public BuffManager(Role role)
    {
        this.self = role;
        buffs = new List<Buff>();
        view = View.GetInstance();

    }


    public bool CheckBuff(BuffType buffType)    //严
    {
        foreach (Buff buff in buffs)
        {
            if (buff.buffType == buffType)
            {
                return true;
            }
        }

        return false;
    }

    public int CheckLayer (string name)
    {
        foreach (Buff buff in buffs)
        {
            if (buff.ToString() == name)
            {
                return  buff.Layer;
            }
        }
        return 0;
    }

    public string CheckName (string name)
    {
        foreach (Buff buff in buffs)
        {
            if (buff.ToString() == name)
            {
                return  buff.Name;
            }
        }
        return "";
    }



    public void BuffProcess(BuffType buffType, Role self)
    {
        foreach (Buff buff in buffs)
        {
            if (buff.buffType == buffType)
            {
                buff.Process(self);
            }
        }
    }


    public void BuffReduceLayer()
    {
        int tmp = buffs.Count;
        for (int i = tmp - 1; i >= 0; i--)
        {
            buffs[i].Layer--;
            if (buffs[i].Active == false)
            {
                buffs.Remove(buffs[i]);
            }
        }

    }


    //判断是否已经有重复Buff
    void AddBuff(Buff buff)
    {
        foreach (Buff b in buffs)
        {
            if (b.name.Equals(buff.name))
            {
                b.Layer += buff.Layer;
                return;
            }
        }
        buffs.Add(buff);
    }


    public void AddBuff(CardName cardName)
    {
        switch (cardName)
        {
            case CardName.Incite:
                AddBuff(new InciteBuff());
                break;

            case CardName.Revenge:
                AddBuff(new RevengeBuff());
                break;

            case CardName.DullAtmosphere:
                AddBuff(new DullAtmosphereBuff());
                break;

            case CardName.Comfort:
                AddBuff(new ComfortBuff());
                break;
        }
        view.ShowBuff(self);

    }


}
