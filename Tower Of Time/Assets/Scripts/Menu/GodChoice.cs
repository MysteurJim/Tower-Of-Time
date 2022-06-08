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

    public MenuCompetences Hammer;
    public MenuCompetences Feu;
    public MenuCompetences Speed;
    public MenuCompetences Eclair;

    public GameObject MenuZeus;
    

    List<string> saves;
    private PlayerController playerControl;

    public void Start()
    {
        StartCoroutine(WaitForStart());
       
    }

    IEnumerator WaitForStart()
    {
        yield return new WaitForSeconds(0.2f);
        GameObject g = GameObject.FindGameObjectWithTag("Player");
        player = g;
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
        data.level_ability = "1/1/1/1/1/";
        data.GodChoose = false;
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
            if (data.GodChoose)
            {
                StartCoroutine(WaitForSomething(data.god));
            }
            
            Debug.Log($"Joueur {save_name.text}");
        }
        catch
        {
            Debug.Log("Joueur Inconnu");
            UnselectedGod();
            CreateNewDatas();
            Edit();
            
        }
        Dieu1.interactable = true;
        Dieu2.interactable = true;
        MenuZeus.GetComponent<AllMenuCompetences>().RefreshAll();
    }

    IEnumerator WaitForSomething(string god)
    {
        yield return new WaitForSeconds(0.3f);
        switch (god)
        {
            case "Zeus":
                Zeus();
                break;
            default:
                Projectil();
                break;
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

    public void RefreshInfos()
    {

        piece.text = playerControl.inventory.coinsCount.ToString();
    }

    public void Zeus()
    {
        player.AddComponent(typeof(Zeus));
        playerControl.god = player.GetComponent<Zeus>();
        god = player.GetComponent<Zeus>();
        god.Setup(playerControl);
        button.interactable = true;
        playerControl.barManager.SetMaxHealth(god.HitPoints);
        Dieu2.interactable = false;
        MenuZeus.SetActive(true);

        Hammer.ability = god.GetComponent<StunZone>();
        Speed.ability = god.GetComponent<BoostVitesse>();
        Feu.ability = god.GetComponent<Dotfeu>();
        Eclair.ability = god.GetComponent<Eclair>();

        PhotonNetwork.NickName = save_name.text;
        playerControl.loadDatas();
        MenuZeus.GetComponent<AllMenuCompetences>().RefreshAll();
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
        PhotonNetwork.NickName = save_name.text;
        playerControl.loadDatas();
    }

    public void UnselectedGod()
    {
        Destroy(player.GetComponent<Zeus>());
        Destroy(player.GetComponent<Demeter>());
        MenuZeus.SetActive(false);
    }

    public void NextRoom()
    {
        
        
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
