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
            collision.gameObject.GetComponent<PlayerController>().god.UpdateMaxHealth(UpdateMaxHealthPoints);
            Destroy(gameObject);
        }
    }
}
