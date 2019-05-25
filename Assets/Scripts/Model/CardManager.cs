﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardManager
{


    public View view;
    //当前选中牌
    [SerializeField]
    Card currentCard;

    //表示未选中卡牌
    Card emptyCard;

    [SerializeField]
    int currentCardIndex;

    [SerializeField]
    public List<Card> cards;

    [SerializeField]
    public int CardsNum
    {
        get
        {
            return cards.Count;
        }
    }

    public Card GetRandomCard()
    {
        if (cards.Count > 0)
        {
            int rand = Random.Range(0, cards.Count - 1);
            return cards[rand];
        }
        return null;

    }


    //是否能抽牌
    bool canAddCard;
    public bool CanAddCard
    {
        get
        {
            return canAddCard;
        }

        set
        {
            canAddCard = value;
        }
    }

    int bonus;

    public int numMax;//牌组数量上限

    [SerializeField]
    int expenseCurrent;//当前费用

    int expenseMax;//费用上限

    public int ExpenseCurrent
    {
        get
        {
            return expenseCurrent;
        }
        set
        {
            expenseCurrent = value > expenseMax ?
                expenseMax : value < 0 ? 0 : value;
        }
    }



    public CardManager()
    {
        view = View.GetInstance();
        emptyCard = new Empty();
        cards = new List<Card>();
        numMax = 10;
        expenseMax = 3;
        expenseCurrent = expenseMax;

        //IEnumerator<Card> iter = cards.GetEnumerator();

    }

    public void ExpenseReset()
    {
        expenseCurrent = expenseMax;
    }

    //开始选择牌
    public void SelectCard()
    {

        currentCard = cards[0];
        currentCardIndex = 0;
    }

    //选择不同牌
    public void MoveSelectedCard(int dir)
    {
        if (cards.Count == 0) return;

        if (dir == 1)                           //为实现卷轴效果改变手牌的顺序
        {
            if (currentCardIndex + 1 >= cards.Count)
            {
                currentCardIndex = cards.Count - 1;
                Card temp = cards[0];
                for (int i = 1; i < cards.Count; i++)
                {
                    cards[i - 1] = cards[i];
                }
                cards[cards.Count - 1] = temp;
                view.ShowPlayerCards();
            }
            else
            {
                currentCardIndex = currentCardIndex + 1;
            }
        }
        else if (dir == -1)
        {
            currentCardIndex = currentCardIndex - 1 < 0 ?
                0 : currentCardIndex - 1;
        }

        currentCard = cards[currentCardIndex];
        view.SelectedPlayerCard(currentCardIndex, currentCard);


    }


    public void AddCard(Card card)
    {
        if (CardsNum < numMax)
        {
            this.cards.Add(card);
        }
    }

    public void AddCard(CardName cardName)
    {
        AddCard(Card.NewCard(cardName));
    }


    //从牌库中获得牌
    public virtual void GetCardsFromLibrary(int num)
    {

    }

    public virtual void AI(Role self, Role target)
    {

    }
    public void PutCard(CardName cardName, Role self, Role target)
    {
        if (expenseCurrent >= currentCard.GetCost)
        {
            expenseCurrent -= currentCard.GetCost;

            foreach (Card card in cards)
            {
                if (card.GetName == cardName)
                {
                    cards.Remove(card);

                    /* check buff */
                    if (self.GetBuffManager.CheckBuff(BuffType.AfterPutCard))
                    {
                        self.GetBuffManager.BuffProcess(BuffType.AfterPutCard, self);
                    }

                    currentCard.TakeEffect(self, target);


                    CardDiscard.GetInstance().AddCard(card);
                    break;
                }
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
                cards.Remove(card);
                currentCard.TakeEffect(self, target);
                CardDiscard.GetInstance().AddCard(card);

            }
        }
    }




    //打出当前牌
    public void PutCurrentCard(Role self, Role target)
    {

        if (expenseCurrent >= currentCard.GetCost
            && !currentCard.Equals(emptyCard))
        {
            expenseCurrent -= currentCard.GetCost;


            cards.Remove(currentCard);

            /* check buff */
            if (self.GetBuffManager.CheckBuff(BuffType.AfterPutCard))
            {
                self.GetBuffManager.BuffProcess(BuffType.AfterPutCard, self);
            }

            currentCard.TakeEffect(self, target);
            CardDiscard.GetInstance().AddCard(currentCard);
            view.ShowPlayerPutCard(currentCard.GetName);
            view.ShowPlayerCards();
            currentCard = emptyCard;
            currentCardIndex = -1;
        }  
    }

        //打出当前牌
    public void PutSelectCard(Role self, Role target,int index)
    {
        currentCard = cards[index];
        if (expenseCurrent >= currentCard.GetCost)
        {
            expenseCurrent -= currentCard.GetCost;

            cards.Remove(currentCard);


            /* check buff */
            if (self.GetBuffManager.CheckBuff(BuffType.AfterPutCard))
            {
                self.GetBuffManager.BuffProcess(BuffType.AfterPutCard, self);
            }

            currentCard.TakeEffect(self, target);
            CardDiscard.GetInstance().AddCard(currentCard);
            view.ShowPlayerPutCard(currentCard.GetName);
            view.ShowPlayerCards();
            currentCard = emptyCard;
            currentCardIndex = -1;
        }  

    }




    //弃牌
    public void DicardCard(int num,CardColor cardColor)
    {
        while (cards.Count > 0 && num > 0)
        {

            foreach(Card card in cards)
            {
                if (card.GetColor == cardColor)
                {
                    cards.Remove(card);

                }
            }
            num--;

        }
    }





    public int GetBonus(CardName cardName)
    {
        int num = 0;
        foreach (Card c in cards)
        {
            if (c.GetName == cardName)
            {
                num++;
            }
        }
        return num;
    }



    //获取此时手中的所有牌用于显示
    public List<Card> GetCards()
    {
        return cards;
    }

    public int CardIndex
    {
        get
        {
            return currentCardIndex;
        }
    }

    public Card CurrentCard
    {
        get
        {
            return currentCard;
        }
    }

}

