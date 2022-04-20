using System;
using UnityEngine;

public class TestGod : God
{
    public override void Setup(PlayerController playerController)
    {
        base.Setup(playerController);

        playerController.gameObject.AddComponent(typeof(ShootProjectile));
        mainAtk = playerController.gameObject.GetComponent<ShootProjectile>();
        ((ShootProjectile)mainAtk).Setup(this, playerController, .5f, (GameObject)Resources.Load("Projectil"), 10f, 10f);

        playerController.gameObject.AddComponent(typeof(TestEnemyTargeting));
        q = playerController.gameObject.GetComponent<TestEnemyTargeting>();
        ((TestEnemyTargeting)q).Setup(this, playerController, 1f, 1000f);
    }
}