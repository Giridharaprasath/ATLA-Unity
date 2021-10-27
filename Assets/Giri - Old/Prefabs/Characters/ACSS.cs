using UnityEngine;

// All Character Selection Script
[CreateAssetMenu(fileName = "New Character", menuName = "Character Selection/Character")]
public class ACSS : ScriptableObject 
{
    [SerializeField]
    private string charName;
    [SerializeField]
    private GameObject charPreview;
    [SerializeField]
    private PCSS charPrefab;

    public string CharName => charName;
    public GameObject CharPreview => charPreview;
    public PCSS CharPrefab => charPrefab;
}