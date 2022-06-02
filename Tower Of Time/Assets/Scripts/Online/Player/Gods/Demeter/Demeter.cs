using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Demeter : God
{
    public override void Setup(PlayerController playerController)
    {
        base.Setup(playerController);

        playerController.gameObject.AddComponent(typeof(ShootProjectile));
        mainAtk = playerController.gameObject.GetComponent<ShootProjectile>();
        ((ShootProjectile)mainAtk).Setup(this, playerController, .5f, (GameObject)Resources.Load(/*"Boule de ronces"*/"Projectil"), 10f, 10f);

        playerController.gameObject.AddComponent(typeof(RoncesEmprisonnantes));
        q = playerController.gameObject.GetComponent<RoncesEmprisonnantes>();
        ((RoncesEmprisonnantes)q).Setup(this, playerController);

        playerController.gameObject.AddComponent(typeof(TestEnemyTargeting));
        e = playerController.gameObject.GetComponent<TestEnemyTargeting>();
        ((TestEnemyTargeting)e).Setup(this, playerController, 0f);

        //A CHANGER
        maxHitPoints = (hitPoints = 100f);
        armor = 10f;
    }
}
