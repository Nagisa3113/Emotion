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


    buff,
    debuff,



    AfterPutCard,//出牌后生效
    GetHurt,//收到伤害后生效
}




[System.Serializable]
public class Buff
{
    [HideInInspector]
    public string name;

    public BuffType buffType;

    bool active;
    public bool Active
    {
        get
        {
            return active;
        }
    }

    [SerializeField]
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

    public string Name
    {
        get
        {
            return name;
        }
        set
        {
            name = value;
        }
    }


    public Buff(string name, int layer)
    {

    }


    public Buff(string name, BuffType buffType, int layer)
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

    public InciteBuff() : base("每打出一张非红，将一张怒火加入手牌", BuffType.AfterPutCard, 2)
    {

    }

    public override void Process(Role self)
    {
        if (self.CardManager.CurrentCard.Color != CardColor.Red)
        {
            self.CardManager.AddCard(CardName.AngerFire);
        }
    }

}

public class RevengeBuff : Buff
{

    public RevengeBuff() : base("每受到一次伤害，将一张怒火加入手牌", BuffType.GetHurt, 1)
    {

    }

    public override void Process(Role self)
    {
        self.CardManager.AddCard(CardName.AngerFire);
    }

}


public class DullAtmosphereBuff : Buff
{
    public DullAtmosphereBuff() : base("每打出一张卡牌，获得一层消沉", BuffType.AfterPutCard, 2)
    {

    }

    public override void Process(Role self)
    {
        self.Despondent++;
    }

}


public class ComfortBuff : Buff
{
    public ComfortBuff() : base("每使用一张卡牌，回复15点", BuffType.AfterPutCard, 2)
    {

    }

    public override void Process(Role self)
    {
        self.GetHeal(15);
    }

}