using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyCardManager : CardManager
{
    bool canPutCard;
    public bool CanPutCard
    {
        get { return canPutCard; }
        set { canPutCard = value; }
    }

    public EnemyCardManager() : base()
    {
        canPutCard = true;
    }

    public override void ExpenseReset()
    {
        if (View.Instance.enemy.GetBuffManager.CheckLayer("WearyBuff") > 0)
            expenseCurrent = expenseMax - 1;
        else
            expenseCurrent = expenseMax;

        canPutCard = true;
    }

    public override void PutCurrentCard(Role self, Role target)
    {

        self.GetSelectedCard(self, target);
        Debug.Log(self.GetType() + "打出一张" + currentCard.cardname);

        if (currentCard != Card.EmptyCard)
        {
            View.Instance.ShowEnemyPutCard(currentCard.Name);
        }
        expenseCurrent -= currentCard.Cost;


        if (self.GetBuffManager.CheckBuff(BuffType.AfterPutCard))
        {
            self.GetBuffManager.BuffProcess(BuffType.AfterPutCard, self);
        }

        currentCard.TakeEffect(self, target);
        cards.Remove(currentCard);

        View.Instance.ShowEnemyCards();

        currentCard = Card.EmptyCard;
    }
}
