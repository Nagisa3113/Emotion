using UnityEngine;
using System;
using System.Collections;

public enum BuffType
{
    Corrode,//腐蚀
    Weary,//疲惫
    Fragile,//脆弱
    Depress,//沉闷氛围
    Dizzy,//眩晕
    Vigour,//活力
    Powerful,//强力
    Provoke,//挑衅
    Mainia,//躁动


    Vengeance,//报复
    Comfort,//安慰


    AfterPutCard,//出牌后生效
    GetHurt,//收到伤害后生效
}





public class Buff
{
    public BuffType buffType;

    bool active;
    public bool Active
    {
        get
        {
            return active;
        }
    }

    protected int layer;
    public int Layer
    {
        get
        {
            return layer;
        }
        set
        {
            layer = value;
            if (layer == 0)
            {
                active = false;
            }
        }
    }

    public Buff()
    {

    }


    public Buff(BuffType buffType, int layer)
    {
        this.buffType = buffType;
        this.layer = layer;
        this.active = true;
    }

    public virtual void Process()
    {

    }

    public virtual void Process(Role self)
    {

    }

    public virtual void Process(Role self, Role target)
    {

    }

    public virtual void ReProcess()
    {

    }

}


public class InciteBuff : Buff
{

    public InciteBuff() : base(BuffType.AfterPutCard, 2)
    {

    }

    public override void Process(Role self)
    {
        if (self.GetCardManager.CurrentCard.GetColor != CardColor.Red)
        {
            self.GetCardManager.AddCard(CardName.AngerFire);
        }
    }

}

public class RevengeBuff : Buff
{

    public RevengeBuff() : base(BuffType.GetHurt, 1)
    {

    }

    public override void Process(Role self)
    {
        self.GetCardManager.AddCard(CardName.AngerFire);
    }

}


public class DullAtmosphereBuff : Buff
{
    public DullAtmosphereBuff() : base(BuffType.AfterPutCard, 2)
    {

    }

    public override void Process(Role self)
    {
        self.GetDespondent++;
    }

}


public class ComfortBuff : Buff
{
    public ComfortBuff() : base(BuffType.AfterPutCard, 2)
    {

    }

    public override void Process(Role self)
    {
        self.GetHP += 15;
    }

}