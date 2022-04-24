using System;
using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;
using UnityEngine.UI;

public class StatusEffectsManager : MonoBehaviour
{
    private Dictionary<string, int> effects;
    private Transform visuals;

    public Dictionary<string, int> Effects => effects;
    public Transform Visuals => visuals;

    // Start is called before the first frame update
    void Start()
    {
        effects = new Dictionary<string, int>();

        for (int i = 0; i < transform.childCount && (visuals = transform.GetChild(i)).name != "Effects"; i++)
            continue;

        
    }

    public void AddEffect(string effect)
    {
        switch (effect)
        {
            case "Burn":
            case "Stun":
                if (effects.ContainsKey(effect))
                    effects[effect]++;
                else
                    effects[effect] = 1;
                break;

            default:
                throw new System.ArgumentException($"There is no effect named \"{effect}\"");
        }

        UpdateEffects();
    }

    public void RemoveEffect(string effect)
    {
        if (!effects.ContainsKey(effect))
            throw new System.ArgumentException($"The enemy isn't under the effect \"{effect}\"");

        if (effects[effect] == 1)
            effects.Remove(effect);
        else
            effects[effect]--;

        UpdateEffects();
    }

    private void UpdateEffects()
    {
        ClearVisuals();

        List<string> keys = new List<string>(effects.Keys);

        int numberOfEffects = keys.Count;

        for (int i = 0; i < numberOfEffects; i++)
        {
            GameObject curr = PhotonNetwork.Instantiate("Statuses/" + keys[i],
                                                        visuals.position + new Vector3(.25f * (2 * i - numberOfEffects + 1), 0, 0),
                                                        Quaternion.identity);

            curr.transform.parent = visuals;
            curr.transform.GetChild(0).GetChild(0).GetComponent<Text>().text = effects[keys[i]].ToString();
        }
    }

    private void ClearVisuals()
    {
        List<GameObject> children = new List<GameObject>();
        foreach (Transform child in visuals)
            children.Add(child.gameObject);
        children.ForEach(DestroyChild);
    }

    private Action<GameObject> DestroyChild = 
        child => {Destroy(child); PhotonNetwork.Destroy(child);};
}
