using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Impact : Card
{
    public Impact() : base(CardName.Impact, CardColor.Colorless, 1)
    {
        this.cName = "冲击";
        this.tip = "造成20点伤害";
    }

    public override void TakeEffect(Role self, Role target)
    {
        //2回合内。你每打出一张非红色卡牌。将一张怒火加入你的手牌
        self.TakeDamage(target, 20);
    }
}

public class Blow : Card
{
    public Blow() : base(CardName.Blow, CardColor.Colorless, 1)
    {
        this.cName = "打击";
        this.tip = "造成30点伤害";
    }

    public override void TakeEffect(Role self, Role target)
    {
        //2回合内。你每打出一张非红色卡牌。将一张怒火加入你的手牌
        self.TakeDamage(target, 30);
    }
}

public class Thump : Card
{
    public Thump() : base(CardName.Thump, CardColor.Colorless, 1)
    {
        this.cName = "重击";
        this.tip = "造成40 点伤害";
    }

    public override void TakeEffect(Role self, Role target)
    {
        //2回合内。你每打出一张非红色卡牌。将一张怒火加入你的手牌
        self.TakeDamage(target, 40);
    }
}



