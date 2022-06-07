using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;
using System;

public class AudioManager : MonoBehaviour
{
    public Music[] playlist;
    public AudioSource audioSource;
    private SceneManager sceneManager;
    // Start is called before the first frame update
    void Awake()
    {  
        DontDestroyOnLoad(gameObject);
        Dictionary<string, AudioClip> staticPlaylist = new Dictionary<string, AudioClip>();
        Array.ForEach<Music>(playlist, music => staticPlaylist.Add(music.name, music.clip));
        StaticAudioManager.playlist = staticPlaylist;
        StaticAudioManager.audioSource = audioSource;
        StaticAudioManager.TryChangeClip("Menu");
    }
}

[Serializable]
public struct Music
{
    public string name;
    public AudioClip clip;
}