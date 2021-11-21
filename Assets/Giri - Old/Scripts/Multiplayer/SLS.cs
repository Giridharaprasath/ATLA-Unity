using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;

// Steam Lobby Script
public class SLS : MonoBehaviour
{
    public static SLS Instance;

    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;
    protected Callback<LobbyEnter_t> lobbyEntered;
    protected Callback<LobbyMatchList_t> Callback_lobbyList;
    protected Callback<LobbyDataUpdate_t> Callback_lobbyInfo;

    private const string HostAddressKey = "HostAddress";

    private NetworkManager networkManager;

    public ulong current_lobbyID;
    public List<CSteamID> lobbyIDs = new List<CSteamID>();

    public List<GameObject> lLIS = new List<GameObject>();
    public GameObject lLISPrefab;
    public GameObject contentPanel;

    private void Start()
    {
        networkManager = GetComponent<NetworkManager>();

        if (!SteamManager.Initialized) { return; }
        MakeInstance();

        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
        lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
        Callback_lobbyList = Callback<LobbyMatchList_t>.Create(OnGetLobbiesList);
        Callback_lobbyInfo = Callback<LobbyDataUpdate_t>.Create(OnGetLobbyInfo);
    }

    void MakeInstance()
    {
        if (Instance == null) Instance = this;
    }

    public void HostLobby()
    {
        Debug.Log("HOSTLOBBY: LOBBY CREATED OF TYPE: PUBLIC WITH MAX 4");
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePublic, networkManager.maxConnections);
    }

    public void JoinLobby(CSteamID lobbyID)
    {
        Debug.Log("JOINLOBBY: JOINING LOBBY WITH STEAM ID: " + lobbyID.ToString());
        SteamMatchmaking.JoinLobby(lobbyID);
    }

    public void GetListOfLobbies()
    {
        if (lobbyIDs.Count > 0) lobbyIDs.Clear();

        SteamMatchmaking.AddRequestLobbyListFilterSlotsAvailable(1);

        SteamAPICall_t try_getList = SteamMatchmaking.RequestLobbyList();
    }

    private void OnLobbyCreated(LobbyCreated_t callback)
    {
        Debug.Log("ONLOBBYCREATED");
        if (callback.m_eResult != EResult.k_EResultOK)
            {
                return;
            }

        networkManager.StartHost();

        SteamMatchmaking.SetLobbyData(
            new CSteamID(callback.m_ulSteamIDLobby),
            HostAddressKey,
            SteamUser.GetSteamID().ToString());

        SteamMatchmaking.SetLobbyData(
            new CSteamID(callback.m_ulSteamIDLobby),
            "Name",
            SteamFriends.GetPersonaName().ToString() + "'s lobby");

        SteamMatchmaking.SetLobbyData(
            new CSteamID(callback.m_ulSteamIDLobby),
            "GameName", "ATLA");
    }

    private void OnGameLobbyJoinRequested(GameLobbyJoinRequested_t callback)
    {
        Debug.Log("ONGAMELOBBYJOINREQUESTED");
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        current_lobbyID = callback.m_ulSteamIDLobby;
        Debug.Log("ONLOBBYENTERED: LOBBY WITH ID: " + current_lobbyID.ToString());
        if (NetworkServer.active) { return; }

        string hostAddress = SteamMatchmaking.GetLobbyData(
            new CSteamID(callback.m_ulSteamIDLobby),
            HostAddressKey);

        networkManager.networkAddress = hostAddress;
        networkManager.StartClient();
        lobbyIDs.Clear();
    }

    private void OnGetLobbiesList(LobbyMatchList_t result)
    {
        int c = 0;
        for (int i = 0; i < result.m_nLobbiesMatching; i++)
        {
            CSteamID lobbyID = SteamMatchmaking.GetLobbyByIndex(i);
            if ((lobbyID.m_SteamID, "GameName").Equals("ATLA"))
            {
                lobbyIDs.Add(lobbyID);
                c++;
                SteamMatchmaking.RequestLobbyData(lobbyID);
            }
        }
        Debug.Log("FOUND " + c + " LOBBIES!!");
    }

    private void OnGetLobbyInfo(LobbyDataUpdate_t result)
    {
        Debug.Log("ONGETLOBBYINFO");
        for (int i = 0; i < lobbyIDs.Count; i++)
        {
            if (lobbyIDs[i].m_SteamID == result.m_ulSteamIDLobby)
            {
                GameObject newLLIS = Instantiate(lLISPrefab) as GameObject;
                LLIS newLLISScript = newLLIS.GetComponent<LLIS>();

                newLLISScript.lobbySteamID = (CSteamID)lobbyIDs[i].m_SteamID;
                newLLISScript.lobbyName = SteamMatchmaking.GetLobbyData((CSteamID)lobbyIDs[i].m_SteamID, "Name");
                newLLISScript.numOfPlayers = SteamMatchmaking.GetNumLobbyMembers((CSteamID)lobbyIDs[i].m_SteamID);
                newLLISScript.maxPlayers = SteamMatchmaking.GetLobbyMemberLimit((CSteamID)lobbyIDs[i].m_SteamID);
                newLLISScript.SetLobbyItem();

                newLLISScript.transform.SetParent(contentPanel.transform);
                newLLISScript.transform.localScale = Vector3.one;

                lLIS.Add(newLLIS);
            }
        }
    }

    public void DestroyLLI()
    {
        foreach (GameObject lLI in lLIS)
        {
            GameObject lLIDes = lLI;
            Destroy(lLIDes);
            lLIDes = null;
        }
        lLIS.Clear();
    }
}
