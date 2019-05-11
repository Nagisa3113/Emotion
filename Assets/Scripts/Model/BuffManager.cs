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

    }


    public bool CheckBuff(BuffType buffType)
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
        for(int i = tmp - 1; i >= 0; i--)
        {
            buffs[i].Layer--;
            if (buffs[i].Active == false)
            {
                buffs.Remove(buffs[i]);
            }
        }

    }




    public void AddBuff(CardName cardName)
    {
        switch (cardName)
        {
            case CardName.Incite:
                buffs.Add(new InciteBuff());
                break;


            case CardName.Revenge:
                buffs.Add(new RevengeBuff());
                break;


            case CardName.DullAtmosphere:
                buffs.Add(new DullAtmosphereBuff());
                break;


            case CardName.Comfort:
                buffs.Add(new ComfortBuff());
                break;
        }



    }




}
