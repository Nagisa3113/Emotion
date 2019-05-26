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

        if (expenseCurrent >= currentCard.GetCost && currentCard != Card.EmptyCard)
        {
            expenseCurrent -= currentCard.GetCost;


            cards.Remove(currentCard);

            /* check buff */
            if (self.GetBuffManager.CheckBuff(BuffType.AfterPutCard))
            {
                self.GetBuffManager.BuffProcess(BuffType.AfterPutCard, self);
            }

            currentCard.TakeEffect(self, target);
            self.GetCardDiscard.Add(currentCard);

            view.ShowPlayerPutCard(currentCard.GetName);
            view.ShowPlayerCards();

            currentCard = Card.EmptyCard;
            currentCardIndex = -1;
        }
    }



    public override void PutSelectCard(Role self, Role target, int index)
    {
        currentCard = cards[index];
        if (expenseCurrent >= currentCard.GetCost)
        {


            Debug.Log("player打出一张" + currentCard.cardname);

            expenseCurrent -= currentCard.GetCost;

            cards.Remove(currentCard);


            /* check buff */
            if (self.GetBuffManager.CheckBuff(BuffType.AfterPutCard))
            {
                self.GetBuffManager.BuffProcess(BuffType.AfterPutCard, self);
            }

            currentCard.TakeEffect(self, target);
            self.GetCardDiscard.Add(currentCard);

            view.ShowPlayerPutCard(currentCard.GetName);
            view.ShowPlayerCards();

            currentCard = Card.EmptyCard;
            currentCardIndex = -1;
        }


    }
}
