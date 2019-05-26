using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyCardManager : CardManager
{

    public EnemyCardManager() : base()
    {

    }


    public void GetSelectedCard(Role self, Role target)
    {
        Card temp = Card.EmptyCard;

        List<Card> library = self.GetCardLibrary;

        int rankLeast = -10;

        bool flag = false; //判断牌内是否有倾诉
        foreach (Card cardTemp in cards)
        {
            if (cardTemp.GetName == CardName.Confess)
            {
                flag = true;
                break;
            }
        }

        foreach (Card card in cards)
        {
            CardName name = card.GetName;
            if (card.GetCost > expenseCurrent)
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
                        if (target.GetHP < 10)
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
                        if (target.GetDespondent > 80 || (target.GetDespondent > 40 && target.GetCardManager.CardsNum > 10))
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
        Debug.Log(currentCard.cardname);


    }


    public bool CheckCanPutCard()
    {
        int expenseLeast = 999;

        foreach (Card cardTemp in cards)
        {
            if (expenseLeast > cardTemp.GetCost)
            {
                expenseLeast = cardTemp.GetCost;
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

        expenseCurrent -= currentCard.GetCost;
        cards.Remove(currentCard);

        /* check buff */
        if (self.GetBuffManager.CheckBuff(BuffType.AfterPutCard))
        {
            self.GetBuffManager.BuffProcess(BuffType.AfterPutCard, self);
        }

        currentCard.TakeEffect(self, target);

        view.ShowPlayerPutCard(currentCard.GetName);
        view.ShowEnemyCards();

        currentCard = Card.EmptyCard;


    }


}
