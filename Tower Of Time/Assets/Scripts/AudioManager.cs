using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public AudioClip[] playlist;
    public AudioSource audioSource;
    private int musicIndex = 0;
    private SceneManager sceneManager;
    // Start is called before the first frame update
    void Start()
    {
        audioSource.clip = playlist[0];
        audioSource.Play();
    }

    // Update is called once per frame
    void Update()
    {
        //DontDestroyOnLoad(audioSource);
        //if (Scene.name == Scene.Int-Medusa || Scene.name == Scene.Int-Minautor || Scene.name == Scene.Int-Cronos || Scene.name == Scene.Int-Charydbe-et-Scylla )
        {
        //    Destroy(audioSource);
        //    audioSource.clip = playlist[1];
        //    audioSource.Play();
        }
        //else if (Scene.name == Scene.Boss-Medusa || Scene.name == Scene.Boss-Minautor || Scene.name == Scene.Boss-Cronos || Scene.name == Scene.Boss-Charydbe-et-Scylla)
        {
         //   Destroy(audioSource);
         //   audioSource.clip = playlist[2];
         //   audioSource.Play();
        }
        
        if (!audioSource.isPlaying)
        {
            PlayNextSong();
        }
    }

    void PlayNextSong()
    {
        musicIndex += 1 % playlist.Length;
        audioSource.clip = playlist[musicIndex];
        audioSource.Play();
    }
}
