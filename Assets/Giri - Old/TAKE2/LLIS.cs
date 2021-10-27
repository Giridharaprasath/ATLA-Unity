using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Steamworks;

// Lobby List Item Script
public class LLIS : MonoBehaviour
{
    public CSteamID lobbySteamID;
    public string lobbyName;
    public int numOfPlayers;
    public int maxPlayers;

    [SerializeField]
    private TMP_Text lobbyNameText;
    [SerializeField]    
    private TMP_Text numOfPlayersText;

    public void SetLobbyItem()
    {
        lobbyNameText.text = lobbyName;
        numOfPlayersText.text = "NO: " + numOfPlayers.ToString() + "/" + maxPlayers.ToString();
    }

    public void JoinLobby()
    {
        Debug.Log("JOINLOBBY: JOINING LOBBY WITH STEAM ID: " + lobbySteamID.ToString());
        SLS.Instance.JoinLobby(lobbySteamID);
    }
}
