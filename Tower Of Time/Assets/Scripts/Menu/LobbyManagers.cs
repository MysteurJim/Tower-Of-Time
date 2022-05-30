using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class LobbyManagers : MonoBehaviourPunCallbacks
{
    public Text nbr_players;
    public GameObject[] Affiche_Players;
    public string namePlayer;
    
    
    private int players_numbers;

    public override void OnJoinedRoom()
    {
        PhotonNetwork.NickName = $"Player {PhotonNetwork.CurrentRoom.PlayerCount}";
        namePlayer = PhotonNetwork.NickName;
        players_numbers = PhotonNetwork.CurrentRoom.PlayerCount;
        UpdatePlayers();
    }

    public override void OnPlayerEnteredRoom(Player newPlayer)
    {
        players_numbers++;
        UpdatePlayers();
        
        Debug.Log("A player join the Room : " + newPlayer.NickName);
    }

    public override void OnPlayerLeftRoom(Player otherPlayer)
    {
        Affiche_Players[players_numbers - 1].SetActive(false);
        players_numbers--;
        UpdatePlayers();
        Debug.Log("A player left the Room : " + otherPlayer.NickName);
    }

    public void UpdatePlayers()
    {
        if(PhotonNetwork.CurrentRoom == null)
        {
            return;
        }
   
        Affiche_Players[players_numbers-1].SetActive(true);
        Affiche_Players[players_numbers-1].GetComponentInChildren<Text>().text = namePlayer;
      
        nbr_players.text = ($"Waiting for Players {PhotonNetwork.CurrentRoom.PlayerCount}/4");
    }

    public void UpdateNickName(Text text)
    {
        PhotonNetwork.NickName = text.text;
        namePlayer = PhotonNetwork.NickName;
    }

}
