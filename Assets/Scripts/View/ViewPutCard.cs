using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ViewPutCard : MonoBehaviour
{
    private View view;
    public GameObject showCard;
    TextMesh text;
    void Start()
    {
        view = View.GetInstance();
        showCard = GameObject.Find("View").transform.GetChild(0).gameObject;
        text = showCard.transform.GetChild(0).gameObject.GetComponent<TextMesh>();
    }
    void OnMouseDown()
    {
        foreach (var prefab in view.deskCards)
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
