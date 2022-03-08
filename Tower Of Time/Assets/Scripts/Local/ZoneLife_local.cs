using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ZoneLife_local : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        GameObject objet = collision.gameObject;
        PlayerMovement script = objet.GetComponent<PlayerMovement>();
        if (script.Life != 5)
        {
            script.Life +=1;
            Destroy(this.gameObject);
        }
    }
}
