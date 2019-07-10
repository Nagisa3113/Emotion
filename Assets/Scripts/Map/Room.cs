using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public enum RoomType
{
    Enemy,
    Boss,
    Store,
    Treasure,
}

public class Room : MonoBehaviour
{
    //0 normal Enemy 1 shore 2 bonus 3 boss
    RoomType type;
    [SerializeField]
    public RoomType Type
    {
        get
        {
            return type;
        }
        set
        {
            switch (value)
            {
                case RoomType.Boss:
                    GetComponent<Image>().sprite = bossImage;
                    break;
                case RoomType.Enemy:
                    GetComponent<Image>().sprite = enemyImage;

                    break;
                case RoomType.Store:
                    GetComponent<Image>().sprite = storeImage;

                    break;
                case RoomType.Treasure:
                    GetComponent<Image>().sprite = treasureImage;

                    break;
            }
        }
    }


    bool isActive;

    public Sprite bossImage;
    public Sprite enemyImage;
    public Sprite storeImage;
    public Sprite treasureImage;

    public bool isBranchMiddle;
    public bool isBranchStart;
    public bool isBranchEnd;


    public bool isTranX;

    public bool IsBranch
    {
        get
        {
            return isBranchEnd || isBranchStart || isBranchMiddle;
        }
    }

    [SerializeField]
    public Room nextRoom;
    [SerializeField]
    public Room prevRoom;

    public Room tranXRoom;

    public Room EnextRoom;
    public Room EprevRoom;



    public static Room NewRoom()
    {
        Room room= GameObject.Instantiate(
            Resources.Load("Node") as GameObject, 
            GameObject.Find("Rooms").transform)
            .GetComponent<Room>();
        room.Type = RoomType.Enemy;
        return room;

    }


}
