using System.Collections;
using Photon.Pun;
using UnityEngine;

public abstract class Ability : MonoBehaviour
{
    protected God god;
    protected string description;
    protected PlayerController playerController;
    protected WaitForSeconds cooldown;
    protected bool isOnCooldown;

    public God God;
    public string Description => description;
    public PlayerController PlayerController => playerController;
    public WaitForSeconds Cooldown => cooldown;
    public bool IsOnCooldown => isOnCooldown;

    public PhotonView View => god.View;

    public virtual void Setup(God god, PlayerController playerController, float cooldown)
    {
        this.god = god;
        this.playerController = playerController;
        this.cooldown = new WaitForSeconds(cooldown);
        isOnCooldown = false;
    }

    public virtual bool TryUse()
    {
        if (!isOnCooldown)
        {
            StartCoroutine(StartCooldown());
            Use();
            return true;
        }

        return false;
    }

    public abstract void Use();

    public IEnumerator StartCooldown()
    {
        isOnCooldown = true;

        yield return cooldown;

        isOnCooldown = false;
    }
}
