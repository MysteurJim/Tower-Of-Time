using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Photon.Pun;
using UnityEngine.SceneManagement;

public class PlayerMovement : MonoBehaviour
{
    public float initial_moveSpeed;
    public GameObject spawn;
    public int Life;
    public Sprite up;
    public Sprite down;
    public Sprite left;
    public Sprite right;
    public Fire fire;
    public GameObject settings;

    PhotonView view;

    private Vector2 direction_right;
    private Vector2 direction_up;
    private float moveSpeed;
    private int InitialLife;
    private GameObject set;


    private void Start()
    {
        Respawn();
        InitialLife = Life;
        moveSpeed = initial_moveSpeed;
        view = GetComponent<PhotonView>();
        set = Instantiate(settings);
        DontDestroyOnLoad(set);
        set.SetActive(false);
        set.GetComponent<SettingsControl>().player = this.gameObject;
    }

    private void Update()
    {
        if (view.IsMine)
        {

            float vertical = Input.GetAxisRaw("Vertical");
            float horizontal = Input.GetAxisRaw("Horizontal");
            Vector3 input = new Vector3(horizontal, vertical, 0);
            transform.position += input.normalized * moveSpeed * Time.deltaTime;

            if (vertical > 0)
            {
                GetComponent<SpriteRenderer>().sprite = up;
            }
            else
            {
                if (vertical < 0){
                    GetComponent<SpriteRenderer>().sprite = down;
                }
            }
            if (horizontal > 0)
            {
                GetComponent<SpriteRenderer>().sprite = right;
            }
            else
            {
                if (horizontal < 0)
                {
                    GetComponent<SpriteRenderer>().sprite = left;
                }
            }
        }
        if (Input.GetKeyUp(KeyCode.Escape))
        {
            if(Time.timeScale == 0)
            {
                set.SetActive(false);
                Time.timeScale = 1;
            }
            else
            {
                set.SetActive(true);
                Time.timeScale = 0;
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
