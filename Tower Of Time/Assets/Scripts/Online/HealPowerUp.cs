using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using System;

public class HealPowerUp : MonoBehaviour
{
    public int healthPoints;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            collision.gameObject.GetComponent<PlayerController>().god.HealPlayer(healthPoints);
            Destroy(gameObject);

            for (int i = 0; i < CurrentStatus.Current.room.Entities.Count;)
            {
                string currName = CurrentStatus.Current.room.Entities[i].Item1;
                if (currName.Substring(0, Math.Min(currName.Length, "Coeur-rouge".Length)) == "Coeur-rouge")
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
