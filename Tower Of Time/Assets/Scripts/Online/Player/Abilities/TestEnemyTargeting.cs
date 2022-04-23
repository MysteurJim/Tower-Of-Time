using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestEnemyTargeting : Targeting
{
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
            target.GetComponent<EnnemiController>().Stun(new WaitForSeconds(10));
        }
    }

}
