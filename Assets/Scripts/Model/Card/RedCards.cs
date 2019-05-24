using System;
using UnityEngine;


public class NoNameFire : Card
{
    public NoNameFire() : base(CardName.NoNameFire, CardColor.Red, 0, 3)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        base.TakeEffect(self, target);
        target.GetHP -= 10 + 2 * self.GetCardManager.GetBonus(this.name);
        CardLibrary.GetInstance().AddCard(CardName.AngerFire);

        if (self.GetCardManager.GetBonus(this.name) > this.upgrade)
        {
            self.GetCardManager.AddCard(CardName.AngerFire);
            self.GetCardManager.AddCard(CardName.AngerFire);
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
        base.TakeEffect(self, target);

        //重复两次？待实现
        target.GetHP -= 15 + 2 * self.GetCardManager.GetBonus(this.name);
        target.GetHP -= 15 + 2 * self.GetCardManager.GetBonus(this.name);



        if (self.GetCardManager.GetBonus(this.name) > this.upgrade)
        {
            target.GetHP -= 15 + 2 * self.GetCardManager.GetBonus(this.name);
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
        base.TakeEffect(self, target);

        //如果该牌杀死一名敌人，这张牌加入你的牌库，待实现
        target.GetHP -= 50 + 8 * self.GetCardManager.GetBonus(this.name);




        if (self.GetCardManager.GetBonus(this.name) > this.upgrade)
        {
            target.GetHP -= 2 * self.GetCardManager.GetBonus(this.name);
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
        base.TakeEffect(self, target);


    }

}


public class Furious : Card
{
    public Furious() : base(CardName.Furious, CardColor.Red, 3, 5)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        base.TakeEffect(self, target);

        int num = UnityEngine.Random.Range(2, 6);

        self.GetCardManager.DicardCard(num, CardColor.Red);
        target.GetHP -= num * (40 + 2 * self.GetCardManager.GetBonus(this.name));



        if (self.GetCardManager.GetBonus(this.name) > this.upgrade)
        {
            CardLibrary.GetInstance().AddCard(CardName.AngerFire);
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
        base.TakeEffect(self, target);

        //将两张红色牌转换为怒火

    }

}


public class Vent : Card
{
    public Vent() : base(CardName.Vent, CardColor.Red, 4, 5)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        base.TakeEffect(self, target);
        self.GetCardManager.PutAllCard(CardName.AngerFire, self, target);
        CardLibrary.GetInstance().PutAllCard(CardName.AngerFire, self, target);

        if (self.GetCardManager.GetBonus(this.name) > this.upgrade)
        {
            //将和弃牌数量相同数量的怒火加入你的牌库

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
        self.GetBuffManager.AddBuff(CardName.Incite);
        if (self.GetCardManager.GetBonus(this.name) > this.upgrade)
        {
            //+1 持续回合
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


        if (self.GetCardManager.GetBonus(this.name) > this.upgrade)
        {
            //每损失3点血量
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
        self.GetBuffManager.AddBuff(CardName.Revenge);
        if (self.GetCardManager.GetBonus(this.name) > this.upgrade)
        {
            //+1 持续回合
        }
        if (self.GetCardManager.GetBonus(this.name) > this.upgradeTwice)
        {
            //再将一张怒火加入到你的牌库
        }
    }


    public class Anger : Card
    {
        public Anger() : base(CardName.Anger, CardColor.Red, 0, 5)
        {

        }

        public override void TakeEffect(Role self, Role target)
        {
            base.TakeEffect(self, target);
            target.GetHP -= 10;
        }


    }

    public class AngerFire : Card
    {
        public AngerFire() : base(CardName.AngerFire, CardColor.Red, 0, 5)
        {

        }

        public override void TakeEffect(Role self, Role target)
        {
            base.TakeEffect(self, target);
            target.GetHP -= 10 + 2 * self.GetCardManager.GetBonus(this.name);
            if (self.GetCardManager.GetBonus(this.name) > this.upgrade)
            {
                self.GetCardManager.AddCard(CardName.AngerFire);
            }
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
        base.TakeEffect(self, target);
        target.GetHP -= 10;
    }


}

public class AngerFire : Card
{
    public AngerFire() : base(CardName.AngerFire, CardColor.Red, 0, 5)
    {

    }

    public override void TakeEffect(Role self, Role target)
    {
        base.TakeEffect(self, target);
        target.GetHP -= 10 + 2 * self.GetCardManager.GetBonus(this.name);
        if (self.GetCardManager.GetBonus(this.name) > this.upgrade)
        {
            self.GetCardManager.AddCard(CardName.AngerFire);
        }
    }


}

