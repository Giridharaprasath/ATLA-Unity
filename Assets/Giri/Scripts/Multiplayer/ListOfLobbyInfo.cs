using UnityEngine;
using TMPro;
using Steamworks;

public class ListOfLobbyInfo : MonoBehaviour
{
    public CSteamID lobbySteamID;
    public string lobbyName;
    public int numOfPlayers;
    public int maxPlayers;
    //public bool isGameStarted;

    [SerializeField]
    private TMP_Text lobbyNameText;
    [SerializeField]
    private TMP_Text numOfPlayersText;
    //[SerializeField]
    //private TMP_Text isGameStartedText;

    private bool isJoinLobby;

    public void SetListOfLobbyInfo()
    {
        lobbyNameText.text = lobbyName;
        numOfPlayersText.text = "NO: " + numOfPlayers.ToString() + "/" + maxPlayers.ToString();
        //if (isGameStarted) isGameStartedText.text = "Game Started";
    }

    public void JoinLobby()
    {
        //SteamLobbyManager.instance.JoinLobby(lobbySteamID);
        isJoinLobby = true;
        CameraManager.instance.StartAnim();
    }

    private void Update()
    {
        if (isJoinLobby && CameraManager.instance.canStartGame) SteamLobbyManager.instance.JoinLobby(lobbySteamID);
    }
}