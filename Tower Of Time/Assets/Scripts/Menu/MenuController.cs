using Photon.Pun;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuController : MonoBehaviour
{
    public GameObject acceuil;
    public GameObject menu_fond;
    public GameObject menu;
    public GameObject settings;

    public void Hide()
    {
        acceuil.SetActive(false);
        menu.SetActive(true);
        menu_fond.SetActive(true);
    }

    public void ShowSettings()
    {
        menu.SetActive(false);
        settings.SetActive(true);
    }

    public void PlaySolo()
    {
        SceneManager.LoadScene("ChooseCharacter");
    }

    public void Close()
    {
        Application.Quit();
    }

    
}
