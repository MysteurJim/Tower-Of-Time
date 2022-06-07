using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class RoncesEmprisonnantes : ZoneEffect
{
    public void Setup(God god, PlayerController playerController)
    {
        base.Setup(god, playerController, 10f, 5f, "Demeter/RoncesEmprisonnantes");
    }

    public override void UseWithZone(bool foundZone = false)
    {
        if (!foundZone)
        {
            runningCoroutine = StartCoroutine(WaitForZoneSelection());
        }
        else
        {
            StartCoroutine(ApplyEffect());
            StartCoroutine(StartCooldown());
        }
    }

    public override void Upgrade(bool pay = true)
    {
        throw new System.NotImplementedException();
    }

    private IEnumerator ApplyEffect()
    {
        

        yield return duration;

        ChangeRendererAlpha(0);
    }
}
