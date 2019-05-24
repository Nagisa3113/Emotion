using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;



public class View : MonoBehaviour
{
    public Image healthOfPlayer;
    public Image healthOfEnemy;
    public Text expenseOfPlayer;
    public Text expenseOfEnemy;

    public Text handcardsOfPlayer;
    public Text handcardsOfEnemy;



    public GameObject[] handCards;

    public GameObject[] deskCards;


    public GameObject backCard ;   //牌背   
    public GameObject motherHandCard;
    public GameObject motherPutCard;


    public GameObject[] buffs;
    Player player;
    Enemy enemy;

    int lastIndex;
    Card lastCard;

    private static View view;

    private View()
    {
        view = this;
    }


    public static View GetInstance()
    {

        return view;
    }


    public void Start()
    {
        enemy = GameObject.Find("Battle").GetComponent<BattleSystem>().GetEnemy();
        player = Player.GetInstance();
        ShowPlayerCards();
        ShowEnemyCards();

        lastIndex = -1;

        //SelectedPlayerCard(0,player.GetCardManager.GetCards()[0]);

    }

    public void Update()
    {
        /*显示各种参数 */
        healthOfPlayer.fillAmount = player.GetHP / (float)player.GetHPMax;
        healthOfEnemy.fillAmount = enemy.GetHP / (float)enemy.GetHPMax;

        expenseOfPlayer.text = player.GetCardManager.ExpenseCurrent.ToString();
        expenseOfEnemy.text = enemy.GetCardManager.ExpenseCurrent.ToString();

        handcardsOfPlayer.text = player.GetCardManager.CardsNum.ToString();
        handcardsOfEnemy.text = enemy.GetCardManager.CardsNum.ToString();


    }





    //显示手牌里的牌
    public void ShowPlayerCards()
    {
        int i = 0;
        lastIndex = -1;

        Vector3 interval = new Vector3(0.3f, 0, -0.01f);
        Vector3 startPosition = new Vector3(3f, -3f, 0);
        GameObject playerCards = GameObject.Find("PlayerCards");

        //摧毁原有牌
        int childCount = playerCards.transform.childCount;
        for (i = 0; i < childCount; i++)
        {
            DestroyImmediate(playerCards.transform.GetChild(0).gameObject);
        }
        i = 0;

        //对于每个手牌里的牌，找到对应handCards库里的prefab，然后生成
        foreach (var card in player.GetCardManager.GetCards())
        {
            GameObject temp = null;
            foreach (var prefab in handCards)
            {
                if (card.GetName.ToString() == prefab.name)
                {
                    temp = prefab;
                    break;
                }
            }

			GameObject itemGo = Instantiate(motherHandCard,startPosition + interval*i, Quaternion.identity);
            itemGo.GetComponent<SpriteRenderer>().sprite = temp.GetComponent<SpriteRenderer>().sprite;
            itemGo.name = temp.name+i.ToString();
			itemGo.transform.SetParent(playerCards.transform);

            i++;
        }
    }



    public void ShowEnemyCards()
	{


        int i = 0;
        Vector3 interval = new Vector3(0.4f, 0, -0.01f);
        Vector3 startPosition = new Vector3(-5f, 3f, 0);
        GameObject enemyCards = GameObject.Find("EnemyCards");

        //摧毁原有牌
        int childCount = enemyCards.transform.childCount;

		for (i = 0; i < childCount; i++)
		{
			Destroy(enemyCards.transform.GetChild(i).gameObject);
		}
        i=0;


        //对于每个手牌里的牌，找到对应handCards库里的prefab，然后生成
        for (int temp = 0; temp < enemy.GetCardManager.CardsNum; temp++)
        {
            GameObject itemGo = Instantiate(backCard, startPosition + interval * i, Quaternion.identity);
            itemGo.transform.SetParent(enemyCards.transform);
            i++;
        }
    }




    // 实现上一次选中牌恢复原状，现在选中牌放大
    public void SelectedPlayerCard(int currentIndex, Card currentCard)
    {
        GameObject playerCard = GameObject.Find("PlayerCards");
        //现在选中在视图的牌
        GameObject selectedCard = null;
        //上次选中在视图的牌
        GameObject lastSelectedCard = null;
        //手牌
        GameObject deskCard = null;
        //放大的桌面牌
        GameObject handCard = null;
        if (lastIndex != -1)
        {
            lastSelectedCard = playerCard.transform.GetChild(lastIndex).gameObject;
            foreach (var prefab in handCards)
            {
                if (lastCard.GetName.ToString() == prefab.name)
                {
                    handCard = prefab;
                    break;
                }
            }
            lastSelectedCard.transform.position = lastSelectedCard.transform.position + new Vector3(0, 0, 2f);
            lastSelectedCard.GetComponent<SpriteRenderer>().sprite = handCard.GetComponent<SpriteRenderer>().sprite;

        }
        selectedCard = playerCard.transform.GetChild(currentIndex).gameObject;
        foreach (var prefab in deskCards)
        {
            if (currentCard.GetName.ToString() == prefab.name)
            {
                deskCard = prefab;
                break;
            }
        }
        selectedCard.GetComponent<SpriteRenderer>().sprite = deskCard.GetComponent<SpriteRenderer>().sprite;
        selectedCard.transform.position = selectedCard.transform.position + new Vector3(0, 0, -2f);
        lastIndex = currentIndex;
        lastCard = currentCard;
    }


    //显示出过的牌
    public void ShowPlayerPutCard(CardName name)
    {

        Vector3 interval = new Vector3(0.3f, 0, -0.01f);
        Vector3 startPosition = new Vector3(-4f, 0f, 0);
        GameObject cardTombs = GameObject.Find("CardTombs");
        int i = cardTombs.transform.childCount; ;

        foreach (var prefab in handCards)
        {
            if (name.ToString() == prefab.name)
            {

                GameObject itemGo = Instantiate(motherPutCard,startPosition+interval*i, Quaternion.identity);
                itemGo.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
			    itemGo.transform.SetParent(cardTombs.transform);
                itemGo.name = prefab.name;

                break;
            }
        }
    }

    public void ShowBuff(Role self)
    {
        if (self == player)
        {
            Vector3 interval = new Vector3(0.3f, 0, -0.01f);
            Vector3 startPosition = new Vector3(-3f, -2f, 0);
            GameObject playerBuffs = GameObject.Find("PlayerBuffs");
            int childCount = playerBuffs.transform.childCount;
            int i;
            for (i = 0; i < childCount; i++)
            {
                DestroyImmediate(playerBuffs.transform.GetChild(0).gameObject);
            }
            i = 0;

            foreach (var buff in player.GetBuffManager.GetBuffs())
            {
                GameObject temp = null;
                foreach (var prefab in buffs)
                {
                    if (buff.ToString() == prefab.name)
                    {
                        temp = prefab;
                        break;
                    }
                }
                GameObject itemGo = Instantiate(temp, startPosition + interval * i, Quaternion.identity);
                itemGo.transform.SetParent(playerBuffs.transform);
                i++;
            }
        }

        else
        {
            Vector3 interval = new Vector3(0.3f, 0, -0.01f);
            Vector3 startPosition = new Vector3(3f, 2f, 0);
            GameObject enemyBuffs = GameObject.Find("EnemyBuffs");
            int childCount = enemyBuffs.transform.childCount;
            int i;
            for (i = 0; i < childCount; i++)
            {
                DestroyImmediate(enemyBuffs.transform.GetChild(0).gameObject);
            }
            i = 0;

            foreach (var buff in enemy.GetBuffManager.GetBuffs())
            {
                GameObject temp = null;
                foreach (var prefab in buffs)
                {
                    if (buff.ToString() == prefab.name)
                    {
                        temp = prefab;
                        break;
                    }
                }
                GameObject itemGo = Instantiate(temp, startPosition + interval * i, Quaternion.identity);
                itemGo.transform.SetParent(enemyBuffs.transform);
                i++;
            }
        }

    }


}


