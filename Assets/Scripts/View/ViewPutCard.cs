using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class ViewPutCard : MonoBehaviour
{
    public float mTime;
    private Vector3 endPos;


    Vector3 bonusPosition;

    Vector3 bigSize;
    Vector3 normalSize;
    GameObject showCard;
     
    void Start () 
    {
        mTime = 0.3f;
        endPos =new Vector3(0f,0,0);
        showCard = GameObject.Find("View").transform.GetChild(0).gameObject;
        bigSize = new Vector3(1.2f,1.2f,1);
        normalSize = new Vector3(1,1,1);
        
    }

    void  OnEnable()
    {
        transform.GetChild(2).gameObject.GetComponent<TextMesh>().text = "";
    }

        //开始前转
    public void StartShow(int bonus)
    {
        transform.GetChild(2).gameObject.GetComponent<TextMesh>().text = bonus.ToString();
        StartCoroutine(ToShow());  
    }
     
    void OnMouseDown()
    {
        foreach (var prefab in View.GetInstance().deskCards)
        {
            if (transform.name == prefab.name)
            {
                showCard.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                break;
            }
        }
        showCard.transform.GetChild(0).gameObject.GetComponent<TextMesh>().text = 
            transform.GetChild(2).gameObject.GetComponent<TextMesh>().text;
        showCard.SetActive(true);
    }

    IEnumerator ToShow()
    {
        Vector3 interval = new Vector3(0.3f, 0, -0.01f);
        Vector3 startPosition = new Vector3(-4f, 0f, 0);
        int x =View. GetInstance().cardTombs.transform.childCount; 
        var dur = 0.0f; 
        Vector3 beginPos = transform.position;
        while (dur <= mTime)
        { 
            dur += Time.deltaTime; 
            transform.position = Vector3.Lerp(beginPos, endPos, dur / mTime); 
            yield return 0; 
        }
        dur = 0;

        while (dur <= mTime)
        {
            transform.localScale = Vector3.Lerp(normalSize, bigSize, dur / mTime); 
            dur += Time.deltaTime; 
            yield return 0; 
        }
        dur = 0;

        while (dur <= mTime)
        {
            transform.localScale = Vector3.Lerp(bigSize, normalSize, dur / mTime); 
            dur += Time.deltaTime; 
            yield return 0; 
        }
        dur = 0;

        while (dur <= mTime)
        { 
            dur += Time.deltaTime; 
            transform.position = Vector3.Lerp(endPos, startPosition + interval*(x-1), dur / mTime); 
            yield return null; 
        }
        
        //Destroy(transform.gameObject);
    }


}
