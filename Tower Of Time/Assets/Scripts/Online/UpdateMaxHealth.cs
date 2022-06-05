using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        }
    }
}
