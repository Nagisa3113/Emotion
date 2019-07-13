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
        self.TakeDamage(target, 10 + 2 * self.CardManager.GetBonus(this.color));
        target.Despondent += 2;

        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //再使敌人获得两层消沉
            target.Despondent += 2;
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
        target.GetBuffManager.AddBuff(BuffName.DullAtmosphereBuff,2);

        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //+1持续回合 
        }
        if (self.CardManager.GetBonus(this.color) > this.upgradeTwice)
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
        self.CardManager.GetRandomCard().Cost--;


        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //再随机减少一张手牌费用
            self.CardManager.GetRandomCard().Cost--;
        }


        if (self.CardManager.GetBonus(this.color) > this.upgradeTwice)
        {
            //再减少一遍
            self.CardManager.GetRandomCard().Cost--;
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
        self.GetCardsFromLibrary(2);

        if (self.CardManager.CardsNum < 3)
        {
            self.GetCardsFromLibrary(1);
        }


        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //+1再抽牌上限
            self.GetCardsFromLibrary(1);
        }

    }
}

public class Depress : Card
{
    public Depress() : base(CardName.Depress, CardColor.Purple, 1, 8)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        //减少敌人血量的20%/+2%
        self.TakeDamage(target,(int)( (20 + 2 * self.CardManager.GetBonus(this.color)) / 100 *target.HP));

        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //再减少敌人血量的20%
            self.TakeDamage(target,(int) ((20 ) / 100 * target.HP));
        }

    }
}

public class Blues : Card
{
    public Blues() : base(CardName.Blues, CardColor.Purple, 1, 4)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        //减少敌人血量25点/+2点
        self.TakeDamage(target, 25 + 2 * self.CardManager.GetBonus(this.color));

        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //BONUS再+2点
            self.TakeDamage(target, 2 * self.CardManager.GetBonus(this.color));
        }

    }
}


public class Pacify : Card
{
    public Pacify() : base(CardName.Pacify, CardColor.Purple, 2, 4)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //先回复自己40点血量
            self.GetHeal(40);
        }
        //减少敌人血量10%并回复相同百分比的自己血量/+1%
        self.TakeDamage(target, (int)((10 + 1 * self.CardManager.GetBonus(this.color)) / 100.0 *target.HP));
        self.GetHeal( (int) ( (10 + 1 * self.CardManager.GetBonus(this.color) ) / 100.0 *self.HP));
         
  

    }
}

public class Compare : Card
{
    public Compare() : base(CardName.Compare, CardColor.Purple, 2, 4)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
       
        //如果敌人血量大于血量上限的50%，减少敌人血量80点，如果自己血量小于自己血量上限的50%回复自己血量40点/+3点
    
        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //bonus +2
            if (target.HP * 2 >target.HPMax)
            {
                self.TakeDamage(target, 80+ 5 * self.CardManager.GetBonus(this.color));
            }
            if(self.HP *2<self.HPMax)
            {
                self.GetHeal(40 + 5 * self.CardManager.GetBonus(this.color));
            }
        } 
        else
        {
            if (target.HP * 2 >target.HPMax)
            {
                self.TakeDamage(target, 80+ 3 * self.CardManager.GetBonus(this.color));
            }
            if(self.HP *2<self.HPMax)
            {
                self.GetHeal(40 + 3 * self.CardManager.GetBonus(this.color));
            }
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
        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            tmp = 0.02f;
        }
        else
        {
            tmp = 0.01f;
        }


        while (target.Despondent > 0)
        {
            self.TakeDamage(target, (int)(target.HP * tmp));
            target.Despondent--;
        }
    }

}


