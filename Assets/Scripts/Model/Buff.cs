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



    PutCard,

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

    int layer;
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


    public virtual void Process(System.Object sender, BuffManager.BuffEventArgs e)
    {

    }

    public virtual void ReProcess(System.Object sender, BuffManager.BuffEventArgs e)
    {

    }

}

public class Buff1:Buff
{

    public override void Process(System.Object sender, BuffManager.BuffEventArgs e)
    {
        Role self = (Role)sender;
        self.GetCardManager.GetCard(CardName.AngerFire);

    }

}

public class Buff2:Buff
{

    public override void Process(System.Object sender, BuffManager.BuffEventArgs e)
    {
        Role self = (Role)sender;
        self.GetHP += 10;

    }

}