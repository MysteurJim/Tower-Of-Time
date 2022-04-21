using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiController : MonoBehaviour
{

    private float hitPoints;
    public float initialHitPoints;
    public BarManager HealthBar;

    private void Start()
    {
        this.hitPoints = initialHitPoints;
        HealthBar.SetMaxHealth(initialHitPoints);
    }
    // Damage Manager
    public void TakeHit(float damage)
    {
        this.hitPoints -= damage;

        if (this.hitPoints <= 0)
            GameObject.Destroy(this.gameObject);
        HealthBar.SetHealth(hitPoints);
        StartCoroutine(TookDamage());
    }

    private IEnumerator TookDamage()
    {
        this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);

        yield return new WaitForSeconds(0.1f);

        this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
    }
}
