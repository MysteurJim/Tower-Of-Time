using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CurrentStatus;

public static class GameLauncher 
{
    public static void LaunchNewGame()
    {
        new Map(1);
        Current.Map.Goto("Start");
        Current.Map.Print();
    }

    public static void UpOneLevel()
    {
        Debug.Log("Going up!");
        new Map(Current.level + 1);
        Current.Map.Goto("Start");
        Current.Map.Print();
    }
}
