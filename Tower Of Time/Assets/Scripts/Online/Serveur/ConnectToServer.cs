using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using Photon.Realtime;
using UnityEngine.UI;
using UnityEngine.SceneManagement;


public class ConnectToServer : MonoBehaviourPunCallbacks
{
    

    public void ConnectOnline()
    {
        PhotonNetwork.ConnectUsingSettings();
        Debug.Log("Connecting...");
    }

    public void ConnectOffline()
    {
        PhotonNetwork.OfflineMode = true;
        
    }

    public override void OnConnectedToMaster()
    {
        
        if (PhotonNetwork.OfflineMode)
        {
            Debug.Log("Connect Offline");
            PhotonNetwork.CreateRoom("offline");
            PhotonNetwork.LoadLevel("Salle Int Medusa");
           
        }
        else
        {
            Debug.Log("Connect Online");
            PhotonNetwork.JoinLobby();
            
        }
        
    }

    public override void OnJoinedLobby()
    {
        if(PhotonNetwork.CountOfRooms == 0)
        {
            PhotonNetwork.CreateRoom("Online");

        }
        else
        {
            PhotonNetwork.JoinRoom("Online");
        }

        
        
        SceneManager.LoadScene("Lobby");

    }
}
