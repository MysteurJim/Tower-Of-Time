using UnityEngine;
using System.Collections;

public class BoostVitesse:Ability
{
    float boostVitesse;
    PlayerMovement pm;

    private float initial_moveSpeed;
    public void Setup(God god,PlayerController playerController)
    {
        base.Setup(god, playerController, 3);
        boostVitesse = 0.50f;
        this.playerController = playerController;
        pm = playerController.gameObject.GetComponent<PlayerMovement>();
    }

    public override void Use()
    {

        Debug.Log("Speed!");
        initial_moveSpeed = pm.moveSpeed;
        pm.moveSpeed = pm.moveSpeed * (1 + boostVitesse);
        playerController.GetComponent<PlayerStatusRef>().plume.SetActive(true);
        StartCoroutine(SpeedBoost());
    }

    public override void Upgrade(bool pay = true)
    {
        boostVitesse += 0.10f;
        if (pay)
        {
            this.inventory.coinsCount -= 3;
        }
        level += 1;
    }

    public override void Downgrade()
    {
        boostVitesse -= 0.10f;
        this.inventory.coinsCount += 2;
        level -= 1;
    }

    IEnumerator SpeedBoost()
    {
        yield return new WaitForSeconds(3);
        pm.moveSpeed = initial_moveSpeed;
        playerController.GetComponent<PlayerStatusRef>().plume.SetActive(false);
    }

}
