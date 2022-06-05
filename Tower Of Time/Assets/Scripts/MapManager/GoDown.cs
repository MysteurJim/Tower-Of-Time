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
            Current.Map.Goto("Bas", this);
    }
    
    void Start()
    {
        transform.position = new Vector3(0, -Current.HalfHeight, 3);
        Debug.Log($"Loaded down door at {transform.position.ToString()}");
    }
}
