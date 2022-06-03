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
    public FonduBlack fb;
    public bool multi;
    public Inventory inventory;

    public string CurrentRooms;
    public bool WithGod = false;


    void Start()
    {
        view = GetComponent<PhotonView>();

        
        TilemapRenderer[] background = FindObjectsOfType<TilemapRenderer>(false);

        foreach (TilemapRenderer tmR in background)
        {
            tmR.sortingOrder = -100 + (tmR.CompareTag("Wall") ? 1 : 0);
        }
        DontDestroyOnLoad(transform.gameObject);
        CurrentRooms = "Etage Medusa";
    }

    // Update is called once per frame
    void Update()
    {
        if (WithGod)
        {
            if (Input.GetButton("Fire1"))
                god.UseMainAtk();


            if (Input.GetKeyDown("q"))
                god.UseQ();


            if (Input.GetKeyDown("e"))
                god.UseE();
        }
       
    }
    //FIN DE GAME
    public void Dead()
    {
        fb.StartFade();
        saveDatas();
        StartCoroutine(WaiForReturnToMenu());
    }

    IEnumerator<WaitForSeconds> WaiForReturnToMenu()
    {
        yield return new WaitForSeconds(6);
        gameObject.GetComponent<PlayerMovement>().set.SetActive(true);
        gameObject.GetComponent<PlayerMovement>().set.GetComponent<SettingsControl>().DisconnectPlayer();
        
    }

    //DATAS MANAGERS
    public void saveDatas()
    {
        Debug.Log("Datas save...");
        Datas datas = new Datas();
        datas.playerName = PhotonNetwork.NickName;
        datas.nbr_piece = inventory.coinsCount;
        datas.current_etage = CurrentRooms;
        datas.hit_points = (int)god.MaxHitPoints;
        DataManagers.Save(datas, datas.playerName + ".ToT");

    }

    public void loadDatas()
    {
        Datas datas = (Datas)DataManagers.Load(PhotonNetwork.NickName + ".ToT");
        inventory.coinsCount = datas.nbr_piece;
        god.UpdateMaxHealth(datas.hit_points);
        god.HealPlayer(datas.hit_points);
        CurrentRooms = datas.current_etage;
    }


    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 100, 1f);
    }
}
