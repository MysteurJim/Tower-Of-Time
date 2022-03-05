using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Tilemaps;

public class Ennemi : MonoBehaviour
{
    // L'ennemi essaiera de rester entre DesiredDistanceMin et DesiredDistanceMax
    public int DesiredDistanceMin; 
    public int DesiredDistanceMax;

    public float moveSpeed;

    // Reference interne au joueur 
    private GameObject player => GameObject.FindWithTag("Player");

    // Parce que velocity existe deja
    private Vector3 vel;
    private int hitPoints;
    public int initialHitPoints;

    // Objet representant la hitbox du GameObject
    private BoxCollider2D hitbox;


    private void Start()
    {
        this.hitPoints = initialHitPoints;
        this.hitbox = this.GetComponent<BoxCollider2D>();
        this.vel = Vector3.zero;
        m_Started = true;
    }

    private void Update()
    {
        Vector3 acceleration = Vector3.zero;

        acceleration += StayInZone();

        acceleration = AddRandom(acceleration);

        acceleration += AvoidEnemies();

        acceleration += AvoidWalls();

        this.vel += acceleration;

        this.vel *= this.vel.magnitude > this.moveSpeed ? this.moveSpeed / this.vel.magnitude : 1;

        transform.position += Time.deltaTime * this.vel;
    }


    // ----- Collision Manager -----
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


    // ----- Movement Manager -----
    private float rand(float seed = 0f) => Mathf.PerlinNoise(Time.time, seed);
    private Vector3 StayInZone()
    {
        Vector3 toPlayer = player.transform.position - this.transform.position;
        float zoneDistance = toPlayer.magnitude - DesiredDistanceMin;

        toPlayer.Normalize();

        if (zoneDistance < 0)
            return -toPlayer;

        if (zoneDistance > DesiredDistanceMax - DesiredDistanceMin)
            return toPlayer;

        float scale = - (DesiredDistanceMax - DesiredDistanceMin) * (rand(1) * rand(2)) + zoneDistance;
        return scale * toPlayer;
    }

    private Vector3 AddRandom(Vector3 v)
    {
        Vector3 normal = Rotate(v, Mathf.PI / 2);

        normal *= 2 * rand() - 1f;

        return v + normal;
    }

    private Vector3 AvoidEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Ennemi");

        Vector3 push = new Vector3(0, 0, 0);

        foreach (GameObject enemy in enemies)
        {
            if (enemy != this.gameObject)
            {
                Vector3 proximity = transform.position - enemy.transform.position;
                float distance = proximity.magnitude;
                proximity *= 1 / (5 * distance * distance);
                push += proximity;
            }
        }

        return push;
    }

    private Vector3 AvoidWalls()
    {
        Vector3 push = new Vector3(0, 0, 0);

        for (float i = 0; i < 4; i++)
        {
            Vector3 dir = Rotate(Vector3.right, i * Mathf.PI / 2);
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, dir);

            float size = i % 2 == 0 ? this.hitbox.size.x : this.hitbox.size.y;

            if (hit.collider != null && hit.distance < size * 1.5f)
            {
                push += Rotate(dir, Mathf.PI);
            }
        }

        return push;
    }

    bool m_Started;
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (m_Started)
        {
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(this.hitbox.transform.position, hitbox.size * 3f);
            Gizmos.DrawLine(this.transform.position, this.transform.position + this.vel);
            Gizmos.DrawWireSphere(transform.position, DesiredDistanceMax);
            Gizmos.DrawWireSphere(transform.position, DesiredDistanceMin);
        }
    }

    public Vector3 Rotate(Vector3 v, float angle)
    {
        return new Vector3(Mathf.Cos(angle) * v.x - Mathf.Sin(angle) * v.y,
                           Mathf.Sin(angle) * v.x + Mathf.Cos(angle) * v.y);
    }
}
