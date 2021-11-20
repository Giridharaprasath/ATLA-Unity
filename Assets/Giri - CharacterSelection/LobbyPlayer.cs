using UnityEngine;
using Mirror;

public class LobbyPlayer : NetworkBehaviour
{
    [Header("LOBBY PLAYER INFO")]
    [SyncVar]
    public int charIndex;
    [SyncVar]
    public string charName;
    [SyncVar]
    public bool isLeader;
    [SyncVar]
    public bool isReady;

    [Header("Door")]
    [SerializeField]
    private LobbyDoor lobbyDoor;
    [SerializeField]
    private bool canChangeDoorState;

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
    private string thisCharName;

    public bool toPause;
    public bool isPaused;

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
        Lobby.lobbyPlayers.Add(this);
        LobbyManager.instance.UpdatePlayerInfos();
    }

    public override void OnStopClient()
    {
        Lobby.lobbyPlayers.Remove(this);
        //Debug.Log(charName + " IS LEAVING");
        if (Lobby.CheckScene()) LobbyManager.instance.UpdatePlayerInfos();
    }

    public override void OnStartAuthority()
    {
        SetIsLeader();
        gameObject.name = "LocalRoomPlayer";
        thirdPersonCamera.SetActive(true);
        playerMove.enabled = true;
        controller.enabled = true;
        capCol.enabled = false;

        LobbyCharSelect.Instance.SetState(false);
        LobbyManager.instance.FindMyPlayer();
        if (isLeader) LobbyManager.instance.SetPlayerKickButton(charIndex);
        //LockCursor();

        InputManager.Controls.UI.Paused.performed += ctx => toPause = true;
        InputManager.Controls.UI.Paused.canceled += ctx => toPause = false;

        InputManager.Controls.EarthBending.OpenWall.performed += ctx => ChangeDoorState();
    }

    [Server]
    public void ServerSetCharIndex(int i) => charIndex = i;

    [Server]
    public void ServerSetCharName() => charName = thisCharName;

    private void SetIsLeader()
    {
        if (isServer && isLocalPlayer) CmdSetIsLeader();
    }

    [Command]
    private void CmdSetIsLeader() => isLeader = true;

    private void Update()
    {
        if (isLocalPlayer)
        {
            if (toPause)
            {
                if (!isPaused) LobbyManager.instance.PauseGame();
                else LobbyManager.instance.ResumeGame();
            }
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
            lobbyDoor = other.gameObject.GetComponent<LobbyDoor>();
            canChangeDoorState = true;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        CmdSetIsReady(false);
        lobbyDoor = null;
        canChangeDoorState = false;
    }

    [Command]
    private void CmdSetIsReady(bool val)
    {
        isReady = val;
        Lobby.CheckIsReady();
    }

    private void ChangeDoorState()
    {
        if (canChangeDoorState) lobbyDoor.ChangeDoorState();
    }

    private void LockCursor()
    {
        Cursor.visible = false;
        Cursor.lockState = CursorLockMode.Locked;
    }

    private void UnlockCursor()
    {
        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.None;
    }

    public void QuitLobby()
    {
        if (hasAuthority)
        {
            if (isLeader) Lobby.StopHost();

            else Lobby.StopClient();
        }
    }

    public void StopPlayer()
    {
        thirdPersonCamera.SetActive(false);
        playerMove.enabled = false;
        UnlockCursor();
    }
}