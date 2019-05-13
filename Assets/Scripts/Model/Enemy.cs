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
    EnemyAI AI;

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
        GetCardManager = new EnemyCardManager();
        AI = new EnemyAI();
    }
    public EnemyAI GetAI
    {
        get
        {
            return AI;
        }
    }


}
