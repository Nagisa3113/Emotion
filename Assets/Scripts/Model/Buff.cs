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

public enum BuffName
{
    CorrodeBuff,//腐蚀
    WearyBuff,//疲惫
    FragileBuff,//脆弱
    DullAtmosphereBuff,//沉闷氛围
    DizzyBuff,//眩晕
    VigourBuff,//活力
    PowerfulBuff,//强力
    ProvokeBuff,//挑衅


    RevengeBuff,//报复
    InciteBuff,//躁动



    ComfortBuff,//安慰

    IncreaseBuff,

}




[System.Serializable]
public class Buff
{
    [HideInInspector]
    public string name;
    public string tip;

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

    public Buff(string name,string tip, BuffType buffType)
    {
        this.name = name;
        this.tip = tip;
        this.buffType = buffType;
        this.layer = 0;
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

public class CorrodeBuff : Buff
{

    public CorrodeBuff(int layer) : base("腐蚀","敌人造成伤害下降20%", BuffType.AfterPutCard)
    {
        this.layer = layer;
    }


    public override void Process(Role self)
    {
    }

}

public class FragileBuff : Buff
{

    public FragileBuff() : base("脆弱","敌人受到伤害增加20%",BuffType.AfterPutCard)
    {

    }


    public override void Process(Role self)
    {

    }

}

public class WearyBuff : Buff
{

    public WearyBuff() : base("疲惫","敌人费用上限-1", BuffType.AfterPutCard)
    {
        
    }


    public override void Process(Role self)
    {
    }

}


public class InciteBuff : Buff
{

    public InciteBuff() : base("躁动","每打出一张非红，将一张怒火加入手牌", BuffType.AfterPutCard)
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

    public RevengeBuff() : base("报复","每受到一次伤害，将一张怒火加入手牌", BuffType.GetHurt)
    {

    }

    public override void Process(Role self)
    {
        self.CardManager.AddCard(CardName.AngerFire);
    }

}


public class DullAtmosphereBuff : Buff
{
    public DullAtmosphereBuff() : base("沉闷气氛","每打出一张卡牌，获得一层消沉", BuffType.AfterPutCard)
    {

    }

    public override void Process(Role self)
    {
        self.Despondent++;
    }

}

public class DizzyBuff : Buff
{
    public DizzyBuff() : base("眩晕","一回合内无法抽牌", BuffType.AfterPutCard)
    {

    }

    public override void Process(Role self)
    {
        
    }
}

public class VigourBuff : Buff
{
    public VigourBuff() : base("活力","每回合开始多抽1张牌", BuffType.AfterPutCard)
    {

    }

    public override void Process(Role self)
    {
        
    }
}

public class PowerfulBuff : Buff
{
    public PowerfulBuff() : base("强力","自己造成伤害增加20%", BuffType.AfterPutCard)
    {

    }

    public override void Process(Role self)
    {
        
    }
}

public class ProvokeBuff : Buff
{
    public ProvokeBuff() : base("挑衅","本回合中你的牌伤害翻倍", BuffType.AfterPutCard)
    {

    }

    public override void Process(Role self)
    {
        
    }
}

public class IncreaseBuff : Buff
{
    public IncreaseBuff() : base("改进","在本局游戏中，每回合开始时，减少5点血量，获得10点护甲/+2护甲", BuffType.AfterPutCard)
    {

    }

    public override void Process(Role self)
    {
        self.GetDamage(5);
        self.GetArmor(10);
    }
}


public class ComfortBuff : Buff
{
    public ComfortBuff() : base("安抚","每使用一张卡牌，回复15点", BuffType.AfterPutCard)
    {
        
    }

    public override void Process(Role self)
    {
        self.GetHeal(15);
    }

}

