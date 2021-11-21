using UnityEngine;
using Mirror;

public class GameSceneManager : NetworkBehaviour
{
    [Header("CONTINUE GAME ANIM")]
    [SerializeField]
    private Animator animator;

    private CSNM lobby;
    private CSNM Lobby
    {
        get
        {
            if (lobby != null) return lobby;
            return lobby = CSNM.singleton as CSNM;
        }
    }

    public override void OnStartClient()
    {
        Debug.Log("GAME SCENE MANGAER CLIENT STARTED");
        CanContinueGame();
    }

    public override void OnStopClient()
    {
        Debug.Log("GAME SCENE MANAGER CLIENT STOPPPED");
    }

    public void CanContinueGame()
    {
        Debug.Log("RUNNING CAN CONTINUE GAME()");
        CmdChangeToGamePlayer();
        InputManager.EnableEverything();
    }

    [Command(requiresAuthority = false)]
    public void CmdChangeToGamePlayer(NetworkConnectionToClient sender = null)
    {
        Debug.Log("RUNNING CMD CHANGE TO GAME PLAYER");
        Lobby.ChangeToGamePlayer(sender);
    }
}
