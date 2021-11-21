using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class MainMenuManager : MonoBehaviour
{
    public static MainMenuManager instance;

    [Header("PAGE PANELS")]
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

    [Header("OPTIONS PAGE")]
    [SerializeField]
    private TMP_Dropdown dropdownQual;
    [SerializeField]
    private TMP_Dropdown dropdownRes;
    [SerializeField]
    private Toggle isFullScreen;

    private void Awake()
    {
        if (instance == null) instance = this;
    }

    private void Start()
    {
        //startScreenPage.SetActive(true);
        mainMenuPage.SetActive(true);
        singlePlayerPage.SetActive(false);
        multiPlayerPage.SetActive(false);
        optionPage.SetActive(false);
        lobbyListPanel.SetActive(false);

        dropdownQual.value = QualitySettings.GetQualityLevel();
        dropdownRes.value = (Screen.width == 1920 ? 0 : (Screen.width == 1366 ? 1 : 2));
        isFullScreen.isOn = Screen.fullScreen;

        Cursor.visible = true;
        Cursor.lockState = CursorLockMode.Confined;
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
        CameraManager.instance.StartAnim();
        multiPlayerPage.SetActive(false);
        //SteamLobbyManager.instance.HostLobby();
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

    public void HostGame()
    {
        SteamLobbyManager.instance.HostLobby();
    }

    public void ChangeQuality(int val)
    {
        QualitySettings.SetQualityLevel(val);
    }

    public void ChangeResolution(int val)
    {
        if (val == 0) Screen.SetResolution(1920, 1080, isFullScreen);
        else if (val == 1) Screen.SetResolution(1366, 768, isFullScreen);
        else if (val == 2) Screen.SetResolution(1280, 720, isFullScreen);
    }

    public void ChangeFull(bool val)
    {
        Screen.fullScreen = val;
    }
}
