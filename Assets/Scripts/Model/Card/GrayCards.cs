using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Iron,  //坚强
// HoldOn,//硬撑
// FightBack,//反击
// JianRenBuBa,//坚韧不拔
// Lesson,//教训
// Accelerate, //加速
// Reinforce,//加强
// Increase,  //改进

public class Iron : Card
{
    public Iron() : base(CardName.Iron, CardColor.Gray, 1, 6)
    {
        this.cName = "坚强";
        this.tip = "获得*点护甲/+2护甲";
        this.tipUpgrade = "获得*点护甲/+4护甲";
        this.oDamage = 30;
    }

    public override void TakeEffect(Role self, Role target)
    {
        //获得30点护甲/+2护甲
        self.GetArmor(30 + 2 * self.CardManager.GetBonus(this.color));
        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //BONUS再+2护甲
            self.GetArmor(2 * self.CardManager.GetBonus(this.color));
        }

    }

}

public class HoldOn : Card
{
    public HoldOn() : base(CardName.HoldOn, CardColor.Gray, 1, 4)
    {
        this.cName = "硬撑";
        this.tip = "获得等同于自己损失血量的护甲";
        this.tipUpgrade = "获得等同于自己损失血量4/3倍的护甲";
    }

    public override void TakeEffect(Role self, Role target)
    {
        //获得等同于自己损失血量的护甲
        self.GetArmor(self.HPMax - self.HP);
        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //再获得等同于自己损失血量1/3的护甲
            self.GetArmor((self.HPMax - self.HP) * 1 / 3);
        }

    }

}

public class FightBack : Card
{
    public FightBack() : base(CardName.FightBack, CardColor.Gray, 2, 4)
    {
        this.cName = "反击";
        this.tip = "自己每有5点护甲便对敌人造成3点伤害";
        this.tipUpgrade = "获得50点护甲，自己每有5点护甲便对敌人造成3点伤害";
    }

    public override void TakeEffect(Role self, Role target)
    {
        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //先获得50护甲
            self.GetArmor(50);
        }

        //自己每有5点护甲便对敌人造成3点伤害
        target.GetDamage(self.Armor * 3 / 5);

    }

}

public class JianRenBuBa : Card
{
    public JianRenBuBa() : base(CardName.JianRenBuBa, CardColor.Gray, 2, 4)
    {
        this.cName = "坚忍不拔";
        this.tip = "下个回合前受到的伤害减少*%/+3%减免";
        this.tipUpgrade = "下个回合前受到的伤害减少*%/+5%减免";
    }

    public override void TakeEffect(Role self, Role target)
    {
        //下个回合前受到的伤害减少50%/+3%减免

    }
}

public class Lesson : Card
{
    public Lesson() : base(CardName.Lesson, CardColor.Gray, 1, 4)
    {
        this.cName = "教训";
        this.tip = "获得现有护甲*%的护甲，将一张教训加入你的牌组/+5%";
        this.tipUpgrade = "获得50点护甲，获得现有护甲*%的护甲，将一张教训加入你的牌组/+5%";
        this.oDamage = 20;
    }

    public override void TakeEffect(Role self, Role target)
    {
        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //先获得50护甲
            self.GetArmor(50);
        }

        //获得现有护甲20%的护甲，将一张教训加入你的牌组/+5%
        self.GetArmor((int)((0.20f + 0.05f * self.CardManager.GetBonus(this.color)) * self.Armor));
        self.CardLibrary.Add(Card.NewCard((CardName.Lesson)));
    }

}

public class Accelerate : Card
{
    public Accelerate() : base(CardName.Accelerate, CardColor.Gray, 2, 3, 6)
    {
        this.cName = "加速";
        this.tip = "使自己获得2回合活力增益效果";
        this.tipUpgrade = "使自己获得3回合活力增益效果";
        this.tipUpgradeTwice = "使自己获得4回合活力增益效果";
    }

    public override void TakeEffect(Role self, Role target)
    {
        //使自己获得2回合活力效果
        self.GetBuffManager.AddBuff(BuffName.VigourBuff, 2);
        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //持续加一
            target.GetBuffManager.BuffAddLayer(BuffName.VigourBuff);
        }


        if (self.CardManager.GetBonus(this.color) > this.upgradeTwice)
        {
            //持续加一
            target.GetBuffManager.BuffAddLayer(BuffName.VigourBuff);
        }
    }

}

public class Reinforce : Card
{
    public Reinforce() : base(CardName.Reinforce, CardColor.Gray, 2, 3, 6)
    {
        this.cName = "加强";
        this.tip = "使自己获得2回合强力增益效果";
        this.tipUpgrade = "使自己获得3回合强力增益效果";
        this.tipUpgradeTwice = "使自己获得4回合强力增益效果";
    }

    public override void TakeEffect(Role self, Role target)
    {
        //使自己获得2回合强力效果
        self.GetBuffManager.AddBuff(BuffName.PowerfulBuff, 2);
        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //持续加一
            target.GetBuffManager.BuffAddLayer(BuffName.PowerfulBuff);
        }


        if (self.CardManager.GetBonus(this.color) > this.upgradeTwice)
        {
            //持续加一
            target.GetBuffManager.BuffAddLayer(BuffName.PowerfulBuff);
        }
    }

}

public class Increase : Card
{
    public Increase() : base(CardName.Increase, CardColor.Gray, 1, 5)
    {
        this.cName = "改进";
        this.tip = "在本局游戏中，每回合开始时，减少5点血量，获得*点护甲/+2护甲";
        this.tipUpgrade = "在本局游戏中，每回合开始时，减少3点血量，获得*点护甲/+2护甲";
        this.oDamage = 10;
    }

    public override void TakeEffect(Role self, Role target)
    {
        //使自己获得2回合改进效果
        self.GetBuffManager.AddBuff(BuffName.IncreaseBuff, 9999);

    }

}

