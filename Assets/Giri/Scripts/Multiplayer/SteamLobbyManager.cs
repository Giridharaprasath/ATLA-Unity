using System.Collections.Generic;
using UnityEngine;
using Mirror;
using Steamworks;

public class SteamLobbyManager : MonoBehaviour
{
    public static SteamLobbyManager instance;

    public bool isOnline; 

    protected Callback<LobbyCreated_t> lobbyCreated;
    protected Callback<GameLobbyJoinRequested_t> gameLobbyJoinRequested;
    protected Callback<LobbyEnter_t> lobbyEntered;
    protected Callback<LobbyMatchList_t> lobbyMatchList;
    protected Callback<LobbyDataUpdate_t> lobbyDataUpdate;

    private const string hostAddressKey = "HostAddress";

    private ATLANetworkManager networkManager;

    public ulong current_lobbyID;
    public List<CSteamID> lobbyIDs = new List<CSteamID>();

    public List<GameObject> listOfLobbyInfos = new List<GameObject>();
    public GameObject listOfLobbyInfoPrefab;
    public GameObject lobbyListParent;

    private void Start()
    {
        networkManager = GetComponent<ATLANetworkManager>();

        if (!SteamManager.Initialized) { isOnline = false; return; }
        else isOnline = true;
        if (instance == null) instance = this;

        lobbyCreated = Callback<LobbyCreated_t>.Create(OnLobbyCreated);
        gameLobbyJoinRequested = Callback<GameLobbyJoinRequested_t>.Create(OnGameLobbyJoinRequested);
        lobbyEntered = Callback<LobbyEnter_t>.Create(OnLobbyEntered);
        lobbyMatchList = Callback<LobbyMatchList_t>.Create(OnGetLobbiesList);
        lobbyDataUpdate = Callback<LobbyDataUpdate_t>.Create(OnGetLobbyInfo);
    }

    public void HostLobby()
    {
        Debug.Log("RUNNIN HOST LOBBY: LOBBY CREATED OF TYPE: PUBLIC MAX 4");
        SteamMatchmaking.CreateLobby(ELobbyType.k_ELobbyTypePublic, networkManager.maxConnections);
    }

    public void JoinLobby(CSteamID lobbyID)
    {
        Debug.Log("RUNNING JOIN LOBBY: JOINING LOBBY WITH STEAM ID: " + lobbyID.ToString());
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
        //Debug.Log("RUNNING ON LOBBY CREATED");
        if (callback.m_eResult != EResult.k_EResultOK) return;

        networkManager.StartHost();

        SteamMatchmaking.SetLobbyData(
            new CSteamID(callback.m_ulSteamIDLobby),
            hostAddressKey,
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
        //Debug.Log("RUNNING ON GAME LOBBY JOIN REQUESTED");
        SteamMatchmaking.JoinLobby(callback.m_steamIDLobby);
    }

    private void OnLobbyEntered(LobbyEnter_t callback)
    {
        current_lobbyID = callback.m_ulSteamIDLobby;
        Debug.Log("RUNNING ON LOBBY ENTERED: LOBBY WITH ID: " + current_lobbyID.ToString());
        if (NetworkServer.active) return;

        string hostAddress = SteamMatchmaking.GetLobbyData(
            new CSteamID(callback.m_ulSteamIDLobby),
            hostAddressKey);

        networkManager.networkAddress = hostAddress;
        networkManager.StartClient();
    }

    private void OnGetLobbiesList(LobbyMatchList_t result)
    {
        int c = 0;
        for (int i = 0; i < result.m_nLobbiesMatching; i++)
        {
            CSteamID lobbyID = SteamMatchmaking.GetLobbyByIndex(i);
            string gN = SteamMatchmaking.GetLobbyData((CSteamID)lobbyID, "GameName");
            //Debug.Log(gN.Equals("ATLA"));
            if (gN.Equals("ATLA"))
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
        //Debug.Log("RUNNING ON GET LOBBY INFO");
        for (int i = 0; i < lobbyIDs.Count; i++)
        {
            if (lobbyIDs[i].m_SteamID == result.m_ulSteamIDLobby)
            {
                GameObject listOfLobbyInfo = Instantiate(listOfLobbyInfoPrefab) as GameObject;
                ListOfLobbyInfo listOfLobbyInfoScript = listOfLobbyInfo.GetComponent<ListOfLobbyInfo>();

                listOfLobbyInfoScript.lobbySteamID = (CSteamID)lobbyIDs[i].m_SteamID;
                listOfLobbyInfoScript.lobbyName = SteamMatchmaking.GetLobbyData((CSteamID)lobbyIDs[i].m_SteamID, "Name");
                listOfLobbyInfoScript.numOfPlayers = SteamMatchmaking.GetNumLobbyMembers((CSteamID)lobbyIDs[i].m_SteamID);
                listOfLobbyInfoScript.maxPlayers = SteamMatchmaking.GetLobbyMemberLimit((CSteamID)lobbyIDs[i].m_SteamID);
                //listOfLobbyInfoScript.isGameStarted = networkManager.isGameStarted;
                listOfLobbyInfoScript.SetListOfLobbyInfo();

                listOfLobbyInfoScript.transform.SetParent(lobbyListParent.transform);
                listOfLobbyInfoScript.transform.localScale = Vector3.one;

                listOfLobbyInfos.Add(listOfLobbyInfo);
            }
        }
    }

    public void ClearLobbyList()
    {
        lobbyIDs.Clear();
        foreach (GameObject gO in listOfLobbyInfos)
        {
            GameObject nO = gO;
            Destroy(nO);
            nO = null;
        }
        listOfLobbyInfos.Clear();
    }
}