using Photon.Pun;
using UnityEngine;

public abstract class God : MonoBehaviour
{
    protected PlayerController playerController;
    protected Ability mainAtk;
    protected Ability q;
    protected string name;

    public PlayerController PlayerController => playerController;
    public Ability MainAtk => mainAtk;
    public Ability Q => q;
    public string Name => Name;

    public PhotonView View => playerController.View;

    public virtual void Setup(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    public virtual bool UseMainAtk() => mainAtk.TryUse();

    public virtual bool UseQ() => q.TryUse(); 
}
