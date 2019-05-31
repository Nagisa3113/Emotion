using System;
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






[System.Serializable]
public class Card
{
    [HideInInspector]
    public string cardname;

    GameObject image;

    public CardName name;

    protected string normalStr;
    protected int upgrade;
    protected string upgradeStr;

    protected int upgradeTwice;
    protected string upgradeTwiceStr;


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




    CardColor color;
    public CardColor GetColor
    {
        get
        {
            return color;
        }
    }

    public int GetUpgrade
    {
        get
        {
            return upgrade;
        }
    }

    public string GetNormalStr
    {
        get
        {
            return normalStr;
        }
    }


    static Card emptyCard;
    static public Card EmptyCard
    {
        get
        {
            if (emptyCard == null)
            {
                emptyCard = new Card();
            }
            return emptyCard;
        }

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
    public Card(CardName cardName, CardColor color, int cost, int upgrade)
    {
        this.name = cardName;
        this.color = color;
        this.upgrade = upgrade;
        this.upgradeTwice = 999;
        this.cost = cost;
        this.cardname = this.name.ToString();
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




    public Card()
    {
        this.cardname = "null";
    }


    public virtual void TakeEffect(Role self, Role target)
    {

    }


    public static Card NewCard(CardName cardName)
    {

        //使用反射创建对象
        Type type = Type.GetType(cardName.ToString());
        object obj = Activator.CreateInstance(type, true);
        if (obj == null)  
            {            
            Debug.Log("No Card for" + cardName.ToString());
            return null;       
            }
        return (Card)obj;
        //switch (cardName)
        //{

        //    case CardName.NoNameFire:
        //        return new NoNameFire();

        //    case CardName.Vent:
        //        return new Vent();

        //    case CardName.Incite:
        //        return new Incite();

        //    case CardName.Revenge:
        //        return new Revenge();

        //    case CardName.Anger:
        //        return new Anger();

        //    case CardName.AngerFire:
        //        return new AngerFire();



        //    case CardName.Complain:
        //        return new Complain();

        //    case CardName.DullAtmosphere:
        //        return new DullAtmosphere();


        //    case CardName.WeiYuChouMou:
        //        return new WeiYuChouMou();

        //    case CardName.OuDuanSiLian:
        //        return new OuDuanSiLian();


        //    case CardName.Confess:
        //        return new Confess();


        //    case CardName.Heal:
        //        return new Heal();

        //    case CardName.Comfort:
        //        return new Comfort();

        //    default:
        //        Debug.Log("No Card for" + cardName.ToString());
        //        return null;

        //}
    }

}
