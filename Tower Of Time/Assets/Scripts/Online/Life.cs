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
                float currX = CurrentStatus.Current.room.Entities[i].Item2;
                float currY = CurrentStatus.Current.room.Entities[i].Item3;
                if (currName == "Item/Coeur-jaune" && (Vector2)transform.position == new Vector2(currX, currY))
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
