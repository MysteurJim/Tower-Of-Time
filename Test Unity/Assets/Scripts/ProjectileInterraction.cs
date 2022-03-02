using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class ProjectileInterraction : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("Player") & this.tag == "Ennemi")
        {
            PhotonNetwork.Destroy(this.gameObject);
            collision.gameObject.GetComponent<PlayerMovement>().Respawn();
        }
    }
}
