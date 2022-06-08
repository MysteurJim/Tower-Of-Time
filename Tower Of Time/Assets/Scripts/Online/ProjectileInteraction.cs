using UnityEngine;
using Photon.Pun;

public class ProjectileInteraction : MonoBehaviour
{
    public float damage;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Wall"))
        {
            PhotonNetwork.Destroy(this.gameObject);
        }
        if (collision.gameObject.CompareTag("Player") & this.tag == "Bullet")
        {
            PhotonNetwork.Destroy(this.gameObject);
            PlayerController player = collision.gameObject.GetComponent<PlayerController>();
            if (!player.invincible)
                player.God.TakeHit(damage);
        }
    }
}
