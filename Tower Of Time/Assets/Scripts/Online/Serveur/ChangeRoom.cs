using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ChangeRoom : MonoBehaviourPunCallbacks
{
    public string scene;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if(collision.tag == "Player")
        PhotonNetwork.LoadLevel(scene);
    }
}
