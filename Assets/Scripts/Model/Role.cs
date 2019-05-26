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
    }

    public int GetHPMax
    {
        get
        {
            return hpMax;
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



    public virtual void GetHeal(int heal)
    {
        int tmp = hpCurrent;
        tmp = tmp > hpMax ? hpMax : tmp += heal;
        hpCurrent = tmp;
    }


    public virtual void GetDamage(int damage)
    {

        float damageBase = 1f;
        foreach (Buff buff in buffManager.buffs)
        {
            if (buff.name.Equals("脆弱"))
            {
                damageBase += 0.2f;
            }
        }

        int tmp = hpCurrent;
        tmp -= (int)(damage * damageBase);

        hpCurrent = tmp < 0 ? 0 : tmp;

        //受到伤害时触发buff
        buffManager.BuffProcess(BuffType.GetHurt, this);

    }



    public virtual void TakeDamage(Role target, int damage)
    {
        float damageBase = 1f;
        foreach (Buff buff in buffManager.buffs)
        {
            if (buff.name.Equals("腐蚀"))
            {
                damageBase -= 0.2f;
            }
            if (buff.name.Equals("强力"))
            {
                damageBase += 0.2f;
            }
            if (buff.name.Equals("挑衅"))
            {
                damageBase *= 2f;
            }
        }


        target.GetDamage((int)(damage * damageBase));
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
    public virtual void PutSelectCard(Role target, int index)
    {

    }

    //获得牌
    public virtual void GetCard()
    {

    }


}

