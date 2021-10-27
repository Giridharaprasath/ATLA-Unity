using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

// Player Character Item Script
public class PCIS : MonoBehaviour
{
    public string pCISName;
    public int pCISConnID;
    public string pCISCharName;

    [SerializeField]
    private TMP_Text pCISNameText;
    [SerializeField]
    private TMP_Text pCISCharText;

    public GameObject pCSSObject;
    public PCSS pCSSScript;

    public bool isLPFound;

    private void Start()
    {
        FindLP();
        IsThisForLP();
    }

    public void FindLP()
    {
        pCSSObject = GameObject.Find("LocalPlayer");
        pCSSScript = pCSSScript.GetComponent<PCSS>();
        isLPFound = true;
    }

    public void IsThisForLP()
    {
        if (this.pCISName == pCSSScript.pCSSName && this.pCISConnID == pCSSScript.pCSSConnID)
        {
            // if (pCSSScript.myPCIS == null)
            //     pCSSScript.myPCIS = this;
        }
    }
}
