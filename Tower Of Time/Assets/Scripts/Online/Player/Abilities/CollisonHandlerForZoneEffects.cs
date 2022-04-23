using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisonHandlerForZoneEffects : MonoBehaviour
{
    private List<GameObject> collisions;

    public List<GameObject> Collisions => collisions;

    // Start is called before the first frame update
    void Start()
    {
        collisions = new List<GameObject>();
    }

    void OnCollisionEnter(Collision collision) => collisions.Add(collision.gameObject);
 
    void OnCollisionExit(Collision collision) => collisions.Remove(collision.gameObject);
}
