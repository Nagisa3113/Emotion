using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ViewOfButton : MonoBehaviour
{
    public Button pauseButton;
    public bool isPause;
    Player player ;
    View view;
    public Sprite[] pauseSprite;
    public GameObject cardLibrary;
     
    float scollIndex;
    void Start()
    {
        isPause=false;
        player = Player.GetInstance();
        view = View.GetInstance();
        scollIndex = 0;
    }
    void Update()
    {
        if(isPause)
        {
            scollIndex -= Input.GetAxis("Mouse ScrollWheel");
            print(Input.GetAxis("Mouse ScrollWheel"));
            if(scollIndex >0.2f)
               {
                    ScrollShow();
               }
        }
    }

    // Update is called once per frame
   public void ClickPause()
    {
        Vector3 interval = new Vector3(2f,0,-0.01f);
        Vector3 startPosition = new Vector3(-4f,2f,-4.1f);
        int i=0;
        if (isPause == false)
        {
            isPause = true;
            pauseButton.image.sprite = pauseSprite[1];
            GameObject.Find("Battle").GetComponent<BattleSystem>().battleStatus = BattleStatus.PlayerPause;
            cardLibrary.SetActive(true); 
            foreach (var card in  CardLibrary.GetInstance().GetCards)
		    {
                GameObject temp = null;
                foreach (var prefab in view.handCards)
                {
                    if (card.GetName.ToString() == prefab.name)
                    {
                        temp = prefab;
                        break;
                    }
                }
			    GameObject itemGo = Instantiate(view.motherPutCard,startPosition + interval*i, Quaternion.identity);
                itemGo.GetComponent<SpriteRenderer>().sprite = temp.GetComponent<SpriteRenderer>().sprite;
                itemGo.name = temp.name;
			    itemGo.transform.SetParent(cardLibrary.transform);
                i++;
                if (i%5 == 0)
                {
                    startPosition += new Vector3(0,1f,0);
                }

		    }


        }
        else
        {
            isPause = false;
            pauseButton.image.sprite = pauseSprite[0];
            GameObject.Find("Battle").GetComponent<BattleSystem>().battleStatus = BattleStatus.Batttling;
            cardLibrary.SetActive(false); 

        }
        
    }

    public void ScrollShow()
    {
        GameObject cardLibrary = GameObject.Find("CardLibrary");
        for (int i=0; i<cardLibrary.transform.childCount;i++)
        {
            cardLibrary.transform.GetChild(i).position += new Vector3(0,-3f,0) ;
            scollIndex = 0;
        }
    }

    public void ClickEnd()
    {
        GameObject.Find("Battle").GetComponent<BattleSystem>().ChangeRoundStatus(RoundStatus.RoundEnd);
    }
}
