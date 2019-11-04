using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Normal,
    Boss,
}

public enum EnemyColor
{
    Red,
    Green,
    Gray,
}

[System.Serializable]
public class Enemy : Role
{
    EnemyColor enemyColor;
    public EnemyColor EnemyColor
    {
        get { return enemyColor; }
    }

    EnemyType enemyType;
    public EnemyType EnemyType
    {
        get { return enemyType; }
    }

    public bool isRound;/// 是否处于出牌阶段

    public Enemy(EnemyType enemyType, EnemyColor enemyColor) : base(600, 10)
    {
        this.enemyType = enemyType;
        this.enemyColor = enemyColor;
        cardManager = new EnemyCardManager();
    }

    public override void GetCardsFromLibrary(int num)
    {

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
        if (!this.cardManager.CanAddCard)
        {
            return;
        }

        else
        {
            for (int i = 0; i < num && cardManager.Cards.Count < cardManager.numMax; i++)
            {
                if (cardLibrary.Count <= 0)
                {
                    InitLibrary();
                }
                int rand = UnityEngine.Random.Range(0, cardLibrary.Count);
                Card tmp = cardLibrary[rand];
                cardManager.Cards.Add(tmp);
                cardLibrary.RemoveAt(rand);
            }

        }

        Debug.Log(this.GetType() + "获得" + num + "张牌");

        View.Instance.ShowEnemyCards();
    }

    public override void GetSelectedCard(Role self, Role target)
    {
        Card temp = Card.EmptyCard;


        foreach (Card card in cardManager.Cards)
        {
            CardName name = card.Name;
            if (card.Cost > cardManager.expenseCurrent)
            {
                continue;
            }
            else
            {
                temp = card;
            }
        }

        if (temp != Card.EmptyCard)
        {
            cardManager.CurrentCard = temp;
        }
        else
        {
            (cardManager as EnemyCardManager).CanPutCard = false;
        }

    }

    public override void PutCurrentCard(Role target)
    {
        cardManager.PutCurrentCard(this, target);
    }

    public static Enemy CreateEnemy(string enemyName)
    {
        Type type = Type.GetType(enemyName);
        object obj = Activator.CreateInstance(type, true);

        return (Enemy)obj;
    }

}
