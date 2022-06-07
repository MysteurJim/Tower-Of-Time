using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CurrentStatus;

public class GoDown : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collided = collision.gameObject;
        if (Current.Cleared && collided.tag == "Player")
        {
            Current.Map.Goto("Bas");
        }
    }
    
    void Start()
    {
        transform.position = new Vector3(0, -Current.HalfHeight - Current.HeightOffset, 3);
    }

    void OnDrawGizmos()
    {
        Gizmos.DrawSphere(transform.position, 100);
    }
}
