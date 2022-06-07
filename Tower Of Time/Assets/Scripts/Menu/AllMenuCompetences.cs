using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AllMenuCompetences : MonoBehaviour
{
    public MenuCompetences[] menus;

    public void RefreshAll()
    {
        foreach(MenuCompetences m in menus)
        {
            m.RefreshButton();
        }
    }
}
