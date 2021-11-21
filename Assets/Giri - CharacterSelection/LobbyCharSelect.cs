using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class LobbyCharSelect : NetworkBehaviour
{
    public static LobbyCharSelect Instance;

    [Header("CHARACTER SELECT")]
    [SerializeField]
    private GameObject charSelectCamera;
    [SerializeField]
    private GameObject charSelectUI;
    [SerializeField]
    private int charIndex;
    [SerializeField]
    private Button charSelectButton;
    [SerializeField]
    private Transform[] charSelectCameraPos;
    [SerializeField]
    private bool isInCharSelect;

    [Header("CHARACTERS")]
    [SyncVar]
    public bool isAirSelected;
    [SyncVar]
    public bool isWaterSelected;
    [SyncVar]
    public bool isEarthSelected;
    [SyncVar]
    public bool isFireSelected;

    private CSNM lobby;
    private CSNM Lobby
    {
        get
        {
            if (lobby != null) return lobby;
            return lobby = CSNM.singleton as CSNM;
        }
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    public override void OnStartClient()
    {
        Debug.Log("LOBBY CHAR SELECT CLIENT STARTED");
        SetState(true);
        SetCharIndex(0);
    }

    public override void OnStopClient()
    {
        Debug.Log("LOBBY CHAR SELECT CLIENT STOPPED");
        SetState(false);
    }

    public void LoadChar()
    {
        CmdLoadChar(charIndex);
    }

    [Command(requiresAuthority = false)]
    private void CmdLoadChar(int i, NetworkConnectionToClient sender = null)
    {
        Lobby.AddLobbyPlayer(sender, i);
    }

    [Server]
    public void ServerSetAir(bool val) => isAirSelected = val;

    [Server]
    public void ServerSetWater(bool val) => isWaterSelected = val;

    [Server]
    public void ServerSetEarth(bool val) => isEarthSelected = val;

    [Server]
    public void ServerSetFire(bool val) => isFireSelected = val;

    public void SetState(bool val)
    {
        charSelectCamera.SetActive(val);
        charSelectUI.SetActive(val);
        isInCharSelect = val;
    }

    public void SetCharIndex(int i)
    {
        charIndex = i;
        charSelectCamera.transform.position = charSelectCameraPos[i].position;
        charSelectCamera.transform.rotation = charSelectCameraPos[i].rotation;
    }

    private void CheckChar(int i)
    {
        if ((i == 0 && isAirSelected) ||
            (i == 1 && isWaterSelected) ||
            (i == 2 && isEarthSelected) ||
            (i == 3 && isFireSelected))
            charSelectButton.interactable = false;

        else charSelectButton.interactable = true;
    }

    private void Update()
    {
        if (isInCharSelect) CheckChar(charIndex);   
    }
}
