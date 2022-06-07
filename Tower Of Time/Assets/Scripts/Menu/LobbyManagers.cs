using Photon.Pun;
using Photon.Realtime;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LobbyManagers : MonoBehaviourPunCallbacks
{
    public Text nbr_players;
    public GameObject[] Affiche_Players;
    public string namePlayer;
    public PhotonView pv;
    public Button Start;
    

    private int index_player;

    private int players_numbers;

    

    public override void OnJoinedRoom()
    {
        PhotonNetwork.NickName = $"Player {PhotonNetwork.CurrentRoom.PlayerCount}";
        namePlayer = PhotonNetwork.NickName;
        players_numbers = PhotonNetwork.CurrentRoom.PlayerCount;
        index_player = players_numbers-1;
        Affiche_Players[index_player].GetComponentInChildren<InputField>().interactable = true;
        Affiche_Players[index_player].GetComponentInChildren<Text>().text = namePlayer;
        RPCNickname(Affiche_Players[index_player].GetComponentInChildren<Text>());
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
        for(int i = 0; i < players_numbers;i++)
        {
            Debug.Log($"Room {i} Update");
            Affiche_Players[i].SetActive(true);
            
        }

        nbr_players.text = ($"Waiting for Players {PhotonNetwork.CurrentRoom.PlayerCount}/4");
        if(players_numbers >= 2)
        {
            Start.GetComponent<Button>().interactable = true;
        }
    }

    public void LeaveMulti()
    {
        StartCoroutine(DisconnectAndLoad());
        
    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect();
        Debug.Log("[Multiplayer] - Waiting to disconnect");
        /*
        while (PhotonNetwork.IsConnected)
        {

            yield return null;
        }
        */
        Debug.Log("[Multiplayer] - Disconnected !");

        SceneManager.LoadScene("Menu");
        yield return true;

    }


    public void StartGame()
    {
        if (pv.IsMine)
        {
            pv.RPC("StartForOther", RpcTarget.All);
        }
        
    }

    [PunRPC]
    public void StartForOther()
    {
       
        PhotonNetwork.LoadLevel("ChooseCharacter");
    }

    public void RPCNickname(Text text)
    {
        PhotonNetwork.NickName = text.text;
        namePlayer = PhotonNetwork.NickName;
        pv.RPC("UpdateNickName", RpcTarget.All,text.text,index_player);
        
    }

    [PunRPC]
    public void UpdateNickName(string text,int p)
    {
        
        Debug.Log($"Player {p} change is name for: {text}");
        Affiche_Players[p].GetComponentInChildren<Text>().text = text;

    }

}
