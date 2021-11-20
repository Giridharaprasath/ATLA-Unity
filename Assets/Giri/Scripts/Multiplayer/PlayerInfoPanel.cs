using UnityEngine;
using TMPro;
using UnityEngine.UI;

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

    public void SetPlayerNameText()
    {
        playerInfoNameText.text = playerInfoName;
    }

    public void SetCharNameText()
    {
        playerInfoCharText.text = playerInfoCharName;
    }
}
