using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using DG.Tweening;

public class View : MonoBehaviour
{
    public Image healthOfPlayer;
    public Image healthOfEnemy;

    public Text expenseOfPlayer;
    public Text expenseOfEnemy;
    public Text handcardsOfPlayer;
    public Text handcardsOfEnemy;

    public GameObject playerCards;
    public GameObject enemyCards;
    public GameObject cardTombs;
    public GameObject playerBuffs;
    public GameObject enemyBuffs;
    public GameObject cardLibrary;
    public GameObject showCard;
    public Sprite[] s_buff;

    public bool canPutCard;

    public int maxShowCount;    //最多一次显示几张牌
    public int left;            //显示左边牌的index
    public int right;           //显示右边牌的index

    public Player player;
    public Enemy enemy;

    static View m_Instance;
    public static View Instance
    {
        get { return m_Instance; }
        set { m_Instance = value; }
    }

    void Awake()
    {
        m_Instance = this;
        foreach (var mr in showCard.GetComponentsInChildren<MeshRenderer>())
        {
            mr.sortingOrder = 3;
        }
    }

    void Start()
    {
        enemy = BattleSystem.Instance.GetComponent<BattleSystem>().Enemy;
        player = Player.Instance;
        canPutCard = true;
        maxShowCount = 5;
        right = maxShowCount - 1;
    }

    void Update()
    {
        handcardsOfPlayer.text = player.CardManager.Cards.Count.ToString();
        handcardsOfEnemy.text = enemy.CardManager.Cards.Count.ToString();

        healthOfPlayer.fillAmount = ((float)player.HP / (float)player.HPMax);
        healthOfEnemy.fillAmount = ((float)enemy.HP / (float)enemy.HPMax);
    }

    public void ShowPlayerCards()
    {
        Vector3 interval = new Vector3(1f, 0, -0.1f);     //这里修改要在ViewCardFront里同步修改
        Vector3 startPosition = new Vector3(0f, -3f, 0);

        //摧毁原有牌
        for (int i = playerCards.transform.childCount - 1; i >= 0; i--)
        {
            //使用Destroy会有bug
            DestroyImmediate(playerCards.transform.GetChild(i).gameObject);
        }

        for (int i = 0; i < player.CardManager.Cards.Count; i++)
        {
            GameObject handCard = Instantiate(GameResources.cardFront, startPosition + interval * i, Quaternion.identity);
            handCard.GetComponent<ViewCardFront>().ShowCard(player, player.CardManager.Cards[i]);
            handCard.transform.SetParent(playerCards.transform);
        }

        for (int i = maxShowCount; i < playerCards.transform.childCount; i++)
        {
            playerCards.transform.GetChild(i).gameObject.SetActive(false);
        }

        left = 0;
        right = (maxShowCount < playerCards.transform.childCount ? maxShowCount : playerCards.transform.childCount) - 1;
    }

    public void ShowEnemyCards()
    {
        int i = 0;

        Vector3 interval = new Vector3(0.8f, 0, -0.01f);
        Vector3 startPosition = new Vector3(-5.5f, 3f, 0);

        int childCount = enemyCards.transform.childCount;

        for (i = 0; i < childCount; i++)
        {
            Destroy(enemyCards.transform.GetChild(i).gameObject);
        }

        i = 0;
        foreach (Card card in enemy.CardManager.Cards)
        {
            GameObject itemGo = Instantiate(GameResources.cardBack, startPosition + interval * i, Quaternion.identity);
            itemGo.transform.SetParent(enemyCards.transform);
            itemGo.name = card.Name.ToString();
            i++;
        }
    }

    public void ShowPlayerPutCard(int index)
    {
        Vector3 interval = new Vector3(0.3f, 0, -0.5f);

        if (cardTombs.transform.childCount == 6)
        {
            Destroy(cardTombs.transform.GetChild(0).gameObject);
            for (int j = 0; j < cardTombs.transform.childCount; j++)
            {
                cardTombs.transform.GetChild(j).position -= interval;
            }
        }

        GameObject card = Instantiate(GameResources.cardFront, playerCards.transform.GetChild(index).position, Quaternion.identity);
        card.transform.SetParent(cardTombs.transform);
        card.GetComponent<ViewCardFront>().CopyContent(playerCards.transform.GetChild(index).GetComponent<ViewCardFront>());
        card.GetComponent<ViewCardFront>().StartCoroutine(card.GetComponent<ViewCardFront>().IEPut());
    }


    public void ShowEnemyPutCard(CardName name)
    {
        Vector3 interval = new Vector3(0.3f, 0, -0.5f);

        if (cardTombs.transform.childCount == 6)
        {
            Destroy(cardTombs.transform.GetChild(0).gameObject);

            for (int j = 0; j < cardTombs.transform.childCount; j++)
            {
                cardTombs.transform.GetChild(j).position -= interval;
            }
        }

        for (int j = 0; j < enemyCards.transform.childCount; j++)
        {
            if (enemyCards.transform.GetChild(j).name == name.ToString())
            {
                GameObject itemGo = Instantiate(GameResources.cardBack, enemyCards.transform.GetChild(j).position, Quaternion.identity);
                itemGo.name = name.ToString();
                itemGo.GetComponent<ViewCardBack>().StartFront(j);
                break;
            }
        }

    }


    public void ShowBuff(Role self)
    {
        GameObject container = self == player ? playerBuffs : enemyBuffs;
        Vector3 interval = new Vector3(0.5f, 0, -0.01f);
        Vector3 startPosition = self == player ? new Vector3(-3f, -2f, 0) : new Vector3(6f, 2f, 0);

        for (int i = 0; i < container.transform.childCount; i++)
        {
            DestroyImmediate(container.transform.GetChild(0).gameObject);
        }

        for (int i = 0; i < player.GetBuffManager.Buffs.Count; i++)
        {
            GameObject itemGo = Instantiate(GameResources.buff, startPosition + interval * i, Quaternion.identity);
            itemGo.transform.SetParent(container.transform);
            itemGo.GetComponentInChildren<SpriteRenderer>().sprite = s_buff[0];//TODO
            itemGo.name = player.GetBuffManager.Buffs[i].ToString();
        }
    }

}


