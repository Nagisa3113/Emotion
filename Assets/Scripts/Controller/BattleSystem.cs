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


    [Header("谁的回合")]
    [SerializeField]
    RoundTurn roundTurn;

    [Header("回合数")]
    public int roundNum;

    [Header("战斗阶段")]
    public BattleStatus battleStatus;

    [Header("回合阶段")]
    public RoundStatus roundStatus;


    /// <summary>
    /// 用来检测敌人出牌
    /// </summary>
    int flag = 0;

    public void Awake()
    {

        roundNum = 1;
        battleStatus = BattleStatus.BattleBegin;
        BattleInit();

    }

    public void Start()
    {
        BattleStart();
    }


    public void Update()
    {
        //if (battleStatus == BattleStatus.PlayerPause)
        //{

        //}
        if (battleStatus == BattleStatus.Batttling)
        {
            if (roundStatus == RoundStatus.Rounding)
            {
                Rounding();
            }
        }
        //else
        //{
        //    BattleEnd();
        //}
    }

    public void BattleInit()
    {

        player = Player.GetInstance();
        player.InitLibrary();

        enemy = new Enemy(EnemyType.Normal);
        enemy.InitLibrary();

        //player.PropReduceCD();

        //player.PropEffect();

        player.GetCardManager.ExpenseReset();

        player.GetCardManager.SelectCard();


    }


    public void BattleStart()
    {
        player.GetCardsFromLibrary(6);

        enemy.GetCardsFromLibrary(6);

        roundTurn = RoundTurn.PlayerRound;

        battleStatus = BattleStatus.Batttling;

        RoundBegin();
    }



    void RoundBegin()
    {
        switch (roundTurn)
        {
            case RoundTurn.PlayerRound:

                player.GetCardManager.ExpenseReset();

                if (player.GetCardManager.CanAddCard)
                {
                    Debug.Log("player get 3 cards");
                    player.GetCardsFromLibrary(3);
                }

                break;

            case RoundTurn.EnemyRound:
                enemy.GetCardManager.ExpenseReset();

                if (enemy.GetCardManager.CanAddCard)
                {
                    Debug.Log("enemy get 3 cards");
                    enemy.GetCardsFromLibrary(3);
                }

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

                if (flag == 0)
                {
                    StartCoroutine("EnemyPutCard");
                }

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


    public void ChangeRoundStatus(RoundStatus nextRoundStatus)
    {
        //Debug.Log("从" + roundStatus.ToString() + "到" + nextRoundStatus.ToString());
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
                Debug.Log("轮到" + roundTurn.ToString() + "的回合");
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

    IEnumerator EnemyPutCard()
    {
        flag = 1;
        GameObject.Find("View").GetComponent<View>().canPutCard = false;
        GameObject.Find("ViewOfButton").GetComponent<ViewOfButton>().endButton.enabled = false;
        while ((enemy.GetCardManager as EnemyCardManager).CheckCanPutCard())
        {

            for (float timer = 0; timer <= 2; timer += Time.deltaTime)
            {
                yield return 0;
            }
            enemy.PutCurrentCard(player);
        }
        ChangeRoundStatus(RoundStatus.RoundEnd);
        GameObject.Find("ViewOfButton").GetComponent<ViewOfButton>().endButton.enabled = true;
        GameObject.Find("View").GetComponent<View>().canPutCard = true;
        flag = 0;
    }

}


