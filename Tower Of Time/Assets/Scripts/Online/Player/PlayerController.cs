using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    private God god;
    private PhotonView view;
    
    public Vector3 position => transform.position;
    public God God => god;
    public PhotonView View => view; 

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();

        god = null;
        SetGod(new TestGod(this));
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1") && view.IsMine && god.Main.TryUse())
        {
            StartCoroutine(god.Main.StartCooldown());
        }
    }

    public bool SetGod(God god, bool enforce = false)
    {
        if (this.god != null && !enforce)
            return false;
          
        this.god = god;
        return true;
    }
}
