using UnityEngine;
using TMPro;
using Steamworks;

public class ListOfLobbyInfo : MonoBehaviour
{
    public CSteamID lobbySteamID;
    public string lobbyName;
    public int numOfPlayers;
    public int maxPlayers;

    [SerializeField]
    private TMP_Text lobbyNameText;
    [SerializeField]
    private TMP_Text numOfPlayersText;

    public void SetListOfLobbyInfo()
    {
        lobbyNameText.text = lobbyName;
        numOfPlayersText.text = "NO: " + numOfPlayers.ToString() + "/" + maxPlayers.ToString();
    }

    public void JoinLobby()
    {
        SteamLobbyManager.instance.JoinLobby(lobbySteamID);
    }
}
