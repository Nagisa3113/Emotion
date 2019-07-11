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

  
    public GameObject[] buffs;
    public GameObject[] handCards;
    public GameObject[] deskCards;

    public GameObject backCard;   //牌背   
    public GameObject motherHandCard;
    public GameObject motherPutCard;
    public GameObject motherBuff;

    public bool canPutCard;         //供使用这个类的用

    public Player player;
    public Enemy enemy;
    public GameObject playerCards;
    public GameObject enemyCards;
    public GameObject cardTombs;
    
    int lastIndex;
    Card lastCard;

    public int maxShowCount;    //最多一次显示几张牌
    public int left;            //显示左边牌的index
    public int right;           //显示右边牌的index

    private static View view;

    private View()
    {
        view = this;
    }


    public static View GetInstance()
    {
        return view;
    }


    public void Awake()
    {
        enemy = GameObject.Find("Battle").GetComponent<BattleSystem>().GetEnemy();
        player = Player.GetInstance();

        lastIndex = -1;

        canPutCard = true;
        playerCards = GameObject.Find("PlayerCards");
        enemyCards = GameObject.Find("EnemyCards");
        cardTombs = GameObject.Find("CardTombs"); 
        int i =0 ;
        handCards = Resources.LoadAll<GameObject>("Prefabs/handCard");
        deskCards = Resources.LoadAll<GameObject>("Prefabs/deskCard");
        buffs = Resources.LoadAll<GameObject>("Prefabs/buff");

        maxShowCount = 5;
        

        //SelectedPlayerCard(0,player.GetCardManager.GetCards()[0]);   //键盘时使用

    }

    public void Update()
    {
        /*显示各种参数 */
        healthOfPlayer.fillAmount = player.HP / (float)player.HPMax  - 0.1f;
        healthOfEnemy.fillAmount = enemy.HP / (float)enemy.HPMax - 0.1f;

        expenseOfPlayer.text = player.CardManager.ExpenseCurrent.ToString();
        expenseOfEnemy.text = enemy.CardManager.ExpenseCurrent.ToString();

        handcardsOfPlayer.text = player.CardManager.CardsNum.ToString();
        handcardsOfEnemy.text = enemy.CardManager.CardsNum.ToString();
    }

    //显示手牌里的牌
    public void ShowPlayerCards()
    {
        int i = 0;
        lastIndex = -1;

        Vector3 interval = new Vector3(1f, 0, -0.1f);
        Vector3 startPosition = new Vector3(0f, -3f, 0);
       
        TextMesh text;
        GameObject mesh;
        TextMesh bonus;


        //摧毁原有牌
        int childCount = playerCards.transform.childCount;
		for (i = 0; i < childCount; i++)
		{
			ObjectPool.GetInstance().RecycleObj("motherHandCard",playerCards.transform.GetChild(0).gameObject);
		}
        i = 0;

        //对于每个手牌里的牌，找到对应handCards库里的prefab，然后生成
        foreach (Card card in player.CardManager.GetCards)
        {
            GameObject temp = null;
            foreach (var prefab in handCards)
            {
                if (card.Name.ToString() == prefab.name)
                {
                    temp = prefab;
                    break;
                }
            }

			GameObject itemGo = ObjectPool.GetInstance().GetObj("motherHandCard",startPosition + interval*i, Quaternion.identity);
            itemGo.GetComponent<SpriteRenderer>().sprite = temp.GetComponent<SpriteRenderer>().sprite;
            itemGo.name = temp.name;
            i++;
            text = itemGo.transform.GetChild(0).gameObject.GetComponent<TextMesh>();
            mesh = itemGo. transform.GetChild(1).gameObject;
            bonus = itemGo.transform.GetChild(2).gameObject.GetComponent<TextMesh>();
            bonus.text = player.CardManager.GetBonus(card.Name).ToString();
            
            itemGo.transform.SetParent(playerCards.transform);
        }
        
        for (i= maxShowCount ;i<playerCards.transform.childCount;i++)
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

        //对于每个手牌里的牌，找到对应handCards库里的prefab，然后生成
        foreach (Card card in enemy.CardManager.GetCards)
        {
            GameObject itemGo = Instantiate(backCard, startPosition + interval * i, Quaternion.identity);
            itemGo.transform.SetParent(enemyCards.transform);
            itemGo.name = card.Name.ToString();
            i++;
        }
    }


    // 实现上一次选中牌恢复原状，现在选中牌放大
    public void SelectedPlayerCard(int currentIndex, Card currentCard)
    {
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
            lastSelectedCard = playerCards.transform.GetChild(lastIndex).gameObject;
            foreach (var prefab in handCards)
            {
                if (lastCard.Name.ToString() == prefab.name)
                {
                    handCard = prefab;
                    break;
                }
            }
            lastSelectedCard.transform.position = lastSelectedCard.transform.position + new Vector3(0, 0, 2f);
            lastSelectedCard.GetComponent<SpriteRenderer>().sprite = handCard.GetComponent<SpriteRenderer>().sprite;

        }
        selectedCard = playerCards.transform.GetChild(currentIndex).gameObject;
        foreach (var prefab in deskCards)
        {
            if (currentCard.Name.ToString() == prefab.name)
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

    public void ShowEnemyPutCard(CardName name)
    {
        Vector3 interval = new Vector3(0.3f, 0, -0.01f);
        Vector3 startPosition = new Vector3(-4f, 0f, 0);

        int i = cardTombs.transform.childCount; 
        if (i == 6)
        {
            ObjectPool.GetInstance().RecycleObj("motherPutCard",cardTombs.transform.GetChild(0).gameObject);
            
            
            for (int j = 0; j < cardTombs.transform.childCount;j++)
            {
                cardTombs.transform.GetChild(j).position -= interval;
            } 

        }

        for (int j=0; j < enemyCards.transform.childCount;j++)
        {
            if (enemyCards.transform.GetChild(j).name == name.ToString())
            {
                GameObject itemGo = Instantiate(backCard, enemyCards.transform.GetChild(j).position, Quaternion.identity);
                itemGo.name = name.ToString();
                itemGo.GetComponent<ViewEnemyCard>().StartFront();
                break;
            }           
        } 

    }


    //显示出过的牌
    public void ShowPlayerPutCard(CardName name,int index)
    {

        Vector3 interval = new Vector3(0.3f, 0, -0.01f);
        Vector3 startPosition = new Vector3(-4f, 0f, 0);
        int i = cardTombs.transform.childCount;
        int bonus = int.Parse(playerCards.transform.GetChild(index).GetChild(2).gameObject.GetComponent<TextMesh>().text); 
        if (i == 6)
        {
            ObjectPool.GetInstance().RecycleObj("motherPutCard",cardTombs.transform.GetChild(0).gameObject);
    
            for (int j=0; j < cardTombs.transform.childCount;j++)
            {
                cardTombs.transform.GetChild(j).position -= interval;    
            } 
        }
        
        foreach (var prefab in handCards)
        {
            if (name.ToString() == prefab.name)
            {
                GameObject itemGo = ObjectPool.GetInstance().GetObj("motherPutCard",playerCards.transform.GetChild(index).position, Quaternion.identity);
                itemGo.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                itemGo.transform.SetParent(cardTombs.transform);
                itemGo.GetComponent<ViewPutCard>().StartShow(bonus);
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

            foreach (var buff in player.GetBuffManager.Buffs)
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
                GameObject itemGo = Instantiate(motherBuff, startPosition + interval * i, Quaternion.identity);
                itemGo.transform.SetParent(playerBuffs.transform);
                itemGo.GetComponent<SpriteRenderer>().sprite = temp.GetComponent<SpriteRenderer>().sprite;
                itemGo.name = buff.ToString();
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

            foreach (var buff in enemy.GetBuffManager.Buffs)
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


