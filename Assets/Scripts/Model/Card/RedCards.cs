using System;
using UnityEngine;

public class Anger : Card
{
    public Anger() : base(CardName.Anger, CardColor.Red, 5, 0)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        base.TakeEffect(self, target);
        target.GetHP -= 10;
    }


}

public class AngerFire : Card
{
    public AngerFire() : base(CardName.AngerFire, CardColor.Red, 5, 0)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        base.TakeEffect(self, target);
        target.GetHP -= 10 + 2 * self.GetCardManager.GetBonus(this.name);
        if (self.GetCardManager.GetBonus(this.name) > this.upgrade)
        {
            self.GetCardManager.AddCard(CardName.AngerFire);
        }
    }


}

public class NoNameFire : Card
{
    public NoNameFire() : base(CardName.NoNameFire, CardColor.Red, 3, 0)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        base.TakeEffect(self, target);
        target.GetHP -= 10 + 2 * self.GetCardManager.GetBonus(this.name);
        self.GetCardManager.AddCard(CardName.AngerFire);

        if (self.GetCardManager.GetBonus(this.name) > this.upgrade)
        {
            self.GetCardManager.AddCard(CardName.AngerFire);
            self.GetCardManager.AddCard(CardName.AngerFire);
        }

    }


}

public class Vent : Card
{
    public Vent() : base(CardName.Vent, CardColor.Red, 5, 4)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        base.TakeEffect(self, target);
        self.GetCardManager.PutAllCard(CardName.AngerFire, self, target);
        CardLibrary.GetInstance().PutAllCard(CardName.AngerFire, self, target);

        if (self.GetCardManager.GetBonus(this.name) > this.upgrade)
        {
            //将和弃牌数量相同数量的怒火加入你的牌库

        }

    }


}



public class Incite : Card
{
    public Incite() : base(CardName.Incite, CardColor.Red, 5, 10, 1)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        self.GetBuffManager.AddBuff(CardName.Incite);
        if (self.GetCardManager.GetBonus(this.name) > this.upgrade)
        {
            //+1 持续回合
        }

    }


}
public class Revenge : Card
{
    public Revenge() : base(CardName.Revenge, CardColor.Red, 3, 6, 2)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        self.GetBuffManager.AddBuff(CardName.Revenge);
        if (self.GetCardManager.GetBonus(this.name) > this.upgrade)
        {
            //+1 持续回合
        }
        if (self.GetCardManager.GetBonus(this.name) > this.upgradeTwice)
        {
            //再将一张怒火加入到你的牌库
        }
    }


}

