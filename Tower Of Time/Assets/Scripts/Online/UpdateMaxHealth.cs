using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class UpdateMaxHealth : MonoBehaviour
{
    public int UpdateMaxHealthPoints;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            Inventory.instance.AddHealth(1);
            collision.gameObject.GetComponent<PlayerController>().god.UpdateMaxHealth(UpdateMaxHealthPoints);
            collision.gameObject.GetComponent<PlayerController>().god.HealPlayer(UpdateMaxHealthPoints);
            Destroy(gameObject);
            for (int i = 0; i < CurrentStatus.Current.room.Entities.Count;)
            {
                string currName = CurrentStatus.Current.room.Entities[i].Item1;
                if (currName.Substring(0, Math.Min(currName.Length, "Coeur-vert".Length)) == "Coeur-vert")
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
