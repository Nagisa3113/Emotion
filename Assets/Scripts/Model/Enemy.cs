﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum EnemyType
{
    Normal,
    Boss,
}


[System.Serializable]
public class Enemy : Role
{
    EnemyType enemyType;


    public EnemyType GetEnemyType
    {
        get
        {
            return enemyType;
        }
    }

    public Enemy(EnemyType enemyType) : base(600, 10)
    {
        this.enemyType = enemyType;
        cardManager = new EnemyCardManager();
    }

    public override void PutCurrentCard(Role target)
    {
        cardManager.PutCurrentCard(this, target);
    }


}
