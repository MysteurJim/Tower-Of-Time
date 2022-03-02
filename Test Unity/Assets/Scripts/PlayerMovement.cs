using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;

public class PlayerMovement : MonoBehaviour
{
    public float initial_moveSpeed;
    public GameObject spawn;
    public int Life;
    public Sprite up;
    public Sprite down;

    PhotonView view;

    private Vector2 direction_right;
    private Vector2 direction_up;
    private float moveSpeed;
    private int InitialLife;




    private void Start()
    {
        Respawn();
        InitialLife = Life;
        moveSpeed = initial_moveSpeed;
        view = GetComponent<PhotonView>();
    }

    private void Update()
    {
        if (view.IsMine)
        {
            float vertical = Input.GetAxisRaw("Vertical");
            Vector3 input = new Vector3(Input.GetAxisRaw("Horizontal"), vertical, 0);
            transform.position += input.normalized * moveSpeed * Time.deltaTime;
            if (vertical > 0)
            {
                GetComponent<SpriteRenderer>().sprite = up;
            }
            else
            {
                GetComponent<SpriteRenderer>().sprite = down;
            }
        }
        
    
    }

    public void Respawn()
    {
        Vector2 co_spawn = new Vector2(spawn.transform.position.x, spawn.transform.position.y);
        Vector2 co = transform.position;
        transform.Translate(co_spawn - co);
        Life -= 1;
    }


}
