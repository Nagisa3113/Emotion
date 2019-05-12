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

    public Button pauseButton;

    public bool isPause=false;
	public Sprite[] pauseSprite;

    public GameObject[] handCards;

    public GameObject[] deskCards;

    public GameObject backCard ;   //牌背     
    Player player ;
    Enemy enemy ;

   //上一次选中牌在cardLibrary里的索引
    int lastIndex;
    //此时选中牌在cardLibrary里的索引
    int currentIndex;

    int lastEnemyCardsNum;
    
    Card lastCard;          //上一次选中牌在cardLibrary里的牌

    bool flagOfPutCard;    //是否显示出的牌，因为这是在
    EnemyAI AI; 

    public void Start()
    {
        enemy =  GameObject.Find("Battle").GetComponent<BattleSystem>().GetEnemy();
        player = Player.GetInstance();
        AI = GameObject.Find("Battle").GetComponent<BattleSystem>().GetAI();
       
        lastIndex = -1;
        lastCard = new Card();
        currentIndex = player.GetCardManager.CardIndex;

        lastEnemyCardsNum  = AI.CardsNum;

        ShowPlayerCards();
        ShowEnemyCards ();
        flagOfPutCard = false ;
    }

    public void Update()
    {   
        /*显示各种参数 */
        // healthOfPlayer.fillAmount =  player.GetHP/(float)player.GetHPMax;
        // healthOfEnemy.fillAmount  =  enemy.GetHP/(float)enemy.GetHPMax;

        // expenseOfPlayer.text = player.GetCardManager.ExpenseCurrent.ToString();
        // expenseOfEnemy. text = enemy .GetCardManager.ExpenseCurrent.ToString();

        // handcardsOfPlayer.text = player.GetCardManager.CardsNum.ToString();
        // handcardsOfEnemy. text = enemy. GetCardManager.CardsNum.ToString();
       

        /*显示玩家和敌人的手牌和每次手牌数变化时更新手牌 */
        if (player.GetCardManager.ChangeViewShow)
        {
            DestoryPlayerCards();
            ShowPlayerCards();
            lastIndex = -1;
            flagOfPutCard = true;
            player.GetCardManager.ChangeViewShow = false;
           

        }

        if(AI.CardsNum != lastEnemyCardsNum)
        {
            DestoryEnemyCards();
            ShowEnemyCards();
            lastEnemyCardsNum = AI.CardsNum;
        }

        
        if (player.GetCardManager.ChangeViewSelect && player.GetCardManager.CardIndex != -1)
        {
            currentIndex = player.GetCardManager.CardIndex;
            SelectedPlayerCard();

            lastIndex = player.GetCardManager.CardIndex;
            lastCard = player.GetCardManager.CurrentCard;
        }
        
        //显示用出来的牌
        if (player.GetCardManager.CardIndex == -1 && flagOfPutCard)
        {
            ShowPlayerPutCard();
            flagOfPutCard = false ;
        }

        
    }


    public void ClickPause()
    {
        if (isPause == false)
        {
            isPause = true;
            pauseButton.image.sprite = pauseSprite[0];
            GameObject.Find("Battle").GetComponent<BattleSystem>().battleStatus = BattleStatus.PlayerPause;
        }
        else
        {
            isPause = false;
            pauseButton.image.sprite = pauseSprite[1];
            GameObject.Find("Battle").GetComponent<BattleSystem>().battleStatus = BattleStatus.Batttling;
        }
        
    }

    //显示手牌里的牌
    void ShowPlayerCards()
	{
         

        int i = 0;
        Vector3 interval = new Vector3(0.3f,0,-0.01f);
        Vector3 startPosition = new Vector3(3f,-3f,0);
		GameObject playerCard = GameObject.Find("PlayerCards");
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

			GameObject itemGo = Instantiate(temp,startPosition + interval*i, Quaternion.identity);
			itemGo.transform.SetParent(playerCard.transform);
            i++;
		}    
    }

    void DestoryPlayerCards()
    {
        GameObject playerCards = GameObject.Find("PlayerCards");
        int childCount = playerCards.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			Destroy(playerCards.transform.GetChild(i).gameObject);
		}
    }

     void ShowEnemyCards()
	{

        int i = 0;
        Vector3 interval = new Vector3(0.4f,0,-0.01f);
        Vector3 startPosition = new Vector3(-5f,3f,0);
		GameObject enemyCards = GameObject.Find("EnemyCards");

        //对于每个手牌里的牌，找到对应handCards库里的prefab，然后生成
		for (int temp = 0;temp < AI.CardsNum;temp ++)
		{
			GameObject itemGo = Instantiate(backCard,startPosition+interval*i, Quaternion.identity);
			itemGo.transform.SetParent(enemyCards.transform);
            i++;
		}    
    }

    void DestoryEnemyCards()
    {
        GameObject enemyCards = GameObject.Find("EnemyCards");
        int childCount = enemyCards.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			Destroy(enemyCards.transform.GetChild(i).gameObject);
		}
    }


    //实现上一次选中牌恢复原状，现在选中牌放大
    void SelectedPlayerCard()
    {
        //SrollPlayerShow();
        GameObject playerCard = GameObject.Find("PlayerCards");
        //现在选中在视图的牌
        GameObject selectedCard = null;
        //上次选中在视图的牌
        GameObject lastSelectedCard = null;
        //手牌
        GameObject deskCard = null;
        //放大的桌面牌
        GameObject handCard = null;

        Vector3 selectedPosition;
        Vector3 lastSelectedPosition;
        Card currentCard = player.GetCardManager.CurrentCard;


        if(lastIndex != -1 && lastIndex != currentIndex)
        {  
            lastSelectedCard = playerCard.transform.GetChild(lastIndex).gameObject;
            lastSelectedPosition=new Vector3(0,0,lastSelectedCard.transform.position.z);
            //GameObject temp = playerCard.transform.GetChild(lastIndex).gameObject;             
            foreach (var prefab in handCards)
            {
                if (lastCard.GetName.ToString() == prefab.name)
                {
                    handCard = prefab;
                    break;
                }
            }
            lastSelectedCard.transform.position = lastSelectedCard.transform.position
                                          -lastSelectedPosition  + new Vector3(0,0,2f);

        lastSelectedCard.GetComponent<SpriteRenderer>().sprite = handCard.GetComponent<SpriteRenderer>().sprite;
        }

        selectedCard = playerCard.transform.GetChild(currentIndex).gameObject;
        selectedPosition=new Vector3(0,0,selectedCard.transform.position.z);

        foreach (var prefab in deskCards)
        {
            if (currentCard.GetName.ToString() == prefab.name)
            {
                deskCard = prefab;
                break;
            }
        }
        selectedCard.GetComponent<SpriteRenderer>().sprite = deskCard.GetComponent<SpriteRenderer>().sprite;
        selectedCard.transform.position = selectedCard.transform.position
                                          -selectedPosition + new Vector3(0,0,-2f);

        
    }


    //显示出过的牌
    void ShowPlayerPutCard()
    {
        
        Vector3 interval = new Vector3(0.3f,0,-0.01f);
        Vector3 startPosition = new Vector3(-4f,0f,0);
		GameObject cardTombs = GameObject.Find("CardTombs");
        int i = cardTombs.transform.childCount;;

        foreach (var prefab in handCards)
        {
            if (lastCard.GetName.ToString() == prefab.name)
            {
                GameObject itemGo = Instantiate(prefab,startPosition+interval*i, Quaternion.identity);
			    itemGo.transform.SetParent(cardTombs.transform);
                break;
            }
        }	
		   
    }
}


