using Photon.Pun;

public abstract class God
{
    protected PlayerController playerController;
    protected Ability main;

    public PlayerController PlayerController => playerController;
    public Ability Main => main;

    public PhotonView View => playerController.View;
    public God(PlayerController playerController)
    {
        this.playerController = playerController;
    }

    
}
