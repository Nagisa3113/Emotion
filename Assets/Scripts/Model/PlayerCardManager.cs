using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCardManager : CardManager
{

    public PlayerCardManager() : base()
    {

    }

    public override void PutCurrentCard(Role self, Role target)
    {

        if (expenseCurrent >= currentCard.Cost && currentCard != Card.EmptyCard)
        {
            expenseCurrent -= currentCard.Cost;

            cards.Remove(currentCard);

            if (self.GetBuffManager.CheckBuff(BuffType.AfterPutCard))
            {
                self.GetBuffManager.BuffProcess(BuffType.AfterPutCard, self);
            }

            currentCard.TakeEffect(self, target);
            self.CardDiscard.Add(currentCard);

            view.ShowPlayerPutCard(currentCard.Name, currentCardIndex);
            view.ShowPlayerCards();

            currentCard = Card.EmptyCard;
            currentCardIndex = -1;
            self.GetHeal(selfControl);
        }
    }

    public override void  ExpenseReset()
    {
        if ( Player.GetInstance().GetBuffManager.CheckLayer("WearyBuff") > 0)
            expenseCurrent = expenseMax -1;
        else
            expenseCurrent = expenseMax;
      
    }


    public override void PutSelectCard(Role self, Role target, int index)
    {
        currentCard = cards[index];
        if (expenseCurrent >= currentCard.Cost)
        {
            Debug.Log("player打出一张" + currentCard.cardname);
            if(index == 0 && cards.Count == 1)
            {
                leftCard = Card.EmptyCard;
                rightCard = Card.EmptyCard;
            }
            else if(index == 0)
            {
                leftCard = Card.EmptyCard;
                rightCard = cards[index+1];
            }
            else if(index == (cards.Count -1))
            {
                leftCard =cards[index -1];
                rightCard = Card.EmptyCard;
    
            }
            else
            {
                leftCard =cards[index -1];
                rightCard = cards [index+1];
            }

            expenseCurrent -= currentCard.Cost;

            cards.Remove(currentCard);

            if (self.GetBuffManager.CheckBuff(BuffType.AfterPutCard))
            {
                self.GetBuffManager.BuffProcess(BuffType.AfterPutCard, self);
            }
            currentCard.TakeEffect(self, target);
            self.CardDiscard.Add(currentCard);

            view.ShowPlayerPutCard(currentCard.Name,index);
            view.ShowPlayerCards();
            
            currentCard = Card.EmptyCard;
            currentCardIndex = -1;
        }


    }
}
