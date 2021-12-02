using UnityEngine;
using Mirror;
using Steamworks;

public class GamePlayer : NetworkBehaviour
{
    [Header("GAME PLAYER INFO")]
    [SyncVar]
    public bool isLeader = false;
    [SyncVar]
    public string playerName;
    [SyncVar]
    public int playerConnID;
    [SyncVar]
    public string playerCharName;
    [SyncVar]
    public int playerCharIndex;
    [SerializeField]
    private string thisCharName;

    [Header("Player Scripts")]
    [SerializeField]
    private GameObject thirdPersonCamera;
    [SerializeField]
    private AangPlayerMovement playerMove;
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private CapsuleCollider capCol;
    [SerializeField]
    private AirBending airBending;
    [SerializeField]
    private EarthBending earthBending;
    [SerializeField]
    private FireBending fireBending;

    [HideInInspector]
    public bool toPause, isPaused;

    private ATLANetworkManager game;
    private ATLANetworkManager Game
    {
        get
        {
            if (game != null) return game;
            return game = ATLANetworkManager.singleton as ATLANetworkManager;
        }
    }

    public override void OnStartClient()
    {
        //Debug.Log("GAME PLAYER CLIENT STARTED");
        Game.gamePlayers.Add(this);
        GamePlayerManager.instance.UpdatePlayerInfos();
    }

    public override void OnStopClient()
    {
        Debug.Log(playerName + " IS LEAVING");
        Game.gamePlayers.Remove(this);
        GamePlayerManager.instance.UpdatePlayerInfos();
    }

    public override void OnStartAuthority()
    {
        SetIsLeader();
        CmdSetPlayerName(SteamFriends.GetPersonaName().ToString());

        gameObject.name = "LocalGamePlayer";
        thirdPersonCamera.SetActive(true);
        playerMove.enabled = true;
        controller.enabled = true;
        capCol.enabled = false;
        SetAbilities();
        GamePlayerManager.instance.FindMyGamePlayer();
        if (isLeader) GamePlayerManager.instance.SetPlayerKickButton(playerCharIndex);

        InputManager.Controls.UI.Paused.performed += ctx => toPause = true;
        InputManager.Controls.UI.Paused.canceled += ctx => toPause = false;
    }

    [Server]
    public void ServerSetCharIndex(int i) => playerCharIndex = i;

    [Server]
    public void ServerSetCharName() => playerCharName = thisCharName;

    [Server]
    public void ServerSetConnID(int id) => playerConnID = id;

    private void SetIsLeader()
    {
        if (isServer && isLocalPlayer) CmdSetIsLeader();
    }

    [Command]
    private void CmdSetIsLeader() => isLeader = true;

    [Command]
    private void CmdSetPlayerName(string name) => playerName = name;

    private void SetAbilities()
    {
        if (thisCharName == "Air")
        {
            airBending = GetComponent<AirBending>();
            airBending.enabled = true;
        }
        if (thisCharName == "Earth")
        {
            earthBending = GetComponent<EarthBending>();
            earthBending.enabled = true;
        }
        else if (thisCharName == "Fire")
        {
            fireBending = GetComponent<FireBending>();
            fireBending.enabled = true;
        }
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            if (toPause)
            {
                if (!isPaused) GamePlayerManager.instance.PauseGame();
                else GamePlayerManager.instance.ResumeGame();
            }

            if (isPaused) UnlockCursor();
            else LockCursor();
        }
    }

    public void QuitGame()
    {
        if (hasAuthority)
        {
            if (isLeader) Game.StopHost();
            else Game.StopClient();

            SteamMatchmaking.LeaveLobby((CSteamID)RoomPlayerManager.instance.lobbyID);
            UnlockCursor();
        }
    }

    private void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
    }
}
