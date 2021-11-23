using System;
using UnityEngine;

public class AirBending : MonoBehaviour
{
    public AangPlayerMovement playerScript;
    public float jumpHeight;
    public bool canHighJump;

    private void Awake()
    {
        playerScript = GetComponent<AangPlayerMovement>();
        InputManager.DisableHighJumping();

        InputManager.Controls.AirBending.HighJump.performed += ctx => PerformedHighJump();

        InputManager.Controls.AirBending.HighJump.canceled += ctx => CanceledHighJump();
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.tag == "HighJump")
        {
            Debug.Log("ENTERED HIGH JUMP AREA");
            canHighJump = true;
            InputManager.EnableHighJumping();
            playerScript.jumpHeight = jumpHeight;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        canHighJump = false;
        InputManager.DisableHighJumping();
        playerScript.jumpHeight = 1.2f;
    }

    private void PerformedHighJump()
    {
        Debug.Log("PERFOERMED HIGH JUMP");
        playerScript.jump = true;
        InputManager.DisableHighJumping();
    }

    private void CanceledHighJump()
    {
        Debug.Log("CANCELED HIGH JUMP");
        playerScript.jump = false;
    }
}