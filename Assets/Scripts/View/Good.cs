using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Good : MonoBehaviour
{
    [SerializeField]
    string contentName;
    public string ContentName
    {
        get { return contentName; }
    }

    [SerializeField]
    string typeName;

    [SerializeField]
    Type type;
    public Type Type
    {
        get { return type; }
    }

    [SerializeField]
    int gold;
    public int Gold
    {
        get { return gold; }
    }

    public bool isProp;
    public bool isCardSet;
    public bool isCard;

    [SerializeField]
    object content;
    public object Content
    {
        set
        {
            content = (object)value;
            contentName = content.ToString();
            type = value.GetType();
            typeName = type.Name;
            isProp = typeof(Prop).IsInstanceOfType(content);
            isCardSet = typeof(CardSet).IsInstanceOfType(content);
            isCard = typeof(Card).IsInstanceOfType(content);

            gold = isProp ? (content as Prop).Cost :
                isCardSet ? (content as CardSet).Cost : -1;

        }
        get { return content; }
    }

    public Image image { get; set; }

    void Start()
    {
        Content = CardSets.GetRandomCardSet();

        ShowContent();
    }

    public void OnPointerEnter()
    {
        if (isCardSet)
        {
            UIManager.Instance.PushPanel(UIPanelType.CardSet, this.content);
        }
    }

    public void OnPointerExit()
    {
        if (isCardSet)
        {
            UIManager.Instance.PopPanel();
        }

    }

    public void OnPointerClick()
    {
        if (Player.Instance.Gold > this.gold)
        {
            Player.Instance.Gold -= this.gold;
            Debug.Log("玩家消费" + this.gold + "钱");
            GetContent(Player.Instance);
        }
        else
        {
            Debug.Log("dont have enough money");
        }
    }

    public void GetContent(Player player)
    {
        transform.gameObject.SetActive(false);
        if (isCardSet)
        {
            player.CardManager.AddCard(content as CardSet);
            Debug.Log("成功购买" + (content as CardSet).Color + (content).ToString());
        }
        else if (isProp)
        {
            player.Props.Add(content as Prop);
            Debug.Log("成功购买" + (content).ToString());
        }
        else
        {
            Player.Instance.CardLibrary.Add(content as Card);
            Debug.Log("成功购买" + (content as Card).Color + (content).ToString());
        }
    }

    public void ShowContent()
    {

        if (isCardSet)
        {
            transform.Find("back").GetComponent<Image>().enabled = true;

            CardColor color = (content as CardSet).Color;
            if ((int)color >= 5)
            {
                return;
            }
            transform.Find("back").GetComponent<Image>().sprite = GameResources.s_cardBack[(int)color];
        }

        else if (isCard)
        {
            transform.Find("back").GetComponent<Image>().enabled = false;

            Card card = content as Card;
            if ((int)card.Color >= 5)
            {
                return;
            }

            transform.Find("front").GetComponent<Image>().sprite = GameResources.s_cardFront[(int)card.Color];

            transform.Find("expense").GetComponent<Image>().sprite = GameResources.s_expenseColor[(int)card.Color];
            transform.Find("expense").GetComponentInChildren<Text>().text = card.Upgrade.ToString();

            transform.Find("upgrade").GetComponent<Image>().sprite = GameResources.s_upgradeColor[(int)card.Color];
            transform.Find("upgrade").GetComponentInChildren<Text>().text = card.Cost.ToString();


            transform.Find("name").GetComponent<Text>().text = card.CName;
            transform.Find("tip").GetComponent<Text>().text = card.Tip();
            //TODO
            //if (card.Color != CardColor.Yellow)
            //{
            //    itemGo.transform.GetChild(4).gameObject.GetComponent<Text>().text = card.Upgrade.ToString();
            //}
        }


    }

}
