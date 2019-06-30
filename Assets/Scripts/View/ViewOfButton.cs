using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewOfButton : MonoBehaviour
{
    public Button pauseButton;
    public Button endButton;
    public GameObject leftButton;
    public GameObject rightButton;

    public bool isPause;
    View view;
    public Sprite[] pauseSprite;
    public GameObject cardLibrary;
    float scollIndex;



    void Start()
    {
        isPause = false;
        view = View.GetInstance();
        scollIndex = 0;

    }
    void Update()
    {
        if (Player.GetInstance().CardManager.CardsNum > view.maxShowCount)
        {
            leftButton.SetActive(true);
            rightButton.SetActive(true);
        }
        else
        {
            leftButton.SetActive(false);
            rightButton.SetActive(false);
        }
        if (isPause)
        {
            scollIndex += Input.GetAxis("Mouse ScrollWheel");

            if (scollIndex > 0.25f)
            {

                ScrollShow(-1);
                scollIndex = 0;
            }
            else if (scollIndex < -0.25f)
            {

                ScrollShow(1);
                scollIndex = 0;
            }
        }
    }

    // Update is called once per frame
    public void ClickPause()
    {
        Vector3 interval = new Vector3(2f, 0, -0.01f);
        Vector3 startPosition = new Vector3(-4f, 2f, -4.1f);
        int i = 0;
        if (isPause == false)
        {
            isPause = true;
            pauseButton.image.sprite = pauseSprite[1];
            GameObject.Find("Battle").GetComponent<BattleSystem>().battleStatus = BattleStatus.PlayerPause;
            cardLibrary.SetActive(true);
            foreach (var card in Player.GetInstance().CardLibrary)
            {
                GameObject temp = null;
                foreach (var prefab in view.handCards)
                {
                    if (card.Name.ToString() == prefab.name)
                    {
                        temp = prefab;
                        break;
                    }
                }
                GameObject itemGo = Instantiate(view.motherPutCard, startPosition + interval * i, Quaternion.identity);
                itemGo.GetComponent<SpriteRenderer>().sprite = temp.GetComponent<SpriteRenderer>().sprite;
                itemGo.name = temp.name;
                itemGo.transform.SetParent(cardLibrary.transform);
                i++;
                if (i % 5 == 0)
                {

                    startPosition += new Vector3(0,-4f,0);
                    i = 0;
                }
            }
            SetCondition(false);

        }
        else
        {
            isPause = false;
            pauseButton.image.sprite = pauseSprite[0];
            GameObject.Find("Battle").GetComponent<BattleSystem>().battleStatus = BattleStatus.Batttling;
            cardLibrary.SetActive(false);
            SetCondition(true);

        }

    }

    public void ScrollShow(int x)
    {

        GameObject cardLibrary = GameObject.Find("CardLibrary");
        if (x == -1)
        {

            if (!(cardLibrary.transform.GetChild(0).position.y-2 <0.001f && cardLibrary.transform.GetChild(0).position.y-2 >-0.001f))
            {
                for (int i = 0; i < cardLibrary.transform.childCount; i++)
                {

                    cardLibrary.transform.GetChild(i).position += new Vector3(0,-8f,0) ;
                    
                } 
            }
        }
        else
        {
            if (!((cardLibrary.transform.GetChild(cardLibrary.transform.childCount - 1).position.y + 2 < 0.001f
                && cardLibrary.transform.GetChild(cardLibrary.transform.childCount - 1).position.y + 2 > -0.001f)
                || (cardLibrary.transform.GetChild(cardLibrary.transform.childCount - 1).position.y - 2 < 0.001f
                && cardLibrary.transform.GetChild(cardLibrary.transform.childCount - 1).position.y - 2 > -0.001f)))
            {
                print(cardLibrary.transform.GetChild(cardLibrary.transform.childCount - 1).position.y);
                for (int i = 0; i < cardLibrary.transform.childCount; i++)
                {

                    cardLibrary.transform.GetChild(i).position += new Vector3(0,8f,0) ;
                    
                } 
            }
        }

    }
    

    public void SetCondition(bool value)
    {
        endButton.enabled = value;
        view.playerCards.SetActive(value);
        view.cardTombs.SetActive(value);
        view.enemyCards.SetActive(value);
    }

    public void ClickEnd()
    {
        GameObject.Find("Battle").GetComponent<BattleSystem>().ChangeRoundStatus(RoundStatus.RoundEnd);
    }

    public void ClickLeft()
    {
        if (view.left > 0)
        {
            view.left --;
            ChangePosition(new Vector3(1f,0,0));
            ChangeActive();
            view.right --;
        }
    }

    public void ClickRight()
    {
        if(view.right< view.playerCards.transform.childCount-1)
        {
            view.right ++;
            ChangePosition(new Vector3(-1f,0,0));
            ChangeActive();
            view.left ++;
        }
    }

    void ChangePosition(Vector3 offset)
    {
        for (int i= 0 ;i<view.playerCards.transform.childCount;i++)
        {
            view.playerCards.transform.GetChild(i).position +=offset;
        }
    }
    void ChangeActive()
    {
        GameObject temp=view.playerCards.transform.GetChild(view.left).gameObject;
        temp.SetActive(! temp.activeInHierarchy);
        temp=view.playerCards.transform.GetChild(view.right).gameObject;
        temp.SetActive(!temp.activeInHierarchy);
    }
}
