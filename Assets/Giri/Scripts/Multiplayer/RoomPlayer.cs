using UnityEngine;
using Mirror;

public class RoomPlayer : NetworkBehaviour
{
    [Header("Character Info")]
    [SyncVar]
    public bool isPlayerReady = false;
    [SyncVar]
    public string playerName;
    [SyncVar]
    public int playerConnID;
    [SyncVar]
    public bool isLeader = false;
    [SyncVar]
    public string playerCharName;
    [SyncVar(hook = nameof(HandleIsCharSelected))]
    public bool isChar1Selected = false;
    [SyncVar(hook = nameof(HandleIsCharSelected))]
    public bool isChar2Selected = false;
    [SyncVar(hook = nameof(HandleIsCharSelected))]
    public bool isChar3Selected = false;
    [SyncVar(hook = nameof(HandleIsCharSelected))]
    public bool isChar4Selected = false;

    [Header("Player Scripts")]
    [SerializeField]
    private GameObject thirdPersonCamera;
    [SerializeField]
    private AangPlayerMovement playerMove;
    [SerializeField]
    private FireBending fireBending;
    [SerializeField]
    private EarthBending earthBending;
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private CapsuleCollider capCol;

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
        Room.roomPlayers.Add(this);
        SelectCharacterManager.instance.UpdateChar();
    }

    public override void OnStopClient()
    {
        Room.roomPlayers.Remove(this);
        SelectCharacterManager.instance.UpdateChar();
    }

    public override void OnStartAuthority()
    {
        gameObject.name = "LocalRoomPlayer";
        SelectCharacterManager.instance.FindRoomPlayer();
        CmdSetCharType(); 
        thirdPersonCamera.SetActive(true);
        playerMove.enabled = true;
        fireBending.enabled = true;
        earthBending.enabled = true;
        controller.enabled = true;
        capCol.enabled = false;
    }

    [Command]
    public void CmdSetCharType()
    {
        Debug.Log("CMDSETCHARTYPE");
        if (playerCharName == "Air")
        {
            isChar1Selected = true;
            this.HandleIsCharSelected(!this.isChar1Selected, this.isChar1Selected);
        }
        if (playerCharName == "Water")
        {
            isChar2Selected = true;
            this.HandleIsCharSelected(!this.isChar2Selected, this.isChar2Selected);
        }
        if (playerCharName == "Earth")
        {
            isChar3Selected = true;
            this.HandleIsCharSelected(!this.isChar3Selected, this.isChar3Selected);
        }
        if (playerCharName == "Fire")
        {
            isChar4Selected = true;
            this.HandleIsCharSelected(!this.isChar4Selected, this.isChar4Selected);
        }
    }

    public void HandleIsCharSelected(bool oldValue, bool newValue)
    {
        if (isClient) SelectCharacterManager.instance.UpdateChar();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "StartGame") CmdSetIsPlayerReady(true);
    }

    private void OnTriggerExit(Collider other)
    {
        CmdSetIsPlayerReady(false);
    }

    [Command]
    private void CmdSetIsPlayerReady(bool val) => isPlayerReady = val;
}