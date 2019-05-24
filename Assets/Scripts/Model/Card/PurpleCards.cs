using System;
using UnityEngine;

public class Complain : Card
{
    public Complain() : base(CardName.Complain, CardColor.Purple, 1, 4)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        target.GetHP -= 10 + 2 * self.GetCardManager.GetBonus(this.name);
        target.GetDespondent += 2;
        if (self.GetCardManager.GetBonus(this.name) > this.upgrade)
        {
            target.GetDespondent += 2;
        }
    }


}

public class DullAtmosphere : Card
{
    public DullAtmosphere() : base(CardName.DullAtmosphere, CardColor.Purple, 2, 4, 10)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        target.GetBuffManager.AddBuff(CardName.DullAtmosphere);
    }


}

public class WeiYuChouMou : Card
{
    public WeiYuChouMou() : base(CardName.WeiYuChouMou, CardColor.Purple, 1, 3, 8)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        self.GetCardManager.GetRandomCard().GetCost--;
        if (self.GetCardManager.GetBonus(this.name) > this.upgrade)
        {
            self.GetCardManager.GetRandomCard().GetCost--;
        }
        if (self.GetCardManager.GetBonus(this.name) > this.upgradeTwice)
        {
            self.GetCardManager.GetRandomCard().GetCost--;
            self.GetCardManager.GetRandomCard().GetCost--;
        }

    }


}

public class OuDuanSiLian : Card
{
    public OuDuanSiLian() : base(CardName.OuDuanSiLian, CardColor.Purple, 1, 4)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        self.GetCardManager.GetCardsFromLibrary(2);
        if (self.GetCardManager.CardsNum < 3)
        {
            self.GetCardManager.GetCardsFromLibrary(1);
        }
        if (self.GetCardManager.GetBonus(this.name) > this.upgrade)
        {
            self.GetCardManager.GetCardsFromLibrary(1);
        }

    }


}



public class Confess : Card
{
    public Confess() : base(CardName.Confess, CardColor.Purple, 4, 10)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        float tmp;
        if (self.GetCardManager.GetBonus(this.name) > this.upgrade)
        {
            tmp = 0.02f;
        }
        else
        {
            tmp = 0.01f;
        }


        while (target.GetDespondent > 0)
        {
            target.GetHP -= (int)(target.GetHP * tmp);
            target.GetDespondent--;
        }
    }

}


