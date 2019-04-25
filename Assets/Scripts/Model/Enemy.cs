using System.Collections;
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

    public Enemy(EnemyType enemyType) : base(200, 10)
    {
        this.enemyType = enemyType;
    }

    public override void PutCard(Role target)
    {
        cardManager.PutCardRandom(this, target);
    }


    public override void GetCard()
    {
        cardManager.GetCardsRandom(3);
    }
}
