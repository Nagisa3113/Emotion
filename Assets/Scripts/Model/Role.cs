using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Role
{

     bool isFeed;

    public bool IsFeed
    {
        get
        {
            return isFeed;
        }
        set
        {
            isFeed = value;
        }
    }
    [SerializeField]
    int despondent;//消沉层数
    public int Despondent
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
    public int HP
    {
        get
        {
            return hpCurrent;
        }
    }

    //血量上限
    [SerializeField]
    protected int hpMax;
    public int HPMax
    {
        get
        {
            return hpMax;
        }
    }

    //护甲
    protected int armor;
    public int Armor
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
    public CardManager CardManager
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
    List<Card> cardDiscard;
    public List<Card> CardDiscard
    {
        get
        {
            return cardDiscard;
        }
    }



    [SerializeField]
    protected List<Card> cardLibrary;
    public List<Card> CardLibrary
    {
        get
        {
            return cardLibrary;
        }
    }


    public virtual void InitLibrary()
    {

    }


    public int PutAllCardInLibrary(CardName cardName, Role self, Role target)
    {
        int num = 0;
        for (int i = cardLibrary.Count - 1; i >= 0; i--)
        {
            Card card = cardLibrary[i];
            if (card.Name == cardName)
            {
                card.TakeEffect(self, target);
                cardDiscard.Add(card);
                cardLibrary.Remove(card);
                num ++;
            }
        }
        return num;

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

        this.buffManager = new BuffManager(this);
        this.cardLibrary = new List<Card>();
        this.cardDiscard = new List<Card>();

    }



    public virtual void GetHeal(int heal)
    {
        int tmp = hpCurrent;
        tmp = tmp + heal > hpMax ? hpMax : tmp += heal;
        hpCurrent = tmp;
    }

    public virtual void GetArmor(int armor)
    {
       this.armor += armor;
    }


    public virtual void GetDamage(int damage)
    {

        float damageBase = 1f;
        foreach (Buff buff in buffManager.Buffs)
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
        foreach (Buff buff in buffManager.Buffs)
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



    //buff结算
    public virtual void BuffReduce()
    {
        buffManager.BuffReduceLayer();
    }

    //打出一张牌
    public virtual void PutCurrentCard(Role target)
    {

    }


    //从牌库中获得牌
    public virtual void GetCardsFromLibrary(int num)
    {


    }


}

