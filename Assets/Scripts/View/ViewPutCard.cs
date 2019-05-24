using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewPutCard : MonoBehaviour
{
    private View view;
    public GameObject showCard;
    void Start()
    {
        view = View.GetInstance();
        showCard = GameObject.Find("View").transform.GetChild(0).gameObject;
    }
    void OnMouseDown()
    {
        foreach (var prefab in view.deskCards)
        {
            if (transform.name == prefab.name)
            {
                showCard.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                break;
            }
        }
        showCard.SetActive(true);
    }

    void OnMouseUp()
    {
      showCard.SetActive(false);  
    }
}
