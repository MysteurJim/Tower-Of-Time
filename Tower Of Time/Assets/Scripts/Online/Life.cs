using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using System.Linq;

public class Life : MonoBehaviour
{
    public int SecondChance;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory.instance.AddSecondChance(1);
            collision.gameObject.GetComponent<PlayerController>().god.UpdateMaxHealth(SecondChance);
            collision.gameObject.GetComponent<PlayerController>().god.HealPlayer(SecondChance);
            Destroy(gameObject);
            for (int i = 0; i < CurrentStatus.Current.room.Entities.Count;)
            {
                string currName = CurrentStatus.Current.room.Entities[i].Item1;
                if (currName.Substring(0, Math.Min(currName.Length, "Coeur-jaune".Length)) == "Coeur-jaune")
                {
                    CurrentStatus.Current.room.Entities.RemoveAt(i);
                }
                else
                {
                    i++;
                }
            }
        }
    }
}
