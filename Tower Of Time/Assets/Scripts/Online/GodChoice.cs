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
    public Button Dieu1;
    public Button Dieu2;

    public Dropdown save;
    public Text piece;
    public Text hp;
    public Text salle;
    public Text dead;
    public Text vie;
   

    public Text save_name;
    public InputField inputfiel;

    

    List<string> saves;
    private PlayerController playerControl;

    public void Start()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("Player");
        player = g[0];
        playerControl = player.GetComponent<PlayerController>();
        RefreshSave();
    }


    //JE GERE LES DATAS FRERO

    public void CreateNewDatas()
    {
        Debug.Log("Cr�ation d'un nouveau fichier");
        Datas data = new Datas();
        data.nbr_piece = 0;
        data.hit_points = 100;
        data.dead = 0;
        data.secondChance = 0;
        data.current_etage = "Etage Medusa";
        DataManagers.Save(data, save_name.text + ".ToT");
        salle.text = data.current_etage;
        piece.text = data.nbr_piece.ToString();
        hp.text = data.hit_points.ToString();
        dead.text = data.dead.ToString();
        vie.text = data.secondChance.ToString();
    }

    public void LoadDatas()
    {
       
        try
        {
            Datas data = (Datas)DataManagers.Load(save_name.text + ".ToT");
            salle.text = data.current_etage;
            piece.text = data.nbr_piece.ToString();
            hp.text = data.hit_points.ToString();
            dead.text = data.dead.ToString();
            vie.text = data.secondChance.ToString();
            Debug.Log($"Joueur {save_name.text}");
        }
        catch
        {
            Debug.Log("Joueur Inconnu");
            CreateNewDatas();
            Edit();
            
        }
        
    }

    public void Edit()
    {
        
        save_name.gameObject.SetActive(false);
        inputfiel.gameObject.SetActive(true);
    }

    public void Rename()
    {
        DataManagers.Rename(save_name.text+".ToT",inputfiel.textComponent.text+".ToT");
        save_name.gameObject.SetActive(true);
        inputfiel.gameObject.SetActive(false);
        save_name.text = inputfiel.textComponent.text;
        inputfiel.text = "";
        RefreshSave();
        button.interactable = false;
    }

    public void Quit()
    {
        save_name.gameObject.SetActive(true);
        inputfiel.gameObject.SetActive(false);
        RefreshSave();
    }

    public void Delete()
    {
        DataManagers.Delete(save_name.text+".ToT");
        RefreshSave();
    }

    public void RefreshSave()
    {
        save.ClearOptions();
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
        
    }

    public void Sword()
    {
        player.AddComponent(typeof(Zeus));
        playerControl.god = player.GetComponent<Zeus>();
        god = player.GetComponent<Zeus>();
        god.Setup(playerControl);
        button.interactable = true;
        playerControl.barManager.SetMaxHealth(god.HitPoints);
        Dieu1.interactable = false;
        Dieu2.interactable = false;
    }

    public void Projectil()
    { 
       
       player.AddComponent(typeof(Demeter));
       playerControl.god = player.GetComponent<Demeter>();
       god = player.GetComponent<Demeter>();
       god.Setup(playerControl);
       button.interactable = true;
       playerControl.barManager.SetMaxHealth(god.HitPoints);
        Dieu1.interactable = false;
        Dieu2.interactable = false;
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
