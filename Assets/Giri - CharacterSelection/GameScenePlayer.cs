using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class GameScenePlayer : NetworkBehaviour
{
    [Header("GAME PLAYER INFO")]
    [SyncVar]
    public int charIndex;
    [SyncVar]
    public string charName;
    [SyncVar]
    public bool isLeader;

    [Header("PAUSE MENU")]
    [SerializeField]
    private GameObject pauseMenuUI;
    [SerializeField]
    private Button resumeB;
    [SerializeField]
    private Button leaveB;

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
    private EarthBending earthBending;
    [SerializeField]
    private FireBending fireBending;

    [SerializeField]
    private string thisCharName;

    private bool toPause;
    private bool isPaused;

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
        //DontDestroyOnLoad(gameObject);
        Lobby.gameScenePlayers.Add(this);
    }

    public override void OnStopClient()
    {
        Lobby.gameScenePlayers.Remove(this);
        Debug.Log(charName + " IS LEAVING");
    }

    public override void OnStartAuthority()
    {
        gameObject.name = "LocalGamePlayer";
        thirdPersonCamera.SetActive(true);
        playerMove.enabled = true;
        controller.enabled = true;
        capCol.enabled = false;
        earthBending.enabled = true;
        fireBending.enabled = true;

        //LockCursor();

        InputManager.Controls.UI.Paused.performed += ctx => toPause = true;
        InputManager.Controls.UI.Paused.canceled += ctx => toPause = false;
    }

    [Server]
    public void ServerSetCharIndex(int i) => charIndex = i;

    [Server]
    public void ServerSetCharName() => charName = thisCharName;

    [Server]
    public void ServerSetIsLeader(bool val) => isLeader = val;

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

    private void Update()
    {
        if (isLocalPlayer)
        {
            if (toPause)
            {
                if (!isPaused) PauseGame();
                else ResumeGame();
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        isPaused = true;
        toPause = false;
        InputManager.DisablePlayer();
        UnlockCursor();
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        isPaused = false;
        toPause = false;
        InputManager.EnablePlayer();
        LockCursor();
    }

    public void QuitLobby()
    {
        if (isLeader) Lobby.StopHost();

        else Lobby.StopClient();
    }
}
