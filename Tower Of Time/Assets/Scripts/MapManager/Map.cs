using System.Collections.Generic;
using static System.Math;
using UnityEngine;
using CurrentStatus;
using System.Linq;

public class Map
{
    private int level;
    private Room[,] floor;
    private (int, int) start = (Size / 2, Size / 2);
    private (int, int) boss;
    private List<Room> deadEnds;

    public int Level => level;
    public Room[,] Floor => floor;
    public (int, int) Start => start;
    public (int, int) Boss => boss;
    public List<Room> DeadEnds => deadEnds;
    public const int Size = 31;

    public int DistToBoss(int x, int y) => Abs(boss.Item1 - x) + Abs(boss.Item1 - y);
    public int DistToBoss(Room room) => Abs(boss.Item1 - room.x) + Abs(boss.Item1 - room.y);
    public int DistToStart(int x, int y) => Abs(start.Item1 - x) + Abs(start.Item1 - y);
    public int DistToStart(Room room) => Abs(start.Item1 - room.x) + Abs(start.Item1 - room.y);
    public Room this[int x, int y]
    { 
        get => this.floor[x,y];
        set => this.floor[x,y] = value;
    }

    public Map(int level)
    {
        this.level = level;
        floor = new Room[Size, Size];
        deadEnds = new List<Room>();
        
        Current.Map = this;
        Current.x = start.Item1;
        Current.y = start.Item2;

        Room startRoom = new StartRoom(this, start.Item1, start.Item2, level);

        

        startRoom.Generate();
        this[start.Item1, start.Item2] = startRoom;

        Room furthest = deadEnds[0];
        int maxDist = DistToStart(furthest);

        foreach (Room deadEnd in deadEnds.Where<Room>(room => this[room.Pos.Item1, room.Pos.Item2] == room))
        {
            int currDist = DistToStart(deadEnd);
            if (currDist > maxDist)
            {
                furthest = deadEnd;
                maxDist = currDist;
            }
        }

        furthest.SetType(RoomType.BossRoom);
        boss = (furthest.x, furthest.y);

        Populate();
    }

    public void Populate()
    {
        for (int i = 0; i < Size * Size; i++)
            this[i / Size, i % Size]?.Populate();
    }

    public void Print()
    {
        string res = "";

        for (int i = 0; i < Size; i++)
        {
            for (int j = 0; j < Size; j++)
            {
                res += "|" + (this[i, j]?.ToString() ?? " ");
            }
            res += "|\n";
        }

        Debug.Log(res + $"\n{Current.x},{Current.y}");
    }
    public void Goto(string direction)   
    {
        switch (direction.ToLower())
        {
            case "haut":
                Current.y -= 1;
                break;

            case "bas":
                Current.y += 1;
                break;

            case "gauche":
                Current.x -= 1;
                break;

            case "droite":
                Current.x += 1;
                break;

            case "start":
                Current.x = start.Item1;
                Current.y = start.Item2;
                break;

            default :
                throw new System.Exception("Shouldna done that");
        }

        MonoBehaviour mono = GameObject.FindGameObjectWithTag("Player").GetComponent<PlayerController>();
        this[Current.x, Current.y].Goto(mono);
    }
}