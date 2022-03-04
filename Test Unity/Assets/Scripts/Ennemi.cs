using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class Ennemi : MonoBehaviour
{
   public GameObject player;

   // L'ennemi essaiera de rester entre DesiredDistanceMin et DesiredDistanceMax
   public int DesiredDistanceMin; 
   public int DesiredDistanceMax;

   public float moveSpeed;

    private void Start()
    {
    }

    private void Update()
    {
        SetPlayer();
        transform.position += moveSpeed * Time.deltaTime * dir;
    }
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

    private Vector3 dir => ChasePlayer() + AvoidEnemies();

    private Vector3 ChasePlayer()
    {
        Vector3 toPlayer = player.transform.position - this.transform.position;
        float playerDistance = toPlayer.magnitude;

        // Dirige l'ennemi dans la zone definie par DesiredDistance
        // /!\ Attention formule longue et compliquee
        if (playerDistance < DesiredDistanceMax)
        {
            float angle = 2 * Mathf.PI * ((Mathf.PerlinNoise(Time.time, 0) - .5f) *
                                           Mathf.Min(playerDistance / DesiredDistanceMin,
                                                     (playerDistance - DesiredDistanceMax) / (DesiredDistanceMin - DesiredDistanceMax)) +
                                           Mathf.Max(0, 1 - playerDistance / DesiredDistanceMin)
                                         );
            toPlayer = Rotate(toPlayer, angle);
        }

        toPlayer.Normalize();

        Debug.Log($"toPlayer X : {toPlayer.x} || Y : {toPlayer.y}");

        return toPlayer;
    }

    private Vector3 AvoidEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Ennemi");

        Vector3 push = new Vector3(0,0,0);

        foreach (GameObject enemy in enemies)
        {
            if (enemy != this.gameObject)
            {
                Vector3 proximity = transform.position - enemy.transform.position;
                float distance = proximity.magnitude;
                proximity *= 1 / (distance * distance);
                push += proximity;
            }

            Debug.Log($"pushing {this.ToString()} from {enemy.ToString()}X : {push.x} || Y : {push.y}");
        }

        

        return push;
    }

    public void SetPlayer()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }

    public Vector3 Rotate(Vector3 v, float angle)
    {
        return new Vector3(Mathf.Cos(angle) * v.x - Mathf.Sin(angle) * v.y,
                           Mathf.Sin(angle) * v.x + Mathf.Cos(angle) * v.y);
    }
}
