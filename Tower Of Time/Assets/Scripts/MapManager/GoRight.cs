using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CurrentStatus;

public class GoRight : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collided = collision.gameObject;
        if (Current.Cleared && collided.tag == "Player")
            Current.Map.Goto("Droite", this);
    }

    void Start()
    {
        transform.position = new Vector3(Current.HalfWidth, 0, 3);
        Debug.Log($"Loaded right door at {transform.position.ToString()}");
    }
}
