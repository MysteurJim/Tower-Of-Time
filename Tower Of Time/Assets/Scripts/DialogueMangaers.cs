using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DialogueMangaers : MonoBehaviour
{
    public Text dialogue;
    public Image spriteSpeaker;
    public Text nameSpeaker;
    public Button next;

    public void Active(string texte)
    {
        gameObject.SetActive(true);
        dialogue.text = texte;
        next.gameObject.SetActive(true);
    }

    public void Next()
    {
        gameObject.SetActive(false);
        next.gameObject.SetActive(false);
    }

}
