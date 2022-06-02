using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class GodChoice : MonoBehaviour
{
    GameObject player;
    God god;
    public Button button;

    public void Start()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("Player");
        player = g[0];

    }

    public void Sword()
    {
        player.AddComponent(typeof(TestGod));
        player.GetComponent<PlayerController>().god = player.GetComponent<TestGod>();
        god = player.GetComponent<TestGod>();
        god.Setup(player.GetComponent<PlayerController>());
        button.interactable = true;
        player.GetComponent<PlayerController>().barManager.SetMaxHealth(god.HitPoints);
    }

    public void Projectil()
    { 
       
       player.AddComponent(typeof(Demeter));
       player.GetComponent<PlayerController>().god = player.GetComponent<Demeter>();
       god = player.GetComponent<Demeter>();
       god.Setup(player.GetComponent<PlayerController>());
       button.interactable = true;
       player.GetComponent<PlayerController>().barManager.SetMaxHealth(god.HitPoints);
    }

    public void NextRoom()
    {
        PhotonNetwork.LoadLevel("Salle Int Medusa");
    }

}
