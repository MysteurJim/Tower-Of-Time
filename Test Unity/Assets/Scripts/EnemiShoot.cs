using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemiShoot : MonoBehaviour
{

    public float cooldown;
    public GameObject projectile;
    public float projectilForce;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartCoroutine(ShootPlayer());

    }

    IEnumerator ShootPlayer()
    {
        yield return new WaitForSeconds(cooldown);
        if(player != null)
        {
            GameObject speel = Instantiate(projectile, transform.position, Quaternion.identity);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Transform pos = player.GetComponent<Transform>();
            Vector2 direction = (pos.position - transform.position).normalized;
            speel.GetComponent<Rigidbody2D>().velocity = direction * projectilForce;
            StartCoroutine(ShootPlayer());
        }
    }
}
