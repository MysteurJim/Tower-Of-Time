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
        if (PhotonNetwork.IsConnected)
        {
            PhotonNetwork.JoinLobby();
        }
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
            PhotonNetwork.LoadLevel("ChooseCharacter");
           
        }
        else
        {
            Debug.Log("Connect Online");
            PhotonNetwork.JoinLobby();
            
        }
        
    }

    public override void OnJoinedLobby()
    {
        if (PhotonNetwork.CountOfRooms != 0)
        {
            PhotonNetwork.JoinRoom("Online");

        }
        PhotonNetwork.CreateRoom("Online");
        StartCoroutine(WaitForJoin());
        

    }

    IEnumerator WaitForJoin()
    {
        
        yield return new WaitForSeconds(0.5f);
        PhotonNetwork.LoadLevel("ChooseCharacter");
    }
}
