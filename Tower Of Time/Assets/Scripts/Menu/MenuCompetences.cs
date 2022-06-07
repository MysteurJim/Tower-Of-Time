using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuCompetences : MonoBehaviour
{
    God god;
    PlayerController playerControl;
    Inventory inv;
    public Ability ability;
    public GodChoice godChoice;
    public AllMenuCompetences all;
    //UI
    public Text niveau;
    public Text prix;
    public Button Moins;
    public Button Plus;
    
    public void Start()
    {
        inv = ability.Inventory;
        all.RefreshAll();
    }


    public void Upgrade()
    {
        ability.Upgrade();
        all.RefreshAll();
        godChoice.RefreshInfos();
    }

    public void Downgrade()
    {
        ability.Downgrade();
        all.RefreshAll();
        godChoice.RefreshInfos();
    }

    public void RefreshButton()
    {
        if (ability.Inventory.coinsCount < 3)
        {
            Plus.interactable = false;
        }
        else
        {
            Plus.interactable = true;
        }

        if(ability.Level == 1)
        {
            Moins.interactable = false;
        }
        else
        {
            Moins.interactable = true;
        }

        niveau.text = "Lvl\n" + ability.Level.ToString();
        
    }
}
