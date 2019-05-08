using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum EffectType
{
    HPEffect,
    ArmorEffect,
    BuffEffect,
    CardEffect,
    ActionEffect,
    Else,
}



public static class EffectProcess
{

    public static void TakeEffect(Card card, Role self, Role target)
    {
        CardName cardName = card.GetName;
        int bonus = self.GetCardManager.GetBonus(cardName);
        bool upgrade = bonus > card.GetUpgrade;
        bool upgradeTwice = bonus > card.GetUpgradeTwice;


        switch (cardName)
        {

            case CardName.Anger:

                break;

            case CardName.AngerFire:
                self.GetHP -= 10;
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



    public static void Effect(EffectType effectType, HPEffectType hPEffectType, Role self, Role target, int value)
    {
        switch (hPEffectType)
        {
            case HPEffectType.Damage:
                target.GetHP -= value;
                break;
        }
    }

}


public enum HPEffectType
{
    Damage,
    SelfDamage,
    RealDamage,
    PercentDamage,
    Heal,
}

public enum ArmorEffect
{
    IncreaseArmor,
    DecreaseArmor,
}


public enum BuffEffect
{
    SelfBuff,
    TargetBuff,
    RemoveBuff,
    MarkBuff,
}

public enum CardBuff
{
    PutCard,
    GetCard,
    AddCard,

}

public enum ActionBuff
{
    DestroyTarget,
}
