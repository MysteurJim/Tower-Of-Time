using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutoriel : MonoBehaviour
{
    public GameObject Dialogue;
    DialogueMangaers dialogueManagers;
    GameObject player;
    God god;
    PlayerController playerControl;

    public void Start()
    {
        GameObject[] g = GameObject.FindGameObjectsWithTag("Player");
        player = g[0];
        playerControl = player.GetComponent<PlayerController>();
        dialogueManagers = Dialogue.GetComponent<DialogueMangaers>();
        dialogueManagers.Active("WESH LE FRERO TU VAS BIEN ?/n Appuie sur les flèches dirrectionnel pour bouger et clique pour attaquer avec ton épée !");

        player.AddComponent(typeof(Demeter));
        playerControl.god = player.GetComponent<Demeter>();
        god = player.GetComponent<Demeter>();
        god.Setup(playerControl);
        playerControl.barManager.SetMaxHealth(god.HitPoints);
        playerControl.WithGod = true;
    }

}
