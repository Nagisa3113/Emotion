using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ViewOfHealth : MonoBehaviour
{
    private Text healthOfPlayer;
	private Text healtheOfEnemy;


    void Start()
    {
        
    }
    public void MouseEnter()
    {
        if(transform.name == "player")
        {
            healthOfPlayer = transform.GetChild(0).gameObject.GetComponent<Text>();
            healthOfPlayer.text =  Player.GetInstance().HP.ToString()+'/'+Player.GetInstance().HPMax.ToString();
        }
        if(transform.name == "enemy")
        {
            healtheOfEnemy = transform.GetChild(0).gameObject.GetComponent<Text>();
            healtheOfEnemy.text = View.GetInstance().enemy.HP.ToString() + '/' + View.GetInstance().enemy.HPMax.ToString();
        }
    }

    public void OnPonitExit()
    {
       if(transform.name == "player")
        {
            healthOfPlayer = transform.GetChild(0).gameObject.GetComponent<Text>();
            healthOfPlayer.text = "";
        }
        if(transform.name == "enemy")
        {
            healtheOfEnemy = transform.GetChild(0).gameObject.GetComponent<Text>();
            healtheOfEnemy.text =  "";
        }
    }

}
