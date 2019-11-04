using System;
using UnityEngine;

public class Heal : Card
{
    public Heal() : base(CardName.Heal, CardColor.Green, 1, 6)
    {
        this.cName = "治疗";
        this.tip = "回复自己*点血量/+2回复";
        this.tipUpgrade = "回复自己*点血量/+4回复";
        this.oDamage = 40;
    }

    public override void TakeEffect(Role self, Role target)
    {
        //回复自己40点血量/+2回复
        self.GetHeal(40 + 2 * self.CardManager.GetBonus(this.color));
        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //BONUS再+2回复
            self.GetHeal(2 * self.CardManager.GetBonus(this.color));
        }
    }
}

public class Comfort : Card
{
    public Comfort() : base(CardName.Comfort, CardColor.Green, 1, 4, 8)
    {
        this.cName = "安慰";
        this.tip = "2回合内，每使用一张卡牌回复15血量";
        this.tipUpgrade = "3回合内，每使用一张卡牌回复15血量";
        this.tipUpgradeTwice = "4回合内，每使用一张卡牌回复15血量";
    }

    public override void TakeEffect(Role self, Role target)
    {
        //2回合内，每使用一张卡牌回复15血量
        self.GetBuffManager.AddBuff(BuffName.ComfortBuff, 2);
        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //持续加一
            target.GetBuffManager.BuffAddLayer(BuffName.ComfortBuff);
        }


        if (self.CardManager.GetBonus(this.color) > this.upgradeTwice)
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
        this.cName = "幸灾乐祸";
        this.tip = "敌人每有一种异常状态，回复自己*血量/+5回复";
        this.tipUpgrade = "使敌人获得一回合腐蚀效果，敌人每有一种异常状态，回复自己*血量/+5回复";
        this.oDamage = 30;
    }

    public override void TakeEffect(Role self, Role target)
    {
        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //先给予对方1回合腐蚀效果
            target.GetBuffManager.AddBuff(BuffName.CorrodeBuff, 1);
        }
        //敌人每有一种异常状态，回复自己30血量/+5回复
        self.GetHeal(30 * target.GetBuffManager.CheckDebuffCount() + 5 * self.CardManager.GetBonus(this.color));

    }

}


public class SelfControl : Card
{
    public SelfControl() : base(CardName.SelfControl, CardColor.Green, 2, 6)
    {
        this.cName = "克己";
        this.tip = "使自己所有手牌获得打出时回复自己*点血量效果/+1回复";
        this.tipUpgrade = "使自己所有手牌获得打出时回复自己*点血量效果/+2回复";
        this.oDamage = 10;
    }

    public override void TakeEffect(Role self, Role target)
    {
        //使自己所有手牌获得打出时回复自己10点血量效果 / +1回复
    }

}


public class Plot : Card
{
    public Plot() : base(CardName.Plot, CardColor.Green, 4, 10)
    {
        this.cName = "暗算";
        this.tip = "使敌人获得3回合腐蚀效果";
        this.tipUpgrade = "使敌人获得4回合腐蚀效果";
        this.tipUpgradeTwice = "使敌人获得5回合腐蚀效果";
    }

    public override void TakeEffect(Role self, Role target)
    {
        //使敌人获得3回合腐蚀效果
        if (self.CardManager.GetBonus(this.color) >= this.upgradeTwice)
        {
            //持续加一
            target.GetBuffManager.AddBuff(BuffName.CorrodeBuff, 5);
        }
        else if (self.CardManager.GetBonus(this.color) >= this.upgrade)
        {
            //持续加一
            target.GetBuffManager.AddBuff(BuffName.CorrodeBuff, 4);
        }
        else
        {
            target.GetBuffManager.AddBuff(BuffName.CorrodeBuff, 3);
        }

    }

}

public class Encumber : Card
{
    public Encumber() : base(CardName.Encumber, CardColor.Green, 1, 2, 4)
    {
        this.cName = "拖累";
        this.tip = "使敌人获得2回合疲惫效果";
        this.tipUpgrade = "使敌人获得3回合疲惫效果";
        this.tipUpgradeTwice = "使敌人获得4回合疲惫效果";
    }

    public override void TakeEffect(Role self, Role target)
    {
        //使敌人获得2回合疲惫效果
        target.GetBuffManager.AddBuff(BuffName.WearyBuff, 2);
        if (self.CardManager.GetBonus(this.color) >= this.upgrade)
        {
            //持续加一
            target.GetBuffManager.BuffAddLayer(BuffName.WearyBuff);
        }


        if (self.CardManager.GetBonus(this.color) >= this.upgradeTwice)
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
        this.cName = "嘲讽";
        this.tip = "使敌人获得3回合脆弱效果";
        this.tipUpgrade = "使敌人获得4回合脆弱效果";
        this.tipUpgradeTwice = "使敌人获得5回合脆弱效果";
    }

    public override void TakeEffect(Role self, Role target)
    {
        //使敌人获得3回合脆弱效果
        target.GetBuffManager.AddBuff(BuffName.FragileBuff, 3);
        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //持续加一
            target.GetBuffManager.BuffAddLayer(BuffName.FragileBuff);
        }

        if (self.CardManager.GetBonus(this.color) > this.upgradeTwice)
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
        this.cName = "妒火中烧";
        this.tip = "随机使敌人获得腐蚀，疲惫，脆弱三种异常状态中的一种*回合/+1回合";
        this.tipUpgrade = "随机使敌人获得腐蚀，疲惫，脆弱三种异常状态中的一种*回合，再随机使敌人获得三种异常状态中的一种2回合/+1回合";
        this.oDamage = 1;
    }

    public override void TakeEffect(Role self, Role target)
    {
        //随机一种负面状态
        int num = UnityEngine.Random.Range(1, 4);
        switch (num)
        {
            case 1:
                target.GetBuffManager.AddBuff(BuffName.FragileBuff, 1);
                break;
            case 2:
                target.GetBuffManager.AddBuff(BuffName.CorrodeBuff, 1);
                break;
            case 3:
                target.GetBuffManager.AddBuff(BuffName.WearyBuff, 1);
                break;
        }

        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            switch (num)
            {
                case 1:
                    target.GetBuffManager.AddBuff(BuffName.FragileBuff, 1);
                    break;
                case 2:
                    target.GetBuffManager.AddBuff(BuffName.CorrodeBuff, 1);
                    break;
                case 3:
                    target.GetBuffManager.AddBuff(BuffName.WearyBuff, 1);
                    break;
            }
        }


    }

}

public class RePastEvent : Card
{
    public RePastEvent() : base(CardName.RePastEvent, CardColor.Green, 2, 2, 4)
    {
        this.cName = "往事重提";
        this.tip = "延长敌人异常状态2回合";
        this.tipUpgrade = "延长敌人异常状态3回合";
        this.tipUpgradeTwice = "延长敌人异常状态4回合";
    }

    public override void TakeEffect(Role self, Role target)
    {

        //延长敌人异常状态2回合
        if (self.CardManager.GetBonus(this.color) >= this.upgradeTwice)
        {
            //持续加一
            foreach (Buff buff in target.GetBuffManager.Buffs)
            {
                if (target.GetBuffManager.IsDebuff(buff))
                {
                    target.GetBuffManager.BuffAddLayer((BuffName)Enum.Parse(typeof(BuffName), buff.ToString()), 4);
                }
            }
        }
        else if (self.CardManager.GetBonus(this.color) >= this.upgrade)
        {
            //持续加一
            foreach (Buff buff in target.GetBuffManager.Buffs)
            {
                if (target.GetBuffManager.IsDebuff(buff))
                {
                    target.GetBuffManager.BuffAddLayer((BuffName)Enum.Parse(typeof(BuffName), buff.ToString()), 3);
                }
            }

        }
        else
        {
            foreach (Buff buff in target.GetBuffManager.Buffs)
            {
                if (target.GetBuffManager.IsDebuff(buff))
                {
                    target.GetBuffManager.BuffAddLayer((BuffName)Enum.Parse(typeof(BuffName), buff.ToString()), 2);
                }
            }
        }

    }

}

public class Obstruct : Card
{
    public Obstruct() : base(CardName.Obstruct, CardColor.Green, 2, 5)
    {
        this.cName = "阻挠";
        this.tip = "清除敌人身上的所有异常状态，每清除一个异常状态，使敌人眩晕一回合";
        this.tipUpgrade = "清除敌人身上的所有异常状态，每清除一个异常状态，使敌人眩晕一回合";
    }

    public override void TakeEffect(Role self, Role target)
    {
        //清除敌人身上的所有异常状态，每清除一个异常状态，使敌人眩晕一回合
        int temp = target.GetBuffManager.CheckDebuffCount();
        target.GetBuffManager.ResetDeBuff();
        target.GetBuffManager.AddBuff(BuffName.DizzyBuff, temp);

        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {


        }
    }

}