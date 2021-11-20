using System.Collections;
using UnityEngine;
using TMPro;
using Mirror;
using Steamworks;

public class RoomPlayerManager : NetworkBehaviour
{
    public static RoomPlayerManager instance;

    [Header("LOADING SCREEN")]
    [SerializeField]
    private CanvasGroup stopLoadCanvas;
    [SerializeField]
    private Animator animator;

    [Header("MY ROOM PLAYER")]
    [SerializeField]
    private RoomPlayer myRoomPlayer;

    [Header("PAUSE MENU UI")]
    [SerializeField]
    private GameObject pauseMenuUI;
    [SerializeField]
    private TMP_Text lobbyNameText;
    [SerializeField]
    private GameObject[] playerInfos;
    [SerializeField]
    private TMP_Text[] playerNameTexts;
    [SerializeField]
    private GameObject[] playerKickButtons;

    [Header("Game Info")]
    public ulong lobbyID;
    [SerializeField]
    private string lobbyName;

    private ATLANetworkManager game;
    private ATLANetworkManager Game
    {
        get
        {
            if (game != null) return game;
            return game = ATLANetworkManager.singleton as ATLANetworkManager;
        }
    }

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public override void OnStartClient()
    {
        //Debug.Log("ROOM PLAYER MANAGER CLIENT STARTED");
        StartCoroutine(StopLoading(2));
        SetState(false);
        UpdateLobbyName();
        animator.enabled = false;
    }

    public override void OnStopClient()
    {
        //Debug.Log("ROOM PLAYER MANAGER CLIENT STOPPED");
        SetState(false);
    }

    private void UpdateLobbyName()
    {
        //Debug.Log("RUNNING UPDATE LOBBY NAME");
        lobbyID = Game.GetComponent<SteamLobbyManager>().current_lobbyID;
        lobbyName = SteamMatchmaking.GetLobbyData((CSteamID)lobbyID, "Name");
        lobbyNameText.text = lobbyName;
    }

    public void FindMyRoomPlayer()
    {
        //Debug.Log("RUNNING FIND MY ROOM PLAYER");
        myRoomPlayer = GameObject.Find("LocalRoomPlayer").GetComponent<RoomPlayer>();
    }

    public void CanStartGameAnim() => StartGameIntro();

    public void StartGameIntro()
    {
        animator.enabled = true;
        InputManager.DisableEverything();
    }

    public void StartGame() => myRoomPlayer.StartGame();

    private void SetState(bool val) => pauseMenuUI.SetActive(val);

    public void PauseGame()
    {
        SetState(true);
        myRoomPlayer.isPaused = true;
        myRoomPlayer.toPause = false;
        InputManager.DisablePlayer();
    }

    public void ResumeGame()
    {
        SetState(false);
        myRoomPlayer.isPaused = false;
        myRoomPlayer.toPause = false;
        InputManager.EnablePlayer();
    }

    public void LeaveLobby() => myRoomPlayer.QuitLobby();

    public void UpdatePlayerInfos()
    {
        SetPlayerInfos();
        foreach (var player in Game.roomPlayers)
        {
            int i = player.playerCharIndex;
            playerInfos[i].SetActive(true);
            playerNameTexts[i].text = player.playerName;
        }
    }

    private void SetPlayerInfos()
    {
        for (int i = 0; i < 4; i++)
        {
            playerInfos[i].SetActive(false);
            playerNameTexts[i].text = string.Empty;
            playerKickButtons[i].SetActive(false);
        }
    }

    public void SetPlayerKickButton(int i)
    {
        for (int j = 0; j < 4; j++)
        {
            if (i != j) playerKickButtons[j].SetActive(true);
        }
    }

    public void KickPlayer(int i)
    {
        foreach (var player in Game.roomPlayers)
        {
            if (player.playerCharIndex == i)
            {
                player.connectionToClient.Disconnect();
            }
        }
    }

    public void Invite()
    {
        SteamFriends.ActivateGameOverlayInviteDialog((CSteamID)lobbyID);
    }

    public void ChangeChar()
    {
        CmdChangeChar(myRoomPlayer);
        ResumeGame();
        SelectCharacterManager.instance.SetState(true);
    }

    [Command(requiresAuthority = false)]
    private void CmdChangeChar(RoomPlayer player) => Game.ChangeMyChar(player);

    private IEnumerator StopLoading(float t)
    {
        float val = stopLoadCanvas.alpha;
        float time = 0;

        while (time < t)
        {
            stopLoadCanvas.alpha = Mathf.Lerp(val, 0, time / t);
            time += Time.deltaTime;
            yield return null;
        }
        stopLoadCanvas.alpha = 0;
    }
}
