using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class CardManager
{

    public View view;
    //当前选中牌
    [SerializeField]
    protected Card currentCard;

    [SerializeField]
    protected List<Card> cards;

    [SerializeField]
    protected int currentCardIndex;

    //获取此时手中的所有牌用于显示
    public List<Card> GetCards
    {
        get
        {
            return cards;
        }
    }

    public Card leftCard;
    public Card rightCard;
    public int selfControl;


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



    [SerializeField]
    public int CardsNum
    {
        get
        {
            return cards.Count;
        }
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


    public int expenseCurrent;//当前费用
    public int expenseMax;//费用上限
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

        cards = new List<Card>();
        numMax = 100;
        expenseMax = 99;
        expenseCurrent = expenseMax;
        canAddCard = true;

        leftCard =new Card();
        rightCard = new Card();
        selfControl = 0 ;

        //IEnumerator<Card> iter = cards.GetEnumerator();
    }

    public virtual void ExpenseReset()
    {
        
    }

    //开始选择牌
    public void SelectCard()
    {

        currentCard = Card.EmptyCard;
        currentCardIndex = -1;
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
        if (CardsNum < numMax)
        {
            this.cards.Add(Card.NewCard(cardName));
        }
    }


    //根据牌名出牌
    public virtual void PutCard(CardName cardName, Role self, Role target)
    {

    }

    //打出手牌中所有牌
    public virtual int  PutAllCard(CardName cardName, Role self, Role target)
    {

        int num  = 0;
        for (int i = cards.Count - 1; i >= 0; i--)
        {
            Card card = cards[i];
            if (card.Name == cardName)
            {
                card.TakeEffect(self, target);
                cards.Remove(card);
                self.CardDiscard.Add(card);
                num ++;
                self.GetHeal(selfControl);
            }
        }
        return num;
    }




    //打出当前牌
    public virtual void PutCurrentCard(Role self, Role target)
    {

    }

    //打出索引位置的牌
    public virtual void PutSelectCard(Role self, Role target, int index)
    {

    }


    //弃掉某种颜色的的牌
    public void DicardCard(int num, CardColor cardColor,Role self)
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

    public int  WashBackCard(int num, CardColor cardColor,Role self)
    {
        bool flag = true;
        int count = 0; 
        while (cards.Count > 0 && num > 0 && flag)
        {
            for (int i = cards.Count - 1; i >= 0; i--)
            {
                Card card = cards[i];
                flag =false ;
                if (card.Color == cardColor)
                {
                    cards.Remove(card);
                    self.CardLibrary.Add(card);
                    flag = true;
                    count ++;
                }
            }
            num--;
        }
        return count ;
    }

    //随机获得一张牌
    public Card GetRandomCard()
    {
        if (cards.Count > 0)
        {
            int rand = Random.Range(0, cards.Count - 1);
            return cards[rand];
        }
        return null;
    }





    //获得某张牌的Bonus
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
        return num;
    }

}

