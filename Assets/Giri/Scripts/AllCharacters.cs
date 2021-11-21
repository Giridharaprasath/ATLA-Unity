using UnityEngine;

[CreateAssetMenu(menuName = "Character Selection/My Character")]
public class AllCharacters : ScriptableObject
{
    [SerializeField]
    private int index;
    [SerializeField]
    private string charName;
    [SerializeField]
    private GameObject charLobbyPreview;
    [SerializeField]
    private RoomPlayer charLobbyPrefab;

    public int Index => index;
    public string CharName => charName;
    public GameObject CharLobbyPreview => charLobbyPreview;
    public RoomPlayer CharLobbyPrefab => charLobbyPrefab;
}