using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using System.Linq;

public class GodChoice : MonoBehaviour
{
    GameObject player;
    God god;
    public Button button;

    public Dropdown save;
    public Text piece;
    public Text hp;
    public Text salle;

    public Text save_name;

    

    List<string> saves;
    private PlayerController playerControl;

    public void Start()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("Player");
        player = g[0];
        playerControl = player.GetComponent<PlayerController>();

        try
        {
            saves = DataManagers.ReadSave();
        }
        catch (Exception e)
        {
            Debug.Log(e.Message);
        }
        saves.Add("NewPlayer");
        save.AddOptions(saves);
        LoadDatas();
        Debug.Log(Application.persistentDataPath);
    }


    //JE GERE LES DATAS FRERO

    public void CreateNewDatas()
    {
        Debug.Log("Cr�ation d'un nouveau fichier");
        Datas data = new Datas();
        data.nbr_piece = 0;
        data.hit_points = 100;
        data.current_etage = "Etage Medusa";
        DataManagers.Save(data, save_name.text + ".ToT");
        salle.text = data.current_etage;
        piece.text = data.nbr_piece.ToString();
        hp.text = data.hit_points.ToString();
    }

    public void LoadDatas()
    {
       
        try
        {
            Datas data = (Datas)DataManagers.Load(save_name.text + ".ToT");
            salle.text = data.current_etage;
            piece.text = data.nbr_piece.ToString();
            hp.text = data.hit_points.ToString();
            
            Debug.Log($"Joueur {save_name.text}");
        }
        catch
        {
            Debug.Log("Joueur Inconnu");
            CreateNewDatas();
        }
        
    }

    public void Sword()
    {
        player.AddComponent(typeof(Zeus));
        playerControl.god = player.GetComponent<Zeus>();
        god = player.GetComponent<Zeus>();
        god.Setup(playerControl);
        button.interactable = true;
        playerControl.barManager.SetMaxHealth(god.HitPoints);
    }

    public void Projectil()
    { 
       
       player.AddComponent(typeof(Demeter));
       playerControl.god = player.GetComponent<Demeter>();
       god = player.GetComponent<Demeter>();
       god.Setup(playerControl);
       button.interactable = true;
       playerControl.barManager.SetMaxHealth(god.HitPoints);
    }

    public void NextRoom()
    {
        PhotonNetwork.NickName = save_name.text;
        playerControl.loadDatas();
        playerControl.WithGod = true;/*
        if(playerControl.CurrentRooms == "Etage Medusa")
        {
            PhotonNetwork.LoadLevel("Salle Int Medusa");
        }
        else
        {
            PhotonNetwork.LoadLevel("Salle Int Minotaure");

        }*/

        GameLauncher.LaunchNewGame();
    }

    public void NextRoomOnline()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }

}
