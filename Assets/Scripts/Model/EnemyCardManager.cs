using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class EnemyCardManager : CardManager
{

    Player player;
    Enemy enemy;
    List<Card> library;
    int ExpenseLeast;

    public EnemyCardManager()
    {
        enemy =  GameObject.Find("Battle").GetComponent<BattleSystem>().GetEnemy();
        player = Player.GetInstance();
        library = new List<Card>();
        InitLibrary(); 
    }

    void InitLibrary()
    {
        int i = 0;
        for (i = 0; i < 16; i++)
        {
            library.Add(Card.NewCard(CardName.Complain));
        }
        for (i = 0; i < 6; i++)
        {
            library.Add(Card.NewCard(CardName.DullAtmosphere));
        }
        for (i = 0; i < 3; i++)
        {
            library.Add(Card.NewCard(CardName.WeiYuChouMou));
        }
        for (i = 0; i < 5; i++)
        {
            library.Add(Card.NewCard(CardName.OuDuanSiLian));
        }
        for (i = 0; i < 1; i++)
        {
            library.Add(Card.NewCard(CardName.Confess));
        }
    }

    public override void GetCardsFromLibrary(int num)
    {
        int i;
        for (i = 0;i  < num && CardsNum <= numMax;i++)
        {
            int rand = UnityEngine.Random.Range(0, library.Count);
            Card tmp = library[rand];
            library.RemoveAt(rand);
            cards.Add(tmp);
        }

        //if( num != 6)
            view.ShowEnemyCards();
    }
    

    public override void AI(Role self, Role target)
    {
        if (cards.Count > 0)
        {
            ExpenseLeast = 0;
        }

        if(cards.Count ==0)
        {
            InitLibrary();
        }

        while (cards.Count > 0 && ExpenseCurrent > ExpenseLeast)
        {

            int rankLeast = -10;
            Card temp = new Card();
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
                if (card.GetCost > ExpenseCurrent )
                {
                    break;
                }
                else
                {
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
                        if (flag && cards.Count < 4)
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
            }

            if (temp.GetName != CardName.Empty)
            {
                temp.TakeEffect(self, target);
                view.ShowPlayerPutCard(temp.GetName);
                cards.Remove(temp);
                ExpenseCurrent -= temp.GetCost;
                
                //System.Threading.Thread.Sleep(2000);
            }
            foreach (Card cardTemp in cards)
            {
                if (ExpenseLeast > cardTemp.GetCost)
                {
                    ExpenseLeast = cardTemp.GetCost;
                }
            }
        

        }

    }

}
