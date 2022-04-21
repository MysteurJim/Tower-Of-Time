using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordMouvement : MonoBehaviour
{
    public Vector3 position => transform.position;
    public GameObject Player;

    // Update is called once per frame
    void Update()
    {
        
        Vector3 mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 distance = mousePosition - transform.position;
        Vector3 playerPosition = Player.GetComponent<Transform>().position;
        float angle = Mathf.Atan(distance.y / distance.x);
        if(distance.x < 0)
        {
            angle += Mathf.PI;
        }
        Vector3 range = Player.GetComponent<Sword>().attackRange * distance.normalized;
        float x =  playerPosition.x + Mathf.Cos(angle)+range.x;
        float y = playerPosition.y + Mathf.Sin(angle)+range.y;
        float z = 0;

        transform.position = new Vector3(x, y, z);
    }
}
