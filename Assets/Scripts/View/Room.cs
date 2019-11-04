using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum RoomType
{
    Start,
    Enemy,
    Boss,
    Shop,
    Treasure,
}

public class Room : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    RoomType type = RoomType.Enemy;

    [SerializeField]
    public RoomType Type
    {
        get { return type; }
        set { type = value; }
    }

    //是否可以看到当前房间的种类
    bool detectived;
    public bool Detectived
    {
        get { return detectived; }
        set
        {
            detectived = value;
            ShowType();
        }
    }

    //是否到达过
    bool reached;
    public bool Reached
    {
        get { return reached; }
        set
        {
            reached = value;
            ShowType();
        }
    }

    public Sprite none;
    public Sprite startImage;
    public Sprite bossImage;
    public Sprite enemyImage;
    public Sprite storeImage;
    public Sprite treasureImage;

    public List<Room> nearRooms = new List<Room>();

    public static Room NewRoom()
    {
        Room room = GameObject.Instantiate(Resources.Load("Prefabs/Room") as GameObject)
            .GetComponent<Room>();
        room.Type = RoomType.Enemy;

        GameObject rooms = GameObject.Find("Rooms").gameObject;
        room.transform.SetParent(rooms.transform);
        return room;
    }

    void Awake()
    {
        GetComponent<Image>().sprite = none;
    }

    public void OnButton()
    {
        if (Player.Instance.currentRoom.nearRooms.Contains(this) || reached || detectived)
        {
            GoToRoom(Player.Instance);
        }
    }

    public void GoToRoom(Player player)
    {
        player.currentRoom = this;

        GameObject.Find("PlayerIcon").transform.position = Player.Instance.currentRoom.transform.position;

        if (this.Reached != true)
        {
            this.Reached = true;

            switch (this.Type)
            {
                case RoomType.Enemy:
                    GameManager.Instance.GotoBattle(EnemyType.Normal);
                    break;

                case RoomType.Boss:
                    GameManager.Instance.GotoBattle(EnemyType.Boss);
                    break;

                case RoomType.Shop:
                    UIManager.Instance.PushPanel(UIPanelType.Shop);
                    break;

                case RoomType.Treasure:
                    UIManager.Instance.PushPanel(UIPanelType.Treasure);
                    break;
            }
        }

        foreach (Room r in nearRooms)
        {
            r.Detectived = true;
        }
    }

    public void ShowType()
    {
        //这是为了解决一个颜色不改变的bug
        GetComponent<Image>().sprite = null;

        switch (type)
        {
            case RoomType.Start:
                GetComponent<Image>().sprite = startImage;
                break;
            case RoomType.Boss:
                GetComponent<Image>().sprite = bossImage;
                break;
            case RoomType.Enemy:
                GetComponent<Image>().sprite = enemyImage;
                break;
            case RoomType.Shop:
                GetComponent<Image>().sprite = storeImage;
                break;
            case RoomType.Treasure:
                GetComponent<Image>().sprite = treasureImage;
                break;
        }

        if (detectived)
        {
            GetComponent<Image>().color = new Color(0, 0, 0, 0.5f);
        }
        if (reached)
        {
            GetComponent<Image>().color = new Color(1, 1, 1, 1);
        }


    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();GameObject.Find("Icons").GetComponent<Icons>().selectIcon
        if (detectived)
        {
            GameObject.Find("SelectIcon").GetComponent<Image>().enabled = true;
            GameObject.Find("SelectIcon").transform.position = this.transform.position;
        }

    }

    public void OnPointerExit(PointerEventData eventData)
    {
        //throw new System.NotImplementedException();
        GameObject.Find("SelectIcon").GetComponent<Image>().enabled = false;

    }

}
