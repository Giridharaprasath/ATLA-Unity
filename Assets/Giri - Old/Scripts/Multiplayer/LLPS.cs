using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;
using Mirror;
using Steamworks;

// Local Lobby Player Script
public class LLPS : NetworkBehaviour
{
    [Header("Lobby Player Info")]
    [SyncVar(hook = nameof(HandlePlayerNameUpdate))]
    public string lLPSName;
    [SyncVar]
    public int lLPSConnID;
    [SyncVar]
    public int lLPSPlayerNumber;
    [SyncVar]
    public bool isLeader = false;
    [SyncVar(hook = nameof(HandleLobbyUIUpdate))]
    public bool isInLobbyUI = false;
    [SyncVar(hook = nameof(HandleSelectCharUIUpdate))]
    public bool isInSelectCharUI = false;
    [SyncVar]
    public bool isInPauseMenuUI = false;
    [SyncVar(hook = nameof(HandleIsCharSelected))]
    public bool isCharSelected = false;
    [SyncVar(hook = nameof(HandleNameOfCharSelected))]
    public string nameOfCharSelected;

    public PLIS myPLIS;

    public bool toPause = false;

    public bool _isLocalPlayer;

    private MNMS game;
    private MNMS Game
    {
        get
        {
            if (game != null) return game;
            return game = MNMS.singleton as MNMS;
        }
    }

    public override void OnStartClient()
    {
        Game.lLPSs.Add(this);
        MCLM.Instance.UpdateUI();
        MCLM.Instance.UpdateLobbyName();
        MCSM.Instance.CheckChar();
    }

    public override void OnStopClient()
    {
        Debug.Log(lLPSName + " IS LEAVING THE GAME");
        Game.lLPSs.Remove(this);
        Debug.Log("REMOVED PLAYER FROM GAME PLAYER LIST: " + lLPSName);
        MCLM.Instance.UpdateUI();
        MCSM.Instance.CheckChar();
    } 

    public override void OnStartAuthority()
    {
        // CmdSetPlayerName(SteamFriends.GetPersonaName().ToString());
        gameObject.name = "LocalLobbyPlayer";
        MCLM.Instance.FindLLP();
        MCLM.Instance.UpdateLobbyName();
        MCSM.Instance.FindLLP();
            
        IM.Controls.Player.LobbyList.performed += ctx => CmdSetLobbyListUI();
        IM.Controls.Player.LobbyList.canceled += ctx => CmdSetLobbyListUI();

        IM.Controls.Player.Pause.performed += ctx => toPause = true;
        IM.Controls.Player.Pause.canceled += ctx => toPause = false;

        CmdSetSelectCharUI(true);
    }

    private void Update()
    {
        _isLocalPlayer = isLocalPlayer;
        if (toPause == true)
        {
            if (isInPauseMenuUI == false)
            {
                MCLM.Instance.PauseGame();
            }
            else if (isInPauseMenuUI == true)
            {
                MCLM.Instance.ResumeGame();
            }
        }
    }

    public void QuitLobby()
    {
        if (hasAuthority)
        {
            if (isLeader) Game.StopHost();
            else Game.StopClient();
        }
    }

    private void OnDestroy()
    {
        if (hasAuthority)
        {
            SteamMatchmaking.LeaveLobby((CSteamID)MCLM.Instance.currLobbyID);
        }
    }

    [Command]
    public void CmdSetChar(int index)
    {
        MCSM.Instance.ServerSetChar(this, index);
    }

    [Command]
    private void CmdSetPlayerName(string name)
    {
        Debug.Log("CMDSETPLAYERNAME: SETTING PLAYER NAME: " + name);
        this.HandlePlayerNameUpdate(this.lLPSName, name);
    }

    public void HandlePlayerNameUpdate(string oldValue, string newValue)
    {
        if (isServer) this.lLPSName = newValue;
        if (isClient) MCLM.Instance.UpdateUI();
    }

    [Command]
    private void CmdSetLobbyListUI()
    {
        isInLobbyUI = !isInLobbyUI;
        this.HandleLobbyUIUpdate(!this.isInLobbyUI, this.isInLobbyUI);
    }

    public void HandleLobbyUIUpdate(bool oldValue, bool newValue)
    {
        if (hasAuthority) MCLM.Instance.LobbyListUI(newValue);
    }

    [Command]
    public void CmdSetSelectCharUI(bool val)
    {
        isInSelectCharUI = val;
        this.HandleSelectCharUIUpdate(!this.isInSelectCharUI, this.isInSelectCharUI);
    }

    public void HandleSelectCharUIUpdate(bool oldValue, bool newValue)
    {
        Debug.Log("SELECT CHAR OFFF");
        if (hasAuthority) MCSM.Instance.SelectCharUI(newValue);
    }

    [Command]
    public void CmdSetIsCharSelected(bool val)
    {
        isCharSelected = true;
        this.HandleIsCharSelected(!this.isCharSelected, this.isCharSelected);
    }

    public void HandleIsCharSelected(bool oldValue, bool newValue)
    {
        if (isClient) MCSM.Instance.UpdateChar();
    }

    [Command]
    public void CmdSetNameOfCharSelected(string name)
    {
        nameOfCharSelected = name;
        this.HandleNameOfCharSelected("Selecting", this.nameOfCharSelected);
    }

    public void HandleNameOfCharSelected(string oldValue, string newValue)
    {
        if (isClient) MCLM.Instance.UpdateUI();
    }
}
