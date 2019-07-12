using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyCardManager : CardManager
{

    public EnemyCardManager() : base()
    {

    }
    public override void  ExpenseReset()
    {
         if (view.enemy.GetBuffManager.CheckLayer("WearyBuff") > 0)
            expenseCurrent = expenseMax -1;
        else
            expenseCurrent = expenseMax;
    }

    public void GetSelectedCard(Role self, Role target)
    {
        Card temp = Card.EmptyCard;

        List<Card> library = self.CardLibrary;

        int rankLeast = -10;

        bool flag = false; //判断牌内是否有倾诉
        foreach (Card cardTemp in cards)
        {
            if (cardTemp.Name == CardName.Confess)
            {
                flag = true;
                break;
            }
        }

        foreach (Card card in cards)
        {
            CardName name = card.Name;
            if (card.Cost > expenseCurrent)
            {
                continue;
            }
            else
            {
                switch (name)
                {

                    case CardName.Anger:
                        break;

                    case CardName.AngerFire:
                        break;

                    case CardName.NoNameFire:

                        break;

                    case CardName.Vent:
                        break;

                    case CardName.Incite:
                        break;

                    case CardName.Revenge:
                        break;

                    case CardName.Complain:
                        if (target.HP < 10)
                        {
                            if (rankLeast < 10)
                            {
                                rankLeast = 10;
                                temp = card;
                            }
                        }
                        else
                        {
                            if (rankLeast < 3)
                            {
                                rankLeast = 3;
                                temp = card;
                            }
                        }
                        break;

                    case CardName.DullAtmosphere:
                        if (rankLeast < 4)
                        {
                            rankLeast = 4;
                            temp = card;
                        }
                        break;


                    case CardName.WeiYuChouMou:
                        if (flag && cards.Count < 4)
                        {
                            if (rankLeast < 6)
                            {
                                rankLeast = 6;
                                temp = card;
                            }
                        }
                        else if (flag && expenseCurrent == 1)
                        {
                            if (rankLeast < 5)
                            {
                                rankLeast = 5;
                                temp = card;
                            }
                        }
                        else
                        {
                            if (rankLeast < 1)
                            {
                                rankLeast = 1;
                                temp = card;
                            }
                        }
                        break;

                    case CardName.OuDuanSiLian:
                        if (flag && cards.Count < 10)
                        {
                            if (rankLeast < 5)
                            {
                                rankLeast = 5;
                                temp = card;
                            }
                        }
                        else if ((!flag) && cards.Count < 12)
                        {
                            if (rankLeast < 5)
                            {
                                rankLeast = 5;
                                temp = card;
                            }
                        }
                        else if (cards.Count < 5)
                        {
                            if (rankLeast < 5)
                            {
                                rankLeast = 5;
                                temp = card;
                            }
                        }
                        else
                        {
                            if (rankLeast < 2)
                            {
                                rankLeast = 2;
                                temp = card;
                            }
                        }

                        break;


                    case CardName.Confess:
                        if (target.Despondent > 80 || (target.Despondent > 40 && target.CardManager.CardsNum > 10))
                        {
                            if (rankLeast < 5)
                            {
                                rankLeast = 5;
                                temp = card;
                            }

                        }
                        else
                        {
                            if (rankLeast < 0)
                            {
                                rankLeast = 0;
                                temp = card;
                            }
                        }
                        break;

                    case CardName.Heal:

                        break;

                    case CardName.Comfort:

                        break;
                }
            }
        }

        if (temp != Card.EmptyCard)
        {
            currentCard = temp;
            temp = Card.EmptyCard;
        }

    }


    public bool CheckCanPutCard()
    {
        int expenseLeast = 999;

        foreach (Card cardTemp in cards)
        {
            if (expenseLeast > cardTemp.Cost)
            {
                expenseLeast = cardTemp.Cost;
            }
        }

        if (cards.Count <= 0 || expenseCurrent < expenseLeast)
        {
            return false;
        }
        else
        {
            return true;
        }
    }


    public override void PutCurrentCard(Role self, Role target)
    {

        GetSelectedCard(self, target);
        Debug.Log("emeny打出一张" + currentCard.cardname);

        expenseCurrent -= currentCard.Cost;
        cards.Remove(currentCard);

        if (self.GetBuffManager.CheckBuff(BuffType.AfterPutCard))
        {
            self.GetBuffManager.BuffProcess(BuffType.AfterPutCard, self);
        }
       
        currentCard.TakeEffect(self, target);
        if(currentCard != Card.EmptyCard)
        {
            view.ShowEnemyPutCard(currentCard.Name);
        }
        self.GetHeal(selfControl);

        view.ShowEnemyCards();

        currentCard = Card.EmptyCard;
    }


    public void PutCard()
    {

    }

}
