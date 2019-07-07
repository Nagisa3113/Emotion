using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Room : MonoBehaviour
{
    //0 normal Enemy 1 shore 2 bonus 3 boss
    public RoomType type;
    public bool isActive;

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
        return GameObject.Instantiate(
            Resources.Load("Node") as GameObject, 
            GameObject.Find("Canvas").transform)
            .GetComponent<Room>();
    }


    public void SetBranch()
    {
        Room room = Room.NewRoom();
        room.transform.SetParent(GameObject.Find("Canvas").transform);
        room.transform.position = transform.position;
        this.transform.localPosition += new Vector3(-40, 0, 0);

        room.transform.localPosition += new Vector3(40, 0, 0);

        isBranchMiddle = true;
        prevRoom.isBranchStart = true;
        prevRoom.EnextRoom = room;
        nextRoom.isBranchEnd = true;
        nextRoom.EprevRoom = room;

        room.nextRoom = this.nextRoom;
        room.prevRoom = this.prevRoom;
    }
}
