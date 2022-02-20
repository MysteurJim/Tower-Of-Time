using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ennemi : MonoBehaviour
{


   

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            player.GetComponent<PlayerMovement>().Respawn();
        }
        

    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            PhotonNetwork.Destroy(collision.gameObject);
            Destroy(this.gameObject);
        }
    }
}
