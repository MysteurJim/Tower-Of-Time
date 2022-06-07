using UnityEngine;
using System.Collections;


public class Dotfeu : Targeting
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
            target.GetComponent<EnnemiController>().Burn(new WaitForSeconds(1.5f), 15f);
        }
    }
}