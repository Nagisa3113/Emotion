using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ViewHandCard : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject showCard;
    TextMesh text;
    TextMesh bonus;
    float time = 0;
    Vector3 bonusPosition;
    int condition ; 
    

    void Awake()
    {
        showCard = GameObject.Find("View").transform.GetChild(0).gameObject;
        text = showCard.transform.GetChild(0).gameObject.GetComponent<TextMesh>();
        bonus = showCard.transform.GetChild(1).gameObject.GetComponent<TextMesh>();
        bonusPosition = transform.GetChild(2).position - transform.position;
        time = Time.time;
        condition = 0;
    }
    void OnEnable()
    {
        condition = 0;
        time = Time.time;
        transform.GetChild(2).position = bonusPosition + transform .position;
    }

    void OnMouseEnter()
    {
        if(condition == 0)
        {
            foreach (var prefab in View.GetInstance().deskCards)
            {
            if (transform.name == prefab.name)
            {
                transform.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                transform.GetChild(2).position -=new Vector3(0,0.3f,0);
                
                for (int i=0;i < transform.GetSiblingIndex();i++)
                {
                    View.GetInstance().playerCards.transform.GetChild(i).position -= new Vector3(0.7f,0,0);
                }
                for( int i =transform.GetSiblingIndex()+1;i <View.GetInstance().playerCards.transform.childCount;i++)
                {
                    View.GetInstance().playerCards.transform.GetChild(i).position += new Vector3(0.8f,0,0);
                }
                transform.position = transform.position - new Vector3(0, 0, 2f);
                break;
            }
            }
            condition = 1;
        }

         
    }

    void OnMouseExit()
    {
        if(condition == 1 )
        {
            foreach (var prefab in View.GetInstance().handCards)
            {
                if (transform.name == prefab.name)
                {
                    transform.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                    transform.GetChild(2).position +=new Vector3(0,0.3f,0);
                    for (int i=0;i < transform.GetSiblingIndex();i++)
                    {
                        View.GetInstance().playerCards.transform.GetChild(i).position += new Vector3(0.7f,0,0);
                    }
                    for( int i =transform.GetSiblingIndex()+1;i <View.GetInstance().playerCards.transform.childCount;i++)
                    {
                    View.GetInstance().playerCards.transform.GetChild(i).position -= new Vector3(0.8f,0,0);
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
        foreach (var prefab in View.GetInstance().deskCards)
        {
            if (transform.name == prefab.name)
            {
                showCard.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                //text.text = Card.NewCard((CardName)Enum.Parse(typeof(CardName), transform.name)).GetNormalStr;
                bonus.text = "12";
                break;
            }
        }
        showCard.transform.GetChild(1).gameObject.GetComponent<TextMesh>().text =
            Player.GetInstance().CardManager.GetBonus((CardName)Enum.Parse(typeof(CardName), transform.name)).ToString();;
        showCard.SetActive(true);

        //当第二次点击鼠标，且时间间隔满足要求时双击鼠标
        if (Time.time - time <= 0.3f && View.GetInstance().canPutCard)
        {
            int index = transform.GetSiblingIndex();
            Player.GetInstance().PutSelectCard(View.GetInstance().enemy, index);
            showCard.SetActive(false);
        }
        time = Time.time;
    }
    void OnMouseUp()
    {
        //showCard.SetActive(false);  
    }

    



}
