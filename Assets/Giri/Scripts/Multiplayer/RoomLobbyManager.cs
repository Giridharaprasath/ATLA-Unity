using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using TMPro;
using Steamworks;

public class RoomLobbyManager : MonoBehaviour
{
    public static RoomLobbyManager instance;

    [Header("Lobby UI")]
    [SerializeField]
    private GameObject lobbyUI;
    [SerializeField]
    private TMP_Text lobbyNameText;
    [SerializeField]
    private GameObject lobbyContentPanel;
    [SerializeField]
    private GameObject playerInfoPanelPrefab;

    public bool havePlayerInfoDone = false;
    public List<PlayerInfoPanel> playerInfoPanels = new List<PlayerInfoPanel>();

    public GameObject myPlayer;
    public RoomLobbyPlayer myPlayerScript;

    [Header("Pause Menu")]
    [SerializeField]
    private GameObject pauseMenuUI;

    [Header("Room Lobby Info")]
    public ulong roomLobbyID;
    public string roomLobbyName;

    private ATLANetworkManager room;
    private ATLANetworkManager Room
    {
        get
        {
            if (room != null) return room;
            return room = ATLANetworkManager.singleton as ATLANetworkManager;
        }
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        SetRoomLobbyUI(false);
    }

    public void SetLobbyUI(bool con) => lobbyUI.SetActive(con);

    public void FindMyPlayer()
    {
        myPlayer = GameObject.Find("LocalRoomLobbyPlayer");
        myPlayerScript = myPlayer.GetComponent<RoomLobbyPlayer>();
    }

    public void UpdateLobbyName()
    {
        lobbyNameText.text = "Testing";
        //roomLobbyID = Room.GetComponent<SteamLobbyManager>().current_lobbyID;
        //roomLobbyName = SteamMatchmaking.GetLobbyData((CSteamID)roomLobbyID, "Name");
        //Debug.Log("UPDATELOBBYNAME: LOBBY NAME: " + roomLobbyName);
        //lobbyNameText.text = roomLobbyName;
    }

    public void SetRoomLobbyUI(bool con) => lobbyUI.SetActive(con);

    private void DoCreatePlayerInfoPanel(RoomLobbyPlayer player)
    {
        GameObject newPanel = Instantiate(playerInfoPanelPrefab) as GameObject;
        PlayerInfoPanel newPanelScript = newPanel.GetComponent<PlayerInfoPanel>();

        newPanelScript.playerInfoName = player.playerName;
        newPanelScript.playerInfoConnID = player.playerConnID;
        newPanelScript.SetPlayerNameText();
        newPanelScript.SetKickPlayerButton(player);

        newPanel.transform.SetParent(lobbyContentPanel.transform);
        newPanel.transform.localScale = Vector3.one;

        playerInfoPanels.Add(newPanelScript);
    }

    public void UpdateLobbyUI()
    {
        if (!havePlayerInfoDone) CreatePlayerInfoPanel();
        if (playerInfoPanels.Count < Room.roomLobbyPlayers.Count) CreateNewPlayerInfoPanel();
        if (playerInfoPanels.Count > Room.roomLobbyPlayers.Count) RemovePlayerInfoPanel();
        if (playerInfoPanels.Count == Room.roomLobbyPlayers.Count) UpdatePlayerInfoPanel();
    }

    private void CreatePlayerInfoPanel()
    {
        foreach (RoomLobbyPlayer player in Room.roomLobbyPlayers)
        {
            Debug.Log("CREATPLAYERINFO: PLAYER: " + player.playerConnID);
            DoCreatePlayerInfoPanel(player);
        }
        havePlayerInfoDone = true;
    }

    private void CreateNewPlayerInfoPanel()
    {
        foreach (RoomLobbyPlayer player in Room.roomLobbyPlayers)
        {
            if (!playerInfoPanels.Any(a => a.playerInfoConnID == player.playerConnID))
            {
                Debug.Log("CREATNEWPLAYERINFOPANEL: PLAYER: " + player.playerName);
                DoCreatePlayerInfoPanel(player);
            }
        }
    }

    private void RemovePlayerInfoPanel()
    {
        List<PlayerInfoPanel> del = new List<PlayerInfoPanel>();
        foreach (PlayerInfoPanel infoPanel in playerInfoPanels)
        {
            if (!Room.roomLobbyPlayers.Any(a => a.playerConnID == infoPanel.playerInfoConnID))
            {
                Debug.Log("REMOVEPLAYERINFOPANEL: PLAYER: " + infoPanel.playerInfoConnID.ToString() + " DOESN'T EXIST");
                del.Add(infoPanel);
            }
        }
        if (del.Count > 0)
        {
            foreach (PlayerInfoPanel pan in del)
            {
                GameObject obj = pan.gameObject;
                playerInfoPanels.Remove(pan);
                Destroy(obj);
                obj = null;
            }
        }
    }

    private void UpdatePlayerInfoPanel()
    {
        Debug.Log("UPDATEPLAYERINFOPANEL");
        foreach (RoomLobbyPlayer player in Room.roomLobbyPlayers)
        {
            foreach (PlayerInfoPanel infoPanel in playerInfoPanels)
            {
                if (infoPanel.playerInfoConnID == player.playerConnID)
                {
                    infoPanel.playerInfoName = player.playerName;
                    infoPanel.SetPlayerNameText();

                    if (player.isCharSelected) infoPanel.playerInfoCharName = player.charName;
                    else infoPanel.playerInfoCharName = "Selecting";

                    infoPanel.SetCharNameText();
                }
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        myPlayerScript.isPaused = true;
        myPlayerScript.toPause = false;
        InputManager.DisablePlayer();
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        myPlayerScript.isPaused = false;
        myPlayerScript.toPause = false;
        InputManager.EnablePlayer();
    }

    public void LeaveLobby()
    {
        myPlayerScript.QuitLobby();
    }

    public void KickPlayer(int connID)
    {
        foreach (RoomLobbyPlayer player in Room.roomLobbyPlayers)
        {
            if (player.playerConnID == connID) player.connectionToClient.Disconnect();
        }
    }

    private void Update()
    {
        CheckIfAllPlayersReady();
    }

    private void CheckIfAllPlayersReady()
    {
        bool allPlayersReady = false;
        if (Room.roomLobbyPlayers.Count == Room.roomPlayers.Count)
        {
            foreach (RoomPlayer player in Room.roomPlayers)
            {
                if (player.isPlayerReady) allPlayersReady = true;
                else
                {
                    Debug.Log("Not all Players are Ready");
                    allPlayersReady = false;
                    break;
                }
            }
        }
        else
        {
            Debug.Log("STILL PLAYERS SELECTING CHARACTER");
        }
        if (allPlayersReady)
        {
            Debug.Log("ALL PLAYERS ARE READY");
            myPlayerScript.CanStartGame();
        }
    }
}
