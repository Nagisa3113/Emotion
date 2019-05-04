using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class CardLibrary
{
    [SerializeField]
    List<Card> cards;

    //单例模式
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
        for (int i = 0; i < 20; i++)
        {
            cards.Add(new Card(CardName.Anger));
            cards.Add(new Card(CardName.Heal));
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


    public Card PutAllCard(CardName cardName,Role self,Role target)
    {

        foreach (Card card in cards)
        {
            if (card.GetName == cardName)
            {
                EffectProcess.TakeEffect(card.GetName, self, target);
                CardDiscard.GetInstance().AddCard(card);
                cards.Remove(card);
            }
        }
        return null;
    }






    public Card GetRandomCard()
    {
        int rand = Random.Range(0, cards.Count);
        Card tmp = cards[rand];
        cards.RemoveAt(rand);

        return tmp;
    }

}

