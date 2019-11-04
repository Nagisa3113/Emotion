using System;
using UnityEngine;

public class Reconcile : Card
{
    public Reconcile() : base(CardName.Reconcile, CardColor.Yellow, 2)
    {
        this.cName = "调和";
        this.tip = "如果这张牌左边和右边的牌颜色不同，抽2张牌，回复自己50点血量，对敌人造成50点伤害";
    }

    public override void TakeEffect(Role self, Role target)
    {
        //如果这张牌左边和右边的牌颜色不同，抽2张牌，回复自己50点血量，对敌人造成50点伤害
        if (self.CardManager.leftCard != Card.EmptyCard && self.CardManager.rightCard != Card.EmptyCard &&
            self.CardManager.leftCard.Color != self.CardManager.rightCard.Color)
        {
            self.GetCardsFromLibrary(2);
            self.GetHeal(50);
            self.TakeDamage(target, 50);

        }
    }

}

public class Feed : Card
{
    public Feed() : base(CardName.Feed, CardColor.Yellow, 2)
    {
        this.cName = "补给";
        this.tip = "抽两张牌，如果两张牌颜色相同，再抽两张牌";
    }

    public override void TakeEffect(Role self, Role target)
    {
        //抽两张牌，如果两张牌颜色相同，再抽两张牌
    }

}

public class Transfer : Card
{
    public Transfer() : base(CardName.Transfer, CardColor.Yellow, 2, 3, 6)
    {
        this.cName = "转化";
        this.tip = "选择一张卡牌，选择一种颜色，将这张牌转化为选择的颜色";
    }

    public override void TakeEffect(Role self, Role target)
    {
        //选择一种颜色，将手牌中所有这种颜色的牌洗回牌库，抽取相同数量的牌
        //ViewOfButton.Instance.StartSuppress();
    }

}

public class Suppress : Card
{
    public Suppress() : base(CardName.Suppress, CardColor.Yellow, 2, 3, 6)
    {
        this.cName = "压制";
        this.tip = "选择一种颜色，将手牌中所有这种颜色的牌洗回牌库，抽取相同数量的牌";
    }

    public override void TakeEffect(Role self, Role target)
    {
        //选择一张卡牌，选择一种颜色，将这张牌转化为选择的颜色
        //ViewOfButton.Instance.StartSuppress();
        //int count = self.CardManager.WashBackCard(10,ViewOfButton.Instance.suppressColor,self);
        //self.GetCardsFromLibrary(2);
    }

}

public class Trick : Card
{
    public Trick() : base(CardName.Trick, CardColor.Yellow, 2, 3, 6)
    {
        this.cName = "戏法";
        this.tip = "复制这张牌左边的牌和右边的牌（不复制铭刻关键词）";
    }

    public override void TakeEffect(Role self, Role target)
    {
        //复制这张牌左边的牌和右边的牌（不复制铭刻关键词）
        if (self.CardManager.leftCard != Card.EmptyCard)
        {
            self.CardLibrary.Add(Card.NewCard(self.CardManager.leftCard.Name));
        }
        if (self.CardManager.rightCard != Card.EmptyCard)
        {
            self.CardLibrary.Add(Card.NewCard(self.CardManager.rightCard.Name));
        }
    }

}