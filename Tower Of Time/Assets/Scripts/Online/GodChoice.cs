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
    

    List<string> saves;
    private PlayerController playerControl;

    public void Start()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("Player");
        player = g[0];
        playerControl = player.GetComponent<PlayerController>();

        saves = DataManagers.ReadSave();
        saves.Add("NewPlayer");
        save.AddOptions(saves);
        LoadDatas();
    }


    //JE GERE LES DATAS FRERO

    public void CreateNewDatas()
    {
        Debug.Log("Création d'un nouveau fichier");
        Datas data = new Datas();
        data.nbr_piece = 0;
        data.hit_points = 100;
        data.current_etage = "Etage Medusa";
        DataManagers.Save(data, save.GetComponentInChildren<Text>().text + ".ToT");
        salle.text = data.current_etage;
        piece.text = data.nbr_piece.ToString();
        hp.text = data.hit_points.ToString();
    }

    public void LoadDatas()
    {
        Debug.Log(Application.persistentDataPath);
        try
        {
            Datas data = (Datas)DataManagers.Load(save.GetComponentInChildren<Text>().text + ".ToT");
            salle.text = data.current_etage;
            piece.text = data.nbr_piece.ToString();
            hp.text = data.hit_points.ToString();
        }
        catch
        {
            Debug.Log("Joueur Inconnu");
            CreateNewDatas();
        }
        
    }

    public void Sword()
    {
        player.AddComponent(typeof(TestGod));
        playerControl.god = player.GetComponent<TestGod>();
        god = player.GetComponent<TestGod>();
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
        PhotonNetwork.NickName = save.GetComponentInChildren<Text>().text;
        playerControl.loadDatas();
        playerControl.WithGod = true;
        if(playerControl.CurrentRooms == "Etage Medusa")
        {
            PhotonNetwork.LoadLevel("Salle Int Medusa");
        }
        else
        {
            PhotonNetwork.LoadLevel("Salle Int Minotaure");

        }
        
    }

    public void NextRoomOnline()
    {
        PhotonNetwork.LoadLevel("Lobby");
    }

}
