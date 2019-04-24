using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CardColor
{
    Red,
    Green,
    Gray,
    Purple,
    Yellow,
}

[System.Serializable]
public class Card
{ 
    CardColor color;

    GameObject image;

    [SerializeField]
    string name;
    public string GetName
    {
        get
        {
            return name;
        }
    }

    int cost;
    public int GetCost
    {
        get
        {
            return cost;
        }
    }

    public CardColor GetColor
    {
        get
        {
            return color;
        }
    }

    Effect effect;
    public Effect GetEffect
    {
        get
        {
            return effect;
        }

        set
        {
            effect = value;
        }
    }


    public Card()
    {
        name = "Empty";
    }


    public Card(string name)
    {

        if(name.Equals("Anger"))
        {
            this.name = "Anger";
            this.color = CardColor.Red;
            this.cost = 0;
            this.effect = new DamageEffect(EffectType.Damage, 10);
        }

        else if(name.Equals("Heal"))
        {
            this.name = "Heal";
            this.color = CardColor.Green;
            this.cost = 1;
            this.effect = new HealEffect(EffectType.Heal, 40);
        }
    }


}
