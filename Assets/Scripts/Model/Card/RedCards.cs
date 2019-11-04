using System;
using UnityEngine;


public class NoNameFire : Card
{
    public NoNameFire() : base(CardName.NoNameFire, CardColor.Red, 0, 3)
    {
        this.cName = "无名火";
        this.tip = "造成*点伤害，将一张怒火加入你的牌库/+2伤害";
        this.tipUpgrade = "造成*点伤害，将三张怒火加入你的牌库/+2伤害";
        this.oDamage = 10;
    }

    public override void TakeEffect(Role self, Role target)
    {
        //造成10点伤害，将一张怒火加入你的牌库 / +2伤害
        self.TakeDamage(target, 10 + 2 * self.CardManager.GetBonus(this.color));
        self.CardLibrary.Add(Card.NewCard(CardName.AngerFire));

        //再将两张怒火加入你的牌库
        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            self.CardLibrary.Add(Card.NewCard(CardName.AngerFire, true));
            self.CardLibrary.Add(Card.NewCard(CardName.AngerFire, true));
        }

    }

}


public class Enrange : Card
{
    public Enrange() : base(CardName.Enrange, CardColor.Red, 1, 5)
    {
        this.cName = "激怒";
        this.tip = "造成*点伤害，重复两次/+2伤害";
        this.tipUpgrade = "造成*点伤害，重复三次/+2伤害";
        this.oDamage = 15;
    }

    public override void TakeEffect(Role self, Role target)
    {
        //造成15点伤害，重复两次 / +2伤害
        self.TakeDamage(target, 15 + 2 * self.CardManager.GetBonus(this.color));
        self.TakeDamage(target, 15 + 2 * self.CardManager.GetBonus(this.color));

        //再重复一次
        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            self.TakeDamage(target, 15 + 2 * self.CardManager.GetBonus(this.color));
        }

    }

}


public class Execute : Card
{
    public Execute() : base(CardName.Execute, CardColor.Red, 2, 3)
    {
        this.cName = "处决";
        this.tip = "造成*点伤害，如果这张牌杀死了一名敌人，将这张牌加入你的牌库/+8伤害";
        this.tipUpgrade = "造成*点伤害，如果这张牌杀死了一名敌人，将这张牌加入你的牌库/+10伤害";
        this.oDamage = 50;
    }

    public override void TakeEffect(Role self, Role target)
    {
        //造成50点伤害，如果这张牌杀死了一名敌人，将这张牌加入你的牌库 / +8伤害
        //如果该牌杀死一名敌人，这张牌加入你的牌库，待实现
        self.TakeDamage(target, 50 + 8 * self.CardManager.GetBonus(this.color));

        if (target.HP <= 0)
        {
            self.CardLibrary.Add(Card.NewCard(CardName.Execute));
        }


        //BONUS再 + 2伤害
        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            self.TakeDamage(target, 2 * self.CardManager.GetBonus(this.color));
        }

    }

}


public class Provoke : Card
{
    public Provoke() : base(CardName.Provoke, CardColor.Red, 3, 4, 8)
    {
        this.cName = "挑衅";
        this.tip = "本回合中你的牌伤害翻倍";
        this.tipUpgrade = "本回合中你的牌伤害翻倍";
        this.tipUpgradeTwice = "本回合中你的牌伤害翻倍";
    }


    public override void TakeEffect(Role self, Role target)
    {
        self.GetBuffManager.AddBuff(BuffName.ProvokeBuff, 2);

    }

}


public class Furious : Card
{
    public Furious() : base(CardName.Furious, CardColor.Red, 3, 5)
    {
        this.cName = "狂暴";
        this.tip = "随机弃掉2~5张红色卡牌，每张被弃掉的卡牌都将造成*伤害/+2伤害";
        this.tipUpgrade = "随机弃掉2~5张红色卡牌，并将和弃牌数量相同数量的怒火加入你的牌库。每张被弃掉的卡牌都将造成*伤害/+2伤害";
        this.oDamage = 40;
    }

    public override void TakeEffect(Role self, Role target)
    {
        //随机弃掉2~5张红色卡牌，每张被弃掉的卡牌都将造成40伤害 / +2
        int num = UnityEngine.Random.Range(2, 6);

        self.CardManager.DicardCard(num, CardColor.Red, self);
        self.TakeDamage(target, num * (40 + 2 * self.CardManager.GetBonus(this.color)));


        //将和弃牌数量相同数量的怒火加入你的牌库
        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            for (int i = 0; i < num; i++)
            {
                self.CardLibrary.Add(Card.NewCard(CardName.AngerFire, true));
            }
        }

    }

}



public class ReasonVanish : Card
{
    public ReasonVanish() : base(CardName.ReasonVanish, CardColor.Red, 2, 3)
    {
        this.cName = "理性蒸发";
        this.tip = "将手牌中的两张非红色卡牌各转换为一张怒火";
        this.tipUpgrade = "将手牌中的两张非红色卡牌各转换为两张怒火";
    }

    public override void TakeEffect(Role self, Role target)
    {

        //将手牌中的两张非红色卡牌转换为怒火
        CardColor temp = (CardColor)UnityEngine.Random.Range(0, 5);
        while (temp == CardColor.Red)
        {
            temp = (CardColor)UnityEngine.Random.Range(0, 5);
        }
        self.CardManager.DicardCard(1, temp, self);
        temp = (CardColor)UnityEngine.Random.Range(0, 5);
        while (temp == CardColor.Red)
        {
            temp = (CardColor)UnityEngine.Random.Range(0, 5);
        }
        self.CardManager.DicardCard(1, temp, self);
        //self.CardLibrary.Add(Card.NewCard((CardName.AngerFire)));
        //self.CardLibrary.Add(Card.NewCard((CardName.AngerFire)));
        self.CardManager.Cards.Add(Card.NewCard(CardName.AngerFire, true));
        self.CardManager.Cards.Add(Card.NewCard(CardName.AngerFire, true));
        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            self.CardManager.Cards.Add(Card.NewCard(CardName.AngerFire, true));
            self.CardManager.Cards.Add(Card.NewCard(CardName.AngerFire, true));
        }



        //转换为两张怒火

    }

}


public class Vent : Card
{
    public Vent() : base(CardName.Vent, CardColor.Red, 4, 5)
    {
        this.cName = "发泄";
        this.tip = "打出牌库和手牌中所有怒火";
        this.tipUpgrade = "打出牌库和手牌中所有怒火，并将和打出怒火相同数量的怒气加入手牌";
    }

    public override void TakeEffect(Role self, Role target)
    {
        //打出牌库和手牌中所有怒火
        int temp = self.CardManager.GetBonus(this.color);
        int num = self.CardManager.PutAllCard(CardName.AngerFire, self, target);
        num += self.PutAllCardInLibrary(CardName.AngerFire, self, target);

        if (temp > this.upgrade)
        {
            //将和弃牌数量相同数量的怒火加入你的牌库
            for (int i = 0; i < num; i++)
            {
                //self.CardLibrary.Add(Card.NewCard((CardName.AngerFire)));
                self.CardManager.Cards.Add(Card.NewCard((CardName.Anger)));
                View.Instance.ShowPlayerCards();

            }

        }

    }


}



public class Incite : Card
{
    public Incite() : base(CardName.Incite, CardColor.Red, 1, 5, 10)
    {
        this.cName = "躁动";
        this.tip = "2回合内。你每打出一张非红色卡牌。将一张怒火加入你的手牌";
        this.tipUpgrade = "3回合内。你每打出一张非红色卡牌。将一张怒火加入你的手牌";
        this.tipUpgradeTwice = "4回合内。你每打出一张非红色卡牌。将一张怒火加入你的手牌";
    }

    public override void TakeEffect(Role self, Role target)
    {
        //2回合内。你每打出一张非红色卡牌。将一张怒火加入你的手牌



        if (self.CardManager.GetBonus(this.color) > this.upgradeTwice)
        {
            //再将一张怒火加入到你的牌库
            self.GetBuffManager.AddBuff(BuffName.InciteBuff, 4);
        }
        else if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //+1 持续回合
            self.GetBuffManager.AddBuff(BuffName.InciteBuff, 3);
        }
        else
        {
            self.GetBuffManager.AddBuff(BuffName.InciteBuff, 2);
        }

    }


}


public class RadicalAction : Card
{
    public RadicalAction() : base(CardName.RadicalAction, CardColor.Red, 1, 6)
    {
        this.cName = "过激反应";
        this.tip = "你每损失5点血量，便对敌人造成*点伤害/+2伤害";
        this.tipUpgrade = "你每损失3点血量，便对敌人造成*点伤害/+2伤害";
        this.oDamage = 10;
    }

    public override void TakeEffect(Role self, Role target)
    {

        //你每损失5点血量，便对敌人造成10点伤害/+2伤害

        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //每损失3点血量
            self.TakeDamage(target, (self.HPMax - self.HP) / 3 * (10 + 2 * self.CardManager.GetBonus(this.color)));
        }
        else
        {
            self.TakeDamage(target, (self.HPMax - self.HP) / 5 * (10 + 2 * self.CardManager.GetBonus(this.color)));
        }

    }


}




public class Revenge : Card
{
    public Revenge() : base(CardName.Revenge, CardColor.Red, 2, 3, 6)
    {
        this.cName = "报复";
        this.tip = "1回合内，你每受到一次伤害就将一张怒火加入你的手牌";
        this.tipUpgrade = "2回合内，你每受到一次伤害就将一张怒火加入你的手牌";
        this.tipUpgradeTwice = "2回合内，你每受到一次伤害就将两张怒火加入你的手牌";
    }

    public override void TakeEffect(Role self, Role target)
    {
        //1回合内，你每受到一次伤害就将一张怒火加入你的手牌
        self.GetBuffManager.AddBuff(BuffName.RevengeBuff, 1);

        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            //+1 持续回合
            self.GetBuffManager.BuffAddLayer(BuffName.RevengeBuff);
        }

        if (self.CardManager.GetBonus(this.color) > this.upgradeTwice)
        {
            //再将一张怒火加入到你的牌库
            self.CardLibrary.Add(Card.NewCard(CardName.AngerFire, true));
        }
    }

}


public class AngerFire : Card
{
    public AngerFire() : base(CardName.AngerFire, CardColor.Red, 0, 5)
    {
        this.cName = "怒火";
        this.tip = "造成*点伤害/+2伤害";
        this.tipUpgrade = "造成*点伤害，并将一张怒火加入你的牌库/+2伤害";
        this.oDamage = 10;
    }

    public override void TakeEffect(Role self, Role target)
    {

        //造成10点伤害 / +2伤害 
        self.TakeDamage(target, 10 + 2 * self.CardManager.GetBonus(this.color));

        //将一张怒火加入你的牌库           
        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            self.CardLibrary.Add(Card.NewCard(CardName.AngerFire, true));
        }
    }

}


public class Anger : Card
{
    public Anger() : base(CardName.Anger, CardColor.Red, 0, 5)
    {
        this.cName = "怒气";
        this.tip = "无效果";
        this.tipUpgrade = "造成10点伤害";
    }

    public override void TakeEffect(Role self, Role target)
    {
        //无

        //造成10点伤害
        if (self.CardManager.GetBonus(this.color) > this.upgrade)
        {
            self.TakeDamage(target, 10);
        }
    }

}

