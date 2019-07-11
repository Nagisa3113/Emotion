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

    }

    public override void TakeEffect(Role self, Role target)
    {
        //获得30点护甲/+2护甲
        self.GetArmor(30 + 2 * self.CardManager.GetBonus(this.name));
        if (self.CardManager.GetBonus(this.name) > this.upgrade)
        {
            //BONUS再+2护甲
            self.GetArmor( 2 * self.CardManager.GetBonus(this.name));
        }

    }

}

public class HoldOn : Card
{
    public HoldOn() : base(CardName.HoldOn, CardColor.Gray, 1,  4)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        //获得等同于自己损失血量的护甲
        self.GetArmor(self.HPMax - self.HP);
        if (self.CardManager.GetBonus(this.name) > this.upgrade)
        {
            //再获得等同于自己损失血量1/3的护甲
            self.GetArmor((self.HPMax - self.HP)*1/3);
        }

    }

}

public class FightBack : Card
{
    public FightBack () : base(CardName.FightBack , CardColor.Gray, 2,  4)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        if (self.CardManager.GetBonus(this.name) > this.upgrade)
        {
            //先获得50护甲
            self.GetArmor(50);
        }

         //自己每有5点护甲便对敌人造成3点伤害
        target.GetDamage(self.Armor *3 / 5);

    }

}

public class JianRenBuBa : Card
{
    public JianRenBuBa() : base(CardName.JianRenBuBa, CardColor.Gray, 2,  4)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
      //下个回合前受到的伤害减少50%/+3%减免

    }
}

public class Lesson : Card
{
    public Lesson () : base(CardName.Lesson , CardColor.Gray, 1,  4)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        if (self.CardManager.GetBonus(this.name) > this.upgrade)
        {
            //先获得50护甲
            self.GetArmor(50);
        }

         //自己每有5点护甲便对敌人造成3点伤害
        self.GetArmor((int)((0.20f+0.05f * self.CardManager.GetBonus(this.name)) * self.Armor));
        self.CardLibrary.Add(Card.NewCard((CardName.Lesson)));
    }

}

public class Reinforce : Card
{
    public Reinforce() : base(CardName.Reinforce, CardColor.Gray, 2, 3, 6)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        //使自己获得2回合活力效果
        self.GetBuffManager.AddBuff(BuffName.VigourBuff,2);
        if (self.CardManager.GetBonus(this.name) > this.upgrade)
        {
            //持续加一
             target.GetBuffManager.BuffAddLayer(BuffName.VigourBuff);
        }


        if (self.CardManager.GetBonus(this.name) > this.upgradeTwice)
        {
            //持续加一
            target.GetBuffManager.BuffAddLayer(BuffName.VigourBuff);
        }
    }

}

public class Accelerate : Card
{
    public Accelerate() : base(CardName.Accelerate, CardColor.Gray, 2, 3, 6)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        //使自己获得2回合强力效果
        self.GetBuffManager.AddBuff(BuffName.PowerfulBuff,2);
        if (self.CardManager.GetBonus(this.name) > this.upgrade)
        {
            //持续加一
             target.GetBuffManager.BuffAddLayer(BuffName.PowerfulBuff);
        }


        if (self.CardManager.GetBonus(this.name) > this.upgradeTwice)
        {
            //持续加一
            target.GetBuffManager.BuffAddLayer(BuffName.PowerfulBuff);
        }
    }

}

public class Increase : Card
{
    public Increase() : base(CardName.Increase, CardColor.Gray, 1,5)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        //使自己获得2回合改进效果
        self.GetBuffManager.AddBuff(BuffName.IncreaseBuff,1);
    
    }

}

