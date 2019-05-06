using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardManager
{

    //当前选中牌
    [SerializeField]
    Card currentCard;

    //表示未选中卡牌
    Card emptyCard;

    [SerializeField]
    int currentCardIndex;

    [SerializeField]
    List<Card> cards;

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

    int numMax;//牌组数量上限

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
        emptyCard = new Card(CardName.Empty);

        currentCard = emptyCard;

        cards = new List<Card>();
        numMax = 15;
        expenseMax = 20;
        expenseCurrent = expenseMax;

        IEnumerator<Card> iter = cards.GetEnumerator();
    }

    public void ExpenseReset()
    {
        expenseCurrent = expenseMax;
    }

    //开始选择牌
    public void SelectCard()
    {
        if (cards[0] != null)
        {
            currentCard = cards[0];
            currentCardIndex = 0;
        }
    }

    //选择不同牌
    public void MoveSelectedCard(int dir)
    {
        if (cards.Count == 0) return;

        if (dir == 1)
        {
            currentCardIndex = currentCardIndex + 1 >= cards.Count ?
            cards.Count - 1 : currentCardIndex + 1;
        }
        else if (dir == -1)
        {
            currentCardIndex = currentCardIndex - 1 < 0 ?
                0 : currentCardIndex - 1;
        }

        currentCard = cards[currentCardIndex];

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
        AddCard(new Card(cardName));
    }


    //从牌库中获得牌
    public void GetCardsFromLibrary(int num)
    {

        for (int i = 0; i < num; i++)
        {

            if (CardLibrary.GetInstance().GetNum == 0 || CardsNum == numMax)
            {
                break;
            }
            else
            {
                cards.Add(CardLibrary.GetInstance().GetRandomCard());
            }

        }
    }



    //随机获得牌,暂用于敌人
    public void GetCardsRandom(int num)
    {

        for (int i = 0; i < num; i++)
        {

            if (CardsNum == numMax)

            {
                break;
            }

            int j = Random.Range(0, 2);
            switch (j)
            {
                case 0:
                    cards.Add(new Card(CardName.Anger));
                    break;
                case 1:
                    cards.Add(new Card(CardName.Heal));
                    break;
            }

        }
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

                    EffectProcess.TakeEffect(currentCard.GetName, self, target);

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
                EffectProcess.TakeEffect(card.GetName, self, target);
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

            EffectProcess.TakeEffect(currentCard.GetName, self, target);

            CardDiscard.GetInstance().AddCard(currentCard);


            currentCard = emptyCard;
            currentCardIndex = -1;
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

