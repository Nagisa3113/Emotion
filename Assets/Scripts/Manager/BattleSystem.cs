using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BattleSystem : MonoBehaviour
{
    [Header("玩家")]
    [SerializeField]
    Player player;

    [Header("敌人")]
    [SerializeField]
    Enemy enemy;

    public Enemy Enemy
    {
        get { return enemy; }
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

    static BattleSystem m_Instance;
    public static BattleSystem Instance
    {
        get { return m_Instance; }
        set { m_Instance = value; }
    }

    void Awake()
    {
        m_Instance = this;

        player = Player.Instance;
        player.InitLibrary();

        enemy = Enemy.CreateEnemy(GameManager.Instance.EnemyName);
        enemy.InitLibrary();

        battleStatus = BattleStatus.BattleBegin;
    }

    void InputControl()
    {
        if (Input.GetKeyDown(KeyCode.X))
        {
            battleStatus = BattleStatus.PlayerWin;
        }
        if (Input.GetKeyDown(KeyCode.C))
        {
            battleStatus = BattleStatus.PlayerLose;
        }
    }

    void Update()
    {
        InputControl();
        switch (battleStatus)
        {
            case BattleStatus.BattleBegin:
                battleStatus = BattleStatus.BattleBegin;

                player.CardManager.ExpenseReset();
                player.CardManager.SelectCard();
                player.GetCardsFromLibrary(6);

                enemy.GetCardsFromLibrary(6);

                roundStatus = RoundStatus.RoundBegin;
                roundTurn = RoundTurn.PlayerRound;
                battleStatus = BattleStatus.Batttling;

                break;

            case BattleStatus.Batttling:

                battleStatus = enemy.HP <= 0 ? BattleStatus.PlayerWin : battleStatus;
                battleStatus = player.HP <= 0 ? BattleStatus.PlayerLose : battleStatus;

                switch (roundStatus)
                {
                    case RoundStatus.RoundBegin:
                        Debug.Log("轮到" + roundTurn.ToString() + "的回合");
                        switch (roundTurn)
                        {
                            case RoundTurn.PlayerRound:
                                player.CardManager.ExpenseReset();
                                player.GetCardsFromLibrary(3);
                                break;

                            case RoundTurn.EnemyRound:
                                enemy.CardManager.ExpenseReset();
                                enemy.GetCardsFromLibrary(3);
                                break;
                        }
                        roundStatus = RoundStatus.Rounding;
                        break;

                    case RoundStatus.Rounding:
                        switch (roundTurn)
                        {
                            case RoundTurn.PlayerRound:
                                break;
                            case RoundTurn.EnemyRound:
                                if (enemy.isRound == false)
                                {
                                    StartCoroutine("EnemyPutCard");
                                    enemy.isRound = true;
                                }
                                break;
                        }
                        break;

                    case RoundStatus.RoundEnd:
                        switch (roundTurn)
                        {
                            case RoundTurn.PlayerRound:
                                player.BuffReduce();
                                roundTurn = RoundTurn.EnemyRound;
                                break;
                            case RoundTurn.EnemyRound:
                                enemy.BuffReduce();
                                roundTurn = RoundTurn.PlayerRound;
                                break;
                        }
                        roundStatus = RoundStatus.RoundBegin;
                        break;
                }
                break;

            case BattleStatus.PlayerLose:
                GameManager.Instance.ReturnToMenu(false);
                battleStatus = BattleStatus.PlayerPause;
                break;

            case BattleStatus.PlayerWin:
                battleStatus = BattleStatus.PlayerPause;
                PlayerWin();
                break;

            case BattleStatus.PlayerPause:
            default:
                break;

        }


    }

    void PlayerWin()
    {
        GameObject.Find("View").GetComponent<View>().enabled = false;
        Card card;

        int x = player.CardLibrary.Count;
        for (int i = x - 1; i >= 0; i--)
        {
            card = player.CardLibrary[i];
            if (card.IsVice)
            {
                player.CardLibrary.Remove(card);
            }
        }

        x = player.CardManager.Cards.Count;
        for (int i = x - 1; i >= 0; i--)
        {
            card = player.CardManager.Cards[i];
            if (card.IsVice)
            {
                player.CardManager.Cards.Remove(card);
            }
            else
            {
                player.CardManager.Cards.Remove(card);
                player.CardLibrary.Add(card);
            }
        }

        x = player.CardDiscard.Count;
        for (int i = x - 1; i >= 0; i--)
        {
            card = player.CardDiscard[i];
            if (card.IsVice)
            {
                player.CardDiscard.Remove(card);
            }
        }

        if (enemy.EnemyType == EnemyType.Boss)
        {
            int num = player.CardDiscard.Count / 2;
            for (int j = 0; j < num; j++)
            {
                int rand = UnityEngine.Random.Range(0, player.CardDiscard.Count);
                Card tmp = player.CardDiscard[rand];
                player.CardLibrary.Add(tmp);
                player.CardDiscard.RemoveAt(rand);
            }
        }

        UIManager.Instance.PushPanel(UIPanelType.Reward, enemy);
    }


    IEnumerator EnemyPutCard()
    {
        View.Instance.canPutCard = false;
        View.Instance.GetComponent<ViewOfButton>().endButton.enabled = false;

        yield return new WaitForSeconds(1f);

        while ((enemy.CardManager as EnemyCardManager).CanPutCard)
        {
            enemy.PutCurrentCard(player);
            yield return new WaitForSeconds(2f);
        }
        enemy.isRound = false;
        roundStatus = RoundStatus.RoundEnd;
        View.Instance.GetComponent<ViewOfButton>().endButton.enabled = true;
        View.Instance.canPutCard = true;
    }

}


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
    RoundPause,
}

public enum RoundTurn
{
    PlayerRound,
    EnemyRound,
}
