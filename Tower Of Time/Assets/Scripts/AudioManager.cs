using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class AudioManager : MonoBehaviour
{
    public Music[] MusicList;
    public AudioSource audioSource;
    private Dictionary<string, AudioClip> playlist;
    private string playing;
    
    void Awake()
    {  
        DontDestroyOnLoad(gameObject);
        playlist = new Dictionary<string, AudioClip>();
        Array.ForEach<Music>(MusicList, music => playlist.Add(music.name, music.clip));
        TryChangeClip("Menu");
    }

    public void TryChangeClip(string room)
    {
        if (playing == null)
        {
            audioSource.clip = playlist[room];
            audioSource.Play();
            playing = room;
        }
        else if (room != playing)
        {
            if (room == "Menu")
                Destroy(gameObject);
            else
            {
                audioSource.clip = playlist[room];
                audioSource.Play();
                playing = room;
            }
        }

        StartCoroutine(WaitForSceneChange());
    }

    public IEnumerator WaitForSceneChange()
    {
        Scene current = SceneManager.GetActiveScene();

        WaitForSeconds RefreshRate = new WaitForSeconds(.1f);

        while (SceneManager.GetActiveScene() == current)
            yield return RefreshRate;

        string next = SceneManager.GetActiveScene().name;

        try
        {
            switch (next.Split()[1])
            {
                case "Int":
                    TryChangeClip("Int");   
                    break;

                case "Boss":
                    TryChangeClip("Boss");
                    break;
                
                default:
                    TryChangeClip("Menu");
                    break;
            }
        }
        catch (Exception e)
        {
            TryChangeClip("Menu");
        }
    }
}

[Serializable]
public struct Music
{
    public string name;
    public AudioClip clip;
}