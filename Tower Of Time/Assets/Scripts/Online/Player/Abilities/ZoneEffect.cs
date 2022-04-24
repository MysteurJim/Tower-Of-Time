using System.Collections;
using System.Collections.Generic;
using Photon.Pun;
using UnityEngine;

public abstract class ZoneEffect : Ability
{
    protected Vector2 zone;
    protected bool isSelectingZone;
    protected GameObject effect;
    protected WaitForSeconds duration;
    protected Coroutine runningCoroutine;
    protected SpriteRenderer effectRenderer;


    public Vector2 Zone => zone;
    public bool IsSelectingZone => isSelectingZone;
    public GameObject Effect => effect;
    public WaitForSeconds Duration => duration;
    public Coroutine RunningCoroutine => runningCoroutine;

 
    public void Setup(God god, PlayerController playerController, float cooldown, float duration, string effectName)
    {
        base.Setup(god, playerController, cooldown);
        zone = Vector2.zero;
        isSelectingZone = false;
        this.duration = new WaitForSeconds(duration);
        this.effect = PhotonNetwork.Instantiate(effectName, zone, Quaternion.identity);
        effectRenderer = this.effect.GetComponent<SpriteRenderer>();
        ChangeRendererAlpha(0);
    }

    protected IEnumerator WaitForZoneSelection()
    {
        ChangeRendererAlpha(.5f);

        isSelectingZone = true;

        while (!Input.GetButtonDown("Fire1"))
        {
            effect.transform.position = (Vector2)Camera.main.ScreenToWorldPoint(Input.mousePosition);
            Debug.Log(effect.GetComponent<CollisionHandlerForZoneEffects>().Collisions.Count);
            yield return null;
        }

        zone = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        ChangeRendererAlpha(1f);
        UseWithZone(true);
        isSelectingZone = false;
    }

    protected void ChangeRendererAlpha(float alpha)
    {
        effectRenderer.color = new Color(effectRenderer.color.r, effectRenderer.color.g, effectRenderer.color.b, alpha); 
    }

    public override bool TryUse()
    {
        if(isSelectingZone)
        {
            isSelectingZone = false;
            StopCoroutine(runningCoroutine);
            return true;
        }
        else
        {   
            if (!isOnCooldown)
            {
                Use();
                return true;
            }

            return false;
        }
    }

    public override void Use() => UseWithZone();

    public abstract void UseWithZone(bool foundTarget = false);
}
