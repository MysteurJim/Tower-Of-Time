using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ProjectileInterraction : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("Player") & this.tag == "Ennemi")
        {
            Destroy(this.gameObject);
            collision.gameObject.GetComponent<PlayerMovement>().Respawn();
        }
    }
}
