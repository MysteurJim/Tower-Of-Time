using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Ability
{
    private GameObject projectile;
    private float damage;
    private float speed;

    public float attackRange = 2f;
    

    public GameObject Projectile => projectile;
    public float Damage => damage;
    public float Speed => speed;

   void Start()
   {
        Transform sword = null;
        int i = 0;
        while (i < playerController.transform.childCount && (sword = playerController.transform.GetChild(i)).name != "Sword")
            i++;

        sword.GetComponent<SwordMouvement>().enabled = true;
   }


    public void Setup(God god, PlayerController playerController, float cooldown, float damage)
    {
        base.Setup(god, playerController, cooldown);
        this.damage = damage;
        playerController.gameObject.GetComponentInChildren<SwordMouvement>().enabled = true;
        this.description = $"Hit with a sword in front of player";
    }

    public override void Use()
    {
        /*
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 direction = (mousePosition - (new Vector2(playerController.position.x, playerController.position.y)));
        RaycastHit2D ray = Physics2D.Raycast(playerController.position, direction.normalized,5f);
        if (ray.collider != null && ray.collider.tag == "Ennemi")
        {
            Debug.Log("Tuch");
            ray.collider.gameObject.GetComponent<Ennemi>().TakeHit(damage);
        }
        Debug.DrawRay(playerController.position, direction, Color.green,10f);
        */
        Collider2D[] hitEnemies = Physics2D.OverlapCircleAll(playerController.SwordPlacement.position, attackRange,playerController.enemyLayers);

        foreach(Collider2D ennemy in hitEnemies)
        {
            ennemy.GetComponent<EnnemiController>().TakeHit(damage);
            StartCoroutine(KnockBack(ennemy));
        }
    }

    IEnumerator KnockBack(Collider2D ennemy)
    {
        yield return new WaitForSeconds(0.1f);
        ennemy.GetComponent<EnnemiMouvement>().knock = true;
        Collider2D[] detectEnemies = Physics2D.OverlapCircleAll(playerController.SwordPlacement.position, attackRange+5, playerController.enemyLayers);
        if(detectEnemies.Length != 0 && detectEnemies[0].GetComponent<EnnemiMouvement>().knock)
        {
            StartCoroutine(KnockBack(ennemy));
        }
        else
        {
            ennemy.GetComponent<EnnemiMouvement>().knock = false;
        }
    }

    //DEBUG
    private void OnDrawGizmos()
    {
        if (playerController.SwordPlacement == null)
            return;
        Gizmos.DrawWireSphere(playerController.SwordPlacement.position, attackRange);
        Gizmos.DrawWireSphere(playerController.SwordPlacement.position, attackRange+5);
    }

}
