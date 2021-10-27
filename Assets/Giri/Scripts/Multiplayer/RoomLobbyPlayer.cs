using UnityEngine;
using Mirror;
using Steamworks;

public class RoomLobbyPlayer : NetworkBehaviour
{
    [Header("Room Lobby Player Info")]
    [SyncVar]
    public bool isLeader = false;
    [SyncVar]
    public int playerConnID;
    [SyncVar(hook = nameof(HandlePlayerNameUpdate))]
    public string playerName;
    [SyncVar(hook = nameof(HandleIsCharSelected))]
    public bool isCharSelected = false;
    [SyncVar(hook = nameof(HandleCharName))]
    public string charName = string.Empty;

    public PlayerInfoPanel myPlayerInfoPanel;
    public bool isRoomLobby = false;
    public bool isPaused = false;

    [HideInInspector]
    public bool toPause = false;

    private ATLANetworkManager room;
    private ATLANetworkManager Room
    {
        get
        {
            if (room != null) return room;
            return room = ATLANetworkManager.singleton as ATLANetworkManager;
        }
    }

    public override void OnStartClient()
    {
        Room.roomLobbyPlayers.Add(this);
        RoomLobbyManager.instance.UpdateLobbyUI();
        SelectCharacterManager.instance.CheckChar();
        SelectCharacterManager.instance.UpdateChar();
    }

    public override void OnStopClient()
    {
        Debug.Log(playerName + " IS LEAVING THE GAME");
        Room.roomLobbyPlayers.Remove(this);
        RoomLobbyManager.instance.UpdateLobbyUI();
        SelectCharacterManager.instance.CheckChar();
        SelectCharacterManager.instance.UpdateChar();
    }

    public override void OnStartAuthority()
    {
        //CmdSetPlayerName(SteamFriends.GetPersonaName().ToString());
        gameObject.name = "LocalRoomLobbyPlayer";
        RoomLobbyManager.instance.FindMyPlayer();
        RoomLobbyManager.instance.UpdateLobbyName();
        SelectCharacterManager.instance.FindMyLobbyPlayer();

        InputManager.Controls.UI.RoomLobby.performed += ctx => isRoomLobby = true;
        InputManager.Controls.UI.RoomLobby.canceled += ctx => isRoomLobby = false;

        InputManager.Controls.UI.Paused.performed += ctx => toPause = true;
        InputManager.Controls.UI.Paused.canceled += ctx => toPause = false;

        SelectCharacterManager.instance.SelectCharUI(true);
    }

    private void Update()
    {
        if (isLocalPlayer)
        {
            if (isRoomLobby) RoomLobbyManager.instance.SetRoomLobbyUI(true);
            else RoomLobbyManager.instance.SetRoomLobbyUI(false);

            if (toPause)
            {
                if (isPaused == false) RoomLobbyManager.instance.PauseGame();
                else if (isPaused == true) RoomLobbyManager.instance.ResumeGame();
            }
        }
    }

    [Command]
    public void CmdSetChar(int index) => SelectCharacterManager.instance.ServerSetChar(this, index);

    [Command]
    private void CmdSetPlayerName(string name)
    {
        Debug.Log("CMDSETPLAYERNAME: PLAYER NAME: " + name);
        this.HandlePlayerNameUpdate(this.playerName, name);
    }

    public void HandlePlayerNameUpdate(string oldValue, string newValue)
    {
        if (isServer) this.playerName = newValue;
        if (isClient) RoomLobbyManager.instance.UpdateLobbyUI();
    }

    [Command]
    public void CmdSetIsCharSelected(bool val)
    {
        isCharSelected = val;
        this.HandleIsCharSelected(!this.isCharSelected, this.isCharSelected);
    }

    public void HandleIsCharSelected(bool oldValue, bool newValue)
    {
        if (isClient) SelectCharacterManager.instance.UpdateChar();
    }

    [Command]
    public void CmdSetCharName(string name)
    {
        charName = name;
        this.HandleCharName("Selecting", this.charName);
    }

    public void HandleCharName(string oldValue, string newValue)
    {
        if (isClient) RoomLobbyManager.instance.UpdateLobbyUI();
    }

    public void QuitLobby()
    {
        if (hasAuthority)
        {
            if (isLeader) Room.StopHost();
            else Room.StopClient();       
        }
    }

    public void PlayerLeave(int connID)
    {
        Debug.Log("LOBBY OWNER WANTS TO KICK PLAYER: " + connID);
        RoomLobbyManager.instance.KickPlayer(connID);
    }

    public void CanStartGame()
    {
        if (hasAuthority) CmdCanStartGame();
    }

    [Command]
    private void CmdCanStartGame() => Room.StartGame();
}
