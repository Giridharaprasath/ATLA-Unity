using UnityEngine;

public class AirBending : MonoBehaviour
{
    public AangPlayerMovement playerScript;

    private void Awake()
    {
        playerScript = GetComponent<AangPlayerMovement>();
        SetPlayerJump();
    }

    private void SetPlayerJump()
    {
        playerScript.jumpHeight = 2f;
    }
}