using UnityEngine;
using Mirror;
using Steamworks;

public class RoomPlayer : NetworkBehaviour
{
    [Header("Character Info")]
    [SyncVar]
    public bool isLeader = false;
    [SyncVar]
    public bool isPlayerReady = false;
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

    [Header("Door")]
    [SerializeField]
    private DoorManager doorManager;
    [SerializeField]
    private bool canChangeDoorState;

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
        //Debug.Log("ROOM PLAYER CLIENT STARTED");
        Game.roomPlayers.Add(this);
        RoomPlayerManager.instance.UpdatePlayerInfos();
    }

    public override void OnStopClient()
    {
        //Debug.Log(playerName + " IS LEAVING");
        Game.roomPlayers.Remove(this);
        if (Game.CheckIsLobbyActiveScene()) RoomPlayerManager.instance.UpdatePlayerInfos();
    }

    public override void OnStartAuthority()
    {
        SetIsLeader();
        CmdSetPlayerName(SteamFriends.GetPersonaName().ToString());

        gameObject.name = "LocalRoomPlayer";

        thirdPersonCamera.SetActive(true);
        playerMove.enabled = true;
        controller.enabled = true;
        capCol.enabled = false;
        //Debug.Log(playerName + " HAS AUTHORITY");

        SelectCharacterManager.instance.SetState(false);
        RoomPlayerManager.instance.FindMyRoomPlayer();
        if (isLeader)
        {
            RoomPlayerManager.instance.SetPlayerKickButton(playerCharIndex);
        }

        InputManager.Controls.UI.Paused.performed += ctx => toPause = true;
        InputManager.Controls.UI.Paused.canceled += ctx => toPause = false;

        InputManager.Controls.EarthBending.OpenWall.performed += ctx => ChangeDoorState();
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

    private void Update()
    {
        if (isLocalPlayer && Game.CheckIsLobbyActiveScene())
        {
            if (toPause)
            {
                if (!isPaused) RoomPlayerManager.instance.PauseGame();
                else RoomPlayerManager.instance.ResumeGame();
            }

            if (isPaused) UnlockCursor();
            else LockCursor();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "StartGame")
        {
            CmdSetIsReady(true);
            Debug.Log("PLAYER IS READY");
        }

        if (other.gameObject.tag == "DoorOpenTrigger")
        {
            doorManager = other.gameObject.GetComponent<DoorManager>();
            canChangeDoorState = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CmdSetIsReady(false);
        doorManager = null;
        canChangeDoorState = false;
    }

    [Command]
    private void CmdSetIsReady(bool val)
    {
        isPlayerReady = val;
        Game.CheckIsReady();
    }

    public void QuitLobby()
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

    private void ChangeDoorState()
    {
        if (canChangeDoorState) doorManager.ChangeDoorState();
    }

    public void StopPlayer()
    {
        thirdPersonCamera.SetActive(false);
        playerMove.enabled = false;
    }

    public void StartGame()
    {
        Debug.Log("RUNNING START GAME");
        if (hasAuthority) CmdStartGame();
    }

    [Command]
    private void CmdStartGame() => Game.StartGame();
}