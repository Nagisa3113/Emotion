using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class ViewBuff : MonoBehaviour
{
    GameObject layer;
    void Start()
    {
        layer = transform.GetChild(0).gameObject;
    }

    void OnMouseEnter()
    {
        int temp;
        temp =  View.GetInstance().player.GetBuffManager.CheckLayer( transform.name);
        layer.SetActive(true);
        layer.GetComponent<TextMesh>().text =temp.ToString();

    }

    void OnMouseExit()
    {
        layer.SetActive(false);
    }
}
