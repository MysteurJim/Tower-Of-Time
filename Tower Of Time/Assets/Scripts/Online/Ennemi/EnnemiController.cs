using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnnemiController : MonoBehaviour
{
    private float hitPoints;
    private float armor;
    private StatusEffectsManager effects;
    private EnemiShoot shooter;
    private EnnemiMouvement movement;

    public float initialHitPoints;
    public float initialArmor;
    public BarManager HealthBar;
    public float HitPoints => hitPoints;
    public float Armor => Armor;
    public StatusEffectsManager Effects => effects;
    public EnemiShoot Shooter => shooter;
    public EnnemiMouvement Movement => movement;

    private void Start()
    {
        hitPoints = initialHitPoints;
        armor = initialArmor;
        HealthBar.SetMaxHealth(initialHitPoints);
        effects = GetComponent<StatusEffectsManager>();
        shooter = GetComponent<EnemiShoot>();
        movement = GetComponent<EnnemiMouvement>();
    }

    // Damage Manager
    public void TakeHit(float damage)
    {
        this.hitPoints -= damage * (1 - armor / (armor + 50));

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

    public void Burn(WaitForSeconds duration, float damage) => StartCoroutine(BurnCoroutine(duration, damage));
    private IEnumerator BurnCoroutine(WaitForSeconds duration, float damage)
    {
        Debug.Log("Set your heart ablaze!");

        bool isBurning = true;
        effects.AddEffect("Burn");
        WaitForSeconds tick = new WaitForSeconds(.5f);

        IEnumerator KeepBurning()
        {
            yield return duration;
            isBurning = false;
        }

        StartCoroutine(KeepBurning());

        while (isBurning)
        {
            TakeHit(damage);
            yield return tick;
        }

        effects.RemoveEffect("Burn");
    }

    public void Stun(WaitForSeconds duration) => StartCoroutine(StunCoroutine(duration));
    private IEnumerator StunCoroutine(WaitForSeconds duration)
    {
        Debug.Log("Bonk");

        bool isStunned = true;
        effects.AddEffect("Stun");
        shooter.StopShooting();
        
        IEnumerator StayStunned()
        {
            yield return duration;
            isStunned = false;
        }

        StartCoroutine(StayStunned());

        while (isStunned)
            yield return null;

        shooter.StartShooting();
        
        effects.RemoveEffect("Stun");
    }
}
