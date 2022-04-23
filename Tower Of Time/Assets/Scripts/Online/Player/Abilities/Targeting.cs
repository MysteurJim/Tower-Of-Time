using System.Collections;
using UnityEngine;

public abstract class Targeting : Ability
{
    protected GameObject target;
    protected bool isSelectingTarget;
    protected Coroutine runningCoroutine;

    public GameObject Target => target;
    public bool IsSelectingTarget => isSelectingTarget;

    public override void Setup(God god, PlayerController playerController, float cooldown)
    {
        base.Setup(god, playerController, cooldown);
        target = null;
        isSelectingTarget = false;
    }

    // Renvoie le GameObject avec le tag Ennemi le plus proche de la souris
    protected GameObject FindTarget(string tag)
    {
        Vector2 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Collider2D[] nearMouse = Physics2D.OverlapCircleAll(mousePosition, 1f);

        float distanceFromMouseCollider(Collider2D collider) 
            => ((Vector2)collider.gameObject.transform.position - mousePosition).magnitude;

        float distanceFromMouseGameObject(GameObject gameObject) 
            => ((Vector2)gameObject.transform.position - mousePosition).magnitude;

        GameObject closest = null;

        foreach (Collider2D collider in nearMouse)
        {
            if (collider.gameObject.tag == tag && (closest == null || distanceFromMouseCollider(collider) < distanceFromMouseGameObject(closest)))
                closest = collider.gameObject;
        }

        return closest;
    }

    protected IEnumerator WaitForTargetSelection(string tag)
    {
        isSelectingTarget = true;
        target = FindTarget(tag);


        target?.GetComponent<Renderer>().material.SetColor("_Color", Color.green);

        while (!Input.GetButtonDown("Fire1") || target == null)
        {
            GameObject next = FindTarget(tag);

            if (next != target)
            {
                target?.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
                target = next;
                target?.GetComponent<Renderer>().material.SetColor("_Color", Color.green);
            }

            yield return null;
        }

        target.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
        StartCoroutine(StartCooldown());
        UseWithTarget(true);
        isSelectingTarget = false;
    }

    public override bool TryUse()
    {
        if(isSelectingTarget)
        {
            target?.GetComponent<Renderer>().material.SetColor("_Color", Color.white);
            isSelectingTarget = false;
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

    public override void Use() => UseWithTarget();

    public abstract void UseWithTarget(bool foundZone = false);
}
