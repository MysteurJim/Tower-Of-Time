using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class StaticAudioManager
{
    public static AudioSource audioSource { get; set; }
    public static Dictionary<string, AudioClip> playlist { get; set; }
    public static string playing { get; set; }

    public static void TryChangeClip(string room)
    {
        if (playing == null || room != playing)
        {
            audioSource.clip = playlist[room];
            audioSource.Play();
            playing = room;
        }
    }
}
