using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using Mirror;

// Player Character Selected Script
public class PCSS : NetworkBehaviour
{
    [Header("Character Info")]
    [SyncVar]
    public string pCSSName;
    [SyncVar]
    public int pCSSConnID;
    [SyncVar]
    public int pCSSPlayerNumber;
    [SyncVar]
    public bool isLeader = false;
    [SyncVar]
    public string pCSSCharName;
    [SyncVar(hook = nameof(HandleIsCS))]
    public bool isC1S = false;
    [SyncVar(hook = nameof(HandleIsCS))]
    public bool isC2S = false;
    [SyncVar(hook = nameof(HandleIsCS))]
    public bool isC3S = false;
    [SyncVar(hook = nameof(HandleIsCS))]
    public bool isC4S = false;

    // public GameObject cam;      

    // public DPM myDPM;           
    // public EBS myEBS;           
    // public FBS myFBS;           

    // public PCIS myPCIS;

    public bool _isLocalPlayer;

    private MNMS game;
    private MNMS Game
    {
        get
        {
            if (game != null) return game;
            return game = MNMS.singleton as MNMS;
        }
    }

    public override void OnStartClient()
    {
        Game.pCSSs.Add(this);
        // MCSM.Instance.SetCSPCSS();
        // MCSM.Instance.UpdateSC();
        MCSM.Instance.UpdateChar();
    } 

    public override void OnStopClient()
    {
        Game.pCSSs.Remove(this);
        // MCSM.Instance.SetCSPCSS();
        // MCSM.Instance.UpdateSC();
        MCSM.Instance.UpdateChar();
    }

    public override void OnStartAuthority()
    {
        gameObject.name = "LocalPlayer";
        MCSM.Instance.FindLP();
        CmdSetCharType();
    }

    [Command]
    public void CmdSetCharType()
    {
        Debug.Log("CMDSETCHARTYPE");
        if (pCSSCharName == "Cube") 
        {
            isC1S = true;
            this.HandleIsCS(!this.isC1S, this.isC1S);
        }
        if (pCSSCharName == "Sphere") 
        {
            isC2S = true;
            this.HandleIsCS(!this.isC2S, this.isC2S);
        }
        if (pCSSCharName == "Capsule") 
        {
            isC3S = true;
            this.HandleIsCS(!this.isC3S, this.isC3S);
        }
        if (pCSSCharName == "Cylinder") 
        {
            isC4S = true;
            this.HandleIsCS(!this.isC4S, this.isC4S);
        }
    }

    public void HandleIsCS(bool oldValue, bool newValue)
    {
        Debug.Log("HOOK HANDLE IS CS");
        if (isClient) MCSM.Instance.UpdateChar();
    }

    private void Update()
    {
        // if (hasAuthority) myDPM.enabled = true;
        _isLocalPlayer = isLocalPlayer;
        // MCSM.Instance.UpdateChar();
    }
}
