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
    
    Player player = Player.GetInstance();
    Enemy enemy ;
    public void Start()
    {
         enemy = GetComponent<BattleController>().GetEnemy();
    }

    public void Update()
    {
        healthOfPlayer.fillAmount =  player.GetHP/(float)player.GetHPMax;
        healthOfEnemy.fillAmount  =  enemy.GetHP/(float)enemy.GetHPMax;

        expenseOfPlayer.text = player.GetCardManager.ExpenseCurrent.ToString();
        expenseOfEnemy. text = enemy .GetCardManager.ExpenseCurrent.ToString();

        handcardsOfPlayer.text = player.GetCardManager.CardsNum.ToString();
        handcardsOfEnemy. text = enemy. GetCardManager.CardsNum.ToString();

        
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

  //  public 

}


