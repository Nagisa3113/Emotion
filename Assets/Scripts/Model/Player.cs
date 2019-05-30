using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Player : Role
{
    //金钱
    //int gold;

    //道具
    List<Prop> propLibrary;

    List<Prop> props;

    //单例模式
    private static Player player;

    private Player() : base(300, 10)
    {
        propLibrary = new List<Prop>();
        props = new List<Prop>();
        cardManager = new PlayerCardManager();
    }

    public static Player GetInstance()
    {
        if (player == null)
        {
            player = new Player();
        }

        return player;
    }

    public void PropReduceCD()
    {
        foreach (Prop prop in props)
        {
            prop.CD--;
        }
    }

    public void PropEffect()
    {
        foreach (Prop prop in props)
        {

        }
    }

    public override void PutCurrentCard(Role target)
    {
        cardManager.PutCurrentCard(this, target);
    }


    public void PutSelectCard(Role target, int index)
    {
        cardManager.PutSelectCard(this, target, index);
    }



    public override void InitLibrary()
    {

        for (int i = 0; i < 10; i++)
        {
            cardLibrary.Add(Card.NewCard(CardName.Heal));
        }
        for (int i = 0; i < 5; i++)
        {
            cardLibrary.Add(Card.NewCard(CardName.Comfort));
        }
        for (int i = 0; i < 5; i++)
        {
            cardLibrary.Add(Card.NewCard(CardName.Incite));
        }
        for (int i = 0; i < 5; i++)
        {
            cardLibrary.Add(Card.NewCard(CardName.Vent));
        }
        for (int i = 0; i < 5; i++)
        {
            cardLibrary.Add(Card.NewCard(CardName.Revenge));
        }
        for (int i = 0; i < 5; i++)
        {
            cardLibrary.Add(Card.NewCard(CardName.NoNameFire));
        }
    }



    public override void GetCardsFromLibrary(int num)
    {
        int i = 0;
        if (this.cardManager.CanAddCard)
        {

            for (i = 0; i < num && cardManager.CardsNum < cardManager.numMax && cardLibrary.Count != 0; i++)
            {
                int rand = UnityEngine.Random.Range(0, cardLibrary.Count);
                Card tmp = cardLibrary[rand];
                cardManager.GetCards.Add(tmp);
                cardLibrary.RemoveAt(rand);
            }
        }

        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + "获得" + i + "张牌");
        cardManager.view.ShowPlayerCards();

    }


}

