using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CurrentStatus;

public static class GameLauncher 
{
    public static void LaunchNewGame(MonoBehaviour mono)
    {
        new Map(1);
        Current.Map.Goto("Start");
        Current.Map.Print();
    }
}
