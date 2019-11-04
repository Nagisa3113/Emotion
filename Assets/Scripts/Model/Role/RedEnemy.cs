using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RedEnemy1 : Enemy
{
    public RedEnemy1() : base(EnemyType.Normal, EnemyColor.Red)
    {

    }

    public override void InitLibrary()
    {
        Card.AddCard(cardLibrary, CardName.Impact, 6);
        Card.AddCard(cardLibrary, CardName.Annoy, 4);
    }
}

public class Annoy : Card
{
    public Annoy() : base(CardName.Annoy, CardColor.Red, 2)
    {
        this.cName = "惹恼";
        this.tip = "添加两张怒气到对手牌库中";

    }

    public override void TakeEffect(Role self, Role target)
    {

        target.CardManager.Cards.Add(Card.NewCard((CardName.Anger)));
        target.CardManager.Cards.Add(Card.NewCard((CardName.Anger)));

    }
}

public class RedEnemy2 : Enemy
{
    public RedEnemy2() : base(EnemyType.Normal, EnemyColor.Red)
    {

    }

    public override void InitLibrary()
    {
        Card.AddCard(cardLibrary, CardName.Blow, 8);
        Card.AddCard(cardLibrary, CardName.Charge, 2);
    }

}

public class Charge : Card
{
    public Charge() : base(CardName.Charge, CardColor.Red, 1)
    {
        this.cName = "蓄力";
        this.tip = "本回合造成伤害翻倍。";

    }

    public override void TakeEffect(Role self, Role target)
    {
        self.GetBuffManager.AddBuff(BuffName.ProvokeBuff, 1);
    }
}


public class RedEnemy3 : Enemy
{
    public RedEnemy3() : base(EnemyType.Normal, EnemyColor.Red)
    {

    }

    public override void InitLibrary()
    {
        Card.AddCard(cardLibrary, CardName.Prolifer, 4);
        Card.AddCard(cardLibrary, CardName.Enrange, 6);
    }
}


public class Prolifer : Card
{
    public Prolifer() : base(CardName.Prolifer, CardColor.Red, 1)
    {
        this.cName = "增殖";
        this.tip = "将两张激怒加入到手牌中";

    }

    public override void TakeEffect(Role self, Role target)
    {
        self.CardManager.Cards.Add(Card.NewCard((CardName.Enrange)));
        self.CardManager.Cards.Add(Card.NewCard((CardName.Enrange)));

    }

}