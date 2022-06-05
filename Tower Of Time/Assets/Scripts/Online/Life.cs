using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
    }
}
