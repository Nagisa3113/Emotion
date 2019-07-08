using System;
using UnityEngine;

public class Heal : Card
{
    public Heal() : base(CardName.Heal, CardColor.Green, 1, 6)
    {
        this.normalStr = "qweewq";
    }

    public override void TakeEffect(Role self, Role target)
    {
        self.GetHeal(40 + 2 * self.CardManager.GetBonus(this.name));
        if (self.CardManager.GetBonus(this.name) > this.upgrade)
        {
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
        self.GetBuffManager.AddBuff(CardName.Comfort);
    }


}



public class XingZaiLehuo : Card
{
    public XingZaiLehuo() : base(CardName.XingZaiLeHuo, CardColor.Green, 1, 4)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        //敌人每有一种异常状态，回复自己30血量/+5回复
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
    }


}

public class Sneer : Card
{
    public Sneer() : base(CardName.Sneer, CardColor.Green, 1, 2, 4)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        //使敌人获得2回合脆弱效果
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
    }


}

public class Obstrucst : Card
{
    public Obstrucst() : base(CardName.RePastEvent, CardColor.Green, 2, 5)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        //清除敌人身上的所有异常状态，每清除一个异常状态，使敌人眩晕一回合
    }


}