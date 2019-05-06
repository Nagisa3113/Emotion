using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public static class EffectProcess
{

    public static void TakeEffect(CardName cardName, Role self, Role target)
    {
        int bonus = self.GetCardManager.GetBonus(cardName);


        switch (cardName)
        {


            case CardName.Anger:

                break;

            case CardName.AngerFire:
                target.GetHP -= 10;
                break;


            case CardName.NoNameFire:
                self.GetCardManager.AddCard(CardName.AngerFire);
                target.GetHP -= 10 + 2 * bonus;
                break;


            case CardName.Vent:
                self.GetCardManager.PutAllCard(CardName.AngerFire, self, target);
                CardLibrary.GetInstance().PutAllCard(CardName.AngerFire, self, target);
                break;

            case CardName.Incite:
                self.GetBuffManager.AddBuff(CardName.Incite);
                break;

            case CardName.Revenge:
                self.GetBuffManager.AddBuff(CardName.Revenge);
                break;



            case CardName.Complain:
                target.GetHP -= 10 + 2 * bonus;
                target.GetDespondent += 2;
                break;


            case CardName.DullAtmosphere:
                target.GetBuffManager.AddBuff(CardName.DullAtmosphere);
                break;

            case CardName.Confess:

                while (target.GetDespondent > 0)
                {
                    target.GetHP -= (int)(target.GetHP * 0.01f);
                    target.GetDespondent--;
                }

                break;


            case CardName.WeiYuChouMou:
                self.GetCardManager.GetRandomCard().GetCost--;
                break;

            case CardName.OuDuanSiLian:
                self.GetCardManager.GetCardsRandom(2);
                if (self.GetCardManager.CardsNum < 3)
                {
                    self.GetCardManager.GetCardsRandom(1);
                }
                break;




            case CardName.Heal:
                self.GetHP += 40 + 2 * bonus;
                break;

            case CardName.Comfort:
                self.GetBuffManager.AddBuff(CardName.Comfort);
                break;




        }





    }




}


