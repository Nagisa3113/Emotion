using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Role
{
    [SerializeField]
    int despondent;//消沉层数
    public int GetDespondent
    {
        get
        {
            return despondent;
        }
        set
        {
            despondent = value;
        }

    }


    //当前血量
    [SerializeField]
    protected int hpCurrent;

    //血量上限
    [SerializeField]
    protected int hpMax;

    public int GetHP
    {
        get
        {
            return hpCurrent;
        }
        set
        {
            int tmp = hpCurrent;
            hpCurrent = value > hpMax ? hpMax : value < 0 ? 0 : value;

            if (tmp < hpCurrent && buffManager.CheckBuff(BuffType.GetHurt))
            {
                buffManager.BuffProcess(BuffType.GetHurt, this);
            }

        }
    }

    public int GetHPMax
    {
        get
        {
            return hpMax;
        }
        set
        {
            hpMax = value;
        }
    }

    //护甲
    protected int armor;
    public int GetArmor
    {
        get
        {
            return armor;
        }
        set
        {
            armor = value;
        }
    }

    //存活
    public bool Alive
    {
        get
        {
            return hpCurrent > 0;
        }
    }


    [SerializeField]
    protected CardManager cardManager;

    public CardManager GetCardManager
    {
        get
        {
            return cardManager;
        }
        set
        {
            cardManager = value;
        }
    }

    [SerializeField]
    protected BuffManager buffManager;
    public BuffManager GetBuffManager
    {
        get
        {
            return buffManager;
        }
        
    }


    public Role(int hp, int armor)
    {
        hpCurrent = hpMax = hp;
        this.armor = armor;
        this.despondent = 0;
        //this.cardManager = new CardManager();
        this.buffManager = new BuffManager(this);
    }


    //打出一张牌
    public virtual void PutCard(Role target)
    {

    }


    //buff结算
    public virtual void BuffReduce()
    {
        buffManager.BuffReduceLayer();
    }

    //打出一张牌
    public virtual void PutCurrentCard(Role target)
    {

    }

    //获得牌
    public virtual void GetCard()
    {

    }


}

