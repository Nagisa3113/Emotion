using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ViewBuff : MonoBehaviour
{
    GameObject layer;
    GameObject tip;
    GameObject back;
    void Start()
    {
        layer = transform.GetChild(0).gameObject;
        tip = transform.GetChild(1).gameObject;
        back = transform.GetChild(2).gameObject;
    }
    void Update()
    {
        int temp;
        if(transform.parent.name == "PlayerBuffs")
        {
            temp =  View.GetInstance().player.GetBuffManager.CheckLayer( transform.name);
        }
        else
        {
            temp =  View.GetInstance().enemy.GetBuffManager.CheckLayer( transform.name);
        }
        
        layer.SetActive(true);
        layer.GetComponent<TextMesh>().text =temp.ToString();
        
    }

    void OnMouseEnter()
    {
        string temp;
        if(transform.parent.name == "PlayerBuffs")
        {
            temp =  View.GetInstance().player.GetBuffManager.CheckTip( transform.name);
        }
        else
        {
            temp =  View.GetInstance().enemy.GetBuffManager.CheckTip( transform.name);
        }
        tip.SetActive(true);
        tip.GetComponent<TextMesh>().text =temp.ToString();
        back.SetActive(true);
        back.transform.localScale = new Vector3(0.1F, 0, 0) *temp.ToString().Length + new Vector3(0.1F, 0.4f, 0);
    }

    void OnMouseExit()
    {
        tip.SetActive(false);
        back.SetActive(false);
    }
}
