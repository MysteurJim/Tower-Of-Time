using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FonduBlack : MonoBehaviour
{
    public GameObject blackOutSquare;
    public Text text;

    public void StartFade()
    {
        StartCoroutine(FadeBlackOutSquare());
        text.gameObject.SetActive(true);
        
    }

    public IEnumerator FadeBlackOutSquare(bool fadeToBlack = true, int fadeSpeed = 2)
    {
        Color objectColor = blackOutSquare.GetComponent<Image>().color;
        Color textColor = text.GetComponent<Text>().color;
        float fadeAmout;

        if (fadeToBlack)
        {
            while (blackOutSquare.GetComponent<Image>().color.a < 1)
            {
                fadeAmout = objectColor.a + (fadeSpeed * Time.deltaTime);

                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmout);
                textColor = new Color(textColor.r, textColor.g, textColor.b, fadeAmout);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                text.GetComponent<Text>().color = textColor;
                yield return null;
            }
        }
        else
        {
            while (blackOutSquare.GetComponent<Image>().color.a > 0)
            {
                fadeAmout = objectColor.a - (fadeSpeed * Time.deltaTime);
                objectColor = new Color(objectColor.r, objectColor.g, objectColor.b, fadeAmout);
                blackOutSquare.GetComponent<Image>().color = objectColor;
                yield return null;
            }
        }
        
    }
}
