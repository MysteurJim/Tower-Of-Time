using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Zeus : God
{
    public override void Setup(PlayerController playerController)
    {
        base.Setup(playerController);

        playerController.gameObject.AddComponent(typeof(Sword));
        mainAtk = playerController.gameObject.GetComponent<Sword>();
        ((Sword)mainAtk).Setup(this, playerController, .5f, 15f);

        playerController.gameObject.AddComponent(typeof(BoostVitesse));
        q = playerController.gameObject.GetComponent<BoostVitesse>();
        ((BoostVitesse)q).Setup(this, playerController);

        playerController.gameObject.AddComponent(typeof(TestEnemyTargeting));
        e = playerController.gameObject.GetComponent<TestEnemyTargeting>();
        ((TestEnemyTargeting)e).Setup(this, playerController, 0f);

        //A CHANGER
        maxHitPoints = (hitPoints = 100f);
        armor = 10f;
    }
}
