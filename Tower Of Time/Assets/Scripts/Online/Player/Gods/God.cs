using System.Collections;
using Photon.Pun;
using UnityEngine;

public abstract class God : MonoBehaviour
{
    protected PlayerController playerController;
    protected float maxHitPoints;
    protected float hitPoints;
    protected float armor;
    protected bool isInvicible;
    protected Ability mainAtk;
    protected Ability q;
    protected new string name;
    protected Ability e;

    public PlayerController PlayerController => playerController;
    public float MaxHitPoints => maxHitPoints;
    public float HitPoints => hitPoints;
    public float Armor => armor;
    public bool IsInvincible => isInvicible;
    public Ability MainAtk => mainAtk;
    public Ability Q => q;
    public string Name => Name;
    public Ability E => e;

    public PhotonView View => playerController.View;

    public virtual void Setup(PlayerController playerController)
    {
        this.playerController = playerController;
        isInvicible = false;
    }

    public void TakeHit(float damage)
    {
        if (!isInvicible)
        {
            this.hitPoints -= damage * (1 - armor / (armor + 50));

            if (hitPoints <= 0)
            {
                gameObject.GetComponent<PlayerMovement>().Respawn();
                hitPoints = maxHitPoints;
            }

            StartCoroutine(TookDamage());
        }
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
