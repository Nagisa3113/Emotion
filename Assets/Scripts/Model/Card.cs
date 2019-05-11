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


    int upgrade;
    public int GetUpgrade
    {
        get
        {
            return upgrade;
        }
    }
    int upgradeTwice;
    public int GetUpgradeTwice
    {
        get
        {
            return upgradeTwice;
        }
    }


    public CardName GetName      //返回联合值

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
                this.upgrade = 5;
                this.cost = 0;
                break;

            case CardName.AngerFire:
                this.color = CardColor.Red;
                this.upgrade = 5;
                this.cost = 0;

                break;

            case CardName.NoNameFire:
                this.color = CardColor.Red;
                this.upgrade = 3;
                this.cost = 0;
                break;

            case CardName.Vent:
                this.color = CardColor.Red;
                this.upgrade = 5;
                this.cost = 4;
                break;

            case CardName.Incite:
                this.color = CardColor.Red;
                this.upgrade = 5;
                this.upgradeTwice = 10;
                this.cost = 1;
                break;

            case CardName.Revenge:
                this.color = CardColor.Red;
                this.upgrade = 3;
                this.upgradeTwice = 6;
                this.cost = 2;
                break;




            case CardName.Complain:
                this.color = CardColor.Purple;
                this.upgrade = 4;
                this.cost = 1;
                break;

            case CardName.DullAtmosphere:
                this.color = CardColor.Purple;
                this.upgrade = 4;
                this.upgradeTwice = 10;
                this.cost = 2;
                break;


            case CardName.WeiYuChouMou:
                this.color = CardColor.Purple;
                this.upgrade = 3;
                this.upgradeTwice = 8;
                this.cost = 1;
                break;

            case CardName.OuDuanSiLian:
                this.color = CardColor.Purple;
                this.upgrade = 4;
                this.cost = 1;
                break;


            case CardName.Confess:
                this.color = CardColor.Purple;
                this.upgrade = 10;
                this.cost = 4;
                break;





            case CardName.Heal:
                this.color = CardColor.Green;
                this.upgrade = 6;
                this.cost = 1;
                break;

            case CardName.Comfort:
                this.color = CardColor.Green;
                this.upgrade = 4;
                this.upgradeTwice = 8;
                this.cost = 1;
                break;


        }


    }







}
