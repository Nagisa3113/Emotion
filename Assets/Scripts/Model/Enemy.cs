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

    /// <summary>
    /// 是否处于出牌阶段
    /// </summary>
    public bool isRound;

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
        Card.AddCard(cardLibrary, CardName.Complain, 16);
        Card.AddCard(cardLibrary, CardName.DullAtmosphere, 6);
        Card.AddCard(cardLibrary, CardName.WeiYuChouMou, 3);
        Card.AddCard(cardLibrary, CardName.OuDuanSiLian, 5);
        Card.AddCard(cardLibrary, CardName.Confess, 1);
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

        Debug.Log(System.Reflection.MethodBase.GetCurrentMethod().DeclaringType.FullName + "获得" + num + "张牌");
        
        cardManager.view.ShowEnemyCards();
    }

}
