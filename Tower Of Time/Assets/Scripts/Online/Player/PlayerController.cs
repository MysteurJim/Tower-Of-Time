using UnityEngine;
using Photon.Pun;

public class PlayerController : MonoBehaviour
{
    private God god;
    private PhotonView view;
    
    public Vector3 position => transform.position;
    public God God => god;
    public PhotonView View => view;
    public Transform SwordPlacement;
    public LayerMask enemyLayers;

    // Start is called before the first frame update
    void Start()
    {
        view = GetComponent<PhotonView>();

        gameObject.AddComponent(typeof(TestGod));
        god = gameObject.GetComponent<TestGod>();
        god.Setup(this);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetButton("Fire1"))
        {
            god.UseMainAtk();
        }

        if (Input.GetKey("q"))
        {
            god.UseQ();
        }
    }

    void OnDrawGizmos()
    {
        Gizmos.color = Color.black;
        Gizmos.DrawWireSphere(Camera.main.ScreenToWorldPoint(Input.mousePosition) + Vector3.forward * 100, 1f);
    }
}
