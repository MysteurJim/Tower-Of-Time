using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class Eclair : ZoneEffect
{
    float damage;
    public Transform[] eclairs;

    public void Setup(God god, PlayerController playerController)
    {
        base.Setup(god, playerController, 10f, 0f, "Dieux/Zeus/ZoneFoudre");
        eclairs = this.effect.GetComponentsInChildren<Transform>(true);
        Debug.Log("Taille des éclaris" + eclairs.Length);
    }

    public override void Upgrade(bool pay = true)
    {
        damage += 15;
        if(pay){
            this.inventory.coinsCount -= 3;
        }
        level += 1;
    }
    public override void Downgrade()
    {
        damage -= 15;
        this.inventory.coinsCount += 2;
        level -= 1;
    }

    

    public override void UseWithZone(bool foundTarget = false)
    {
        if (!foundTarget)
        {
            runningCoroutine = StartCoroutine(WaitForZoneSelection());
        }
        else
        {
            StartCoroutine(ZoneAnimations());
            StartCoroutine(Damage());
            StartCoroutine(StartCooldown());
        }
    }

    IEnumerator ZoneAnimations()
    {
        for (int i = 1; i < eclairs.Length; i++)
        {

            eclairs[i].gameObject.SetActive(true);
            yield return new WaitForSeconds(0.2f);
            eclairs[i].gameObject.SetActive(false);
        }

    }


    IEnumerator Damage()
    {
        List<GameObject> collisions = effect.GetComponent<CollisionHandlerForZoneEffects>().Collisions;
        foreach(GameObject g in collisions)
        {
            g.GetComponent<EnnemiController>().TakeHit(damage);
        }
        yield return new WaitForSeconds(0.6f);
        effect.transform.position = Vector2.zero;
        ChangeRendererAlpha(0);
    }
    
}
