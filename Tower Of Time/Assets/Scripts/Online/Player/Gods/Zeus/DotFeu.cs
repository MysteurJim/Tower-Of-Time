using UnityEngine;
using System.Collections;


public class Dotfeu : Targeting
{
    float damage = 15f;
    public override void Setup(God god, PlayerController playerController, float cooldown)
    {
        base.Setup(god, playerController, cooldown);
    }

    public override void UseWithTarget(bool foundTarget = false)
    {
        if (!foundTarget)
        {
            runningCoroutine = StartCoroutine(WaitForTargetSelection("Ennemi"));
        }
        else
        {
            target.GetComponent<EnnemiController>().Burn(new WaitForSeconds(1.5f), damage);
        }
    }

    public override void Upgrade(bool pay = true)
    {
        damage += 5f;
        if (pay)
        {
            this.inventory.coinsCount -= 3;
        }
        level += 1;
    }

    public override void Downgrade()
    {
        damage -= 5f;
        this.inventory.coinsCount += 2;
        level -= 1;
    }
}