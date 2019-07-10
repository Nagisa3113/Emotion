using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class Map : MonoBehaviour
{
    Room start;
    Room end;

    public List<List<Room>> lines = new List<List<Room>>();

    public List<Room> l0;
    public List<Room> l1;
    public List<Room> l2;

    public List<Room> tranX12 = new List<Room>();
    public List<Room> tranX23 = new List<Room>();

    public Vector3[] offsetX = new Vector3[3];
    public float roomlengthmax;


    public Vector3 branchOffset;
    public float RandomX;
    public float RandomY;


    [ContextMenu("Init")]
    void Init()
    {

        start = Room.NewRoom();
        end = Room.NewRoom();

        end.Type = RoomType.Boss;
        end.transform.localPosition += new Vector3(0, roomlengthmax + 100, 0);

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

        for (int i = 0; i < 3; i++)
        {
            foreach (Room room in lines[i])
            {
                room.Type = RoomType.Enemy;
            }
        }

        int Storenum = Random.Range(1, 3);
        int Treasurenum = Random.Range(1, 3);

        for (int i = 0; i < 3; i++)
        {

            for (int n = 0; n < Storenum; n++)
            {
                int num = Random.Range(0, lines[i].Count);
                lines[i][num].Type = RoomType.Store;
            }
            for (int n = 0; n < Treasurenum; n++)
            {
                int num = Random.Range(0, lines[i].Count);
                lines[i][num].Type = RoomType.Treasure;
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

                Room room = Room.NewRoom();
                room.transform.position = lines[i][num].transform.position;

                lines[i][num].transform.localPosition -= branchOffset;

                room.transform.localPosition += branchOffset;

                lines[i][num].isBranchMiddle = true;
                lines[i][num].prevRoom.isBranchStart = true;
                lines[i][num].prevRoom.EnextRoom = room;
                lines[i][num].nextRoom.isBranchEnd = true;
                lines[i][num].nextRoom.EprevRoom = room;

                room.nextRoom = lines[i][num].nextRoom;
                room.prevRoom = lines[i][num].prevRoom;
            }

        }
    }

    //添加转换路径
    void SetTranX()
    {

        for (int i = 0; i < 2; i++)
        {

            int num1 = Random.Range(4, 8);
            int num2 = Random.Range(4, 8);
            int num = Random.Range(1, 3);


            while (lines[i][num1].isTranX || lines[i][num1].isBranchMiddle)
            {
                num1 = Random.Range(4, 8);
            }
            while (lines[i + 1][num2].isTranX || lines[i + 1][num2].isBranchMiddle)
            {
                num2 = Random.Range(4, 8);
            }

            lines[i][num1].isTranX = true;
            lines[i + 1][num2].isTranX = true;


            switch (num)
            {

                case 1:
                    Room room = Room.NewRoom();
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
                    Room room1 = Room.NewRoom();
                    Room room2 = Room.NewRoom();

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

        Vector3 offset;

        offset = tranX12[tranX12.Count - 1].transform.position - tranX12[0].transform.position;
        offset /= (tranX12.Count - 1);

        for (int i = 1; i < tranX12.Count - 1; i++)
        {
            tranX12[i].transform.position = tranX12[0].transform.position + i * offset;
        }


        offset = tranX23[tranX23.Count - 1].transform.position - tranX23[0].transform.position;
        offset /= (tranX23.Count - 1);

        for (int i = 1; i < tranX23.Count - 1; i++)
        {
            tranX23[i].transform.position = tranX23[0].transform.position + i * offset;
        }



    }

    void Start()
    {
        Init();
        SetStoreAndBonus();

        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < lines[i].Count; j++)
            {
                Vector3 offsetY = new Vector3(0, roomlengthmax / lines[i].Count, 0);

                lines[i][j].transform.localPosition = (offsetX[i] + offsetY * j);

            }
        }

        SetBranch();
        SetTranX();
        RandomMove();
        Draw();
    }

    //画线
    void Draw()
    {



        foreach (Room room in GameObject.Find("Rooms").GetComponentsInChildren<Room>())
        {
            if (room.nextRoom != null)
            {

                LineRenderer lineRenderer = GameObject.Instantiate(Resources.Load("Line") as GameObject, GameObject.Find("Lines").transform).GetComponent<LineRenderer>();
                lineRenderer.positionCount = 2;


                Vector3 v1 = Camera.main.ScreenToWorldPoint(room.transform.position);
                v1.z = 0;
                Vector3 v2 = Camera.main.ScreenToWorldPoint(room.nextRoom.transform.position);
                v2.z = 0;
                lineRenderer.SetPosition(0, v1);
                lineRenderer.SetPosition(1, v2);
            }
            if (room.EnextRoom != null)
            {
                LineRenderer lineRenderer = GameObject.Instantiate(Resources.Load("Line") as GameObject, GameObject.Find("Lines").transform).GetComponent<LineRenderer>();
                lineRenderer.positionCount = 2;

                Vector3 v1 = Camera.main.ScreenToWorldPoint(room.transform.position);
                v1.z = 0;
                Vector3 v2 = Camera.main.ScreenToWorldPoint(room.EnextRoom.transform.position);
                v2.z = 0;
                lineRenderer.SetPosition(0, v1);
                lineRenderer.SetPosition(1, v2);
            }
            if (room.tranXRoom != null)
            {
                LineRenderer lineRenderer = GameObject.Instantiate(Resources.Load("Line") as GameObject, GameObject.Find("Lines").transform).GetComponent<LineRenderer>();
                lineRenderer.positionCount = 2;

                Vector3 v1 = Camera.main.ScreenToWorldPoint(room.transform.position);
                v1.z = 0;
                Vector3 v2 = Camera.main.ScreenToWorldPoint(room.tranXRoom.transform.position);
                v2.z = 0;
                lineRenderer.SetPosition(0, v1);
                lineRenderer.SetPosition(1, v2);
            }
        }


    }

    //房间在地图上随机偏移
    void RandomMove()
    {
        foreach (Room room in GameObject.Find("Rooms").GetComponentsInChildren<Room>())
        {

            float x = Random.Range(-RandomX, RandomX);
            float y = Random.Range(-RandomY, RandomY);
            room.transform.position += new Vector3(x, y, 0);
        }
    }


}
