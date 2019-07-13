using System;
using UnityEngine;

public class Heal : Card
{
    public Heal() : base(CardName.Heal, CardColor.Green, 1, 6)
    {
    }

    public override void TakeEffect(Role self, Role target)
    {
        //回复自己40点血量/+2回复
        self.GetHeal(40 + 2 * self.CardManager.GetBonus(this.name));
        if (self.CardManager.GetBonus(this.name) > this.upgrade)
        {
            //BONUS再+2回复
            self.GetHeal(2 * self.CardManager.GetBonus(this.name));
        }
    }
}

public class Comfort : Card
{
    public Comfort() : base(CardName.Comfort, CardColor.Green, 1, 4, 8)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        //2回合内，每使用一张卡牌回复15血量
        self.GetBuffManager.AddBuff(BuffName.ComfortBuff,2);
        if (self.CardManager.GetBonus(this.name) > this.upgrade)
        {
            //持续加一
             target.GetBuffManager.BuffAddLayer(BuffName.ComfortBuff);
        }


        if (self.CardManager.GetBonus(this.name) > this.upgradeTwice)
        {
            //持续加一
            target.GetBuffManager.BuffAddLayer(BuffName.ComfortBuff);
        }
    }


}



public class XingZaiLeHuo : Card
{
    public XingZaiLeHuo() : base(CardName.XingZaiLeHuo, CardColor.Green, 1, 4)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        if (self.CardManager.GetBonus(this.name) > this.upgrade)
        {
            //先给予对方1回合腐蚀效果
            target.GetBuffManager.AddBuff(BuffName.CorrodeBuff,1);
        }
        //敌人每有一种异常状态，回复自己30血量/+5回复
        self.GetHeal(30 *target.GetBuffManager.CheckCount() + 5 * self.CardManager.GetBonus(this.name));
      
    }


}


public class SelfControl : Card
{
    public SelfControl() : base(CardName.SelfControl, CardColor.Green, 2, 6)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        //使自己所有手牌获得打出时回复自己10点血量效果 / +1回复
        self.CardManager.selfControl = 10 + self.CardManager.GetBonus(this.name) ;
        if (self.CardManager.GetBonus(this.name) > this.upgrade)
        {
            //bonus加一
            self.CardManager.selfControl += self.CardManager.GetBonus(this.name) ;
        }
    }


}


public class Plot : Card
{
    public Plot() : base(CardName.Plot, CardColor.Green, 1, 2, 4)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        //使敌人获得3回合腐蚀效果
        target.GetBuffManager.AddBuff(BuffName.CorrodeBuff,3);
        if (self.CardManager.GetBonus(this.name) > this.upgrade)
        {
            //持续加一
             target.GetBuffManager.BuffAddLayer(BuffName.CorrodeBuff);
        }


        if (self.CardManager.GetBonus(this.name) > this.upgradeTwice)
        {
            //持续加一
            target.GetBuffManager.BuffAddLayer(BuffName.CorrodeBuff);
        }

    }


}

public class Encumber : Card
{
    public Encumber() : base(CardName.Encumber, CardColor.Green, 1, 2, 4)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        //使敌人获得2回合疲惫效果
        target.GetBuffManager.AddBuff(BuffName.WearyBuff,2);
        if (self.CardManager.GetBonus(this.name) > this.upgrade)
        {
            //持续加一
             target.GetBuffManager.BuffAddLayer(BuffName.WearyBuff);
        }


        if (self.CardManager.GetBonus(this.name) > this.upgradeTwice)
        {
            //持续加一
            target.GetBuffManager.BuffAddLayer(BuffName.WearyBuff);
        }
    }


}

public class Sneer : Card
{
    public Sneer() : base(CardName.Sneer, CardColor.Green, 1, 2, 4)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        //使敌人获得3回合脆弱效果
        target.GetBuffManager.AddBuff(BuffName.FragileBuff,3);
        if (self.CardManager.GetBonus(this.name) > this.upgrade)
        {
            //持续加一
             target.GetBuffManager.BuffAddLayer(BuffName.FragileBuff);
        }


        if (self.CardManager.GetBonus(this.name) > this.upgradeTwice)
        {
            //持续加一
            target.GetBuffManager.BuffAddLayer(BuffName.FragileBuff);
        }
    }

}

public class OverHeated : Card
{
    public OverHeated() : base(CardName.OverHeated, CardColor.Green, 3, 6)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        //随机一种负面状态
        int num = UnityEngine.Random.Range(1, 4);
        switch (num)
        {
            case 1:
                target.GetBuffManager.AddBuff(BuffName.FragileBuff,1);
                break;
            case 2:
                target.GetBuffManager.AddBuff(BuffName.CorrodeBuff,1);
                break;
            case 3:
                target.GetBuffManager.AddBuff(BuffName.WearyBuff,1);
                break;
        }

        if (self.CardManager.GetBonus(this.name) > this.upgrade)
        {
            switch (num)
            {
                case 1:
                    target.GetBuffManager.AddBuff(BuffName.FragileBuff,1);
                    break;
                case 2:
                    target.GetBuffManager.AddBuff(BuffName.CorrodeBuff,1);
                    break;
                case 3:
                    target.GetBuffManager.AddBuff(BuffName.WearyBuff,1);
                    break;
            }
        }


    }


}

public class RePastEvent : Card
{
    public RePastEvent() : base(CardName.RePastEvent, CardColor.Green, 2, 2, 4)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        //延长敌人异常状态2回合
        foreach(Buff buff in target.GetBuffManager.Buffs)
        {
            target.GetBuffManager.BuffAddLayer((BuffName)Enum.Parse(typeof(BuffName), buff.ToString()),2);
        }
    }


}

public class Obstruct : Card
{
    public Obstruct() : base(CardName.Obstruct, CardColor.Green, 2, 5)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        //清除敌人身上的所有异常状态，每清除一个异常状态，使敌人眩晕一回合
        int temp = target.GetBuffManager.CheckCount();
        target.GetBuffManager.ResetBuff();
        target.GetBuffManager.AddBuff(BuffName.DizzyBuff,temp);
        
        if (self.CardManager.GetBonus(this.name) > this.upgrade)
        {
            //费用减一
             
        }
    }


}