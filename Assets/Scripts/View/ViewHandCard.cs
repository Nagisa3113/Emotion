﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ViewHandCard : MonoBehaviour
{
    // Start is called before the first frame update

    private View view;
    public GameObject showCard;
    TextMesh text;
    float time = 0;
    Player player;
    Enemy enemy;
    

    void Awake()
    {
        view = View.GetInstance();
        player = Player.GetInstance();
        enemy = GameObject.Find("Battle").GetComponent<BattleSystem>().GetEnemy();
        showCard = GameObject.Find("View").transform.GetChild(0).gameObject;
        text = showCard.transform.GetChild(0).gameObject.GetComponent<TextMesh>();
        time = Time.time;
    }

    void OnMouseEnter()
    {
        foreach (var prefab in view.deskCards)
        {
            if (transform.name== prefab.name)
            {
                transform.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                for (int i=0;i < transform.GetSiblingIndex();i++)
                {
                    view.playerCards.transform.GetChild(i).position -= new Vector3(0.5f,0,0);
                }
                for( int i =transform.GetSiblingIndex()+1;i <view.playerCards.transform.childCount;i++)
                {
                    view.playerCards.transform.GetChild(i).position += new Vector3(0.7f,0,0);
                }
                transform.position = transform.position - new Vector3(0, 0, 2f);
                break;
            }
        }
    }

    void OnMouseExit()
    {
        foreach (var prefab in view.handCards)
        {
            if (transform.name == prefab.name)
            {
                transform.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                for (int i=0;i < transform.GetSiblingIndex();i++)
                {
                    view.playerCards.transform.GetChild(i).position += new Vector3(0.5f,0,0);
                }
                for( int i =transform.GetSiblingIndex()+1;i <view.playerCards.transform.childCount;i++)
                {
                    view.playerCards.transform.GetChild(i).position -= new Vector3(0.7f,0,0);
                }
                transform.position = transform.position + new Vector3(0, 0, 2f);
                break;
            }
        }
    }

    void OnMouseDown()
    {
        foreach (var prefab in view.deskCards)
        {
            if (transform.name == prefab.name)
            {
                showCard.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                //text.text = Card.NewCard((CardName)Enum.Parse(typeof(CardName), transform.name)).GetNormalStr;
                break;
            }
        }
        showCard.SetActive(true);

        //当第二次点击鼠标，且时间间隔满足要求时双击鼠标
        if (Time.time - time <= 0.3f && view.canPutCard)
        {
            int index = transform.GetSiblingIndex();
            player.PutSelectCard(enemy, index);
            showCard.SetActive(false);
        }
        time = Time.time;
    }
    void OnMouseUp()
    {
        //showCard.SetActive(false);  
    }



}
