using UnityEngine;
using System.Collections;
using System.Collections.Generic;


[System.Serializable]
public class CardDiscard
{
    [SerializeField]
    List<Card> cards;

    //单例模式
    private static CardDiscard cardDiscard;

    private CardDiscard()
    {
        cards = new List<Card>();
    }

    public static CardDiscard GetInstance()
    {
        if (cardDiscard == null)
        {
            cardDiscard = new CardDiscard();
        }
        return cardDiscard;
    }

    public void AddCard(Card card)
    {
        cards.Add(card);
    }

    public void GetHalfCards()
    {

    }

}