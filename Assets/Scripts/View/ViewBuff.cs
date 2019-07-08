using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ViewBuff : MonoBehaviour
{
    GameObject layer;
    GameObject tip;
    void Start()
    {
        layer = transform.GetChild(0).gameObject;
        tip = transform.GetChild(1).gameObject;
    }
    void Update()
    {
        int temp;
        temp =  View.GetInstance().player.GetBuffManager.CheckLayer( transform.name);
        layer.SetActive(true);
        layer.GetComponent<TextMesh>().text =temp.ToString();
    }

    void OnMouseEnter()
    {
        string temp;
        temp =  View.GetInstance().player.GetBuffManager.CheckName( transform.name);
        tip.SetActive(true);
        tip.GetComponent<TextMesh>().text =temp.ToString();
    }

    void OnMouseExit()
    {
        tip.SetActive(false);
    }
}
