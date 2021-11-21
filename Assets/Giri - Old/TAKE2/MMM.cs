using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// Main Menu Manager
public class MMM : MonoBehaviour
{
    [Header("Start Screen Page")]
    public GameObject sSPPanel;
    public GameObject sSPToMMPButton;

    [Header("Main Menu Page")]
    public GameObject mMPPanel;

    [Header("Single Player Page")]
    public GameObject sPPPanel;

    [Header("Multi Player Page")]
    public GameObject mPPPanel;

    [Header("Options Page")]
    public GameObject oPPanel;

    [Header("Join Lobby Page")]
    public GameObject jLPanel;

    private void Start() 
    {
        sSPPanel.SetActive(true);
        Cursor.visible = false;
        EventSystem.current.SetSelectedGameObject(sSPToMMPButton);
    }

    public void SSPToMMP()
    {
        Debug.Log("GOING TO MAIN MENU PAGE");
        sSPPanel.SetActive(false);
        mMPPanel.SetActive(true);
    }

    public void MMPToSPP()
    {
        Debug.Log("GOING TO SINGLE PLAYER PAGE");
        mMPPanel.SetActive(false);
        sPPPanel.SetActive(true);
    }

    public void MMPToMPP()
    {
        Debug.Log("GOING TO MULTI PLAYER PAGE");
        mMPPanel.SetActive(false);
        mPPPanel.SetActive(true);
    }

    public void MMPToOP()
    {
        Debug.Log("GOING TO OPTIONS PAGE");
        mMPPanel.SetActive(false);
        oPPanel.SetActive(true);
    }

    public void QuitGame()
    {
        Debug.Log("QUITING GAME");
        Application.Quit();
    }

    public void ContinueGame()
    {
        Debug.Log("CONTINUE GAME");
    }

    public void NewGame()
    {
        Debug.Log("START NEW GAME");
    }

    public void SPPToMMP()
    {
        Debug.Log("BACK TO MAIN MENU PAGE");
        sPPPanel.SetActive(false);
        mMPPanel.SetActive(true);
    }

    public void HostLobby()
    {
        Debug.Log("HOSTING LOBBY");
        SLS.Instance.HostLobby();
    }

    public void JoinLobby()
    {
        Debug.Log("GETTING LIST OF LOBBIES");
        jLPanel.SetActive(true);
        SLS.Instance.GetListOfLobbies();
    }

    public void BackToMPP()
    {
        Debug.Log("CLOSING JOIN LOBBY PAGE");
        jLPanel.SetActive(false);
        SLS.Instance.DestroyLLI();
    }

    public void MPPToMMP()
    {
        Debug.Log("BACK TO MAIN MENU PAGE");
        mPPPanel.SetActive(false);
        mMPPanel.SetActive(true);
    }

    public void OPToMMP()
    {
        Debug.Log("BACK TO MAIN MENU PAGE");
        oPPanel.SetActive(false);
        mMPPanel.SetActive(true);
    }
}
