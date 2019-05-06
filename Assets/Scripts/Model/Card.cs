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
    CardName name;
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



    public Card()
    {
        name = CardName.Empty;
    }


    public Card(CardName name)
    {

        this.name = name;

        switch (name)
        {

            case CardName.Empty:
                break;



            case CardName.Anger:
                this.color = CardColor.Red;
                this.cost = 0;
                break;

            case CardName.AngerFire:
                this.color = CardColor.Red;
                this.cost = 0;
                break;

            case CardName.NoNameFire:
                this.color = CardColor.Red;
                this.cost = 0;
                break;

            case CardName.Vent:
                this.color = CardColor.Red;
                this.cost = 4;
                break;

            case CardName.Incite:
                this.color = CardColor.Red;
                this.cost = 1;
                break;

            case CardName.Revenge:
                this.color = CardColor.Red;
                this.cost = 2;
                break;


            

            case CardName.Complain:
                this.color = CardColor.Purple;
                this.cost = 1;
                break;

            case CardName.DullAtmosphere:
                this.color = CardColor.Purple;
                this.cost = 2;
                break;


            case CardName.WeiYuChouMou:
                this.color = CardColor.Purple;
                this.cost = 1;
                break;

            case CardName.OuDuanSiLian:
                this.color = CardColor.Purple;
                this.cost = 1;
                break;


            case CardName.Confess:
                this.color = CardColor.Purple;
                this.cost = 4;
                break;






            case CardName.Heal:
                this.color = CardColor.Green;
                this.cost = 1;
                break;

            case CardName.Comfort:
                this.color = CardColor.Green;
                this.cost = 1;
                break;


        }


    }


}
