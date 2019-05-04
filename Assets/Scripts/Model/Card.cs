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

public enum CardName
{
    Empty,
    Anger,
    AngerFire,
    NoNameFire,
    Heal,
}



/*
   存放牌的图片，建议写一个方法去获得gameobject的名字，然后直接赋予给card。 
   由于可能要改成鼠标操作，所以也可以专门写一个牌的脚本。
   以牌为核心来执行。 --lvkiller 4/25
*/
//public GameObject[] images;

[System.Serializable]
public class Card
{ 
    CardColor color;

    GameObject image;

    [SerializeField]
    CardName name;
    public CardName GetName
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
        name = CardName.Empty;
    }


    public Card(CardName name)
    {

        this.name = name;

        switch (name)
        {

            case CardName.Empty:
                break;


            case CardName.Anger:
                this.color = CardColor.Red;
                this.cost = 0;

                break;

            case CardName.AngerFire:
                this.color = CardColor.Red;
                this.cost = 0;
                break;

            case CardName.NoNameFire:
                this.color = CardColor.Red;
                this.cost = 0;
                break;

            case CardName.Heal:
                this.color = CardColor.Green;
                this.cost = 1;
                break;



        }



        //if (name==CardName.Anger)
        //{

        //    this.effect = new DamageEffect(EffectType.Damage, 10);
        //}

        //else if (name.Equals("Heal"))
        //{

        //    this.effect = new HealEffect(EffectType.Heal, 40);
        //}







    }


}
