using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System;

[System.Serializable]
public class Player : Role
{
    [SerializeField]
    int gold;
    public int Gold
    {
        get { return gold; }
        set { gold = value; }
    }

    [SerializeField]
    List<Prop> props;
    public List<Prop> Props
    {
        get { return props; }
        set { props = value; }
    }

    public Room currentRoom = null;

    static Player player;
    public static Player Instance
    {
        get
        {
            if (player == null)
            {
                player = new Player();
            }
            return player;
        }
    }

    Player() : base(300, 10)
    {
        this.gold = 100;
        props = new List<Prop>();
        cardManager = new PlayerCardManager();
    }

    public void ResetPlayer()
    {
        player = new Player();
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
        if (num == 3)
        {
            if (GetBuffManager.IsBuff("活力"))
            {
                num++;
            }
            if (GetBuffManager.IsBuff("眩晕"))
            {
                num = 0;
            }
        }

        if (this.cardManager.CanAddCard)
        {
            for (i = 0; i < num && cardManager.Cards.Count < cardManager.numMax && cardLibrary.Count != 0; i++)
            {
                int rand = UnityEngine.Random.Range(0, cardLibrary.Count);
                Card tmp = cardLibrary[rand];

                //if (tmp.Name == CardName.Provoke)
                //if (tmp.Name == CardName.Obstruct)

                cardManager.Cards.Add(tmp);
                Debug.Log("Player获得" + tmp.cardname);
                cardLibrary.RemoveAt(rand);
            }

        }

        View.Instance.ShowPlayerCards();

        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + "获得" + i + "张牌");
    }


}

