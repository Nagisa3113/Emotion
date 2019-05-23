using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ViewHandCard : MonoBehaviour
{
    // Start is called before the first frame update

    private View view;
    public GameObject showCard;
    float time = 0;
    Player player;
    Enemy enemy;

    void Start()
    {
       view = View.GetInstance();
       player = Player.GetInstance();
       enemy =  GameObject.Find("Battle").GetComponent<BattleSystem>().GetEnemy();
       showCard = GameObject.Find("View").transform.GetChild(0).gameObject;
       time = Time.time;
    }
    void OnMouseEnter()
    {
        foreach (var prefab in view.deskCards)
        {
            if (transform.name == prefab.name)
            {
                transform.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                transform.position = transform.position- new Vector3(0,0,2f);
                break;
            }
        }
    }
    
    void OnMouseExit()
    {
        foreach (var prefab in view.handCards)
        {
            if (transform.name == prefab.name)
            {
                transform.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                transform.position = transform.position+ new Vector3(0,0,2f);
                break;
            }
        }
    }

    void OnMouseDown()
    {
        foreach (var prefab in view.deskCards)
        {
            if (transform.name == prefab.name)
            {
                showCard.GetComponent<SpriteRenderer>().sprite = prefab.GetComponent<SpriteRenderer>().sprite;
                break;
            }
        }
        showCard.SetActive(true);
      //当第二次点击鼠标，且时间间隔满足要求时双击鼠标
       if (Time.time - time <= 0.3f)
        {
           player.PutCurrentCard(enemy);
        }
        time = Time.time;
    }
    void OnMouseUp()
    {
      showCard.SetActive(false);  
    }



}
