using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum CardColor
{
    Gray,
    Green,
    Purple,
    Red,
    Yellow,
    Colorless,
}


public enum CardName
{
    RedStart = 0,
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
    RedEnd,


    PurpleStart = 20,
    Complain,//抱怨
    DullAtmosphere,//沉闷氛围
    Confess,//倾诉
    WeiYuChouMou,//未雨绸缪
    OuDuanSiLian,//藕断丝连
    Depress,//忧郁
    Blues,//蓝调
    Pacify,//安抚
    Compare,//攀比
    PurpleEnd,


    GreenStart = 40,
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
    GreenEnd,

    GrayStart = 60,
    Iron,  //坚强
    HoldOn,//硬撑
    FightBack,//反击
    JianRenBuBa,//坚韧不拔
    Lesson,//教训
    Accelerate, //加速
    Reinforce,//加强
    Increase,  //改进
    GrayEnd,

    YellowStart = 80,
    Reconcile,//调和
    Suppress,//压制
    Transfer,//转化
    Feed,//补给
    Trick,//戏法
    YellowEnd,


    Impact,//冲击
    Blow,//打击
    Thump,//重击

    Annoy,//惹恼
    Charge,//蓄力
    Prolifer,//增殖
    Weaken,//弱化
    Stuns,//击晕

}


[System.Serializable]
public class Card
{
    public Card()
    {
        this.cardname = "null";
    }
    public Card(CardName cardName, CardColor color, int cost)
    {
        this.name = cardName;
        this.color = color;
        this.upgrade = 999;
        this.upgradeTwice = 999;
        this.cost = cost;
        this.cardname = this.name.ToString();

        this.costMax = cost;
        this.tip = "tip";
    }
    public Card(CardName cardName, CardColor color, int cost, int upgrade)
    {
        this.name = cardName;
        this.color = color;
        this.upgrade = upgrade;
        this.upgradeTwice = 999;
        this.cost = cost;
        this.cardname = this.name.ToString();

        this.costMax = cost;
        this.tip = "tiptip";
    }
    public Card(CardName cardName, CardColor color, int cost, int upgrade, int upgradeTwice)
    {
        this.name = cardName;
        this.color = color;
        this.upgrade = upgrade;
        this.upgradeTwice = upgradeTwice;
        this.cost = cost;
        this.cardname = this.name.ToString();

        this.costMax = cost;
        this.tip = "tiptiptip";
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

    [HideInInspector]
    public string cardname;
    protected CardName name;
    protected string cName;
    public CardName Name
    {
        get { return name; }
    }

    public string CName
    {
        get { return cName; }
    }

    protected CardColor color;
    public CardColor Color
    {
        get { return color; }
    }


    [SerializeField]
    int cost;
    public int Cost
    {
        get { return cost; }
        set
        {
            cost = value;
            if (cost < 0)
            {
                Debug.Log("Cost<0");
            }
        }
    }

    int costMax;
    public int CostMax
    {
        get { return costMax; }
        set
        {
            costMax = value;
            if (costMax < 0)
            {
                Debug.Log("costMax<0");
            }
        }
    }

    protected int oDamage;
    protected int bDamage;

    protected bool isVice; //是否为副卡 

    public bool IsVice
    {
        get { return isVice; }
        set { isVice = value; }
    }
    
    protected int upgrade;
    protected int upgradeTwice;
    public int Upgrade
    {
        get { return upgrade; }
    }
    public int UpgradeTwice
    {
        get
        { return upgradeTwice; }
    }

    protected string tip;
    protected string tipUpgrade;
    protected string tipUpgradeTwice;
    public string Tip(Role self)
    {
        string temp = ProcessStr(tip, self);
        return temp;
    }
    public string Tip()
    {
        string temp = ProcessStr(tip);
        return temp;
    }
    public string TipUpgrade(Role self)
    {
        string temp = ProcessStr(tipUpgrade, self);
        return temp;
    }
    public string TipUpgradeTwice(Role self)
    {
        string temp = ProcessStr(tipUpgradeTwice, self);
        return temp;
    }
    string ProcessStr(string str, Role self)
    {
        string temp = "";
        string temp1 = "";
        int a;
        for (int i = 0; i < str.Length; i++)
        {
            if (str.Substring(i, 1) == "*")
            {
                for (int j = i; j < str.Length; j++)
                {
                    if (int.TryParse(str.Substring(j, 1), out a))
                    {
                        if (int.TryParse(str.Substring(j + 1, 1), out a))
                        {
                            bDamage = 10;
                        }
                        else
                        {
                            bDamage = int.Parse((str.Substring(j, 1)));
                        }
                    }

                }
                temp1 += (oDamage + bDamage * self.CardManager.GetBonus(color)).ToString();
            }
            else
            {
                temp1 += str.Substring(i, 1);
            }

        }
        for (int i = 0; i < temp1.Length; i++)
        {
            if ((i + 1) % 13 == 0)
            {
                temp += temp1.Substring(i, 1) + "\n";
            }
            else
            {
                temp += temp1.Substring(i, 1);
            }
        }
        return temp;
    }

    string ProcessStr(string str)
    {
        string temp = "";
        string temp1 = "";
        int a;
        for (int i = 0; i < str.Length; i++)
        {
            if (str.Substring(i, 1) == "*")
            {
                for (int j = i; j < str.Length; j++)
                {
                    if (int.TryParse(str.Substring(j, 1), out a))
                    {
                        if (int.TryParse(str.Substring(j + 1, 1), out a))
                        {
                            bDamage = 10;
                        }
                        else
                        {
                            bDamage = int.Parse((str.Substring(j, 1)));
                        }
                    }

                }
                temp1 += (oDamage).ToString();
            }
            else
            {
                temp1 += str.Substring(i, 1);
            }

        }
        for (int i = 0; i < temp1.Length; i++)
        {
            if ((i + 1) % 13 == 0)
            {
                temp += temp1.Substring(i, 1) + "\n";
            }
            else
            {
                temp += temp1.Substring(i, 1);
            }
        }
        return temp;
    }

    public static void AddCard(List<Card> cards, CardName cardName, int num)
    {
        //使用反射创建对象
        Type type = Type.GetType(cardName.ToString());
        object obj = Activator.CreateInstance(type, true);

        if (obj == null)
        {
            Debug.Log("No Card for" + cardName.ToString());
        }

        for (int i = 0; i < num; i++)
        {
            cards.Add((Card)obj);

        }
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
    }

    public static Card NewCard(CardName cardName, bool isVice)
    {
        //使用反射创建对象
        Type type = Type.GetType(cardName.ToString());
        object obj = Activator.CreateInstance(type, true);
        if (obj == null)
        {
            Debug.Log("No Card for" + cardName.ToString());
            return null;
        }
        (obj as Card).IsVice = isVice;
        return (Card)obj;
    }

    public virtual void TakeEffect(Role self, Role target)
    {

    }

    public static Card GetRandomCard(CardColor cardColor)
    {
        int i;
        int start, end;

        start = (int)(CardName)Enum.Parse(typeof(CardName), cardColor.ToString() + "Start");
        end = (int)(CardName)Enum.Parse(typeof(CardName), cardColor.ToString() + "End");

        i = UnityEngine.Random.Range(start + 1, end);

        Card card = Card.NewCard((CardName)i);
        return card;
    }

    public static Card[] GetRandomCards(CardColor cardColor, int num)
    {
        Card[] cards = new Card[num];

        for (int i = 0; i < num; i++)
        {
            cards[i] = Card.GetRandomCard(cardColor);
        }
        return cards;
    }


}
