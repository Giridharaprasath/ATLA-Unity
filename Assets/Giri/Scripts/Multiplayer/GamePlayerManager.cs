using UnityEngine;
using TMPro;
using Mirror;
using Steamworks;

public class GamePlayerManager : NetworkBehaviour
{
    public static GamePlayerManager instance;

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
    [SerializeField]
    private GameObject inviteButton;

    [Header("MY Game PLAYER")]
    [SerializeField]
    private GamePlayer myGamePlayer;

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
        SetState(false);
        UpdateLobbyName();
        CanContinueGame();
        //animator.enabled = false;
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

    public void FindMyGamePlayer()
    {
        //Debug.Log("RUNNING FIND MY ROOM PLAYER");
        myGamePlayer = GameObject.Find("LocalGamePlayer").GetComponent<GamePlayer>();
    }

    private void SetState(bool val) => pauseMenuUI.SetActive(val);

    public void PauseGame()
    {
        SetState(true);
        myGamePlayer.isPaused = true;
        myGamePlayer.toPause = false;
        InputManager.DisablePlayer();
        CheckPlayerCount();
    }

    public void ResumeGame()
    {
        SetState(false);
        myGamePlayer.isPaused = false;
        myGamePlayer.toPause = false;
        InputManager.EnablePlayer();
    }

    public void LeaveLobby() => myGamePlayer.QuitGame();

    private void CheckPlayerCount()
    {
        if (Game.numPlayers == 4) inviteButton.SetActive(false);
        else inviteButton.SetActive(true);
    }

    public void Invite()
    {
        SteamFriends.ActivateGameOverlayInviteDialog((CSteamID)lobbyID);
    }

    private void CanContinueGame()
    {
        //Debug.Log("RUNNING CAN CONTINUE GAME");
        CmdChangeToGamePlayer();
        InputManager.EnableEverything();
    }

    [Command(requiresAuthority = false)]
    private void CmdChangeToGamePlayer(NetworkConnectionToClient sender = null)
    {
        //Debug.Log("RUNNING CMD TO CHANGE TO GAME PLAYER");
        Game.ChangeToGamePlayer(sender);
    }

    public void UpdatePlayerInfos()
    {
        SetPlayerInfos();
        foreach (var player in Game.gamePlayers)
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
        foreach (var player in Game.gamePlayers)
        {
            if (player.playerCharIndex == i)
            {
                player.connectionToClient.Disconnect();
            }
        }
    }
}