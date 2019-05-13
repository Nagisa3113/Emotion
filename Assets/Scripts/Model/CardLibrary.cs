using UnityEngine;
using System;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CardLibrary
{
    [SerializeField]
    List<Card> cards;

    /* 单例模式 */
    private static CardLibrary cardLibrary;

    private CardLibrary()
    {
        cards = new List<Card>();
    }


    public static CardLibrary GetInstance()
    {
        if (cardLibrary == null)
        {
            cardLibrary = new CardLibrary();
        }
        return cardLibrary;
    }


    public int GetNum
    {
        get
        {
            return cards.Count;
        }
    }


    public void InitLibrary()
    {

        foreach (CardName cardName in Enum.GetValues(typeof(CardName)))
        {

            if ( cardName != CardName.Empty)
            
            cards.Add(CardManager.GetNewCard(cardName));


        }


    }


    public void RemoveFromLibrary(Card card)
    {
        foreach (Card i in cards)
        {
            if (i.Equals(card))
            {
                cards.Remove(i);
                break;
            }
        }
    }


    public void PutAllCard(CardName cardName, Role self, Role target)
    {

        for (int i = cards.Count - 1; i >= 0; i--)
        {
            Card card = cards[i];
            if (card.GetName == cardName)
            {
                card.TakeEffect(self, target);
                CardDiscard.GetInstance().AddCard(card);
                cards.Remove(card);
            }
        }

    }


    public Card GetRandomCard()
    {
        int rand = UnityEngine.Random.Range(0, cards.Count);
        Card tmp = cards[rand];
        cards.RemoveAt(rand);

        return tmp;
    }

}

