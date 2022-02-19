using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fire : MonoBehaviour
{
    public GameObject projectile;
    public float minDamage;
    public float maxDamage;
    public float projectilForce;
    public Transform LuncherFire;

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            GameObject speel = Instantiate(projectile, LuncherFire.position, Quaternion.identity);
            Vector2 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Vector2 myPos = transform.position;
            Vector2 direction = (mousePos - myPos).normalized;
            speel.GetComponent<Rigidbody2D>().velocity = direction * projectilForce;
        }
    }

}
