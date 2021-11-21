using UnityEngine;
using Mirror;

public class DoorManager : NetworkBehaviour
{
    [SyncVar]
    public bool isOpen;

    [SerializeField]
    private Animator animator;

    public void ChangeDoorState()
    {
        //Debug.Log("WANT TO CHANGE DOOR STATE");
        if (!isOpen) CmdSetDoorState(true);
        else CmdSetDoorState(false);
    }

    [Command(requiresAuthority = false)]
    private void CmdSetDoorState(bool val)
    {
        isOpen = val;
        animator.SetBool("OpenDoor", val);
        animator.SetBool("CloseDoor", !val);
    }
}