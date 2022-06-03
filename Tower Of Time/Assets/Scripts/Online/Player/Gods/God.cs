using System.Collections;
using Photon.Pun;
using UnityEngine;

public abstract class God : MonoBehaviour
{
    protected PlayerController playerController;
    public float maxHitPoints;
    protected float hitPoints;
    protected float armor;
    protected bool isInvicible;
    protected Ability mainAtk;
    protected Ability q;
    protected new string name;
    protected Ability e;

    protected string arme;
    protected string capacite;
    protected string descrip;

    public PlayerController PlayerController => playerController;
    public BarManager BarManager => PlayerController.barManager;
    public float MaxHitPoints => maxHitPoints;
    public float HitPoints => hitPoints;
    public float Armor => armor;
    public bool IsInvincible => isInvicible;
    public Ability MainAtk => mainAtk;
    public Ability Q => q;
    public string Name => Name;
    public Ability E => e;

    public string Arme => arme;
    public string Capacite => capacite;
    public string Descript => descrip;

    public bool dead = false;

    public PhotonView View => playerController.View;

    public virtual void Setup(PlayerController playerController)
    {
        this.playerController = playerController;
        isInvicible = false;
    }

    public static God instance;

    private void Awake()
    {
        if(instance != null)
        {
            Debug.LogWarning("Il y a plus d'une instance de God dans la scène");
            return;
        }
    }

    public void TakeHit(float damage)
    {
        if (!isInvicible)
        {
            this.hitPoints -= damage * (1 - armor / (armor + 50));

            if (hitPoints <= 0 & !dead)
            {
                dead = true;
                playerController.Dead();
            }

            PlayerController.barManager.SetHealth(this.hitPoints);

            StartCoroutine(TookDamage());
        }
    }

    public void HealPlayer(int amount)
    {
        hitPoints += amount;
        if (hitPoints > maxHitPoints)
            hitPoints = maxHitPoints;
        BarManager.SetHealth(hitPoints);
    }

    public void UpdateMaxHealth(int amount)
    {
        maxHitPoints += amount;
        BarManager.SetMaxHealth(maxHitPoints);
    }

    private IEnumerator TookDamage()
    {
        this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.red);
        isInvicible = true;

        yield return new WaitForSeconds(0.1f);

        this.gameObject.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        isInvicible = false;
    }

    public virtual bool UseMainAtk() => mainAtk.TryUse();

    public virtual bool UseQ() => q.TryUse(); 

    public virtual bool UseE() => e.TryUse();
}
