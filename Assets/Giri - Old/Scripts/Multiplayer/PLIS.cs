using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Player List Item Script
public class PLIS : MonoBehaviour
{
    public string pLISName;
    public int pLISConnID;
    public string pLISCharName;

    [SerializeField]
    private TMP_Text pLISNameText;
    [SerializeField]
    private TMP_Text pLISCharText;

    public GameObject lLPSObject;
    public LLPS lLPSScript;

    public bool isLLPFound;

    private void Start()
    {
        FindLLP();
        IsThisForLLP();
    }

    public void SetPLIS()
    {
        pLISNameText.text = pLISName;
    }

    public void FindLLP()
    {
        lLPSObject = GameObject.Find("LocalLobbyPlayer");
        lLPSScript = lLPSObject.GetComponent<LLPS>();
        isLLPFound = true;
    }

    public void IsThisForLLP()
    {
        if (this.pLISName == lLPSScript.lLPSName && this.pLISConnID == lLPSScript.lLPSConnID)
        {
            if (lLPSScript.myPLIS == null)
                lLPSScript.myPLIS = this;
        }
    }
    
    public void SetCharNameText()
    {
        pLISCharText.text = pLISCharName;
    }
}
