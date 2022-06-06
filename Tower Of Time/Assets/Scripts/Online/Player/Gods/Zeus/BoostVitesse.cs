using UnityEngine;
using System.Collections;

public class BoostVitesse:Ability
{
    PlayerController playerController;
    God god;
    float boostVitesse;
    PlayerMovement pm;

    private float initial_moveSpeed;
    public void Setup(God god,PlayerController playerController)
    {
        base.Setup(god, playerController, 20);
        boostVitesse = 0.75f;
        pm = playerController.gameObject.GetComponent<PlayerMovement>();
    }

    public override void Use()
    {

        Debug.Log("Speed!");
        initial_moveSpeed = pm.moveSpeed;
        pm.moveSpeed = pm.moveSpeed * (1 + boostVitesse);
        StartCoroutine(SpeedBoost());
    }

    IEnumerator SpeedBoost()
    {
        yield return new WaitForSeconds(1);
        pm.moveSpeed = initial_moveSpeed;
    }

}
