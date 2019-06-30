using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ViewPutCard : MonoBehaviour
{
    public GameObject showCard;
    TextMesh text;
    void Start()
    {
        showCard = GameObject.Find("View").transform.GetChild(0).gameObject;
        text = showCard.transform.GetChild(0).gameObject.GetComponent<TextMesh>();
    }
    void OnMouseDown()
    {
        foreach (var prefab in View.GetInstance().deskCards)
        {
            if (transform.name == prefab.name)
            {
                showCard.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                text.text = Card.NewCard((CardName)Enum.Parse(typeof(CardName), transform.name)).GetNormalStr;
                break;
            }
        }
        showCard.SetActive(true);
    }

    
}
