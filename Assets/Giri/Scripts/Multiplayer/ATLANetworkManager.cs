using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;

public class ATLANetworkManager : NetworkManager
{
    [Header("Scenes")]
    [Scene] [SerializeField]
    private string mainMenu = string.Empty;
    [Scene] [SerializeField]
    private string multiplayerLobby = string.Empty;

    [Header("Lobby")]
    [SerializeField]
    private RoomLobbyPlayer roomLobbyPlayerPrefab = null;

    public List<RoomLobbyPlayer> roomLobbyPlayers { get; } = new List<RoomLobbyPlayer>();

    public List<RoomPlayer> roomPlayers { get; } = new List<RoomPlayer>();

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        if (SceneManager.GetActiveScene().path == mainMenu || SceneManager.GetActiveScene().path == multiplayerLobby)
        {
            bool isLeader = roomLobbyPlayers.Count == 0;
            RoomLobbyPlayer roomLobbyPlayerInstance = Instantiate(roomLobbyPlayerPrefab);

            roomLobbyPlayerInstance.isLeader = isLeader;
            roomLobbyPlayerInstance.playerConnID = conn.connectionId;
            NetworkServer.AddPlayerForConnection(conn, roomLobbyPlayerInstance.gameObject);
            Debug.Log("PLAYER ADDED. PLAYER NAME: ");
        }
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        if (conn.identity != null)
        {
            RoomLobbyPlayer player = conn.identity.GetComponent<RoomLobbyPlayer>();
            roomLobbyPlayers.Remove(player);

            RoomPlayer pl = conn.identity.GetComponent<RoomPlayer>();
            roomPlayers.Remove(pl);
        }
        base.OnServerDisconnect(conn);
    }

    public override void OnStopServer()
    {
        roomLobbyPlayers.Clear();
        roomPlayers.Clear();
    }

    public void StartGame()
    {
        if (CanStartGame() && SceneManager.GetActiveScene().path == multiplayerLobby)
        {

        }
    }

    private bool CanStartGame()
    {
        if (roomPlayers.Count == roomLobbyPlayers.Count)
        {
            foreach (RoomPlayer player in roomPlayers)
            {
                if (!player.isPlayerReady) return false;
            }
            foreach (RoomLobbyPlayer player in roomLobbyPlayers)
            {
                if (!player.isCharSelected) return false;
            }
        }
        return true;
    }
}
