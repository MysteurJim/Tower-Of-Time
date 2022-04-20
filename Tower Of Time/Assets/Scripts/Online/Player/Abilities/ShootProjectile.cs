using UnityEngine;
using Photon.Pun;
public class ShootProjectile : Ability
{
    private GameObject projectile;
    private float damage;
    private float speed;

    public GameObject Projectile => projectile;
    public float Damage => damage;
    public float Speed => speed;

    public void Setup(God god, PlayerController playerController, float cooldown, GameObject projectile, float damage, float speed)
    {
        base.Setup(god, playerController, cooldown);
        this.projectile = projectile;
        this.damage = damage;
        this.speed = speed;

        this.description = $"Shoots a projectile towards the mouse at a speed of {this.speed} for {this.damage} damage";
    }

    public override void Use()
    {
        GameObject projectileObject = PhotonNetwork.Instantiate(projectile.name, playerController.position, Quaternion.identity);
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector2 position = playerController.position;
        Vector2 direction = (mousePosition - position).normalized;
        projectileObject.GetComponent<Rigidbody2D>().velocity = direction * speed;
        projectileObject.GetComponent<ProjectileInteraction>().damage = damage;
    }
}
