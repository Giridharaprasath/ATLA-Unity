using System.Collections;
using UnityEngine;
using TMPro;
using Mirror;

public class LobbyManager : NetworkBehaviour
{
    public static LobbyManager instance;

    [Header("START GAME ANIM")]
    [SerializeField]
    private Animator animator;

    [Header("MY ROOM PLAYER")]
    [SerializeField]
    private LobbyPlayer myPlayer;

    [Header("PAUSE MENU UI")]
    [SerializeField]
    private GameObject pauseMenuUI;
    [SerializeField]
    private GameObject[] playerInfos;
    [SerializeField]
    private TMP_Text[] playerNameTexts;
    [SerializeField]
    private GameObject[] playerKickButtons;
    [SerializeField]
    private CanvasGroup loadCanvas;

    private CSNM lobby;
    private CSNM Lobby
    {
        get
        {
            if (lobby != null) return lobby;
            return lobby = CSNM.singleton as CSNM;
        }
    }

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    public override void OnStartClient()
    {
        Debug.Log("ROOM PLAYER MANAGER CLIENT STARTED");
        SetState(false);
        StartCoroutine(StopLoadLobby(2));
    }

    public override void OnStopClient()
    {
        Debug.Log("ROOM PLAYER MANAGER CLIENT STOPPED");
    }

    public void FindMyPlayer()
    {
        myPlayer = GameObject.Find("LocalRoomPlayer").GetComponent<LobbyPlayer>();
    }

    public void CanStartGameAnim()
    {
        Debug.Log("RUNNING CAN START GAME ANIM");
        StartGameIntro();
    }

    public void StartGameIntro()
    {
        Debug.Log("RUNNING START GAME INTRO");
        animator.enabled = true;
        InputManager.DisableEverything();
    }

    public void StartGame()
    {
        Debug.Log("RUNNING START GAME");
        Lobby.StartGame();
    }

    public void SetState(bool val)
    {
        pauseMenuUI.SetActive(val);
    }

    public void PauseGame()
    {
        SetState(true);
        myPlayer.isPaused = true;
        myPlayer.toPause = false;
        InputManager.DisablePlayer();
    }

    public void ResumeGame()
    {
        SetState(false);
        myPlayer.isPaused = false;
        myPlayer.toPause = false;
        InputManager.EnablePlayer();
    }

    public void LeaveLobby() => myPlayer.QuitLobby();

    public void UpdatePlayerInfos()
    {
        SetPlayerInfos();
        foreach (var player in Lobby.lobbyPlayers)
        {
            int i = player.charIndex;
            playerInfos[i].SetActive(true);
            playerNameTexts[i].text = player.charName;
        }
    }

    private void SetPlayerInfos()
    {
        for (int i = 0; i < 4; i++)
        {
            playerInfos[i].SetActive(false);
            playerNameTexts[i].text = string.Empty;
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
        Debug.Log("RUNNING KICK PLAYER");
        foreach (var player in Lobby.lobbyPlayers)
        {
            if (player.charIndex == i)
            {
                //Debug.Log(player);
                player.GetComponent<NetworkIdentity>().connectionToClient.Disconnect();
            }
        }
    }

    public void ChangeChar()
    {
        //Debug.Log("RUNNING CHANGE CHAR");
        CmdChangeChar(myPlayer);
        SetState(false);
        InputManager.EnablePlayer();
        LobbyCharSelect.Instance.SetState(true);
    }

    [Command(requiresAuthority = false)]
    private void CmdChangeChar(LobbyPlayer player)
    {
        //Debug.Log("RUNNING CMD CHANGE CHAR FOR: " + player);
        Lobby.ChangeMyChar(player);
    }

    private IEnumerator StopLoadLobby(float t)
    {
        float val = loadCanvas.alpha;
        float time = 0;

        while (time < t)
        {
            loadCanvas.alpha = Mathf.Lerp(val, 0, time / t);
            time += Time.deltaTime;
            yield return null;
        }
        loadCanvas.alpha = 0;
    }
}
