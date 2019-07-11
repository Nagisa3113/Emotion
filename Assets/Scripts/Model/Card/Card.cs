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



    Iron,  //坚强
    HoldOn,//硬撑
    FightBack,//反击
    JianRenBuBa,//坚韧不拔
    Lesson,//教训
    Accelerate, //加速
    Reinforce,//加强
    Increase,  //改进

    Reconcile,//调和
    Suppress,//压制
    Transfer,//转化
    Feed,//补给
    Trick,//戏法

}






[System.Serializable]
public class Card
{
    [HideInInspector]
    public string cardname;

    GameObject image;

    protected CardName name;
    public CardName Name
    {
        get
        {
            return name;
        }
    }


    [SerializeField]
    int cost;
    public int Cost
    {
        get
        {
            return cost;
        }

        set
        {
            cost = value;
            if (cost < 0)
            {
                Debug.Log("Cost<0");
            }
        }
    }




    CardColor color;
    public CardColor Color
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

    protected  string normalStr;
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


    public Card()
    {
        this.cardname = "null";
    }

    protected int upgrade;
    protected int upgradeTwice;



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

    public virtual void TakeEffect(Role self, Role target)
    {

    }


    public static void AddCard(List<Card> cards,CardName cardName,int num)
    {
        //使用反射创建对象
        Type type = Type.GetType(cardName.ToString());
        object obj = Activator.CreateInstance(type, true);

        if (obj == null)
        {
            Debug.Log("No Card for" + cardName.ToString());
        }

        for (int i=0;i<num;i++)
        {
            cards.Add((Card)obj);

        }
    }

    public static Card NewCard(CardName cardName)
    {
        //使用反射创建对象
        Type type = Type.GetType(cardName.ToString());   
           object obj = Activator.CreateInstance(type, true) ;   
             if (obj == null)
              {            
                  Debug.Log("No Card for" + cardName.ToString());
                          return null;       }
                    return (Card)obj;
    }


}
