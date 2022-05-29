using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ConnectToServer : MonoBehaviourPunCallbacks
{
    

    public void ConnectOnline()
    {
        PhotonNetwork.ConnectUsingSettings();
        
    }

    public void ConnectOffline()
    {
        PhotonNetwork.OfflineMode = true;
        
    }

    public override void OnConnectedToMaster()
    {
        if (PhotonNetwork.OfflineMode)
        {
            PhotonNetwork.CreateRoom("offline");
            PhotonNetwork.LoadLevel("Salle Int Medusa");
        }
        else
        {
            PhotonNetwork.JoinLobby();
        }
    }

    public override void OnJoinedLobby()
    {
        SceneManager.LoadScene("Lobby");
    }
}
