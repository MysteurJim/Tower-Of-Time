using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestGod : God
{
    public TestGod(PlayerController playerController)
    : base (playerController)
    {
        main = new ShootProjectile(this, playerController, (GameObject)Resources.Load("Projectil"), .05f, 10f, 10f);
    }
}
