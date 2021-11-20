using UnityEngine;
using Mirror;
using Steamworks;

public class GamePlayer : NetworkBehaviour
{
    [Header("GAME PLAYER INFO")]
    [SyncVar]
    private bool isLeader = false;
    [SyncVar]
    public string playerName;
    [SyncVar]
    public int playerConnID;
    [SyncVar]
    public string playerCharName;
    [SyncVar]
    public int playerCharIndex;
    [SerializeField]
    private string thisCharName;

    [Header("Player Scripts")]
    [SerializeField]
    private GameObject thirdPersonCamera;
    [SerializeField]
    private AangPlayerMovement playerMove;
    [SerializeField]
    private CharacterController controller;
    [SerializeField]
    private CapsuleCollider capCol;
    [SerializeField]
    private EarthBending earthBending;
    [SerializeField]
    private FireBending fireBending;

    private ATLANetworkManager game;
    private ATLANetworkManager Game
    {
        get
        {
            if (game != null) return game;
            return game = ATLANetworkManager.singleton as ATLANetworkManager;
        }
    }

    public override void OnStartClient()
    {
        //Debug.Log("GAME PLAYER CLIENT STARTED");
        Game.gamePlayers.Add(this);
    }

    public override void OnStopClient()
    {
        Debug.Log(playerName + " IS LEAVING");
        Game.gamePlayers.Remove(this);
    }

    public override void OnStartAuthority()
    {
        SetIsLeader();
        CmdSetPlayerName(SteamFriends.GetPersonaName().ToString());
        gameObject.name = "LocalGamePlayer";
        thirdPersonCamera.SetActive(true);
        playerMove.enabled = true;
        controller.enabled = true;
        capCol.enabled = false;
        earthBending.enabled = true;
        fireBending.enabled = true;
    }

    [Server]
    public void ServerSetCharIndex(int i) => playerCharIndex = i;

    [Server]
    public void ServerSetCharName() => playerCharName = thisCharName;

    [Server]
    public void ServerSetConnID(int id) => playerConnID = id;

    private void SetIsLeader()
    {
        if (isServer && isLocalPlayer) CmdSetIsLeader();
    }

    [Command]
    private void CmdSetIsLeader() => isLeader = true;

    [Command]
    private void CmdSetPlayerName(string name) => playerName = name;

    private void Update()
    {
        if (isLocalPlayer)
        {
            if (isLeader)
                Debug.Log("SHUT THE FUCK UP");
        }
    }
}
