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

    public bool isPause = false;

    public Sprite[] pauseSprite;

    GameObject cardLibrary;
    float scollIndex = 0f;

    void Start()
    {
        cardLibrary = View.Instance.cardLibrary;
    }

    void Update()
    {
        if (Player.Instance.CardManager.Cards.Count > View.Instance.maxShowCount)
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

    public void ClickPause()
    {
        Vector3 interval = new Vector3(2f, 0, 0f);
        Vector3 startPosition = new Vector3(-4f, 2f, 0f);
        int i = 0;
        if (isPause == false)
        {
            isPause = true;
            pauseButton.image.sprite = pauseSprite[1];
            BattleSystem.Instance.GetComponent<BattleSystem>().battleStatus = BattleStatus.PlayerPause;
            cardLibrary.SetActive(true);
            foreach (var card in Player.Instance.CardLibrary)
            {
                GameObject cardFront = Instantiate(GameResources.cardFront, startPosition + interval * i, Quaternion.identity);
                cardFront.GetComponent<ViewCardFront>().ShowLibraryCard(Player.Instance, card);
                cardFront.transform.SetParent(cardLibrary.transform);
                i++;
                if (i % 5 == 0)
                {
                    startPosition += new Vector3(0, -4f, 0);
                    i = 0;
                }
            }
            SetCondition(false);
        }
        else
        {
            isPause = false;
            pauseButton.image.sprite = pauseSprite[0];
            BattleSystem.Instance.GetComponent<BattleSystem>().battleStatus = BattleStatus.Batttling;
            cardLibrary.SetActive(false);
            SetCondition(true);
        }

    }

    void ScrollShow(int x)
    {
        if (x == -1)
        {
            if (!(cardLibrary.transform.GetChild(0).position.y - 2 < 0.001f && cardLibrary.transform.GetChild(0).position.y - 2 > -0.001f))
            {
                for (int i = 0; i < cardLibrary.transform.childCount; i++)
                {
                    cardLibrary.transform.GetChild(i).position += new Vector3(0, -8f, 0);
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

                    cardLibrary.transform.GetChild(i).position += new Vector3(0, 8f, 0);

                }
            }
        }

    }

    void SetCondition(bool value)
    {
        endButton.enabled = value;
        View.Instance.playerCards.SetActive(value);
        View.Instance.cardTombs.SetActive(value);
        View.Instance.enemyCards.SetActive(value);
    }

    public void ClickEnd()
    {
        BattleSystem.Instance.GetComponent<BattleSystem>().roundStatus = RoundStatus.RoundEnd;
    }

    public void ClickLeft()
    {
        if (View.Instance.left > 0)
        {
            View.Instance.left--;
            ChangePosition(new Vector3(1f, 0, 0));
            ChangeActive();
            View.Instance.right--;
        }
    }

    public void ClickRight()
    {
        if (View.Instance.right < View.Instance.playerCards.transform.childCount - 1)
        {
            View.Instance.right++;
            ChangePosition(new Vector3(-1f, 0, 0));
            ChangeActive();
            View.Instance.left++;
        }
    }

    void ChangePosition(Vector3 offset)
    {
        for (int i = 0; i < View.Instance.playerCards.transform.childCount; i++)
        {
            View.Instance.playerCards.transform.GetChild(i).position += offset;
        }
    }
    void ChangeActive()
    {
        GameObject temp = View.Instance.playerCards.transform.GetChild(View.Instance.left).gameObject;
        temp.SetActive(!temp.activeInHierarchy);
        temp = View.Instance.playerCards.transform.GetChild(View.Instance.right).gameObject;
        temp.SetActive(!temp.activeInHierarchy);
    }
}
