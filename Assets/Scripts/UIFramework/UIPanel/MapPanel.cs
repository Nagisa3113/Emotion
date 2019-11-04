using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MapPanel : BasePanel
{
    CanvasGroup canvasGroup;

    public Image playerIcon;
    public Image selectIcon;

    public Room start;
    public Room end;

    public List<Room> l1 = new List<Room>();
    public List<Room> l2 = new List<Room>();
    public List<Room> l3 = new List<Room>();

    public List<List<Room>> lines;

    public Transform t_start;
    public Transform t_end;
    public List<Transform> t_l1 = new List<Transform>();
    public List<Transform> t_l2 = new List<Transform>();
    public List<Transform> t_l3 = new List<Transform>();
    public List<List<Transform>> t_lines;

    void Awake()
    {
        canvasGroup = GetComponent<CanvasGroup>();
        lines = new List<List<Room>> { l1, l2, l3 };
        t_lines = new List<List<Transform>> { t_l1, t_l2, t_l3 };

        //初识化如果放在Start会出现缩放问题
        start = Room.NewRoom();
        start.Type = RoomType.Start;
        start.transform.position = t_start.position;
        end = Room.NewRoom();
        end.Type = RoomType.Boss;
        end.transform.position = t_end.position;


        for (int i = 0; i < t_lines.Count; i++)
        {
            for (int j = 0; j < t_lines[i].Count; j++)
            {
                Room room = Room.NewRoom();
                lines[i].Add(room);
                room.transform.position = t_lines[i][j].position;
            }
        }


        for (int i = 0; i < lines.Count; i++)
        {
            lines[i][0].nearRooms.Add(start);
            start.nearRooms.Add(lines[i][0]);

            for (int j = 0; j < lines[i].Count - 1; j++)
            {
                lines[i][j].nearRooms.Add(lines[i][j + 1]);
            }
            for (int j = 1; j < lines[i].Count; j++)
            {
                lines[i][j].nearRooms.Add(lines[i][j - 1]);
            }

            lines[i][lines[i].Count - 1].nearRooms.Add(end);
            end.nearRooms.Add(lines[i][lines[i].Count - 1]);

            lines[0][2].nearRooms.Add(lines[1][1]);
            lines[0][2].nearRooms.Add(lines[1][2]);
            lines[1][1].nearRooms.Add(lines[0][2]);
            lines[1][1].nearRooms.Add(lines[2][2]);
            lines[1][2].nearRooms.Add(lines[0][2]);
            lines[2][2].nearRooms.Add(lines[1][1]);

            int n = Random.Range(0, lines.Count);
            lines[i][n].Type = i > lines.Count / 2 ? RoomType.Shop : RoomType.Treasure;
        }
        start.GoToRoom(Player.Instance);
    }

    public override void OnEnter(object args = null)
    {
        base.OnEnter();
    }

    public override void OnExit()
    {
        base.OnExit();
        UIManager.Instance.MapPanel = null;
        DestroyImmediate(this.gameObject);
    }

    public override void OnPause()
    {
        base.OnPause();
        //canvasGroup.blocksRaycasts = false;
    }

    public override void OnResume()
    {
        base.OnResume();
        //canvasGroup.blocksRaycasts = true;
    }

    void OnDisable()
    {
        selectIcon.gameObject.GetComponent<Image>().enabled = false;
    }

}
