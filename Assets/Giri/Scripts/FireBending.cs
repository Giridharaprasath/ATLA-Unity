using UnityEngine;
using Mirror;

public class FireBending : NetworkBehaviour
{
    [SerializeField]
    private GameObject fireTorch;
    [SerializeField]
    private Animator animator;
    [SyncVar]
    public bool isTorch;

    public override void OnStartAuthority()
    {
        fireTorch.SetActive(false);
        isTorch = false;

        InputManager.Controls.FireBending.FireTorch.started += ctx => CmdIsTorch();
    }

    private void Update()
    {
        if (isTorch)
        {
            CmdSetTorch(true);
        }
        else
        {
            CmdSetTorch(false);
        }
    }

    [Command]
    private void CmdIsTorch() => isTorch = !isTorch;

    private void SetTorch(bool val)
    {
        fireTorch.SetActive(val);
        animator.SetBool("IsFireTorch", val);
    }

    [Command]
    private void CmdSetTorch(bool val)
    {
        SetTorch(val);
        RpcSetTorch(val);
    }

    [ClientRpc]
    private void RpcSetTorch(bool val) => SetTorch(val);
}