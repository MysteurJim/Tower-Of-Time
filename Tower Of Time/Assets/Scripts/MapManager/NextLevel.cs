using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using CurrentStatus;

public class NextLevel : MonoBehaviour
{
    void OnCollisionEnter2D(Collision2D collision)
    {
        GameObject collided = collision.gameObject;
        if (Current.Cleared && collided.tag == "Player")
            GameLauncher.UpOneLevel();
    }

    void Start()
    {
        transform.position = new Vector3(0, 0, 3);
    }
}
