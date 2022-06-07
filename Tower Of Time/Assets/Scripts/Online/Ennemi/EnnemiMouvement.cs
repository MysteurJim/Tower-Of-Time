using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnnemiMouvement : MonoBehaviour
{
    // L'ennemi essaiera de rester entre DesiredDistanceMin et DesiredDistanceMax
    public int DesiredDistanceMin; 
    public int DesiredDistanceMax;

    public float moveSpeed;

    //Repoussage
    public bool stun;

    // Reference interne au joueur 
    private GameObject player => GameObject.FindWithTag("Player");

    // Parce que velocity existe deja
    public Vector2 vel;
    private Vector2 acceleration;


    // Objet representant la hitbox du GameObject
    private BoxCollider2D hitbox;


    private void Start()
    {
        
        this.hitbox = this.GetComponent<BoxCollider2D>();
        this.vel = Vector2.zero;
        m_Started = true;
        this.stun = false;
    }

    private void Update()
    {
        acceleration = Vector2.zero;
        if (!stun)
        {
            acceleration += StayInZone();
            acceleration = AddRandom(acceleration);
            acceleration += AvoidEnemies();
            acceleration += AvoidWalls();
            this.vel += acceleration;
            this.vel *= this.vel.magnitude > this.moveSpeed ? this.moveSpeed / this.vel.magnitude : 1;
        }
        else
        {
            this.vel = Vector2.zero;
        }
        

        Vector3 v = this.vel;

        transform.position +=  Time.deltaTime * v;
    }

   

    // ----- Collision Manager -----
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.CompareTag("Player"))
        {
            GameObject player = collision.gameObject;
            player.GetComponent<PlayerController>().God.TakeHit(55f);
        }
        
        if (collision.gameObject.CompareTag("Wall"))
        {
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, collision.transform.position - this.transform.position);

            Vector3 normal = hit.normal;
            normal = hit.transform.TransformDirection(normal);

            if (normal == hit.transform.up || normal == -hit.transform.up)
            {
                this.vel.y = 0;
            }

            if (normal == hit.transform.right || normal == -hit.transform.right)
            {
                this.vel.x = 0;
            }
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Projectile"))
        {
            this.GetComponent<EnnemiController>().TakeHit(collision.GetComponent<ProjectileInteraction>().damage);
            PhotonNetwork.Destroy(collision.gameObject);
            
            this.vel += collision.GetComponent<Rigidbody2D>().velocity;
            //StartCoroutine(TookDamage());
        }
        
    }


    // ----- Movement Manager -----
    private float rand(float seed = 0f) => Mathf.PerlinNoise(Time.time, seed);
    private Vector2 StayInZone()
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

    private Vector2 AddRandom(Vector3 v)
    {
        Vector3 normal = Rotate(v, Mathf.PI / 2);

        normal *= 2 * rand() - 1f;

        return v + normal;
    }

    private Vector2 AvoidEnemies()
    {
        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Ennemi");

        Vector3 push = new Vector3(0, 0, 0);
        float size = Mathf.Max(this.hitbox.size.x, this.hitbox.size.y);

        foreach (GameObject enemy in enemies)
        {
            if (enemy != this.gameObject)
            {
                Vector3 proximity = transform.position - enemy.transform.position;
                float distance = proximity.magnitude;
                proximity *= 1 / (5 * distance * distance + 1);
                push += proximity* Mathf.Max(this.hitbox.size.x, this.hitbox.size.y);
            }
        }

        return push;
    }

    private Vector2 AvoidWalls()
    {
        Vector3 push = new Vector3(0, 0, 0);

        float size = Mathf.Max(this.hitbox.size.x, this.hitbox.size.y);

        for (float i = 0; i < 8; i++)
        {
            Vector3 dir = Rotate(Vector3.right, i * Mathf.PI / 4);
            RaycastHit2D hit = Physics2D.Raycast(this.transform.position, dir);

            if (hit.collider != null && hit.distance < size * .5f && hit.collider.gameObject.CompareTag("Wall"))
            {
                push += Rotate(dir, Mathf.PI);
            }
        }

        return push;
    }





    // Gizmos for debugging
    bool m_Started;
    void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        //Check that it is being run in Play Mode, so it doesn't try to draw this in Editor mode
        if (m_Started)
        {
            //Draw a cube where the OverlapBox is (positioned where your GameObject is as well as a size)
            Gizmos.DrawWireCube(this.hitbox.transform.position, hitbox.size * 3f);
            Gizmos.DrawWireSphere(transform.position, DesiredDistanceMax);
            Gizmos.DrawWireSphere(transform.position, DesiredDistanceMin);
        }
    }

    // Miscelanneous
    public Vector3 Rotate(Vector3 v, float angle)
    {
        return new Vector3(Mathf.Cos(angle) * v.x - Mathf.Sin(angle) * v.y,
                           Mathf.Sin(angle) * v.x + Mathf.Cos(angle) * v.y);
    }
}
