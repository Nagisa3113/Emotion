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
        //for (int i = 0; i < 20; i++)
        {
            //int ran = UnityEngine.Random.Range(1, 7);
            //Array array = Enum.GetValues(typeof(CardName));
            //Card card = new Card((CardName)array.GetValue(ran));

            cards.Add(new Card(CardName.NoNameFire));
            cards.Add(new Card(CardName.NoNameFire));
            cards.Add(new Card(CardName.NoNameFire));
            cards.Add(new Card(CardName.NoNameFire));
            cards.Add(new Card(CardName.NoNameFire));
            cards.Add(new Card(CardName.NoNameFire));


            cards.Add(new Card(CardName.Vent));
            cards.Add(new Card(CardName.Vent));
            cards.Add(new Card(CardName.AngerFire));
            cards.Add(new Card(CardName.AngerFire));
            cards.Add(new Card(CardName.AngerFire));
            cards.Add(new Card(CardName.AngerFire));
            cards.Add(new Card(CardName.AngerFire));
            //cards.Add(new Card(CardName.AngerFire));
            //cards.Add(new Card(CardName.AngerFire));



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

        for(int i = cards.Count - 1; i >= 0; i--)
        {
            Card card = cards[i];
            if (card.GetName == cardName)
            {
                EffectProcess.TakeEffect(card.GetName, self, target);
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

