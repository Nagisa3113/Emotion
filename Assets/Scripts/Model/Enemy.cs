using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Normal,
    Boss,
}


[System.Serializable]
public class Enemy : Role
{
    EnemyType enemyType;


    public EnemyType GetEnemyType
    {
        get
        {
            return enemyType;
        }
    }

    public Enemy(EnemyType enemyType) : base(600, 10)
    {
        this.enemyType = enemyType;
        cardManager = new EnemyCardManager();
    }

    public override void PutCurrentCard(Role target)
    {
        cardManager.PutCurrentCard(this, target);
    }

    public override void InitLibrary()
    {
        int i = 0;
        for (i = 0; i < 16; i++)
        {
            cardLibrary.Add(Card.NewCard(CardName.Complain));
        }
        for (i = 0; i < 6; i++)
        {
            cardLibrary.Add(Card.NewCard(CardName.DullAtmosphere));
        }
        for (i = 0; i < 3; i++)
        {
            cardLibrary.Add(Card.NewCard(CardName.WeiYuChouMou));
        }
        for (i = 0; i < 5; i++)
        {
            cardLibrary.Add(Card.NewCard(CardName.OuDuanSiLian));
        }
        for (i = 0; i < 1; i++)
        {
            cardLibrary.Add(Card.NewCard(CardName.Confess));
        }
    }



    public override void GetCardsFromLibrary(int num)
    {
        if (!this.cardManager.CanAddCard)
        {
            return;

        }
        else
        {
            for (int i = 0; i < num && cardManager.CardsNum < cardManager.numMax; i++)
            {

                if (cardLibrary.Count <= 0)
                {
                    InitLibrary();
                }

                int rand = UnityEngine.Random.Range(0, cardLibrary.Count);
                Card tmp = cardLibrary[rand];
                cardManager.GetCards.Add(tmp);
                cardLibrary.RemoveAt(rand);

            }
        }

        cardManager.view.ShowEnemyCards();
    }

}
