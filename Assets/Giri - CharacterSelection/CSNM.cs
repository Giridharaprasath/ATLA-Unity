using System;
using System.Collections.Generic;
using UnityEngine;
using Mirror;
using UnityEngine.SceneManagement;

public class CSNM : NetworkManager
{
    public bool isGameStarted;

    [Scene] public string charS;
    [Scene] public string newS;

    public LobbyCharSelect lobbyCharSelect;
    public LobbyManager lobbyManager;
    public GameSceneManager gameSceneManager;

    public LobbyPlayer[] lobbyPlayerPrefab;
    public GameScenePlayer[] gameScenePlayerPrefab;

    public int playerCount;

    public bool isAirSelected;
    public bool isWaterSelected;
    public bool isEarthSelected;
    public bool isFireSelected;

    public List<LobbyPlayer> lobbyPlayers = new List<LobbyPlayer>();
    public List<GameScenePlayer> gameScenePlayers = new List<GameScenePlayer>();

    public override void OnServerConnect(NetworkConnection conn)
    {
        if (numPlayers >= maxConnections)
        {
            Debug.Log("LOBBY FULL");
            conn.Disconnect();
            return;
        }
        //Debug.Log("NUMBER OF PLAYERS: " + numPlayers + " " + playerCount);
        base.OnServerConnect(conn);
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        if (conn.identity != null)
        {
            //Debug.Log(conn + " IS LEAVING");
            //Debug.Log("NUMBER OF PLAYERS: " + numPlayers + " " + playerCount);
            //RemoveChar(conn);
            if (conn.identity.gameObject.GetComponent<LobbyPlayer>() != null)
            {
                RemoveLobbyPlayer(conn);
            }
            else if (conn.identity.gameObject.GetComponent<GameScenePlayer>() != null)
            {
                RemoveGameScenePlayer(conn);
            }
        }
        base.OnServerDisconnect(conn);
    }

    public override void OnStopServer()
    {
        lobbyPlayers.Clear();
        gameScenePlayers.Clear();
        isAirSelected = false;
        isWaterSelected = false;
        isEarthSelected = false;
        isFireSelected = false;
    }

    private int CheckCharAvailable()
    {
        if (!isAirSelected) return 0;
        else if (!isWaterSelected) return 1;
        else if (!isEarthSelected) return 2;
        else return 3;
    }

    //private void RemoveChar(NetworkConnection conn)
    //{
    //    Debug.Log(conn.identity.gameObject.GetComponent<LobbyPlayer>());
    //    if (SceneManager.GetActiveScene().path == charS && lobbyPlayers.Count > 0)
    //    {
    //        LobbyPlayer player = conn.identity.gameObject.GetComponent<LobbyPlayer>();
    //        //Debug.Log(player);
    //        int i = player.charIndex;
    //        //Debug.Log("PLAYER LEAVING INDEX: " + i);
    //        ChangeLobbyCharSelect(i, false);
    //        SetCharNM(i, false);
    //        playerCount--;
    //    }
    //    if (SceneManager.GetActiveScene().path == newS && gameScenePlayers.Count > 0)
    //    {
    //        GameScenePlayer player = conn.identity.gameObject.GetComponent<GameScenePlayer>();

    //        int i = player.charIndex;

    //        SetCharNM(i, false);
    //        playerCount--;
    //    }
    //}

    public void RemoveLobbyPlayer(NetworkConnection conn)
    {
        Debug.Log("RUNNING REMOVE LOBBY PLAYER");
        LobbyPlayer player = conn.identity.gameObject.GetComponent<LobbyPlayer>();
        int i = player.charIndex;
        SetCharNM(i, false);
        playerCount--;
        if (SceneManager.GetActiveScene().path == charS) ChangeLobbyCharSelect(i, false);
    }

    private void RemoveGameScenePlayer(NetworkConnection conn)
    {
        Debug.Log("RUNNING REMOVE GAME SCENE PLAYER");
        GameScenePlayer player = conn.identity.GetComponent<GameScenePlayer>();
        int i = player.charIndex;
        SetCharNM(i, false);
        playerCount--;
    }

    public void AddLobbyPlayer(NetworkConnection conn, int i)
    {
        if (SceneManager.GetActiveScene().path == charS)
        {
            Debug.Log(conn);
            LobbyPlayer plPrefab = lobbyPlayerPrefab[i];
            //Debug.Log(i);

            LobbyPlayer pl = Instantiate(plPrefab, startPositions[i].position, startPositions[i].rotation);

            pl.ServerSetCharIndex(i);
            pl.ServerSetCharName();

            NetworkServer.Destroy(conn.identity.gameObject);
            NetworkServer.ReplacePlayerForConnection(conn, pl.gameObject, true);
            ChangeLobbyCharSelect(i, true);
            SetCharNM(i, true);
            playerCount++;
            //Debug.Log("CURRENT PLAYER COUNT: " + playerCount + " " + numPlayers);
        }
    }

    //public void AddGamePlayer()
    //{
    //    if (SceneManager.GetActiveScene().path == newS)
    //    {
    //        for (int i = lobbyPlayers.Count - 1; i >= 0; i--)
    //        {
    //            var conn = lobbyPlayers[i].connectionToClient;
    //            int index = lobbyPlayers[i].charIndex;
    //            GameScenePlayer gl = Instantiate(gameScenePlayerPrefab[index]);

    //            gl.ServerSetCharIndex(lobbyPlayers[i].charIndex);
    //            gl.ServerSetCharName();
    //            gl.ServerSetIsLeader(lobbyPlayers[i].isLeader);

    //            NetworkServer.Destroy(conn.identity.gameObject);
    //            NetworkServer.ReplacePlayerForConnection(conn, gl.gameObject);
    //        }
    //    }
    //}

    private void ChangeGamePlayer(NetworkConnection conn)
    {
        Debug.Log("RUNNING CHANGE GAME PLAYER");
        Debug.Log(conn);
        int i = conn.identity.gameObject.GetComponent<LobbyPlayer>().charIndex;
        Debug.Log(i);
        GameScenePlayer glPrefab = gameScenePlayerPrefab[i];
        Debug.Log(glPrefab);

        GameScenePlayer gl = Instantiate(glPrefab);

        gl.ServerSetCharIndex(i);
        gl.ServerSetCharName();

        NetworkServer.Destroy(conn.identity.gameObject);
        NetworkServer.ReplacePlayerForConnection(conn, gl.gameObject, true);

        //SetCharNM(i, true);

    }

    private void AddGamePlayer(NetworkConnection conn)
    {
        Debug.Log("RUNNING ADD GAME PLAYER");
        //Debug.Log(conn);
        int i = CheckCharAvailable();
        //Debug.Log(i);
        GameScenePlayer glPrefab = gameScenePlayerPrefab[i];
        //Debug.Log(glPrefab);

        GameScenePlayer gl = Instantiate(glPrefab);

        gl.ServerSetCharIndex(i);
        gl.ServerSetCharName();

        NetworkServer.Destroy(conn.identity.gameObject);
        NetworkServer.ReplacePlayerForConnection(conn, gl.gameObject, true);

        SetCharNM(i, true);
        playerCount++;
    }

    public void ChangeToGamePlayer(NetworkConnection conn)
    {
        if (SceneManager.GetActiveScene().path == newS && isGameStarted)
        {
            if (CheckPlayerNotExits(conn))
            {
                AddGamePlayer(conn);
                //GameScenePlayer gl = Instantiate(glPrefab);

                //gl.ServerSetCharIndex(i);
                //gl.ServerSetCharName();

                //NetworkServer.Destroy(conn.identity.gameObject);
                //NetworkServer.ReplacePlayerForConnection(conn, gl.gameObject, true);

                //SetCharNM(i, true);
                //playerCount++;
            }
            else
            {
                ChangeGamePlayer(conn);
            }
        }
    }

    private bool CheckPlayerNotExits(NetworkConnection conn)
    {
        foreach (var player in lobbyPlayers)
        {
            if (conn == player.connectionToClient) return false;
        }
        return true;
    }

    //public bool CheckChar(int i)
    //{
    //    if (isAirSelected && i == 0)
    //    {
    //        Debug.Log("AIR ALREADY SELECTED");
    //        return true;
    //    }
    //    if (isWaterSelected && i == 1) return true;

    //    return false;
    //}

    private void SetCharNM(int i, bool val)
    {
        if (i == 0) isAirSelected = val;
        if (i == 1) isWaterSelected = val;
        if (i == 2) isEarthSelected = val;
        if (i == 3) isFireSelected = val;
    }

    private void ChangeLobbyCharSelect(int i, bool val)
    {
        if (i == 0) LobbyCharSelect.Instance.ServerSetAir(val);
        if (i == 1) LobbyCharSelect.Instance.ServerSetWater(val);
        if (i == 2) LobbyCharSelect.Instance.ServerSetEarth(val);
        if (i == 3) LobbyCharSelect.Instance.ServerSetFire(val);
    }

    //public void NotifyReady()
    //{
    //    foreach (var player in lobbyPlayers) player.ReadyStart(IsReady());
    //}

    //private bool IsReady()
    //{
    //    foreach (var player in lobbyPlayers)
    //    {
    //        if (!player.isReady) return false;
    //    }
    //    return true;
    //}

    public void StartGame()
    {
        if (SceneManager.GetActiveScene().path == charS)
        {
            isGameStarted = true;
            ServerChangeScene(newS);
        }
    }

    public override void ServerChangeScene(string newSceneName)
    {
        //    if (SceneManager.GetActiveScene().path == charS && newSceneName == newS)
        //    {
        //        for (int i = lobbyPlayers.Count - 1; i >= 0; i--)
        //        {
        //            var conn = lobbyPlayers[i].connectionToClient;
        //            int index = lobbyPlayers[i].charIndex;
        //            GameScenePlayer gl = Instantiate(gameScenePlayerPrefab[index]);

        //            gl.ServerSetCharIndex(lobbyPlayers[i].charIndex);
        //            gl.ServerSetCharName(lobbyPlayers[i].charName);
        //            gl.ServerSetIsLeader(lobbyPlayers[i].isLeader);

        //            NetworkServer.Destroy(conn.identity.gameObject);
        //            NetworkServer.ReplacePlayerForConnection(conn, gl.gameObject);
        //        }
        //    }
        if (SceneManager.GetActiveScene().path == charS && newSceneName == newS)
        {
            Debug.Log("GOING TO NEW SCENE");
            foreach (LobbyPlayer player in lobbyPlayers)
            {
                DontDestroyOnLoad(player.gameObject);
                player.StopPlayer();
            }
        }
        base.ServerChangeScene(newSceneName);
    }

    public override void OnServerSceneChanged(string sceneName)
    {
        if (sceneName == charS)
        {
            LobbyCharSelect gO = Instantiate(lobbyCharSelect);
            NetworkServer.Spawn(gO.gameObject);

            LobbyManager lM = Instantiate(lobbyManager);
            NetworkServer.Spawn(lM.gameObject);
        }
        if (sceneName == newS)
        {
            GameSceneManager gO = Instantiate(gameSceneManager);
            NetworkServer.Spawn(gO.gameObject);
        }
    }

    public void CheckIsReady()
    {
        Debug.Log("CURRENT PLAYER COUNT: " + playerCount + " " + numPlayers);
        if (numPlayers == playerCount)
        {
            foreach (var player in lobbyPlayers)
            {
                if (!player.isReady) return;
                else LobbyManager.instance.CanStartGameAnim();
            }
        }
    }

    public void ChangeMyChar(LobbyPlayer player)
    {
        //Debug.Log("RUNNING CHANGE MY CHARACTER FOR PLAYER: " + player);
        var conn = player.connectionToClient;
        int i = player.charIndex;
        GameObject gO = Instantiate(playerPrefab);
        //Debug.Log(conn);
        NetworkServer.Destroy(conn.identity.gameObject);
        //Debug.Log(conn);
        NetworkServer.ReplacePlayerForConnection(conn, gO, true);
        //Debug.Log(conn);

        ChangeLobbyCharSelect(i, false);
        SetCharNM(i, false);
        playerCount--;
    }

    public bool CheckScene()
    {
        if (SceneManager.GetActiveScene().path == charS) return true;
        else return false;
    }
}