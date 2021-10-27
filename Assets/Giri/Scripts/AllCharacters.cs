using UnityEngine;

[CreateAssetMenu(menuName = "Character Selection/My Character")]
public class AllCharacters : ScriptableObject
{
    [SerializeField]
    private string charName;
    [SerializeField]
    private GameObject charPreview;
    [SerializeField]
    private RoomPlayer charPrefab;

    public string CharName => charName;
    public GameObject CharPreview => charPreview;
    public RoomPlayer CharPrefab => charPrefab;
}
