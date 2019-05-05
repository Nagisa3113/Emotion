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

    public void BuffProcessAfterPutCard(Card card)
    {
        foreach (Buff buff in buffs)
        {
            if (buff.buffType == BuffType.AfterPutCard)
            {
                buff.Process(card, self);
            }
        }
    }

    public void BuffProcessGetHurt(Role self)
    {
        foreach (Buff buff in buffs)
        {
            if (buff.buffType == BuffType.GetHurt)
            {
                buff.Process(self);
            }
        }
    }


    public void BuffReduceLayer()
    {
        foreach (Buff buff in buffs)
        {
            buff.Layer--;
            if (buff.Active == false)
            {
                buffs.Remove(buff);
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



        }



    }




}
