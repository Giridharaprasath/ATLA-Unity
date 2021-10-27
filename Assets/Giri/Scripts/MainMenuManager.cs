using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    [SerializeField]
    private GameObject startScreenPage;
    [SerializeField]
    private GameObject mainMenuPage;
    [SerializeField]
    private GameObject singlePlayerPage;
    [SerializeField]
    private GameObject multiPlayerPage;
    [SerializeField]
    private GameObject optionPage;
    [SerializeField]
    private GameObject lobbyListPanel;
    [SerializeField]
    private TMP_Text infoText;

    private void Start()
    {
        //startScreenPage.SetActive(true);
        //mainMenuPage.SetActive(false);
        //singlePlayerPage.SetActive(false);
        //multiPlayerPage.SetActive(false);
        //optionPage.SetActive(false);
        //lobbyListPanel.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#endif
    }

    public void SinglePlayerMode()
    {
        mainMenuPage.SetActive(false);
        singlePlayerPage.SetActive(true);
    }

    public void MultiPlayerMode()
    {
        if (SteamLobbyManager.instance.isOnline)
        {
            mainMenuPage.SetActive(false);
            multiPlayerPage.SetActive(true);
            infoText.text = "";
        }
        else
            infoText.text = "Not Connected To Steam";
    }

    public void OptionsPage()
    {
        mainMenuPage.SetActive(false);
        optionPage.SetActive(true);
    }

    public void BackToMainMenu()
    {
        mainMenuPage.SetActive(true);
        singlePlayerPage.SetActive(false);
        multiPlayerPage.SetActive(false);
        optionPage.SetActive(false);
    }

    public void HostLobby()
    {
        Debug.Log("HOSTING LOBBY");
        SteamLobbyManager.instance.HostLobby();
    }

    public void JoinLobby()
    {
        Debug.Log("GETTING LIST OF LOBBIES");
        lobbyListPanel.SetActive(true);
        SteamLobbyManager.instance.GetListOfLobbies();
    }

    public void CloseLobbyList()
    {
        Debug.Log("CLOSING LIST OF LOBBIES PANEL");
        lobbyListPanel.SetActive(false);
        SteamLobbyManager.instance.ClearLobbyList();
    }

    private void Update()
    {
        SteamLobbyManager.instance.lobbyListParent = GameObject.Find("LobbyContentParent");
    }
}
