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
    public int level;
    protected Inventory inventory;

    public Inventory Inventory => inventory;
    public God God;
    public string Description => description;
    public PlayerController PlayerController => playerController;
    public WaitForSeconds Cooldown => cooldown;
    public bool IsOnCooldown => isOnCooldown;
    public int Level => level;
    public PhotonView View => god.View;
    

    public virtual void Setup(God god, PlayerController playerController, float cooldown)
    {
        this.god = god;
        this.playerController = playerController;
        this.cooldown = new WaitForSeconds(cooldown);
        this.level = 1;
        isOnCooldown = false;
        this.inventory = playerController.gameObject.GetComponentInChildren<Inventory>();
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
    public virtual void Upgrade(bool pay = true) {}
    public virtual void Downgrade() {}

    public virtual void AddLevels(int levelsUp)
    {
        for (int i = 0; i < levelsUp-1; i++)
        {
            Upgrade(false);
        }
    }

    public IEnumerator StartCooldown()
    {
        isOnCooldown = true;

        yield return cooldown;

        isOnCooldown = false;
    }
}
