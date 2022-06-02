using UnityEngine;
using Photon.Pun;
using System.Collections.Generic;
using UnityEngine.Tilemaps;

public class PlayerController : MonoBehaviour
{
    public God god;
    private PhotonView view;
    
    public Vector3 position => transform.position;

    public God God => god;
    public PhotonView View => view;
    public Transform SwordPlacement;
    public LayerMask enemyLayers;
    public BarManager barManager;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();
       

        TilemapRenderer[] background = FindObjectsOfType<TilemapRenderer>(false);

        foreach (TilemapRenderer tmR in background)
        {
            tmR.sortingOrder = -100 + (tmR.CompareTag("Wall") ? 1 : 0);
        }
        DontDestroyOnLoad(transform.gameObject);
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
