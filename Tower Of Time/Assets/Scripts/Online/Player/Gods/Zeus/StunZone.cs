using UnityEngine;
using System.Collections;
using Photon.Pun;
using System.Collections.Generic;

public class StunZone : Ability
{
    string effectName = "Dieux/Zeus/ZoneMarteau";
    Transform marteau;
    SpriteRenderer effectRenderer;

    protected GameObject effect;
    protected float duration;
    


    public GameObject Effect => effect;
    public float Duration => duration;

    public override void Setup(God god, PlayerController playerController, float cooldown)
    {
        base.Setup(god, playerController, cooldown);
        this.effect = PhotonNetwork.Instantiate(effectName, Vector2.zero, Quaternion.identity);
        DontDestroyOnLoad(this.effect);
        this.effect.transform.position = Vector2.zero;
        this.marteau = this.effect.GetComponentsInChildren<Transform>(true)[1];
        this.duration = 0.8f;
        this.effectRenderer = effect.GetComponent<SpriteRenderer>();
        effectRenderer.color = new Color(effectRenderer.color.r, effectRenderer.color.g, effectRenderer.color.b, 0);
        
    }

    public override void Use()
    {
        effectRenderer.color = new Color(effectRenderer.color.r, effectRenderer.color.g, effectRenderer.color.b, 1);
        this.effect.transform.position = playerController.transform.position;
        List<GameObject> collisions = effect.GetComponent<CollisionHandlerForZoneEffects>().Collisions;
        foreach(GameObject g in collisions)
        {
            if(g != null) 
            {
                g.GetComponent<EnnemiController>().Stun(new WaitForSeconds(this.duration));
            }
            
        }
        StartCoroutine(Animation());
        StartCoroutine(StartCooldown());
        

    }

    public override void Upgrade(bool pay = true)
    {
        this.duration += 0.25f;
        if (pay)
        {
            this.inventory.coinsCount -= 3;
        }
        level += 1;
    }

    public override void Downgrade()
    {
        this.duration -= 0.25f;
        this.inventory.coinsCount += 2;
        level -= 1;
    }

    IEnumerator Animation()
    {
        this.marteau.gameObject.SetActive(true);
        Vector3 v = marteau.transform.position;
        for (int i = 0;i<12;i++)
        {
            this.marteau.transform.position += new Vector3(0, -300, 0) * Time.deltaTime;
            
            yield return new WaitForSeconds(0.05f);
        }
        
        yield return new WaitForSeconds(1f);
        this.marteau.gameObject.SetActive(false);
        this.marteau.transform.position = v;
        effectRenderer.color = new Color(effectRenderer.color.r, effectRenderer.color.g, effectRenderer.color.b, 0);
    }
}
