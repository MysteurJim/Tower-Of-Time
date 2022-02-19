using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float initial_moveSpeed;
    public GameObject spawn;
    public int Life;
    
    

    private Vector2 direction_right;
    private Vector2 direction_up;
    private float moveSpeed;
    private int InitialLife;


    private void Start()
    {
        Respawn();
        InitialLife = Life;
        moveSpeed = initial_moveSpeed;

    }

    private void Update()
    {
        float horizontalMovement = Input.GetAxis("Horizontal") * moveSpeed * Time.deltaTime;
        direction_right = new Vector2(horizontalMovement, 0);
        transform.Translate(direction_right * moveSpeed * Time.deltaTime);

        float verticalMovement = Input.GetAxis("Vertical") * moveSpeed * Time.deltaTime;
        direction_up = new Vector2(0, verticalMovement);
        transform.Translate(direction_up * moveSpeed * Time.deltaTime);
        
    
    }

    public void Respawn()
    {
        Vector2 co_spawn = new Vector2(spawn.transform.position.x, spawn.transform.position.y);
        Vector2 co = transform.position;
        transform.Translate(co_spawn - co);
        Life -= 1;
    }


}
