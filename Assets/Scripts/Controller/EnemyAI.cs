using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    Player player;
    Enemy enemy;
    List<Card> cards;
    List<Card> handCards;
    int ExpenseLeast;

    void Start()
    {
        enemy = GameObject.Find("Battle").GetComponent<BattleSystem>().GetEnemy();
        player = Player.GetInstance();
        cards = new List<Card>();
        handCards = new List<Card>();

    }

    void InitLibrary()
    {
        int i = 0;
        for (i = 0; i < 16; i++)
        {
            cards.Add(CardManager.GetNewCard(CardName.Complain));
        }
        for (i = 0; i < 6; i++)
        {
            cards.Add(CardManager.GetNewCard(CardName.DullAtmosphere));
        }
        for (i = 0; i < 3; i++)
        {
            cards.Add(CardManager.GetNewCard(CardName.WeiYuChouMou));
        }
        for (i = 0; i < 5; i++)
        {
            cards.Add(CardManager.GetNewCard(CardName.OuDuanSiLian));
        }
        for (i = 0; i < 1; i++)
        {
            cards.Add(CardManager.GetNewCard(CardName.Confess));
        }
    }


    public void AI(Role self, Role target)
    {
        if (handCards.Count > 0)
        {
            ExpenseLeast = handCards[0].GetCost;
        }

        while (handCards.Count > 0 && enemy.GetCardManager.ExpenseCurrent > ExpenseLeast)
        {

            int rankLeast = -10;
            Card temp = new Card();
            bool flag = false; //判断牌内是否有倾诉
            foreach (Card cardTemp in handCards)
            {
                if (cardTemp.GetName == CardName.Confess)
                {
                    flag = true;
                    break;
                }
            }
            foreach (Card card in handCards)
            {
                CardName name = card.GetName;
                switch (name)
                {
                    case CardName.Empty:
                        break;

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
                        if (player.GetHP < 10)
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
                        if (flag && handCards.Count < 4)
                        {
                            if (rankLeast < 6)
                            {
                                rankLeast = 6;
                                temp = card;
                            }
                        }
                        else if (flag && enemy.GetCardManager.ExpenseCurrent == 1)
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
                        if (flag && handCards.Count < 10)
                        {
                            if (rankLeast < 5)
                            {
                                rankLeast = 5;
                                temp = card;
                            }
                        }
                        else if ((!flag) && handCards.Count < 12)
                        {
                            if (rankLeast < 5)
                            {
                                rankLeast = 5;
                                temp = card;
                            }
                        }
                        else if (handCards.Count < 5)
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
                        if (player.GetDespondent > 80 || (player.GetDespondent > 40 && player.GetCardManager.CardsNum > 10))
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

            if (temp.GetName != CardName.Empty)
            {
                temp.TakeEffect(self, target);
                handCards.Remove(temp);
                enemy.GetCardManager.ExpenseCurrent -= temp.GetCost;
            }
            foreach (Card cardTemp in handCards)
            {
                if (ExpenseLeast < cardTemp.GetCost)
                {
                    ExpenseLeast = cardTemp.GetCost;
                }
            }

        }

    }
}
