using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

public class SelectCharacterManager : NetworkBehaviour
{
    public static SelectCharacterManager instance;

    [Header("Select Character UI")]
    [SerializeField]
    private GameObject selectCharCamera;
    [SerializeField]
    private GameObject selectCharUI;
    [SerializeField]
    private int charIndex = 0;
    [SerializeField]
    private Button selectCharButton;
    [SerializeField]
    private Transform charPreviewParent;
    [SerializeField]
    private bool isInSelectCharUI = true;

    [SerializeField]
    private AllCharacters[] allCharacters;
    private List<GameObject> charInstance = new List<GameObject>();
    [SerializeField]
    private Transform startPosition;

    [SyncVar]
    public bool isChar1Selected;
    [SyncVar]
    public bool isChar2Selected;
    [SyncVar]
    public bool isChar3Selected;
    [SyncVar]
    public bool isChar4Selected;

    [Header("Character Spawned")]
    public RoomPlayer char1Player;
    public RoomPlayer char2Player;
    public RoomPlayer char3Player;
    public RoomPlayer char4Player;

    [Header("My Players")]
    public GameObject roomLobbyPlayer;
    public RoomLobbyPlayer roomLobbyPlayerScript;

    public GameObject roomPlayer;
    public RoomPlayer roomPlayerScript;

    private ATLANetworkManager room;
    private ATLANetworkManager Room
    {
        get
        {
            if (room != null) return room;
            return room = ATLANetworkManager.singleton as ATLANetworkManager;
        }
    }

    private void Awake()
    {
        if (instance == null) instance = this;
        charIndex = 0;
    }

    public override void OnStartClient()
    {
        if (charPreviewParent.childCount == 0)
        {
            foreach (var chars in allCharacters)
            {
                GameObject charIn = Instantiate(chars.CharPreview, charPreviewParent);
                charIn.SetActive(false);
                charInstance.Add(charIn);
            }
        }
        charInstance[charIndex].SetActive(true);
        CheckChar();
    }

    public override void OnStopClient()
    {
        CheckChar();
        SelectCharUI(false);
    }

    private void Update()
    {
        if (isInSelectCharUI) CheckChar();   
    }

    public void CheckChar()
    {
        Debug.Log("CHECKING CHARACTERS AVAILABLE");
        if ((isChar1Selected && charIndex == 0) || (isChar2Selected && charIndex == 1)
            || (isChar3Selected && charIndex == 2) || (isChar4Selected && charIndex == 3))
        {
            Debug.Log("CHARACTER ALREADY TAKEN");
            selectCharButton.interactable = false;
        }
        else selectCharButton.interactable = true;
    }

    public void FindMyLobbyPlayer()
    {
        roomLobbyPlayer = GameObject.Find("LocalRoomLobbyPlayer");
        roomLobbyPlayerScript = roomLobbyPlayer.GetComponent<RoomLobbyPlayer>();
    }

    public void FindRoomPlayer()
    {
        roomPlayer = GameObject.Find("LocalRoomPlayer");
        roomPlayerScript = roomPlayer.GetComponent<RoomPlayer>();
    }

    public void SelectCharUI(bool con)
    {
        selectCharUI.SetActive(con);
        selectCharCamera.SetActive(con);
        isInSelectCharUI = con;
    }

    public void SetCharIndex(int i)
    {
        charInstance[charIndex].SetActive(false);
        charIndex = i;
        charInstance[charIndex].SetActive(true);
    }

    public void UpdateChar()
    {
        Debug.Log("UPDATE CHAR");
        foreach (RoomPlayer player in Room.roomPlayers)
        {
            if (player.isChar1Selected == true)
            {
                this.char1Player = player;
                CmdSetIsChar1Selected(true);
            }
            else if (player.isChar2Selected == true)
            {
                this.char2Player = player;
                CmdSetIsChar2Selected(true);
            }
            else if (player.isChar3Selected == true)
            {
                this.char3Player = player;
                CmdSetIsChar3Selected(true);
            }
            else if (player.isChar4Selected == true)
            {
                this.char4Player = player;
                CmdSetIsChar4Selected(true);
            }
        }
        if (this.char1Player == null) CmdSetIsChar1Selected(false);
        if (this.char2Player == null) CmdSetIsChar2Selected(false);
        if (this.char3Player == null) CmdSetIsChar3Selected(false);
        if (this.char4Player == null) CmdSetIsChar4Selected(false);
    }

    [Command(requiresAuthority = false)]
    public void CmdSetIsChar1Selected(bool val) => isChar1Selected = val;

    [Command(requiresAuthority = false)]
    public void CmdSetIsChar2Selected(bool val) => isChar2Selected = val;

    [Command(requiresAuthority = false)]
    public void CmdSetIsChar3Selected(bool val) => isChar3Selected = val;

    [Command(requiresAuthority = false)]
    public void CmdSetIsChar4Selected(bool val) => isChar4Selected = val;

    public void SetChar()
    {
        roomLobbyPlayerScript.CmdSetChar(charIndex);
        roomLobbyPlayerScript.CmdSetIsCharSelected(true);
        roomLobbyPlayerScript.CmdSetCharName(allCharacters[charIndex].CharName);
        SelectCharUI(false);
    }

    public void ServerSetChar(RoomLobbyPlayer player, int i)
    {
        var conn = player.connectionToClient;
        Debug.Log(conn);
        RoomPlayer roomPlayer = Instantiate(allCharacters[i].CharPrefab, startPosition.position, startPosition.rotation);

        roomPlayer.playerConnID = player.playerConnID;
        roomPlayer.playerName = player.playerName;
        roomPlayer.isLeader = player.isLeader;
        roomPlayer.playerCharName = allCharacters[i].CharName;

        NetworkServer.ReplacePlayerForConnection(conn, roomPlayer.gameObject, true);
    }
}
