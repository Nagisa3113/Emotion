using UnityEngine;
using System.Collections;
using System.Collections.Generic;

[System.Serializable]
public class Player : Role
{
    //金钱
    //int gold;

    //道具
    List<Prop> propLibrary;

    List<Prop> props;

    //单例模式
    private static Player player;

    private Player() : base(300, 10)
    {
        propLibrary = new List<Prop>();
        props = new List<Prop>();
        GetCardManager = new PlayerCardManager();
    }

    public static Player GetInstance()
    {
        if (player == null)
        {
            player = new Player();
        }

        return player;
    }

    public void PropReduceCD()
    {
        foreach(Prop prop in props)
        {
            prop.CD--;
        }
    }

    public void PropEffect()
    {
        foreach(Prop prop in props)
        {

        }
    }

    public override void PutCurrentCard(Role target)
    {
        cardManager.PutCurrentCard(this, target);
    }


    public void GetCardFromLibrary(int num)
    {
        cardManager.GetCardsFromLibrary(num);
    }

}

