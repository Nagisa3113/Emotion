﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System;

public class ViewOfHealth : MonoBehaviour
{
    private Text healthOfPlayer;
	private Text healtheOfEnemy;
    private Player player;
    private Enemy enemy;

    void Start()
    {
        enemy =  GameObject.Find("Battle").GetComponent<BattleSystem>().GetEnemy();
        player = Player.GetInstance();
        
    }
    public void MouseEnter()
    {
        if(transform.name == "player")
        {
            healthOfPlayer = transform.GetChild(0).gameObject.GetComponent<Text>();
            healthOfPlayer.text =  player.GetHP.ToString()+'/'+player.GetHPMax.ToString();
        }
        if(transform.name == "enemy")
        {
            healtheOfEnemy = transform.GetChild(0).gameObject.GetComponent<Text>();
            healtheOfEnemy.text =  enemy.GetHP.ToString()+'/'+enemy.GetHPMax.ToString();
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