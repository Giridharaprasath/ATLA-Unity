using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class ATLANetworkManager : NetworkManager
{
    [Header("SCENES")]
    [Scene] [SerializeField]
    private string mainMenu = string.Empty;
    [Scene] [SerializeField]
    private string multiplayerLobby = string.Empty;
    [Scene] [SerializeField]
    private string gameScene = string.Empty;

    [Header("PREFABS")]
    [SerializeField]
    private RoomPlayer[] roomPlayerPrefab = null;
    [SerializeField]
    private GamePlayer[] gamePlayerPrefab = null;
    [SerializeField]
    private SelectCharacterManager selectCharacterManager = null;
    [SerializeField]
    private RoomPlayerManager roomPlayerManager = null;
    //[SerializeField]
    //private GamePlayerManager gamePlayerManager = null;

    [Header("PLAYER INSTANCES")]
    public List<RoomPlayer> roomPlayers = new List<RoomPlayer>();
    public List<GamePlayer> gamePlayers = new List<GamePlayer>();

    [Header("CHARACTERS SELECTED")]
    [SerializeField]
    private bool isAirSelected;
    [SerializeField]
    private bool isWaterSelected;
    [SerializeField]
    private bool isEarthSelected;
    [SerializeField]
    private bool isFireSelected;

    public int playerCount;
    public bool isGameStarted;

    public override void OnServerConnect(NetworkConnection conn)
    {
        //Debug.Log("RUNNING ON SERVER CONNECT");
        if (numPlayers >= maxConnections)
        {
            Debug.Log("LOBBY FULL DISCONNECTING " + conn);
            conn.Disconnect();
            return;
        }
        //Debug.Log("NUMBER OF PLAYERS: " + numPlayers + " " + playerCount);
        base.OnServerConnect(conn);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        //Debug.Log("RUNNING ON SERVER DISCONNECT");
        if (conn.identity != null)
        {
            //Debug.Log(conn + " IS LEAVING");
            if (conn.identity.gameObject.GetComponent<RoomPlayer>() != null)
            {
                RemoveRoomPlayer(conn);
            }
            else
            {
                RemoveGamePlayer(conn);
            }
        }
        //Debug.Log("NUMBER OF PLAYERS: " + numPlayers + " " + playerCount);
        base.OnServerDisconnect(conn);
    }

    public override void OnStopServer()
    {
        //Debug.Log("RUNNING ON STOP SERVER");
        roomPlayers.Clear();
        gamePlayers.Clear();
        isAirSelected = false;
        isWaterSelected = false;
        isEarthSelected = false;
        isFireSelected = false;
    }

    public override void ServerChangeScene(string newSceneName)
    {
        //Debug.Log("RUNNING SERVER CHANGE SCENE");
        if (SceneManager.GetActiveScene().path == multiplayerLobby && newSceneName == gameScene)
        {
            foreach (RoomPlayer player in roomPlayers)
            {
                //Debug.Log("MAKING ROOMPLAYER DDOL");
                DontDestroyOnLoad(player.gameObject);
                //player.StopPlayer();
            }
        }
        //Debug.Log("GOING TO NEW SCENE");
        base.ServerChangeScene(newSceneName);
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        //Debug.Log("RUNNING ON SERVER SCENE CHANGED");
        if (sceneName == multiplayerLobby)
        {
            //Debug.Log("INSTANTIATING SELECT CHARACTER MANAGER");
            SelectCharacterManager sCM = Instantiate(selectCharacterManager);
            NetworkServer.Spawn(sCM.gameObject);

            //Debug.Log("INSTANTIATING ROOM PLAYER MANAGER");
            RoomPlayerManager rPM = Instantiate(roomPlayerManager);
            NetworkServer.Spawn(rPM.gameObject);
        }
        //if (sceneName == gameScene)
        //{
        //    //Debug.Log("INSTANTIATING GAME PLAYER MANAGER");
        //    GamePlayerManager gO = Instantiate(gamePlayerManager);
        //    NetworkServer.Spawn(gO.gameObject);
        //}
    }

    private int CheckCharacterAvailable()
    {
        //Debug.Log("RUNNING CHECK CHARACTER AVAILABLE");
        if (!isAirSelected) return 0;
        else if (!isWaterSelected) return 1;
        else if (!isEarthSelected) return 2;
        else return 3;
    }

    private void SetNMCharacterSelected(int i, bool val)
    {
        //Debug.Log("RUNNING SET NETWORK MANAGER CHARACTER SELECTED");
        if (i == 0) isAirSelected = val;
        if (i == 1) isWaterSelected = val;
        if (i == 2) isEarthSelected = val;
        if (i == 3) isFireSelected = val;
    }

    private void SetSelectCharacterManager(int i, bool val)
    {
        //Debug.Log("SET SELECT CHARACTER MANAGER CHARACTER SELECTED");
        if (i == 0) SelectCharacterManager.instance.ServerSetAir(val);
        if (i == 1) SelectCharacterManager.instance.ServerSetWater(val);
        if (i == 2) SelectCharacterManager.instance.ServerSetEarth(val);
        if (i == 3) SelectCharacterManager.instance.ServerSetFire(val);
    }

    private bool CheckPlayerNotExists(NetworkConnection conn)
    {
        //Debug.Log("RUNNING CHECK PLAYER NOT EXISTS");
        foreach (var player in roomPlayers)
        {
            if (conn == player.connectionToClient) return false;
        }
        return true;
    }

    public void AddRoomPlayer(NetworkConnection conn, int i)
    {
        //Debug.Log("RUNNING ADD ROOM PLAYER");
        if (SceneManager.GetActiveScene().path == multiplayerLobby)
        {
            //Debug.Log(conn);
            RoomPlayer plPrefab = roomPlayerPrefab[i];
            //Debug.Lob(i);
            RoomPlayer player = Instantiate(plPrefab, startPositions[i].position, startPositions[i].rotation);
            //Debug.Log(player);

            player.ServerSetCharIndex(i);
            player.ServerSetCharName();
            player.ServerSetConnID(conn.connectionId);

            NetworkServer.Destroy(conn.identity.gameObject);
            NetworkServer.ReplacePlayerForConnection(conn, player.gameObject, true);

            SetSelectCharacterManager(i, true);
            SetNMCharacterSelected(i, true);
            playerCount++;
            //Debug.Log("NUMBER OF PLAYERS: " + numPlayers + " " + playerCount);
        }
    }

    private void RemoveRoomPlayer(NetworkConnection conn)
    {
        //Debug.Log("RUNNING REMOVE ROOM PLAYER");
        RoomPlayer player = conn.identity.gameObject.GetComponent<RoomPlayer>();
        int i = player.playerCharIndex;
        SetNMCharacterSelected(i, false);
        playerCount--;
        if (SceneManager.GetActiveScene().path == multiplayerLobby) SetSelectCharacterManager(i, false);
    }

    public void ChangeToGamePlayer(NetworkConnection conn)
    {
        //Debug.Log("RUNNING CHANGE TO GAME PLAYER");
        //Debug.Log(conn);
        if (SceneManager.GetActiveScene().path == gameScene && isGameStarted)
        {
            if (CheckPlayerNotExists(conn))
            {
                //Debug.Log("PLAYER DOESN'T EXISTS. ADD NEW GAME PLAYER");
                int i = CheckCharacterAvailable();
                //Debug.Log(i);
                AddGamePlayer(conn, i);

                SetNMCharacterSelected(i, true);
                playerCount++;
            }
            else
            {
                //Debug.Log("PLAYER ALREADY EXISTS. CHANGE GAME PLAYER");
                int i = conn.identity.GetComponent<RoomPlayer>().playerCharIndex;
                //Debug.Log(i);
                AddGamePlayer(conn, i);
            }
        }
    }

    private void AddGamePlayer(NetworkConnection conn, int i)
    {
        //Debug.Log("RUNNING ADD GAME PLAYER");
        GamePlayer glPrefab = gamePlayerPrefab[i];
        //Debug.Log(glPrefab);
        GamePlayer gl = Instantiate(glPrefab);
        //Debug.Log(gl);

        gl.ServerSetCharIndex(i);
        gl.ServerSetCharName();
        gl.ServerSetConnID(conn.connectionId);

        NetworkServer.Destroy(conn.identity.gameObject);
        NetworkServer.ReplacePlayerForConnection(conn, gl.gameObject, true);
    }

    private void RemoveGamePlayer(NetworkConnection conn)
    {
        //Debug.Log("RUNNING REMOVE GAME PLAYER");
        GamePlayer player = conn.identity.gameObject.GetComponent<GamePlayer>();
        int i = player.playerCharIndex;
        SetNMCharacterSelected(i, false);
        playerCount--;

    }

    public void StartGame()
    {
        //Debug.Log("RUNNING START GAME");
        if (SceneManager.GetActiveScene().path == multiplayerLobby)
        {
            isGameStarted = true;
            ServerChangeScene(gameScene);
        }
    }

    public void CheckIsReady()
    {
        //Debug.Log("RUNNING CHECK IS PLAYER READY");
        //Debug.Log("NUMBER OF PLAYER: " + numPlayers + " " + playerCount);
        if (numPlayers == playerCount)
        {
            foreach (var player in roomPlayers)
            {
                if (!player.isPlayerReady) return;

                RoomPlayerManager.instance.CanStartGameAnim();
            }
        }
    }

    public bool CheckIsLobbyActiveScene()
    {
        if (SceneManager.GetActiveScene().path == multiplayerLobby) return true;
        else return false;
    }

    public void ChangeMyChar(RoomPlayer player)
    {
        //Debug.Log("RUNNING CHANGE MY CHARACTER FOR: " + player);
        var conn = player.connectionToClient;
        int i = player.playerCharIndex;

        GameObject gO = Instantiate(playerPrefab);

        NetworkServer.Destroy(conn.identity.gameObject);
        NetworkServer.ReplacePlayerForConnection(conn, gO, true);

        SetSelectCharacterManager(i, false);
        SetNMCharacterSelected(i, false);
        playerCount--;
    }
}
