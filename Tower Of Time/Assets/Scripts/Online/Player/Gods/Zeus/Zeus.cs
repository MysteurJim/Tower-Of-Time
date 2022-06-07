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

        playerController.gameObject.AddComponent(typeof(Dotfeu));
        e = playerController.gameObject.GetComponent<Dotfeu>();
        ((Dotfeu)e).Setup(this, playerController, 10);

        playerController.gameObject.AddComponent(typeof(Eclair));
        r = playerController.gameObject.GetComponent<Eclair>();
        ((Eclair)r).Setup(this, playerController);

        playerController.gameObject.AddComponent(typeof(StunZone));
        f = playerController.gameObject.GetComponent<StunZone>();
        ((StunZone)f).Setup(this, playerController,8f);

        //A CHANGER
        maxHitPoints = (hitPoints = 100f);
        armor = 10f;
    }
}
