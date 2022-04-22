using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class GodChoice : MonoBehaviour
{
    //public God[] gods;
    public string[] gods;
    public int index = 0;
    public Text GodText;

    public void Start()
    {
        GodText.text = gods[index];
    }

    public void Left()
    {
        if (index == 0)
            return;
        index -= 1;
        GodText.text = gods[index];
        //GodText.text = gods[index].Name;
    }
    
    public void Right()
    {
        if (index == gods.Length-1)
            return;
        index += 1;
        GodText.text = gods[index];
        //GodText.text = gods[index].Name;
    }


}
