using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollisionHandlerForZoneEffects : MonoBehaviour
{
    private List<GameObject> collisions;

    public List<GameObject> Collisions => collisions;

    // Start is called before the first frame update
    void Start()
    {
        collisions = new List<GameObject>();
    }
   
    void OnTriggerEnter2D(Collider2D collision)
    { 
        if (collision.tag == "Ennemi")
            collisions.Add(collision.gameObject);
    }
    
    void OnTriggerExit2D(Collider2D collision) => collisions.Remove(collision.gameObject);
}
