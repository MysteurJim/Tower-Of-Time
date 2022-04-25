using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI ;

public class GodChoice : MonoBehaviour
{
    public God[] gods;
    public int index = 0;
    public Text GodText;
    public God god;
    public GameObject vide;

    public Text arme;
    public Text capacite;
    public Text descript;

    public void Start()
    {
        gameObject.AddComponent(typeof(TestGod));
        god = gameObject.GetComponent<TestGod>();
        god.Setup(vide.GetComponent<PlayerController>());
        gods[index] = god;
        GodText.text = gods[index].Name;
        Update_infos();
    }


    public void Update_infos()
    {
        God g = gods[index];
        arme.text = g.Arme;
        capacite.text = g.Capacite;
        descript.text = g.Descript;


    }

    public void Left()
    {
        if (index == 0)
            return;
        index -= 1;
        GodText.text = gods[index].Name;
        Update_infos();
    }
    
    public void Right()
    {
        if (index == gods.Length-1)
            return;
        index += 1;
        GodText.text = gods[index].Name;
        Update_infos();
    }


}
