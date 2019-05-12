using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CardColor
{
    Red,
    Green,
    Gray,
    Purple,
    Yellow,
}


public enum CardName
{
    Empty,//空,未选择时

    Anger,//怒气
    AngerFire,//怒火
    NoNameFire,//无名火

    Vent,//发泄
    Incite,//躁动
    Revenge,//报复

    Complain,//抱怨

    DullAtmosphere,//沉闷氛围
    Confess,//倾诉
    WeiYuChouMou,//未雨绸缪
    OuDuanSiLian,//藕断丝连


    Heal,//治疗
    Comfort,//安慰
}




/*
   存放牌的图片，建议写一个方法去获得gameobject的名字，然后直接赋予给card。 
   由于可能要改成鼠标操作，所以也可以专门写一个牌的脚本。
   以牌为核心来执行。 --lvkiller 4/25
*/
//public GameObject[] images;

[System.Serializable]
public class Card
{

    CardColor color;

    GameObject image;

    [SerializeField]
    protected CardName name;


    protected int upgrade;

    protected int upgradeTwice;


    public CardName GetName

    {
        get
        {
            return name;
        }
    }


    [SerializeField]
    int cost;
    public int GetCost
    {
        get
        {
            return cost;
        }

        set
        {
            cost = value >= 0 ? value : 0;
        }

    }

    public CardColor GetColor
    {
        get
        {
            return color;
        }
    }



    public Card(CardName cardName, CardColor color, int upgrade, int upgradeTwice, int cost)
    {
        this.name = cardName;
        this.color = color;
        this.upgrade = upgrade;
        this.upgradeTwice = upgradeTwice;
        this.cost = cost;
    }
    public Card(CardName cardName, CardColor color, int upgrade, int cost)
    {
        this.name = cardName;
        this.color = color;
        this.upgrade = upgrade;
        this.upgradeTwice = 999;
        this.cost = cost;
    }
    public Card(CardName cardName, CardColor color, int cost)
    {
        this.name = cardName;
        this.color = color;
        this.upgrade = 999;
        this.upgradeTwice = 999;
        this.cost = cost;
    }

    public Card()
    {

    }

    public virtual void TakeEffect(Role self, Role target)
    {

    }

}

public class Empty : Card
{
    public Empty()
    {
        this.name = CardName.Empty;
    }
}