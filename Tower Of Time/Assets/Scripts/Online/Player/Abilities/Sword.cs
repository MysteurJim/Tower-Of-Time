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

   


    public void Setup(God god, PlayerController playerController, float cooldown, float damage)
    {
        base.Setup(god, playerController, cooldown);
        this.damage = damage;
       

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
  
        }
    }

    private void OnDrawGizmos()
    {
        if (playerController.SwordPlacement == null)
            return;
        Gizmos.DrawWireSphere(playerController.SwordPlacement.position, attackRange);
    }

}
