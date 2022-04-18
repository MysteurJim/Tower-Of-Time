using System.Collections;
using Photon.Pun;
using UnityEngine;

public abstract class Ability
{
    protected God god;
    protected string description;
    protected PlayerController playerController;
    protected float cooldown;
    protected bool isOnCooldown;

    public God God;
    public string Description => description;
    public PlayerController PlayerController => playerController;
    public float Cooldown => cooldown;
    public bool IsOnCooldown => isOnCooldown;

    public PhotonView View => god.View;
    public Ability(God god, PlayerController playerController, float cooldown)
    {
        this.god = god;
        this.playerController = playerController;
        this.cooldown = cooldown;
        isOnCooldown = false;
    }


    public bool TryUse()
    {
        if (isOnCooldown)
            return false;

        Use();
        return true;
    }

    public abstract void Use();

    public IEnumerator StartCooldown()
    {
        Debug.Log("Started cooldown");

        isOnCooldown = true;

        yield return new WaitForSeconds(cooldown);

        isOnCooldown = false;
    }
}
