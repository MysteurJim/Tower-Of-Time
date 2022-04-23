using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class EnemiShoot : MonoBehaviour
{
    private bool isShooting;
    private Coroutine shooting;

    public bool IsShooting => isShooting;
    public float cooldown;
    public GameObject projectile;
    public float projectilForce;

    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        StartShooting();
    }

    public void StartShooting()
    {
        if (!isShooting)
        {
            isShooting = true;
            shooting = StartCoroutine(ShootPlayer());
        }
    }

    public void StopShooting()
    {
        if (isShooting)
        {
            isShooting = false;
            StopCoroutine(shooting);
        }
    }

    IEnumerator ShootPlayer()
    {
        yield return new WaitForSeconds(cooldown);
        if(player != null)
        {
            GameObject speel = PhotonNetwork.Instantiate(projectile.name, transform.position, Quaternion.identity);
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            Transform pos = player.GetComponent<Transform>();
            Vector2 direction = (pos.position - transform.position).normalized;
            speel.GetComponent<Rigidbody2D>().velocity = direction * projectilForce;
            shooting = StartCoroutine(ShootPlayer());
        }
    }
}
