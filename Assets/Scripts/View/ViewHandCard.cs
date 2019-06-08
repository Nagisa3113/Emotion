using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ViewHandCard : MonoBehaviour
{
    // Start is called before the first frame update

    private View view;
    public GameObject showCard;
    TextMesh text;
    TextMesh bonus;
    float time = 0;
    Player player;
    Enemy enemy;
    int condition ; 
    

    void Awake()
    {
        view = View.GetInstance();
        player = Player.GetInstance();
        enemy = GameObject.Find("Battle").GetComponent<BattleSystem>().GetEnemy();
        showCard = GameObject.Find("View").transform.GetChild(0).gameObject;
        text = showCard.transform.GetChild(0).gameObject.GetComponent<TextMesh>();
        bonus = showCard.transform.GetChild(1).gameObject.GetComponent<TextMesh>();
        time = Time.time;
        condition = 0;
    }
    void OnEnable()
    {
        condition = 0;
    }

    void OnMouseEnter()
    {
        foreach (var prefab in view.deskCards)
        {
            if (transform.name== prefab.name)
            {
                transform.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                transform.GetChild(2).position -=new Vector3(0,0.3f,0);
                for (int i=0;i < transform.GetSiblingIndex();i++)
                {
                    view.playerCards.transform.GetChild(i).position -= new Vector3(0.7f,0,0);
                }
                for( int i =transform.GetSiblingIndex()+1;i <view.playerCards.transform.childCount;i++)
                {
                    view.playerCards.transform.GetChild(i).position += new Vector3(0.8f,0,0);
                }
                 transform.position = transform.position - new Vector3(0, 0, 2f);
                break;
            }
        }

          condition = 1;
    }

    void OnMouseExit()
    {
        if(condition == 1 )
        {
            foreach (var prefab in view.handCards)
            {
                if (transform.name == prefab.name)
                {
                    transform.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                    transform.GetChild(2).position +=new Vector3(0,0.3f,0);
                    for (int i=0;i < transform.GetSiblingIndex();i++)
                    {
                        view.playerCards.transform.GetChild(i).position += new Vector3(0.7f,0,0);
                    }
                    for( int i =transform.GetSiblingIndex()+1;i <view.playerCards.transform.childCount;i++)
                    {
                    view.playerCards.transform.GetChild(i).position -= new Vector3(0.8f,0,0);
                    }
                     transform.position = transform.position + new Vector3(0, 0, 2f);
                    break;
                }
            }
            condition = 0;
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
                bonus.text = "12";
                break;
            }
        }
        showCard.transform.GetChild(1).gameObject.GetComponent<TextMesh>().text = player.CardManager.GetBonus((CardName)Enum.Parse(typeof(CardName), transform.name)).ToString();;
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
