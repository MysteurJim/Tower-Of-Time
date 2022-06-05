using System.Collections.Generic;
using static System.Activator;
using static System.Math;
using UnityEngine;

using static RoomTemplates;

public abstract class RoomTemplates
{
    public static System.Type[] UpRooms = 
    {
        typeof(UpDown),
        typeof(UpLeft),
        typeof(UpRight)
    };
    
    public static System.Type[] DownRooms = 
    {
        typeof(UpDown),
        typeof(DownLeft),
        typeof(DownRight)
    };

    public static System.Type[] LeftRooms = 
    {
        typeof(UpLeft),
        typeof(DownLeft),
        typeof(LeftRight)
    };

    public static System.Type[] RightRooms = 
    {
        typeof(UpRight),
        typeof(DownRight),
        typeof(LeftRight)
    };

    public Dictionary<string, System.Type> DeadEnds = new Dictionary<string, System.Type>
    {
        {"Up" , typeof(Up)},
        {"Down", typeof(Down)},
        {"Left" , typeof(Left)},
        {"Right", typeof(Right)}
    };

    public const int MaxAttempts = 10;
}

public class Up : Room
{
    public Up(Map map, int x, int y, int level, RoomType type = RoomType.Intermediate) 
        : base (map, x, y, level, type)
    {
        map.DeadEnds.Add(this);
    }
}

public class Down : Room
{
    public Down(Map map, int x, int y, int level, RoomType type = RoomType.Intermediate) 
        : base (map, x, y, level, type)
    {
        map.DeadEnds.Add(this);
    }
}

public class Left : Room
{
    public Left(Map map, int x, int y, int level, RoomType type = RoomType.Intermediate) 
        : base (map, x, y, level, type)
    {
        map.DeadEnds.Add(this);
    }
}

public class Right : Room
{
    public Right(Map map, int x, int y, int level, RoomType type = RoomType.Intermediate) 
        : base (map, x, y, level, type)
    {
        map.DeadEnds.Add(this);
    }
}

public class UpDown : Room
{
    public UpDown(Map map, int x, int y, int level, RoomType type = RoomType.Intermediate) 
        : base (map, x, y, level, type)
    {}

    public override void Generate(int depth)
    {
        bool fromUp = map[x, y - 1] != null;
        Room next = null;
        int attempts = 0;

        while ((next?.isFull ?? true) && attempts < MaxAttempts)
        {
            bool nextIsDeadEnd = Random.Range(0f,1f) < ((depth - minDepth) / (float)(maxDepth - minDepth)) + Max(0, 2 * attempts / MaxAttempts - 1);
            attempts++;

            if (fromUp)
                if (nextIsDeadEnd)
                    next = new Up(map, x, y + 1, level);
                else
                    next = (Room)CreateInstance(UpRooms[Random.Range(0, UpRooms.Length)], new object[]{map, x, y + 1, level, RoomType.Intermediate});
            else
                if(nextIsDeadEnd)
                    next = new Down(map, x, y - 1, level);
                else
                    next = (Room)CreateInstance(DownRooms[Random.Range(0, DownRooms.Length)], new object[]{map, x, y - 1, level, RoomType.Intermediate});
        }

        next.Generate(depth + 1);
    }

    public new bool isFull => map[x, y - 1] != null && map[x, y + 1] != null;
}

public class UpLeft : Room
{
    public UpLeft(Map map, int x, int y, int level, RoomType type = RoomType.Intermediate) 
        : base (map, x, y, level, type)
    {}

    public override void Generate(int depth)
    {
        bool fromUp = map[x, y - 1] != null;
        Room next = null;
        int attempts = 0;

        while ((next?.isFull ?? true) && attempts < MaxAttempts)
        {
            bool nextIsDeadEnd = Random.Range(0f,1f) < ((depth - minDepth) / (float)(maxDepth - minDepth)) + Max(0, 2 * attempts / MaxAttempts - 1);
            attempts++;

            if (fromUp)
                if (nextIsDeadEnd)
                    next = new Right(map, x - 1, y, level);
                else
                    next = (Room)CreateInstance(RightRooms[Random.Range(0, RightRooms.Length)], new object[]{map, x - 1, y, level, RoomType.Intermediate});
            else
                if(nextIsDeadEnd)
                    next = new Down(map, x, y - 1, level);
                else
                    next = (Room)CreateInstance(DownRooms[Random.Range(0, DownRooms.Length)], new object[]{map, x, y - 1, level, RoomType.Intermediate});
        }

        next.Generate(depth + 1);
    }

    public new bool isFull => map[x, y - 1] != null && map[x - 1, y] != null;
}

public class UpRight : Room
{
    public UpRight(Map map, int x, int y, int level, RoomType type = RoomType.Intermediate) 
        : base (map, x, y, level, type)
    {}

    public override void Generate(int depth)
    {
        bool fromUp = map[x, y - 1] != null;
        Room next = null;
        int attempts = 0;

        while ((next?.isFull ?? true) && attempts < MaxAttempts)
        {
            bool nextIsDeadEnd = Random.Range(0f,1f) < ((depth - minDepth) / (float)(maxDepth - minDepth)) + Max(0, 2 * attempts / MaxAttempts - 1);
            attempts++;

            if (fromUp)
                if (nextIsDeadEnd)
                    next = new Left(map, x + 1, y, level);
                else
                    next = (Room)CreateInstance(LeftRooms[Random.Range(0, LeftRooms.Length)], new object[]{map, x + 1, y, level, RoomType.Intermediate});
            else
                if(nextIsDeadEnd)
                    next = new Down(map, x, y - 1, level);
                else
                    next = (Room)CreateInstance(DownRooms[Random.Range(0, DownRooms.Length)], new object[]{map, x, y - 1, level, RoomType.Intermediate});
        }

        next.Generate(depth + 1);
    }

    public new bool isFull => map[x, y - 1] != null && map[x + 1, y] != null;
}

public class DownLeft : Room
{
    public DownLeft(Map map, int x, int y, int level, RoomType type = RoomType.Intermediate) 
        : base (map, x, y, level, type)
    {}

    public override void Generate(int depth)
    {
        bool fromDown = map[x, y + 1] != null;
        Room next = null;
        int attempts = 0;

        while ((next?.isFull ?? true) && attempts < MaxAttempts)
        {
            bool nextIsDeadEnd = Random.Range(0f,1f) < ((depth - minDepth) / (float)(maxDepth - minDepth)) + Max(0, 2 * attempts / MaxAttempts - 1);
            attempts++;

            if (fromDown)
                if (nextIsDeadEnd)
                    next = new Right(map, x - 1, y, level);
                else
                    next = (Room)CreateInstance(RightRooms[Random.Range(0, RightRooms.Length)], new object[]{map, x - 1, y, level, RoomType.Intermediate});
            else
                if(nextIsDeadEnd)
                    next = new Up(map, x, y + 1, level);
                else
                    next = (Room)CreateInstance(UpRooms[Random.Range(0, UpRooms.Length)], new object[]{map, x, y + 1, level, RoomType.Intermediate});
        }

        next.Generate(depth + 1);
    }

    public new bool isFull => map[x, y + 1] != null && map[x - 1, y] != null;
}

public class DownRight : Room
{
    public DownRight(Map map, int x, int y, int level, RoomType type = RoomType.Intermediate) 
        : base (map, x, y, level, type)
    {}

    public override void Generate(int depth)
    {
        bool fromDown = map[x, y + 1] != null;
        Room next = null;
        int attempts = 0;

        while ((next?.isFull ?? true) && attempts < MaxAttempts)
        {
            bool nextIsDeadEnd = Random.Range(0f,1f) < ((depth - minDepth) / (float)(maxDepth - minDepth)) + Max(0, 2 * attempts / MaxAttempts - 1);
            attempts++;

            if (fromDown)
                if (nextIsDeadEnd)
                    next = new Left(map, x + 1, y, level);
                else
                    next = (Room)CreateInstance(LeftRooms[Random.Range(0, LeftRooms.Length)], new object[]{map, x + 1, y, level, RoomType.Intermediate});
            else
                if(nextIsDeadEnd)
                    next = new Up(map, x, y + 1, level);
                else
                    next = (Room)CreateInstance(UpRooms[Random.Range(0, UpRooms.Length)], new object[]{map, x, y + 1, level, RoomType.Intermediate});
        }

        next.Generate(depth + 1);
    }

    public new bool isFull => map[x, y + 1] != null && map[x + 1, y] != null;
}

public class LeftRight : Room
{
    public LeftRight(Map map, int x, int y, int level, RoomType type = RoomType.Intermediate) 
        : base (map, x, y, level, type)
    {}

    public override void Generate(int depth)
    {
        bool fromLeft = map[x - 1, y] != null;
        Room next = null;
        int attempts = 0;

        while ((next?.isFull ?? true) && attempts < MaxAttempts)
        {
            bool nextIsDeadEnd = Random.Range(0f,1f) < ((depth - minDepth) / (float)(maxDepth - minDepth)) + Max(0, 2 * attempts / MaxAttempts - 1);
            attempts++;

            if (fromLeft)
                if (nextIsDeadEnd)
                    next = new Left(map, x + 1, y, level);
                else
                    next = (Room)CreateInstance(LeftRooms[Random.Range(0, LeftRooms.Length)], new object[]{map, x + 1, y, level, RoomType.Intermediate});
            else
                if(nextIsDeadEnd)
                    next = new Right(map, x - 1, y, level);
                else
                    next = (Room)CreateInstance(RightRooms[Random.Range(0, RightRooms.Length)], new object[]{map, x - 1, y, level, RoomType.Intermediate});
        }

        next.Generate(depth + 1);
    }

    public new bool isFull => map[x - 1, y] != null && map[x + 1, y] != null;
}

public class StartRoom : Room
{
    public StartRoom(Map map, int x, int y, int level, RoomType type = RoomType.StartRoom) 
        : base (map, x, y, level, type)
    {}

    public override void Generate(int depth = 0)
    {
        // Up
        Room startUp = (Room)CreateInstance(DownRooms[Random.Range(0, DownRooms.Length)], new object[]{map, map.Start.Item1, map.Start.Item2 - 1, level, RoomType.Intermediate});
        startUp.Generate(depth + 1);

        // Down
        Room startDown = (Room)CreateInstance(UpRooms[Random.Range(0, UpRooms.Length)], new object[]{map, map.Start.Item1, map.Start.Item2 + 1, level, RoomType.Intermediate});
        startDown.Generate(depth + 1);

        // Left
        Room startLeft = (Room)CreateInstance(RightRooms[Random.Range(0, RightRooms.Length)], new object[]{map, map.Start.Item1 - 1, map.Start.Item2, level, RoomType.Intermediate});
        startLeft.Generate(depth + 1);

        // Roght
        Room startRight = (Room)CreateInstance(LeftRooms[Random.Range(0, LeftRooms.Length)], new object[]{map, map.Start.Item1 + 1, map.Start.Item2, level, RoomType.Intermediate});
        startRight.Generate(depth + 1);
    }
}