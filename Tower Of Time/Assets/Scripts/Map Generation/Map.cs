using System.Collections.Generic;
using static System.Math;
using UnityEngine;

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

        Room startRoom = new StartRoom(this, start.Item1, start.Item2, level);

        startRoom.Generate();
        this[start.Item1, start.Item2].SetType(RoomType.StartRoom);

        Room furthest = deadEnds[0];
        int maxDist = DistToBoss(furthest);

        foreach (Room deadEnd in deadEnds)
        {
            int currDist = DistToBoss(deadEnd);
            if (currDist > maxDist)
            {
                furthest = deadEnd;
                maxDist = currDist;
            }
        }

        furthest.SetType(RoomType.BossRoom);
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

        Debug.Log(res);
    }
}
