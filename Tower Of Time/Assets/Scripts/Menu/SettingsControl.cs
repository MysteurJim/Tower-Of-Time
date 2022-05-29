using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class SettingsControl : MonoBehaviourPunCallbacks
{
    public Dropdown resolutionDropdown;
    public GameObject menu;
    public GameObject player;

    Resolution[] resolutions;

    void Start()
    {
        resolutions = Screen.resolutions;

        resolutionDropdown.ClearOptions();

        List<string> options = new List<string>();

        int currentResolutionIndex = 0;
        for(int i = 0; i < resolutions.Length; i++)
        {
            string option = resolutions[i].width + " x " + resolutions[i].height;
            options.Add(option);

            if( resolutions[i].width == Screen.currentResolution.width && resolutions[i].height == Screen.currentResolution.height)
            {
                currentResolutionIndex = i;
            }
        }

        resolutionDropdown.AddOptions(options);
        resolutionDropdown.value = currentResolutionIndex;
        resolutionDropdown.RefreshShownValue();

        Time.timeScale = 1;
    }

    public void SetResolution (int resolutionIndex)
    {
        Resolution resolution = resolutions[resolutionIndex];
        Screen.SetResolution(resolution.width,resolution.height,Screen.fullScreen);
    }

    public void SetQuality (int qualityIndex)
    {
        Debug.Log("Change Quality");
        QualitySettings.SetQualityLevel(qualityIndex);
    }

    public void SetFullscreen (bool isFullscreen)
    {
        Screen.fullScreen = isFullscreen;
    }

    public void Close()
    {
        this.gameObject.SetActive(false);
  
        if( this.gameObject.scene.name == "Menu")
        {
            menu.SetActive(true);
        }
        Time.timeScale = 1;
        
    }

    public void DisconnectPlayer()
    {
        
        StartCoroutine(DisconnectAndLoad());
    }

    IEnumerator DisconnectAndLoad()
    {
        PhotonNetwork.Disconnect();
        while (PhotonNetwork.IsConnected)
        {
            Debug.Log("[Multiplayer] - Waiting to disconnect");
            yield return null;
        }
        Debug.Log("[Multiplayer] - Disconnected !");

        SceneManager.LoadScene("Menu");

    }




}
