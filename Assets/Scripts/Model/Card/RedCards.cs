﻿using System;
using UnityEngine;


public class NoNameFire : Card
{
    public NoNameFire() : base(CardName.NoNameFire, CardColor.Red, 0, 3)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        //造成10点伤害，将一张怒火加入你的牌库 / +2伤害
        self.TakeDamage(target, 10 + 2 * self.CardManager.GetBonus(this.name));
        self.CardLibrary.Add(Card.NewCard(CardName.AngerFire));

        //再将两张怒火加入你的牌库
        if (self.CardManager.GetBonus(this.name) > this.upgrade)
        {
            self.CardLibrary.Add(Card.NewCard(CardName.AngerFire));
            self.CardLibrary.Add(Card.NewCard(CardName.AngerFire));
        }

    }

}


public class Enrange : Card
{
    public Enrange() : base(CardName.Enrange, CardColor.Red, 1, 5)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        //造成15点伤害，重复两次 / +2伤害
        self.TakeDamage(target, 15 + 2 * self.CardManager.GetBonus(this.name));
        self.TakeDamage(target, 15 + 2 * self.CardManager.GetBonus(this.name));

        //再重复一次
        if (self.CardManager.GetBonus(this.name) > this.upgrade)
        {
            self.TakeDamage(target, 15 + 2 * self.CardManager.GetBonus(this.name));
        }

    }

}


public class Execute : Card
{
    public Execute() : base(CardName.Execute, CardColor.Red, 2, 3)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        //造成50点伤害，如果这张牌杀死了一名敌人，将这张牌加入你的牌库 / +8伤害
        //如果该牌杀死一名敌人，这张牌加入你的牌库，待实现
        self.TakeDamage(target, 50 + 8 * self.CardManager.GetBonus(this.name));

        if(target.HP <= 0)
        {
            self.CardLibrary.Add(Card.NewCard(CardName.Execute));
        }


        //BONUS再 + 2伤害
        if (self.CardManager.GetBonus(this.name) > this.upgrade)
        {
            self.TakeDamage(target, 2 * self.CardManager.GetBonus(this.name));
        }

    }

}


public class Provoke : Card
{
    public Provoke() : base(CardName.Provoke, CardColor.Red, 3, 4, 8)
    {

    }


    public override void TakeEffect(Role self, Role target)
    {
        self.GetBuffManager.AddBuff(BuffName.ProvokeBuff,2);         

    }

}


public class Furious : Card
{
    public Furious() : base(CardName.Furious, CardColor.Red, 3, 5)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        //随机弃掉2~5张红色卡牌，每张被弃掉的卡牌都将造成40伤害 / +2
        int num = UnityEngine.Random.Range(2, 6);

        self.CardManager.DicardCard(num, CardColor.Red,self);
        self.TakeDamage(target, num * (40 + 2 * self.CardManager.GetBonus(this.name)));


        //将和弃牌数量相同数量的怒火加入你的牌库
        if (self.CardManager.GetBonus(this.name) > this.upgrade)
        {
            for (int i = 0; i<num ;i++)
            {
                self.CardLibrary.Add(Card.NewCard((CardName.AngerFire)));
            }
        }

    }

}



public class ReasonVanish : Card
{
    public ReasonVanish() : base(CardName.ReasonVanish, CardColor.Red, 2, 3)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {

        //将手牌中的两张非红色卡牌转换为怒火
        CardColor temp =(CardColor) UnityEngine.Random.Range(0, 5);
        while (temp == CardColor.Red)
        {
            temp =(CardColor) UnityEngine.Random.Range(0, 5);
        }
        self.CardManager.DicardCard(1,temp,self);
        temp =(CardColor) UnityEngine.Random.Range(0, 5);
        while (temp == CardColor.Red)
        {
            temp =(CardColor) UnityEngine.Random.Range(0, 5);
        }
        self.CardManager.DicardCard(1,temp,self);
        //self.CardLibrary.Add(Card.NewCard((CardName.AngerFire)));
        //self.CardLibrary.Add(Card.NewCard((CardName.AngerFire)));
        self.CardManager.GetCards.Add(Card.NewCard((CardName.AngerFire)));
        self.CardManager.GetCards.Add(Card.NewCard((CardName.AngerFire)));
        if (self.CardManager.GetBonus(this.name) > this.upgrade)
        {
            self.CardManager.GetCards.Add(Card.NewCard((CardName.AngerFire)));
            self.CardManager.GetCards.Add(Card.NewCard((CardName.AngerFire)));
        }



        //转换为两张怒火

    }

}


public class Vent : Card
{
    public Vent() : base(CardName.Vent, CardColor.Red, 4, 5)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        //打出牌库和手牌中所有怒火 
        int num = self.CardManager.PutAllCard(CardName.AngerFire, self, target);
        num +=self.PutAllCardInLibrary(CardName.AngerFire, self, target);

        if (self.CardManager.GetBonus(this.name) > this.upgrade)
        {
            //将和弃牌数量相同数量的怒火加入你的牌库
            for (int i = 0 ; i<num ;i++)
            {
                //self.CardLibrary.Add(Card.NewCard((CardName.AngerFire)));
                self.CardManager.GetCards.Add(Card.NewCard((CardName.AngerFire)));
            }

        }

    }


}



public class Incite : Card
{
    public Incite() : base(CardName.Incite, CardColor.Red, 1, 5, 10)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        //2回合内。你每打出一张非红色卡牌。将一张怒火加入你的手牌
        self.GetBuffManager.AddBuff(BuffName.InciteBuff,2);

        if (self.CardManager.GetBonus(this.name) > this.upgrade)
        {
            //+1 持续回合
            self.GetBuffManager.BuffAddLayer(BuffName.InciteBuff);
        }

    }


}


public class RadicalAction : Card
{
    public RadicalAction() : base(CardName.RadicalAction, CardColor.Red, 1, 6)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {

        //你每损失5点血量，便对敌人造成10点伤害/+2伤害

        if (self.CardManager.GetBonus(this.name) > this.upgrade)
        {
            //每损失3点血量
            self.TakeDamage(target,(self.HPMax -self.HP) / 3 *(10 +2 *self.CardManager.GetBonus(this.name)));
        }
        else
        {
            self.TakeDamage(target,(self.HPMax -self.HP) / 5 *(10 +2 *self.CardManager.GetBonus(this.name)));
        }

    }


}




public class Revenge : Card
{
    public Revenge() : base(CardName.Revenge, CardColor.Red, 2, 3, 6)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        //1回合内，你每受到一次伤害就将一张怒火加入你的手牌
        self.GetBuffManager.AddBuff(BuffName.RevengeBuff,1);

        if (self.CardManager.GetBonus(this.name) > this.upgrade)
        {
            //+1 持续回合
             self.GetBuffManager.BuffAddLayer(BuffName.RevengeBuff);
        }

        if (self.CardManager.GetBonus(this.name) > this.upgradeTwice)
        {
            //再将一张怒火加入到你的牌库
            self.CardLibrary.Add(Card.NewCard((CardName.AngerFire)));
        }
    }

}


public class AngerFire : Card
{
    public AngerFire() : base(CardName.AngerFire, CardColor.Red, 0, 5)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {

        //造成10点伤害 / +2伤害 
        self.TakeDamage(target, 10 + 2 * self.CardManager.GetBonus(this.name));

        //将一张怒火加入你的牌库           
        if (self.CardManager.GetBonus(this.name) > this.upgrade)
        {
            self.CardLibrary.Add(Card.NewCard(CardName.AngerFire));
        }
    }


}


public class Anger : Card
{
    public Anger() : base(CardName.Anger, CardColor.Red, 0, 5)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        //无

        //造成10点伤害
        if (self.CardManager.GetBonus(this.name) > this.upgrade)
        {
            self.TakeDamage(target, 10);
        }
    }


}

