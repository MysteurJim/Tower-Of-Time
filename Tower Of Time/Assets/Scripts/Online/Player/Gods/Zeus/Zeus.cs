using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Zeus : God
{
    public override void Setup(PlayerController playerController)
    {
        base.Setup(playerController);
        this.name = "Zeus";
        playerController.gameObject.AddComponent(typeof(Sword));
        mainAtk = playerController.gameObject.GetComponent<Sword>();
        ((Sword)mainAtk).Setup(this, playerController, .5f, 15f);

        playerController.gameObject.AddComponent(typeof(BoostVitesse));
        q = playerController.gameObject.GetComponent<BoostVitesse>();
        ((BoostVitesse)q).Setup(this, playerController);

        playerController.gameObject.AddComponent(typeof(Dotfeu));
        e = playerController.gameObject.GetComponent<Dotfeu>();
        ((Dotfeu)e).Setup(this, playerController, 10);

        playerController.gameObject.AddComponent(typeof(Eclair));
        r = playerController.gameObject.GetComponent<Eclair>();
        ((Eclair)r).Setup(this, playerController);

        playerController.gameObject.AddComponent(typeof(StunZone));
        f = playerController.gameObject.GetComponent<StunZone>();
        ((StunZone)f).Setup(this, playerController,8f);

        //A CHANGER
        maxHitPoints = (hitPoints = 100f);
        armor = 10f;
    }

    public override string EncodeLevels()
    {
        return mainAtk.Level.ToString() +"/"+ q.Level.ToString() + "/" + e.Level.ToString() + "/" + r.Level.ToString() + "/" + f.Level.ToString()+"/";
    }

    public override void DecodeLevels(string decodeString)
    {

        string temp = "";
        char c;
        int index = 0;
        for (int i = 0; i < decodeString.Length; i++)
        {
            c = decodeString[i];
            if(c == '/')
            {
                {
                    switch (index)
                    {
                        case 0:
                            Debug.Log("Main Atk up to " + temp);
                            mainAtk.AddLevels(Int32.Parse(temp));
                            break;
                        case 1:
                            Debug.Log("q up to " + temp);
                            q.AddLevels(Int32.Parse(temp));
                            break;
                        case 2:
                            Debug.Log("e up to " + temp);
                            e.AddLevels(Int32.Parse(temp));
                            break;
                        case 3:
                            Debug.Log("r up to " + temp);
                            r.AddLevels(Int32.Parse(temp));
                            break;
                        case 4:
                            Debug.Log("f up to " + temp);
                            f.AddLevels(Int32.Parse(temp));
                            break;
                    }
                    index++;
                }
                temp = "";
            }
            else
            {
                temp += c;
            }
        }
        
    }
}
