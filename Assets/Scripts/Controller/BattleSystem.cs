using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BattleStatus
{
    BattleBegin,
    Batttling,
    PlayerWin,
    PlayerLose,
    PlayerPause,
}

public enum RoundStatus
{
    RoundBegin,
    Rounding,
    RoundEnd,
}

public enum RoundTurn
{
    PlayerRound,
    EnemyRound,
}


public class BattleSystem : MonoBehaviour
{
    [Header("玩家")]
    [SerializeField]
    Player player;

    [Header("敌人")]
    [SerializeField]
    Enemy enemy;


    public Enemy GetEnemy()
    {
        return enemy;
    }


    [Header("牌库")]
    [SerializeField]
    CardLibrary cardLibrary;

    [Header("弃牌")]
    [SerializeField]
    CardDiscard cardDiscard;

    [Header("谁的回合")]
    [SerializeField]
    RoundTurn roundTurn;

    [Header("回合数")]
    public int roundNum;

    [Header("战斗阶段")]
    public BattleStatus battleStatus;

    [Header("回合阶段")]
    public RoundStatus roundStatus;

    public void Awake()
    {
        
        roundNum = 1;
        battleStatus = BattleStatus.BattleBegin;
        BattleStart();

    }

    public void Update()
    {
        //if (battleStatus == BattleStatus.PlayerPause)
        //{

        //}
        if (battleStatus == BattleStatus.Batttling)
        {
            if(roundStatus==RoundStatus.Rounding)
            {
                Rounding();
            }
        }
        //else
        //{
        //    BattleEnd();
        //}
    }

    public void BattleStart()
    {

        cardLibrary = CardLibrary.GetInstance();
        cardDiscard = CardDiscard.GetInstance();

        player = Player.GetInstance();
        enemy = new Enemy(EnemyType.Normal);

        CardLibrary.GetInstance().InitLibrary();

        player.GetCardManager.GetCardsFromLibrary(9);

        enemy.GetCardManager.GetCardsFromLibrary(6);

        //player.PropReduceCD();

        //player.PropEffect();

        player.GetCardManager.ExpenseReset();

        player.GetCardManager.SelectCard();

        roundTurn = RoundTurn.PlayerRound;

        battleStatus = BattleStatus.Batttling;


        RoundBegin();

    }

    void RoundBegin()
    {
        switch (roundTurn)
        {
            case RoundTurn.PlayerRound:


                player.GetCardFromLibrary(3);
                player.GetCardManager.ExpenseReset();



                break;

            case RoundTurn.EnemyRound:
                enemy.GetCardManager.ExpenseReset();
                enemy.GetCardManager.GetCardsFromLibrary(3);

                //enemy.BuffReduceLayer();

                //enemy.BuffProcess();

                break;
        }

        ChangeRoundStatus(RoundStatus.Rounding);
    }


    void Rounding()
    {


        switch (roundTurn)
        {
            case RoundTurn.PlayerRound:
                //InputMouse();
                InputHandle();
                break;


            case RoundTurn.EnemyRound:
                enemy.GetCardManager.AI(enemy,player);
                ChangeRoundStatus(RoundStatus.RoundEnd);
                break;

        }



    }



    void BattleEnd()
    {
        switch (battleStatus)
        {
            case BattleStatus.PlayerLose:

                //so something

                break;
            case BattleStatus.PlayerWin:

                //do something

                break;
        }
    }


    void ChangeRoundStatus(RoundStatus nextRoundStatus)
    {
        switch (nextRoundStatus)
        {
            case RoundStatus.RoundEnd:


                roundStatus = RoundStatus.RoundEnd;

                if (roundTurn == RoundTurn.EnemyRound)
                {
                    enemy.BuffReduce();
                    roundTurn = RoundTurn.PlayerRound;
                    roundNum++;
                }
                else
                {
                    player.BuffReduce();
                    roundTurn = RoundTurn.EnemyRound;
                }

                ChangeRoundStatus(RoundStatus.RoundBegin);
                break;


            case RoundStatus.RoundBegin:
                roundStatus = RoundStatus.RoundBegin;
                RoundBegin();
                break;

            case RoundStatus.Rounding:
                roundStatus = RoundStatus.Rounding;
                break;
        }


    }


 



    void InputHandle()
    {
        if (Input.GetKeyDown(KeyCode.D))
        {
            player.GetCardManager.MoveSelectedCard(1);
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            player.GetCardManager.MoveSelectedCard(-1);
        }

        if (Input.GetKeyDown(KeyCode.K))
        {

            player.PutCurrentCard(enemy);

            if (!player.Alive)
            {
                battleStatus = BattleStatus.PlayerLose;
            }

            if (!enemy.Alive)
            {
                battleStatus = BattleStatus.PlayerWin;
            }

        }

        if (Input.GetKeyDown(KeyCode.J))
        {

            ChangeRoundStatus(RoundStatus.RoundEnd);
            return;
        }
    }


    void InputMouse()
    {
        // 按鼠标左
         
    }

}


