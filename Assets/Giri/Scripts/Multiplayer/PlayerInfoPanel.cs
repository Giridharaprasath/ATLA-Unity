using UnityEngine;
using TMPro;

public class PlayerInfoPanel : MonoBehaviour
{
    public string playerInfoName;
    public int playerInfoConnID;
    public string playerInfoCharName;

    [SerializeField]
    private TMP_Text playerInfoNameText;
    [SerializeField]
    private TMP_Text playerInfoCharText;
    [SerializeField]
    private GameObject kickPlayerButton;

    public GameObject roomLobbyPlayerObject;
    public RoomLobbyPlayer roomLobbyPlayerScript;

    private void Start()
    {
        roomLobbyPlayerObject = GameObject.Find("LocalRoomLobbyPlayer");
        roomLobbyPlayerScript = roomLobbyPlayerObject.GetComponent<RoomLobbyPlayer>();
        if (this.playerInfoConnID == roomLobbyPlayerScript.playerConnID)
        {
            if (roomLobbyPlayerScript.myPlayerInfoPanel == null) roomLobbyPlayerScript.myPlayerInfoPanel = this;
            kickPlayerButton.SetActive(false);
        }
    }

    public void SetPlayerNameText()
    {
        playerInfoNameText.text = playerInfoName;
    }

    public void SetCharNameText()
    {
        playerInfoCharText.text = playerInfoCharName;
    }

    public void SetKickPlayerButton(RoomLobbyPlayer player)
    {
        if (!player.isLeader) kickPlayerButton.SetActive(true);
    }

    public void KickPlayer()
    {
        roomLobbyPlayerScript.PlayerLeave(playerInfoConnID);
    }
}
