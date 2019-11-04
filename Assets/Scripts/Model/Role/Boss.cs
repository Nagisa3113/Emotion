using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedBoss : Enemy
{
    public RedBoss() : base(EnemyType.Boss, EnemyColor.Red)
    {

    }

    public override void InitLibrary()
    {
        Card.AddCard(cardLibrary, CardName.NoNameFire, 5);
        Card.AddCard(cardLibrary, CardName.Enrange, 10);
        Card.AddCard(cardLibrary, CardName.ReasonVanish, 2);
        Card.AddCard(cardLibrary, CardName.Revenge, 5);
        Card.AddCard(cardLibrary, CardName.RadicalAction, 3);
        Card.AddCard(cardLibrary, CardName.Accelerate, 2);
        Card.AddCard(cardLibrary, CardName.Reinforce, 2);
    }

    public override void GetSelectedCard(Role self, Role target)
    {
        Card temp = Card.EmptyCard;

        int rankLeast = -10;

        foreach (Card card in cardManager.Cards)
        {
            CardName name = card.Name;
            if (card.Cost > cardManager.expenseCurrent)
            {
                continue;
            }
            else
            {
                switch (name)
                {
                    case CardName.Accelerate:
                        if (rankLeast < 1 && cardManager.Cards.Count > 10)
                        {
                            rankLeast = 1;
                            temp = card;
                        }
                        else if (rankLeast < 7)
                        {
                            rankLeast = 7;
                            temp = card;
                        }
                        break;
                    case CardName.Reinforce:
                        if (rankLeast < 7)
                        {
                            rankLeast = 7;
                            temp = card;
                        }
                        break;

                    case CardName.ReasonVanish:
                        if (rankLeast < 3 && cardManager.Cards.Count - cardManager.GetBonus(CardColor.Red) >= 3)
                        {
                            rankLeast = 3;
                            temp = card;
                        }
                        else if (rankLeast < 0)
                        {
                            rankLeast = 0;
                            temp = card;
                        }

                        break;


                    case CardName.Revenge:
                        if (rankLeast < 7)
                        {
                            rankLeast = 7;
                            temp = card;
                        }
                        break;


                    case CardName.NoNameFire:
                        if ((10 + 2 * cardManager.GetBonus(CardColor.Red)) * target.DamageBase(self, target) > target.HP)
                        {
                            rankLeast = 100;
                            temp = card;
                        }
                        else if (rankLeast < 6 && cardManager.Cards.Count < 10)
                        {
                            rankLeast = 6;
                            temp = card;
                        }
                        else if (rankLeast < 4)
                        {
                            rankLeast = 4;
                            temp = card;
                        }
                        break;

                    case CardName.Enrange:
                        if (rankLeast < 100)
                        {
                            if ((15 * 2 + 2 * cardManager.GetBonus(CardColor.Red)) * target.DamageBase(self, target) > target.HP)
                            {
                                rankLeast = 100;
                                temp = card;
                            }
                            if (cardManager.GetBonus(card.Color) > card.Upgrade && (15 * 4 + 2 * cardManager.GetBonus(CardColor.Red)) * target.DamageBase(self, target) > target.HP)
                            {
                                rankLeast = 100;
                                temp = card;
                            }

                        }

                        if (rankLeast < 5)
                        {
                            rankLeast = 5;
                            temp = card;
                        }
                        break;

                    case CardName.RadicalAction:
                        if (((self.HPMax - self.HP) * 2 + 2 * cardManager.GetBonus(CardColor.Red)) * target.DamageBase(self, target) > target.HP)
                        {
                            rankLeast = 100;
                            temp = card;
                        }
                        else if (rankLeast < 6 && self.HP * 5 < self.HPMax)
                        {
                            rankLeast = 6;
                            temp = card;
                        }
                        else if (rankLeast < 4)
                        {
                            rankLeast = 4;
                            temp = card;
                        }
                        break;

                    case CardName.AngerFire:
                        if ((10 + cardManager.GetBonus(CardColor.Red)) * target.DamageBase(self, target) > target.HP)
                        {
                            rankLeast = 100;
                            temp = card;
                        }
                        else if (rankLeast < 3)
                        {
                            rankLeast = 3;
                            temp = card;
                        }
                        break;
                }
            }
        }

        if (temp != Card.EmptyCard)
        {
            cardManager.CurrentCard = temp;
            temp = Card.EmptyCard;
        }
        else
        {
            (cardManager as EnemyCardManager).CanPutCard = false;
        }

    }

}

public class GrayBoss : Enemy
{
    public GrayBoss() : base(EnemyType.Boss, EnemyColor.Gray)
    {

    }

    public override void InitLibrary()
    {
        Card.AddCard(cardLibrary, CardName.Iron, 10);
        Card.AddCard(cardLibrary, CardName.FightBack, 10);
        Card.AddCard(cardLibrary, CardName.Lesson, 3);
        Card.AddCard(cardLibrary, CardName.Increase, 3);
        Card.AddCard(cardLibrary, CardName.Blues, 10);
        Card.AddCard(cardLibrary, CardName.Pacify, 5);
    }

    public override void GetSelectedCard(Role self, Role target)
    {
        Card temp = Card.EmptyCard;

        int rankLeast = -10;


        foreach (Card card in cardManager.Cards)
        {
            CardName name = card.Name;
            if (card.Cost > cardManager.expenseCurrent)
            {
                continue;
            }
            else
            {
                switch (name)
                {

                    case CardName.Iron:
                        if (rankLeast < 4)
                        {
                            rankLeast = 4;
                            temp = card;
                        }
                        break;
                    case CardName.Increase:
                        if (rankLeast < 7)
                        {
                            rankLeast = 7;
                            temp = card;
                        }
                        break;

                    case CardName.Lesson:
                        if (rankLeast < 5 && self.Armor > 150)
                        {
                            rankLeast = 5;
                            temp = card;
                        }
                        else if (rankLeast < 3)
                        {
                            rankLeast = 3;
                            temp = card;
                        }
                        break;


                    case CardName.Pacify:
                        if (rankLeast < 6 && (float)self.HP / self.HPMax < 0.2f)
                        {
                            rankLeast = 6;
                            temp = card;

                        }
                        else if (rankLeast < 2)
                        {
                            rankLeast = 2;
                            temp = card;
                        }
                        break;






                    case CardName.Blues:
                        if ((25 + 2 * cardManager.GetBonus(card.Color)) * target.DamageBase(self, target) > target.HP)
                        {
                            rankLeast = 100;
                            temp = card;
                        }
                        else if (rankLeast < 4)
                        {
                            rankLeast = 4;
                            temp = card;
                        }
                        break;

                    case CardName.FightBack:
                        if (rankLeast < 100)
                        {
                            if (self.Armor / 5 * 3 * target.DamageBase(self, target) > target.HP)
                            {
                                rankLeast = 100;
                                temp = card;
                            }
                            if (cardManager.GetBonus(card.Color) > card.Upgrade && ((self.Armor + 50) / 5 * 3 * target.DamageBase(self, target) > target.HP))
                            {
                                rankLeast = 100;
                                temp = card;
                            }

                        }

                        if (rankLeast < 6 && self.Armor > 150)
                        {
                            rankLeast = 6;
                            temp = card;
                        }
                        else if (rankLeast < 3)
                        {
                            rankLeast = 3;
                            temp = card;
                        }
                        break;

                }
            }
        }

        if (temp != Card.EmptyCard)
        {
            cardManager.CurrentCard = temp;
            temp = Card.EmptyCard;
        }
        else
        {
            (cardManager as EnemyCardManager).CanPutCard = false;
        }


    }

}

public class GreenBoss : Enemy
{
    public GreenBoss() : base(EnemyType.Boss, EnemyColor.Green)
    {

    }

    public override void InitLibrary()
    {
        Card.AddCard(cardLibrary, CardName.OverHeated, 5);
        Card.AddCard(cardLibrary, CardName.RePastEvent, 3);
        Card.AddCard(cardLibrary, CardName.Obstruct, 2);
        Card.AddCard(cardLibrary, CardName.XingZaiLeHuo, 5);
        Card.AddCard(cardLibrary, CardName.ReasonVanish, 3);
        Card.AddCard(cardLibrary, CardName.Incite, 3);
        Card.AddCard(cardLibrary, CardName.Vent, 1);
        Card.AddCard(cardLibrary, CardName.RePastEvent, 3);
        Card.AddCard(cardLibrary, CardName.NoNameFire, 5);
    }

    public override void GetSelectedCard(Role self, Role target)
    {
        Card temp = Card.EmptyCard;

        List<Card> library = self.CardLibrary;

        int rankLeast = -10;

        //bool flag = false; //判断牌内是否有倾诉
        foreach (Card cardTemp in cardManager.Cards)
        {
            if (cardTemp.Name == CardName.Confess)
            {
                //flag = true;
                break;
            }
        }

        foreach (Card card in cardManager.Cards)
        {
            CardName name = card.Name;
            if (card.Cost > cardManager.expenseCurrent)
            {
                continue;
            }
            else
            {
                switch (name)
                {

                    case CardName.OverHeated:
                        if (rankLeast < 6)
                        {
                            rankLeast = 6;
                            temp = card;
                        }
                        break;

                    case CardName.RePastEvent:
                        if (rankLeast < 5 && target.GetBuffManager.Buffs.Count > 0)
                        {
                            rankLeast = 5;
                            temp = card;
                        }
                        break;


                    case CardName.Obstruct:
                        if (rankLeast < 7 && target.GetBuffManager.Buffs.Count > 1)
                        {
                            rankLeast = 7;
                            temp = card;

                        }
                        else if (rankLeast < 3 && target.GetBuffManager.Buffs.Count > 0)
                        {
                            rankLeast = 5;
                            temp = card;
                        }
                        break;

                    case CardName.XingZaiLeHuo:
                        if (rankLeast < 7 && target.GetBuffManager.Buffs.Count > 0 && (float)self.HP / self.HPMax < 0.2f)
                        {

                            rankLeast = 7;
                            temp = card;

                        }
                        else if (rankLeast < 3 && target.GetBuffManager.Buffs.Count > 0)
                        {
                            rankLeast = 3;
                            temp = card;
                        }

                        break;


                    case CardName.ReasonVanish:
                        if (rankLeast < 4 && cardManager.Cards.Count - cardManager.GetBonus(CardColor.Red) >= 3)
                        {
                            rankLeast = 4;
                            temp = card;
                        }
                        else if (rankLeast < 0)
                        {
                            rankLeast = 0;
                            temp = card;
                        }

                        break;

                    case CardName.Incite:
                        if (rankLeast < 7)
                        {
                            rankLeast = 7;
                            temp = card;
                        }
                        break;

                    case CardName.Vent:
                        if (cardManager.GetAngerFireNum(self) * 10 * target.DamageBase(self, target) > target.HP)
                        {
                            rankLeast = 100;
                            temp = card;
                        }
                        else if (rankLeast < 4 && cardManager.GetAngerFireNum(self) > 10)
                        {
                            rankLeast = 4;
                            temp = card;
                        }
                        break;
                    case CardName.NoNameFire:
                        if ((10 + 2 * cardManager.GetBonus(CardColor.Red)) * target.DamageBase(self, target) > target.HP)
                        {
                            rankLeast = 100;
                            temp = card;
                        }
                        else if (rankLeast < 4)
                        {
                            rankLeast = 4;
                            temp = card;
                        }
                        break;
                    case CardName.AngerFire:
                        if ((10 + 2 * cardManager.GetBonus(CardColor.Red)) * target.DamageBase(self, target) > target.HP)
                        {
                            rankLeast = 100;
                            temp = card;
                        }
                        else if (rankLeast < 2)
                        {
                            rankLeast = 2;
                            temp = card;
                        }
                        break;
                }
            }
        }

        if (temp != Card.EmptyCard)
        {
            cardManager.CurrentCard = temp;
            temp = Card.EmptyCard;
        }
        else
        {
            (cardManager as EnemyCardManager).CanPutCard = false;
        }

    }

}