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


    NoNameFire,//无名火
    Enrange,//激怒
    Execute,//处决
    Provoke,//挑衅
    Furious,//狂暴
    ReasonVanish,//理性蒸发
    Vent,//发泄
    Incite,//躁动
    RadicalAction,//过激反应
    Revenge,//报复
    Anger,//怒气
    AngerFire,//怒火





    Complain,//抱怨




    DullAtmosphere,//沉闷氛围
    Confess,//倾诉
    WeiYuChouMou,//未雨绸缪
    OuDuanSiLian,//藕断丝连


    Heal,//治疗
    Comfort,//安慰
    XingZaiLeHuo,//幸灾乐祸
    SelfControl,//克己
    Plot,//暗算
    Encumber,//拖累
    Sneer,//嘲讽
    OverHeated,//怒火中烧
    RePastEvent,//往事重提
    Obstruct,//阻挠



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
    [HideInInspector]
    public string cardname;

    CardColor color;

    GameObject image;

    public CardName name;


    protected int upgrade;

    protected int upgradeTwice;


    public CardName GetName

    {
        get
        {
            return name;
        }
    }

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



    public Card(CardName cardName, CardColor color, int cost, int upgrade, int upgradeTwice)
    {
        this.name = cardName;
        this.color = color;
        this.upgrade = upgrade;
        this.upgradeTwice = upgradeTwice;
        this.cost = cost;
        this.cardname = this.name.ToString();
    }
    public Card(CardName cardName, CardColor color, int cost, int upgrade)
    {
        this.name = cardName;
        this.color = color;
        this.upgrade = upgrade;
        this.upgradeTwice = 999;
        this.cost = cost;
        this.cardname = this.name.ToString();
    }
    public Card(CardName cardName, CardColor color, int cost)
    {
        this.name = cardName;
        this.color = color;
        this.upgrade = 999;
        this.upgradeTwice = 999;
        this.cost = cost;
        this.cardname = this.name.ToString();
    }

    public Card()
    {
        this.name = CardName.Empty;
    }

    public virtual void TakeEffect(Role self, Role target)
    {

    }



    public static Card NewCard(CardName cardName)
    {
        switch (cardName)
        {

            case CardName.Empty:
                break;

            case CardName.NoNameFire:
                return new NoNameFire();

            case CardName.Vent:
                return new NoNameFire();

            case CardName.Incite:
                return new Incite();

            case CardName.Revenge:
                return new Revenge();

            case CardName.Anger:
                return new Anger();

            case CardName.AngerFire:
                return new AngerFire();



            case CardName.Complain:
                return new Complain();

            case CardName.DullAtmosphere:
                return new DullAtmosphere();


            case CardName.WeiYuChouMou:
                return new WeiYuChouMou();

            case CardName.OuDuanSiLian:
                return new OuDuanSiLian();


            case CardName.Confess:
                return new Confess();


            case CardName.Heal:
                return new Heal();

            case CardName.Comfort:
                return new Comfort();

            default:
                return null;

        }
        return null;
    }



}

public class Empty : Card
{
    public Empty()
    {
        this.name = CardName.Empty;
    }
}