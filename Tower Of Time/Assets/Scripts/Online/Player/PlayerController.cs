using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    private PlayerHealth playerHealth;
    private God god;
    private PhotonView view;
    
    public Vector3 position => transform.position;
    public God God => god;
    public PlayerHealth PlayerHealth => playerHealth;
    public PhotonView View => view;
    public Transform SwordPlacement;
    public LayerMask enemyLayers;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
        /*
        gameObject.AddComponent(typeof(Demeter));
        god = gameObject.GetComponent<Demeter>();
        */
        gameObject.AddComponent(typeof(TestGod));
        god = gameObject.GetComponent<TestGod>();
        god.Setup(this);
        playerHealth = GetComponent<PlayerHealth>();
        TilemapRenderer[] background = FindObjectsOfType<TilemapRenderer>(false);

        foreach (TilemapRenderer tmR in background)
        {
            tmR.sortingOrder = -100 + (tmR.CompareTag("Wall") ? 1 : 0);
        }

        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
            god.UseMainAtk();
        

        if (Input.GetKeyDown("q"))
            god.UseQ();
        

        if (Input.GetKeyDown("e"))
            god.UseE();
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 100, 1f);
    }
}
