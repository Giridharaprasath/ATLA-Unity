using UnityEngine;
using Mirror;

public class CollectablesManager : NetworkBehaviour
{
    [SyncVar]
    public bool isCollected;

    [SerializeField]
    private Animator animator;
    [SerializeField]
    private GameObject openTrigger;

    public void CollectChest()
    {
        CmdCollectChest();
    }

    [Command(requiresAuthority = false)]
    private void CmdCollectChest()
    {
        isCollected = true;
        animator.enabled = true;
        openTrigger.SetActive(false);
    }
}
