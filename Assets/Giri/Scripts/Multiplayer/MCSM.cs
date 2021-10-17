using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Mirror;

// Multiplayer Character Selection Manager
public class MCSM : NetworkBehaviour
{
    public static MCSM Instance;

    [Header("Select Character UI")]
    [SerializeField]
    private GameObject selectCharCamera;
    [SerializeField]
    private GameObject selectCharUI;
    [SerializeField]
    private int curr_CharIndex = 0;
    [SerializeField]
    private Button selectCharButton;
    [SerializeField]
    private Transform selectCharPrePar;
    [SerializeField]
    private Button[] charButtons;
    [SerializeField]
    private bool isInSelectCharUI = false;

    [SerializeField]
    private ACSS[] aCSS;
    [SerializeField]
    private List<GameObject> aCSSInstance = new List<GameObject>();
    [SerializeField]
    private Transform startPosition;

    [SyncVar]
    public bool isC1S;
    [SyncVar]
    public bool isC2S;
    [SyncVar]
    public bool isC3S;
    [SyncVar]
    public bool isC4S;
    
    public int pCount = 0;
    public PCSS c1SPCSS;
    public PCSS c2SPCSS;
    public PCSS c3SPCSS;
    public PCSS c4SPCSS;

    public GameObject lLPSObject;
    public LLPS lLPSScript;

    public GameObject pCSSObject;
    public PCSS pCSSScript;

    private MNMS game;
    private MNMS Game
    {
        get
        {
            if (game != null) return game;
            return game = MNMS.singleton as MNMS;
        }
    }

    private void Awake()
    {
        if (Instance == null) Instance = this;
        curr_CharIndex = 0;
    }

    public override void OnStartClient()
    {
        if (selectCharPrePar.childCount == 0)
        {
            foreach (var acss in aCSS)
            {
                GameObject charInstance = Instantiate(acss.CharPreview, selectCharPrePar);
                charInstance.SetActive(false);
                aCSSInstance.Add(charInstance);
            }
        }
        aCSSInstance[curr_CharIndex].SetActive(true);
        CheckChar();
    }

    public override void OnStopClient()
    {
        SelectCharUI(false);
        CheckChar();
    }

    public void FindLLP()
    {
        lLPSObject = GameObject.Find("LocalLobbyPlayer");
        lLPSScript = lLPSObject.GetComponent<LLPS>();
    }

    public void FindLP()
    {
        Debug.Log("FOUND LP");
        pCSSObject = GameObject.Find("LocalPlayer");
        pCSSScript = pCSSObject.GetComponent<PCSS>();
    }

    public void CheckChar()
    {
        Debug.Log("CHECKING CHAR");
        if ((isC1S == true && curr_CharIndex == 0) || 
            (isC2S == true && curr_CharIndex == 1) ||
            (isC3S == true && curr_CharIndex == 2) ||
            (isC4S == true && curr_CharIndex == 3))
        {
            Debug.Log("CHARACTER ALREADY TAKEN");
            selectCharButton.interactable = false;
        }
        else
        {
            selectCharButton.interactable = true;
        }
    }

    public void SelectCharUI(bool val)
    {
        selectCharUI.SetActive(val);
        selectCharCamera.SetActive(val);
        isInSelectCharUI = val;
        CheckChar();
    }

    public void SetCurr_CharIndex(int i) 
    {
        aCSSInstance[curr_CharIndex].SetActive(false);
        curr_CharIndex = i;
        aCSSInstance[curr_CharIndex].SetActive(true);
        CheckChar();
    }

    public void UpdateChar()
    {
        Debug.Log("UPDATE CHAR");
        foreach (PCSS player in Game.pCSSs)
        {
            if (player.isC1S == true)
            {
                Debug.Log("CHARACTER 1 READY");
                this.c1SPCSS = player;
                CmdSetIsC1S(true);
            }
            if (player.isC2S == true)
            {
                Debug.Log("CHARACTER 2 READY");
                this.c2SPCSS = player;
                CmdSetIsC2S(true);
            }
            if (player.isC3S == true)
            {
                Debug.Log("CHARACTER 3 READY");
                this.c3SPCSS = player;
                CmdSetIsC3S(true);
            }
            if (player.isC4S == true)
            {
                Debug.Log("CHARACTER 4 READY");
                this.c4SPCSS = player;
                CmdSetIsC4S(true);
            }
        }
        if (this.c1SPCSS == null) CmdSetIsC1S(false);
        if (this.c2SPCSS == null) CmdSetIsC2S(false);
        if (this.c3SPCSS == null) CmdSetIsC3S(false);
        if (this.c4SPCSS == null) CmdSetIsC4S(false);
    }

    [Command(requiresAuthority = false)]
    public void CmdSetIsC1S(bool val) => isC1S = val;

    [Command(requiresAuthority = false)]
    public void CmdSetIsC2S(bool val) => isC2S = val;

    [Command(requiresAuthority = false)]
    public void CmdSetIsC3S(bool val) => isC3S = val;

    [Command(requiresAuthority = false)]
    public void CmdSetIsC4S(bool val) => isC4S = val;

    private void Update()
    {
        if (isInSelectCharUI) CheckChar();
    }

    public void SetChar()
    {
        lLPSScript.CmdSetChar(curr_CharIndex);
        lLPSScript.CmdSetIsCharSelected(true);
        lLPSScript.CmdSetSelectCharUI(false);
        lLPSScript.CmdSetNameOfCharSelected(aCSS[curr_CharIndex].CharName);
    }

    public void ServerSetChar(LLPS lLPS, int i)
    {
        var conn = lLPS.connectionToClient;
        Debug.Log(conn);
        PCSS pCSS = Instantiate(aCSS[i].CharPrefab, startPosition.position, startPosition.rotation);

        pCSS.pCSSConnID = lLPS.lLPSConnID;
        pCSS.pCSSPlayerNumber = lLPS.lLPSPlayerNumber;
        pCSS.isLeader = lLPS.isLeader;
        pCSS.pCSSCharName = aCSS[i].CharName;
        
        // Destroy(lLPS.gameObject);
        // lLPS.gameObject.transform.SetParent(pCSS.gameObject.transform);
        NetworkServer.ReplacePlayerForConnection(conn, pCSS.gameObject, true);
    }
}
