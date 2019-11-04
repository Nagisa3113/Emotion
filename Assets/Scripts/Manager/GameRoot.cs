using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameRoot : MonoBehaviour
{
    static bool ifStart = false;

    void Awake()
    {
        if (ifStart == false)
        {
            DontDestroyOnLoad(this);

            GameResources. s_cardBack = Resources.LoadAll<Sprite>("Sprites/card_back");
            GameResources.s_cardFront = Resources.LoadAll<Sprite>("Sprites/card_front");
            GameResources.s_expenseColor = Resources.LoadAll<Sprite>("Sprites/card_expense");
            GameResources.s_upgradeColor = Resources.LoadAll<Sprite>("Sprites/upgradeColor");
            GameResources.s_upgradeTwiceColor = Resources.LoadAll<Sprite>("Sprites/upgradeColor");
            GameResources.cardFront = Resources.Load<GameObject>("Prefabs/CardFront");
            GameResources.cardBack = Resources.Load<GameObject>("Prefabs/CardBack");
            GameResources.buff = Resources.Load<GameObject>("Prefabs/Buff");
            ifStart = true;
        }
    }
}
