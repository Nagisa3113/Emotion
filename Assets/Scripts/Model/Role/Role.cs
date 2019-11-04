using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Role
{
    #region property
    [SerializeField]
    protected int hpCurrent;
    public int HP
    {
        get { return hpCurrent; }
    }

    [SerializeField]
    protected int hpMax;
    public int HPMax
    {
        get { return hpMax; }
        set { hpMax = value; }
    }

    [SerializeField]
    protected int armor;
    public int Armor
    {
        get { return armor; }
        set { armor = value; }
    }

    [SerializeField]
    int despondent;//消沉层数
    public int Despondent
    {
        get { return despondent; }
        set { despondent = value; }
    }

    public bool Alive
    {
        get { return hpCurrent > 0; }
    }

    [SerializeField]
    protected CardManager cardManager;
    public CardManager CardManager
    {
        get { return cardManager; }
        set { cardManager = value; }
    }

    [SerializeField]
    protected BuffManager buffManager;
    public BuffManager GetBuffManager
    {
        get { return buffManager; }
    }


    [SerializeField]
    List<Card> cardDiscard;
    public List<Card> CardDiscard
    {
        get { return cardDiscard; }
    }


    [SerializeField]
    protected List<Card> cardLibrary;
    public List<Card> CardLibrary
    {
        get { return cardLibrary; }
    }

    #endregion

    public Role(int hp, int armor)
    {
        hpCurrent = hpMax = hp;
        this.armor = armor;
        this.despondent = 0;

        this.buffManager = new BuffManager(this);
        this.cardLibrary = new List<Card>();
        this.cardDiscard = new List<Card>();
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
                num++;
            }
        }
        return num;
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

    public virtual float DamageBase(Role self, Role target)
    {
        float damageBase = 1f;
        foreach (Buff buff in self.GetBuffManager.Buffs)
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
        foreach (Buff buff in target.GetBuffManager.Buffs)
        {
            if (buff.name.Equals("脆弱"))
            {
                damageBase += 0.2f;
            }
        }
        return damageBase;

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

        buffManager.BuffProcess(BuffType.GetHurt, this);
    }

    public virtual void BuffReduce()
    {
        buffManager.BuffReduceLayer();
    }

    public virtual void InitLibrary()
    {

    }

    public virtual void GetSelectedCard(Role self, Role target)
    {

    }

    public virtual void PutCurrentCard(Role target)
    {

    }

    public virtual void GetCardsFromLibrary(int num)
    {


    }

}

