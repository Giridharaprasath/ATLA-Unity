using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using Mirror;
using Steamworks;

// Multiplayer Character Lobby Manager Script 
public class MCLM : MonoBehaviour
{
    public static MCLM Instance;

    [Header("Lobby List UI")]
    [SerializeField]
    private GameObject lobbyListUI;
    [SerializeField]
    private TMP_Text lobbyNameText;
    [SerializeField]
    private GameObject lobbyListContentPanel;
    [SerializeField]
    private GameObject pLISPrefab;

    [Header("Character Select UI")]
    [SerializeField]
    private GameObject selectCharUI;
    [SerializeField]
    private GameObject selectCharCam;

    [Header("Pause Menu UI")]
    [SerializeField]
    private GameObject pauseMenuUI;

    public bool havePLISDone = false;
    public List<PLIS> pLISs = new List<PLIS>();

    public GameObject lLPSObject;
    public LLPS lLPSScript;

    public ulong currLobbyID;

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
        MakeInstance();
    }

    private void MakeInstance()
    {
        if (Instance == null) Instance = this;
    }

    public void LobbyListUI(bool con)
    {
        lobbyListUI.SetActive(con);
    }

    public void FindLLP()
    {
        lLPSObject = GameObject.Find("LocalLobbyPlayer");
        lLPSScript = lLPSObject.GetComponent<LLPS>();
    }

    public void UpdateLobbyName()
    {
        // lobbyNameText.text = "TESTING";
        currLobbyID = Game.GetComponent<SLS>().current_lobbyID;
        string lobbyName = SteamMatchmaking.GetLobbyData((CSteamID)currLobbyID, "Name");
        Debug.Log("UPDATELOBBYNAME: NEW LOBBY NAME: " + lobbyName);
        lobbyNameText.text = lobbyName;
    }

    public void UpdateUI()
    {
        if (!havePLISDone) CreatePLISs();
        if (pLISs.Count < Game.lLPSs.Count) CreateNewPLISs();
        if (pLISs.Count > Game.lLPSs.Count) RemovePLISs();
        if (pLISs.Count == Game.lLPSs.Count) UpdatePLISs();
    }

    private void CreatePLISs()
    {
        Debug.Log("CREATEPLISS: THIS MANY PLAYERS TO CREATE: " + Game.lLPSs.Count.ToString());
        foreach (LLPS player in Game.lLPSs)
        {
            GameObject newPLISObject = Instantiate(pLISPrefab) as GameObject;
            PLIS newPLISScript = newPLISObject.GetComponent<PLIS>();

            newPLISScript.pLISName = player.lLPSName;
            newPLISScript.pLISConnID = player.lLPSConnID;
            newPLISScript.SetPLIS();

            newPLISObject.transform.SetParent(lobbyListContentPanel.transform);
            newPLISObject.transform.localScale = Vector3.one;

            pLISs.Add(newPLISScript);
        }
        havePLISDone = true;
    }

    private void CreateNewPLISs()
    {
        foreach (LLPS player in Game.lLPSs)
        {
            if (!pLISs.Any(b => b.pLISConnID == player.lLPSConnID))
            {
                Debug.Log("CREATENEWPLISS: PLAYER NOT FOUND IN PLISS : " + player.lLPSName);
                GameObject newPLISObject = Instantiate(pLISPrefab) as GameObject;
                PLIS newPLISScript = newPLISObject.GetComponent<PLIS>();

                newPLISScript.pLISName = player.lLPSName;
                newPLISScript.pLISConnID = player.lLPSConnID;
                newPLISScript.SetPLIS();

                newPLISObject.transform.SetParent(lobbyListContentPanel.transform);
                newPLISObject.transform.localScale = Vector3.one;

                pLISs.Add(newPLISScript);
            }
        }
    }

    private void RemovePLISs()
    {
        List<PLIS> pLISRemove = new List<PLIS>();
        foreach (PLIS pLIS in pLISs)
        {
            if (!Game.lLPSs.Any(b => b.lLPSConnID == pLIS.pLISConnID))
            {
                Debug.Log("REMOVEPLISS: PLIS FROM CONNTECTION ID: " + pLIS.pLISConnID.ToString() + " DOEN'T EXISTS IN GAME PLAYER LIST");
                pLISRemove.Add(pLIS);
            }
        } 
        if (pLISRemove.Count > 0)
        {
            foreach (PLIS pLISRe in pLISRemove)
            {
                GameObject pLISRObject = pLISRe.gameObject;
                pLISs.Remove(pLISRe);
                Destroy(pLISRObject);
                pLISRObject = null;
            }
        }
    }

    private void UpdatePLISs()
    {
        Debug.Log("UPDATEPLISS");
        foreach (LLPS lLPS in Game.lLPSs)
        {
            foreach (PLIS pLIS in pLISs)
            {
                if (pLIS.pLISConnID == lLPS.lLPSConnID)
                {
                    pLIS.pLISName = lLPS.lLPSName;
                    pLIS.SetPLIS();
                    if (lLPS.isCharSelected) 
                    {
                        pLIS.pLISCharName = lLPS.nameOfCharSelected;
                        pLIS.SetCharNameText();
                    }
                    else 
                    {
                        pLIS.pLISCharName = "Selecting";
                        pLIS.SetCharNameText();
                    }
                }
            }
        }
    }

    public void PauseGame()
    {
        pauseMenuUI.SetActive(true);
        lLPSScript.isInPauseMenuUI = true;
        lLPSScript.toPause = false;
    }

    public void ResumeGame()
    {
        pauseMenuUI.SetActive(false);
        lLPSScript.isInPauseMenuUI = false;
        lLPSScript.toPause = false;
    }

    public void LeaveLobby()
    {
        lLPSScript.QuitLobby();
    }
}
