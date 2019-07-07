using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum RoomType
{
    Enemy,
    Store,
    Treasure,
}


public class Map : MonoBehaviour
{
    Room start;
    Room end;

    //[SerializeField]
    public List<List<Room>> lines = new List<List<Room>>();

    public List<Room> l0;
    public List<Room> l1;
    public List<Room> l2;

    public List<Room> tranX12 = new List<Room>();
    public List<Room> tranX23 = new List<Room>();


    [ContextMenu("Init")]
    void Init()
    {

        start = Room.NewRoom();
        end = Room.NewRoom();

        lines.Add(new List<Room>());
        lines.Add(new List<Room>());
        lines.Add(new List<Room>());
        l0 = lines[0];
        l1 = lines[1];
        l2 = lines[2];
        for (int i = 0; i < 3; i++)
        {
            //lines[i] = new List<Room>();
            int num = Random.Range(10, 14);

            for (int j = 0; j < num; j++)
            {
                lines[i].Add(Room.NewRoom());
            }
            for (int j = 0; j < num; j++)
            {
                if (j == 0)
                    lines[i][j].prevRoom = start;
                else
                    lines[i][j].prevRoom = lines[i][j - 1];

                if (j == num - 1)

                    lines[i][j].nextRoom = end;
                else
                    lines[i][j].nextRoom = lines[i][j + 1];
            }
        }

    }

    void SetStoreAndBonus()
    {

        int Storenum = Random.Range(1, 3);
        int Treasurenum = Random.Range(1, 3);

        for (int i = 0; i < 3; i++)
        {

            for (int n = 0; n < Storenum; n++)
            {
                int num = Random.Range(0, lines[i].Count);
                lines[i][num].type = RoomType.Store;
            }
            for (int n = 0; n < Treasurenum; n++)
            {
                int num = Random.Range(0, lines[i].Count);
                lines[i][num].type = RoomType.Treasure;
            }

        }

    }

    //随机添加分支
    void SetBranch()
    {
        for (int i = 0; i < 3; i++)
        {
            int branchNum = Random.Range(0, 4);

            for (int n = 0; n < branchNum; n++)
            {
                int num = Random.Range(0, lines[i].Count);
                while (lines[i][num].IsBranch)
                {
                    num = Random.Range(0, lines[i].Count);
                }
                lines[i][num].SetBranch();
            }

        }
    }

    void SetTranX()
    {

        for (int i = 0; i < 2; i++)
        {

            int num1 = Random.Range(4, 8);
            int num2 = Random.Range(4, 8);

            int num = Random.Range(1, 3);



            while (lines[i][num1].isTranX)
            {
                num1 = Random.Range(4, 8);
            }
            while (lines[i + 1][num2].isTranX)
            {
                num2 = Random.Range(4, 8);
            }

            lines[i][num1].isTranX = true;
            lines[i + 1][num2].isTranX = true;


            switch (num)
            {

                case 1:

                    Room room = new Room();

                    if (i == 0)
                    {
                        tranX12.Add(lines[i][num1]);
                        tranX12.Add(room);
                        tranX12.Add(lines[i + 1][num2]);
                    }
                    else
                    {
                        tranX23.Add(lines[i][num1]);
                        tranX23.Add(room);
                        tranX23.Add(lines[i + 1][num2]);
                    }


                    lines[i][num1].tranXRoom = room;
                    room.prevRoom = lines[i][num1];
                    room.nextRoom = lines[i + 1][num2];
                    lines[i + 1][num2].tranXRoom = room;
                    break;
                case 2:
                    Room room1 = new Room();
                    Room room2 = new Room();


                    if (i == 0)
                    {
                        tranX12.Add(lines[i][num1]);
                        tranX12.Add(room1);
                        tranX12.Add(room2);
                        tranX12.Add(lines[i + 1][num2]);
                    }
                    else
                    {
                        tranX23.Add(lines[i][num1]);
                        tranX23.Add(room1);
                        tranX23.Add(room2);
                        tranX23.Add(lines[i + 1][num2]);
                    }
                    lines[i][num1].tranXRoom = room1;
                    room1.prevRoom = lines[i][num1];
                    room1.nextRoom = room2;
                    room2.prevRoom = room1;

                    lines[i + 1][num2].tranXRoom = room2;
                    break;

                default:
                    break;
            }
        }

    }

    //[ContextMenu("Draw")]
    void Start()
    {
        Init();
        SetStoreAndBonus();


        Vector3[] offsetX = new Vector3[3];
        offsetX[0] = new Vector3(-300, 20, 0);
        offsetX[1] = new Vector3(0, 20, 0);
        offsetX[2] = new Vector3(300, 20, 0);


        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < lines[i].Count; j++)
            {
                Vector3 offsetY = new Vector3(0, 1200 / lines[i].Count, 0);

                lines[i][j].transform.localPosition = (offsetX[i] + offsetY * j);

            }
        }

        SetBranch();

    }

}
