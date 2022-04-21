using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyTargeting : Targeting
{
    private float damage;

    public float Damage => damage;

    public void Setup(God god, PlayerController playerController, float cooldown, float damage)
    {
        base.Setup(god, playerController, cooldown);
        this.damage = damage;
    }

    public override void UseWithTarget(bool foundTarget = false)
    {
        Debug.Log($"Used Q {(foundTarget ? "with" : "without")} target");
        if (!foundTarget)
        {
            StartCoroutine(WaitForTargetSelection("Ennemi"));
        }
        else
        {
            target.GetComponent<EnnemiController>().TakeHit(damage);
        }
    }

}
