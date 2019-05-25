using System;
using UnityEngine;

public class Complain : Card
{
    public Complain() : base(CardName.Complain, CardColor.Purple, 1, 4)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        //减少敌人血量10点，使敌人获得两层消沉 / +2点
        self.TakeDamage(target, 10 + 2 * self.GetCardManager.GetBonus(this.name));
        target.GetDespondent += 2;

        if (self.GetCardManager.GetBonus(this.name) > this.upgrade)
        {
            //再使敌人获得两层消沉
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
        //两回合内，敌人每打出一张卡牌，获得一层消沉
        target.GetBuffManager.AddBuff(CardName.DullAtmosphere);

        if (self.GetCardManager.GetBonus(this.name) > this.upgrade)
        {
            //+1持续回合 
        }
        if (self.GetCardManager.GetBonus(this.name) > this.upgradeTwice)
        {
            //再获得一层消沉
        }

    }


}

public class WeiYuChouMou : Card
{
    public WeiYuChouMou() : base(CardName.WeiYuChouMou, CardColor.Purple, 1, 3, 8)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        //随机减少一张手牌的费用1点
        self.GetCardManager.GetRandomCard().GetCost--;


        if (self.GetCardManager.GetBonus(this.name) > this.upgrade)
        {
            //再随机减少一张手牌费用
            self.GetCardManager.GetRandomCard().GetCost--;
        }


        if (self.GetCardManager.GetBonus(this.name) > this.upgradeTwice)
        {
            //再减少一遍
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
        //抽取2张牌，当手牌数小于3时再抽一张牌 
        self.GetCardManager.GetCardsFromLibrary(2);

        if (self.GetCardManager.CardsNum < 3)
        {

            self.GetCardManager.GetCardsFromLibrary(1);
        }


        if (self.GetCardManager.GetBonus(this.name) > this.upgrade)
        {
            //+1再抽牌上限
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

        //移除敌人身上的所有消沉，每移除一层消沉，减少敌人血量的1 %
        //再减少1%

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
            self.TakeDamage(target, (int)(target.GetHP * tmp));
            target.GetDespondent--;
        }
    }

}


