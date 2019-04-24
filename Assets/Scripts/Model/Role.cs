using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Role
{
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
            hpCurrent = value > hpMax ? hpMax : value < 0 ? 0 : value;
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
    }


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
        this.cardManager = new CardManager();
        this.buffManager = new BuffManager(this);
    }


    //打出一张牌
    public virtual void PutCard(Role target)
    {

    }


    //获得牌
    public virtual void GetCard()
    {

    }


}

