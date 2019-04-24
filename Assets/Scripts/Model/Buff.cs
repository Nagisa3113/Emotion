using UnityEngine;
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
    Comfort//安慰
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


    public virtual void Process(Role role)
    {

    }

    public virtual void ReProcess(Role role)
    {

    }

}



public class BuffDizzy:Buff
{
    public BuffDizzy() : base(BuffType.Dizzy, 1)
    {

    }

    public override void Process(Role role)
    {
        role.GetCardManager.CanAddCard = false;
    }

    public override void ReProcess(Role role)
    {
        role.GetCardManager.CanAddCard = true;
    }
}