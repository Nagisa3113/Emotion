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
    Player player ;
    Enemy enemy ;

   //上一次选中牌在cardLibrary里的索引
    int lastIndex;
    //此时选中牌在cardLibrary里的索引
    int currentIndex;

    int lastCardsNum;
    
    Card lastCard;   //上一次选中牌在cardLibrary里的牌

    public void Start()
    {
        enemy = GetComponent<BattleController>().GetEnemy();
        player = Player.GetInstance();

        lastIndex = -1;
        lastCard = new Card();
        currentIndex = player.GetCardManager.CardIndex;
        lastCardsNum = player.GetCardManager.CardsNum;
        ShowCards();
    }

    public void Update()
    {
        
        healthOfPlayer.fillAmount =  player.GetHP/(float)player.GetHPMax-0.1f;
        healthOfEnemy.fillAmount  =  enemy.GetHP/(float)enemy.GetHPMax-0.1f;

        expenseOfPlayer.text = player.GetCardManager.ExpenseCurrent.ToString();
        expenseOfEnemy. text = enemy .GetCardManager.ExpenseCurrent.ToString();

        handcardsOfPlayer.text = player.GetCardManager.CardsNum.ToString();
        handcardsOfEnemy. text = enemy. GetCardManager.CardsNum.ToString();

        if (player.GetCardManager.CardsNum != lastCardsNum)
        {
            DestoryCards();
            ShowCards();

             lastCardsNum = player.GetCardManager.CardsNum;
        }
        
        //当CardIndex发生改变时，调用SelectCard(),并更新上次选中的索引和牌
        if (lastIndex != player.GetCardManager.CardIndex)
        {
            currentIndex = player.GetCardManager.CardIndex;
            SelectedCard();

            lastIndex = player.GetCardManager.CardIndex;
            lastCard = player.GetCardManager.CurrentCard;
        }






        
    }


    public void ClickPause()
    {
        if (isPause == false)
        {
            isPause = true;
            pauseButton.image.sprite = pauseSprite[0];
        }
        else
        {
            isPause = false;
            pauseButton.image.sprite = pauseSprite[1];
        }
        
    }

    //显示手牌里的牌
    void ShowCards()
	{

        int i = 0;
        Vector3 interval = new Vector3(0.3f,0,-0.01f);
        Vector3 startPosition = new Vector3(4f,-3f,0);
        startPosition -= player.GetCardManager.CardsNum/2*interval;

		GameObject playerCard = GameObject.Find("PlayerCards");

        //对于每个手牌里的牌，找到对应handCards库里的prefab，然后生成
		foreach (var card in player.GetCardManager.GetCards())
		{
            GameObject temp = null;
            foreach (var prefab in handCards)
            {
                if (card.GetName == prefab.name)
                {
                    temp = prefab;
                    break;
                }
            }

			GameObject itemGo = Instantiate(temp,startPosition+interval*i, Quaternion.identity);
			itemGo.transform.SetParent(playerCard.transform);
		
            i++;
		}    
    }

    void DestoryCards()
    {
        GameObject playerCards = GameObject.Find("PlayerCards");
        int childCount = playerCards.transform.childCount;
		for (int i = 0; i < childCount; i++)
		{
			Destroy(playerCards.transform.GetChild(i).gameObject);
		}
    }

    //实现上一次选中牌恢复原状，现在选中牌放大
    void SelectedCard()
    {
        GameObject playerCard = GameObject.Find("PlayerCards");
        //现在选中在视图的牌
        GameObject selectedCard = null;
        //上次选中在视图的牌
        GameObject laseSelectedCard = null;
        //手牌
        GameObject deskCard = null;
        //放大的桌面牌
        GameObject handCard = null;

       
        Vector3 selectedPosition;
        Vector3 lastSelectedPosition;
        Card currentCard = player.GetCardManager.CurrentCard;


        if(lastIndex != -1)
        {
                
            laseSelectedCard = playerCard.transform.GetChild(lastIndex).gameObject;
            lastSelectedPosition=new Vector3(0,0,laseSelectedCard.transform.position.z);
            foreach (var prefab in handCards)
            {
                if (lastCard.GetName == prefab.name)
                {
                    handCard = prefab;
                    break;
                }
            }

            laseSelectedCard.GetComponent<SpriteRenderer>().sprite = handCard.GetComponent<SpriteRenderer>().sprite;
            
            laseSelectedCard.transform.position = laseSelectedCard.transform.position-
                                                  lastSelectedPosition + new Vector3(0,0,2f);
        }

        selectedCard = playerCard.transform.GetChild(currentIndex).gameObject;
        selectedPosition=new Vector3(0,0,selectedCard.transform.position.z);

        foreach (var prefab in deskCards)
        {
            if (currentCard.GetName == prefab.name)
            {
                deskCard = prefab;
                break;
            }
        }
        selectedCard.GetComponent<SpriteRenderer>().sprite = deskCard.GetComponent<SpriteRenderer>().sprite;
        selectedCard.transform.position = selectedCard.transform.position
                                          -selectedPosition + new Vector3(0,0,-2f);
    }

}


