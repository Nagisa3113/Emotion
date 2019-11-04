using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardManager
{
    public CardManager()
    {
        cards = new List<Card>();
        numMax = 100;
        expenseMax = 99;
        expenseCurrent = expenseMax;
        canAddCard = true;

        leftCard = new Card();
        rightCard = new Card();
    }

    public int numMax;//牌组数量上限
    public int expenseCurrent;//当前费用
    public int expenseMax;//费用上限
    public int ExpenseCurrent
    {
        get { return expenseCurrent; }
        set
        {
            expenseCurrent = value > expenseMax ?
                expenseMax : value < 0 ? 0 : value;
        }
    }

    [SerializeField]
    protected Card currentCard;
    [SerializeField]
    protected int currentCardIndex;

    public Card CurrentCard
    {
        get { return currentCard; }
        set { currentCard = value; }
    }

    public int CardIndex
    {
        get { return currentCardIndex; }
    }

    [SerializeField]
    protected List<Card> cards;
    public List<Card> Cards
    {
        get { return cards; }
    }

    bool canAddCard;
    public bool CanAddCard
    {
        get { return canAddCard; }
        set { canAddCard = value; }
    }


    // leftCard是为了黄色卡牌设计
    public Card leftCard;
    public Card rightCard;

    int bonus;

    public int GetBonus(CardColor cardColor)
    {
        int num = 0;
        foreach (Card c in cards)
        {
            if (c.Color == cardColor)
            {
                num++;
            }
        }
        return num - 1;
    }

    public void DicardCard(int num, CardColor cardColor, Role self)
    {
        bool flag = true;
        while (cards.Count > 0 && num > 0 && flag)
        {
            flag = false;
            for (int i = cards.Count - 1; i >= 0; i--)
            {
                Card card = cards[i];
                if (card.Color == cardColor)
                {
                    flag = true;
                    cards.Remove(card);
                    self.CardDiscard.Add(card);
                }
            }
            num--;

        }
    }

    public int WashBackCard(int num, CardColor cardColor, Role self)
    {
        bool flag = true;
        int count = 0;
        while (cards.Count > 0 && num > 0 && flag)
        {
            for (int i = cards.Count - 1; i >= 0; i--)
            {
                Card card = cards[i];
                flag = false;
                if (card.Color == cardColor)
                {
                    cards.Remove(card);
                    self.CardLibrary.Add(card);
                    flag = true;
                    count++;
                }
            }
            num--;
        }
        return count;
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

    public void SelectCard()
    {

        currentCard = Card.EmptyCard;
        currentCardIndex = -1;
    }

    #region AddCard
    public void AddCard(Card card)
    {
        if (Cards.Count < numMax)
        {
            this.cards.Add(card);
            Debug.Log("玩家新加入卡牌：" + card.ToString());
        }
        else
        {
            Debug.Log("couldn't add more card!");
        }
    }
    public void AddCard(CardName cardName)
    {
        AddCard(Card.NewCard(cardName));
        View.Instance.ShowPlayerCards();
    }
    public void AddCard(Card[] cards)
    {
        for (int i = 0; i < cards.Length; i++)
        {
            AddCard(cards[i]);
        }
    }
    public void AddCard(List<Card> cards)
    {
        for (int i = 0; i < cards.Count; i++)
        {
            AddCard(cards[i]);
        }
    }
    public void AddCard(CardSet card)
    {
        AddCard(card.Cards);
    }
    #endregion


    //打出手牌中所有牌
    public virtual int PutAllCard(CardName cardName, Role self, Role target)
    {

        int num = 0;
        for (int i = cards.Count - 1; i >= 0; i--)
        {
            Card card = cards[i];
            if (card.Name == cardName)
            {
                card.TakeEffect(self, target);
                cards.Remove(card);
                self.CardDiscard.Add(card);
                num++;
            }
        }
        return num;
    }

    public int GetAngerFireNum(Role self)
    {
        int num = 0;
        foreach (Card c in cards)
        {
            if (c.Name == CardName.AngerFire)
            {
                num++;
            }
        }
        foreach (Card c in self.CardLibrary)
        {
            if (c.Name == CardName.AngerFire)
            {
                num++;
            }
        }
        return num;
    }

    public virtual void ExpenseReset()
    {

    }

    public virtual void PutCurrentCard(Role self, Role target)
    {

    }

    //打出索引位置的牌
    public virtual void PutSelectCard(Role self, Role target, int index)
    {

    }

    //根据牌名出牌
    public virtual void PutCard(CardName cardName, Role self, Role target)
    {

    }

}

