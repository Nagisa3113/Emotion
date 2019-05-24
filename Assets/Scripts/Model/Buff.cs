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




[System.Serializable]
public class Buff
{ 
    public BuffType buffType;


    public string name;

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


    public Buff(string  name,BuffType buffType, int layer)
    {
        this.name = name;
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

    public InciteBuff() : base("每打出一张非红，将一张怒火加入手牌",BuffType.AfterPutCard, 2)
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

    public RevengeBuff() : base("每收到一次伤害，将一张怒火加入手牌",BuffType.GetHurt, 1)
    {

    }

    public override void Process(Role self)
    {
        self.GetCardManager.AddCard(CardName.AngerFire);
    }

}


public class DullAtmosphereBuff : Buff
{
    public DullAtmosphereBuff() : base("每打出一张卡牌，获得一层消沉",BuffType.AfterPutCard, 2)
    {

    }

    public override void Process(Role self)
    {
        self.GetDespondent++;
    }

}


public class ComfortBuff : Buff
{
    public ComfortBuff() : base( "每使用一张卡牌，回复15点",BuffType.AfterPutCard, 2)
    {

    }

    public override void Process(Role self)
    {
        self.GetHP += 15;
    }

}