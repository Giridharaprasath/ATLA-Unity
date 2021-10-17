using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using Steamworks;

// Main Menu
public class MM : MonoBehaviour
{
    [Serializable]
    public class pageGameObject
    {
        public GameObject sSPGO;    // Start Screen Page Game Object
        public GameObject mMPGO;    // Main Menu Page Game Object
        public GameObject sPPGO;    // Singleplayer Page Game Object
        public GameObject mPPGO;    // Multiplayer Page Game Object
        public GameObject sPGO;     // Setting Page Game Object
    }      

    [Serializable]
    public class currentPage
    {
        public bool sSPCP;      // Start Screen Page Current Page
        public bool mMPCP;      // Main Menu Page Current Page
        public bool sPPCP;      // Singleplayer Page Current Page
        public bool mPPCP;      // Multiplayer Page Current Page                    
        public bool sPCP;       // Setting Page Current Page
    }

    [Serializable]
    public class mainMenuPage
    {
        public int mMPBC;                                           // Main Menu Page Button Count
        public List<GameObject> mMPBL = new List<GameObject>();     // Main Menu Page Button List
    }

    [Serializable]
    public class singleplayerPage
    {
        public int sPPBC;                                           // Singleplayer Page Button Count
        public List<GameObject> sPPBL = new List<GameObject>();     // Singleplayer Page Button List
    }

    [Serializable]
    public class multiplayerPage
    {
        public int mPPBC;                                           // Multiplayer Page Button Count
        public List<GameObject> mPPBL = new List<GameObject>();     // Multiplayer Page Button List
    }

    [Serializable]
    public class settingPage
    {
        [Serializable]
        public class settingPageQualityLevel
        {
            public int sPQLBC;                                          // Setting Page Quality Level Button Count
            public List<GameObject> sPQLBL = new List<GameObject>();    // Setting Page Quality Level Button List
        }

        [Serializable]
        public class settingPageScreenMode
        {
            public int sPSMBC;                                          // Setting Page Screen Mode Button Count
            public List<GameObject> sPSMBL = new List<GameObject>();    // Setting Page Screen Mode Button List
        }

        [Serializable]
        public class settingPageResolution
        {
            public int sPRSBC;                                          // Setting Page Resolution Set Button Count
            public List<GameObject> sPRSBL = new List<GameObject>();    // Setting Page Resolution Set Button List
        }

        [Serializable]
        public class settingPageVSync
        {
            public int sPVSBC;                                          // Setting Page VSync Button Count
            public List<GameObject> sPVSBL = new List<GameObject>();    // Setting Page VSync Button List
        }

        [Serializable]
        public class settingFPS
        {
            public int sPFSBC;                                          // Setting Page FPS Set Button Count
            public List<GameObject> sPFSBL = new List<GameObject>();    // Setting Page FPS Set Button List
        }

        public int sPBC;                                                // Setting Page Button Count
        public List<GameObject> sPBL = new List<GameObject>();          // Setting Page Button List

        public settingPageQualityLevel sPQL;                            
        public settingPageScreenMode sPSM;
        public settingPageResolution sPRS;
        public settingPageVSync sPVS;
        public settingFPS sPFS;
        
        public bool sPQLCP;                                             // Setting Page Quality Level Current Page                                         
        public bool sPSMCP;                                             // Setting Page Screen Mode Current Page
        public bool sPRSCP;                                             // Setting Page Resolution Set Current Page
        public bool sPVSCP;                                             // Setting Page VSync Current Page    
        public bool sPFSCP;                                             // Setting Page FPS Set Current 
    }

    [Serializable]
    public class inputActions
    {
        public bool selectPerformed;    // Select Performed
        public bool backPerformed;      // Back Performed
        public int navigateUD;          // Navigate Up Down
        public int navigateLR;          // Navigate Left Right
    }

    [Serializable]
    public class screenSettings
    {
        public bool isFullScreen;     
        public int screenWidth;
        public int screenHeight;
        public int screenFPS;
    }

    [Header("PAGE GAME OBJECT")]
    [Tooltip("Place Game Object of the Page")]
    [SerializeField]
    private pageGameObject pGO;

    [Header("CURRENT PAGE")]
    [Tooltip("True for Current Page")]
    [SerializeField]
    private currentPage cP;

    [Header("MAIN MENU PAGE")]
    [Tooltip("Place Game Object of Main Menu Page")]
    [SerializeField]
    private mainMenuPage mMP;

    [Header("SINGLEPLAYER PAGE")]
    [Tooltip("Place Game Object of Singleplayer Page")]
    [SerializeField]
    private singleplayerPage sPP;

    [Header("MULTIPLAYER PAGE")]
    [Tooltip("Place Game Object of Multiplayer Page")]
    [SerializeField]
    private multiplayerPage mPP;

    [Header("SETTING PAGE")]
    [Tooltip("Place Game Object of Setting Page")]
    [SerializeField]
    private settingPage sP;

    [Header("INPUT ACTIONS")]
    [Tooltip("Shows Input Values")]
    [SerializeField]
    private inputActions iA;

    [Header("SCREEN SETTINGS")]
    [Tooltip("Shows Screen Settings")]
    [SerializeField]
    private screenSettings sS;

    private void Awake()
    {
        pGO.sSPGO.SetActive(true);
        pGO.mMPGO.SetActive(false);
        pGO.sPPGO.SetActive(false);
        pGO.mPPGO.SetActive(false);
        pGO.sPGO.SetActive(false); 
        cP.sSPCP = true;

        mMP.mMPBC = 0;

        sPP.sPPBC = -1;

        mPP.mPPBC = 0;

        sP.sPBC = 0;
        sP.sPQL.sPQLBC = QualitySettings.GetQualityLevel();
        sP.sPRS.sPRSBC = (Screen.width == 1920 ? 0 : (Screen.width == 1600 ? 1 : (Screen.width == 1366 ? 2 : 3)));
        sP.sPSM.sPSMBC = Screen.fullScreen == false ? 0 : 1;
        sP.sPVS.sPVSBC = QualitySettings.vSyncCount;
        sP.sPFS.sPFSBC = (Application.targetFrameRate == 30 ? 0 : (Application.targetFrameRate == 60 ? 1 : (Application.targetFrameRate == 120 ? 2 : 3)));

        IM.Controls.UI.Select.performed += ctx => iA.selectPerformed = true;
        IM.Controls.UI.Back.performed += ctx => iA.backPerformed = true;
        IM.Controls.UI.Navigate.performed += ctx => SetNavigateUI(ctx.ReadValue<Vector2>());

        IM.Controls.UI.Select.canceled += ctx => iA.selectPerformed = false;
        IM.Controls.UI.Back.canceled += ctx => iA.backPerformed = false;
        IM.Controls.UI.Navigate.canceled += ctx => { iA.navigateUD = 0; iA.navigateLR = 0; };
    }

    private int MoveUD(int a, int y, int c)
    {
        int ans = a - y;
        if ( ans > 0 && ans < c ) return ans;
        else if ( ans < 0 ) return c-1;
        else return 0;
    }

    private int MoveLR(int a, int x, int c)
    {
        int ans = a + x;
        if ( ans > 0 && ans < c ) return ans;
        else if ( ans < 0 ) return c-1;
        else return 0; 
    }

    private void DeactivateAll(List<GameObject> BTD)
    {
        for ( int i = 0; i < BTD.Count; i++)
            BTD[i].SetActive(false);
    }

    private void SetNavigateUI(Vector2 ud)
    {
        iA.navigateUD = (int)ud.y;
        iA.navigateLR = (int)ud.x;
        if (cP.mMPCP) 
        {
            mMP.mMPBC = MoveUD(mMP.mMPBC, iA.navigateUD, mMP.mMPBL.Count);
            DeactivateAll(mMP.mMPBL);
        }
        if (cP.sPPCP)
        {
            sPP.sPPBC = MoveUD(sPP.sPPBC, iA.navigateUD, sPP.sPPBL.Count);
            DeactivateAll(sPP.sPPBL);
        }
        if (cP.mPPCP)
        {
            mPP.mPPBC = MoveUD(mPP.mPPBC, iA.navigateUD, mPP.mPPBL.Count);
            DeactivateAll(mPP.mPPBL);
        }
        if (cP.sPCP)
        {
            sP.sPBC = MoveUD(sP.sPBC, iA.navigateUD, sP.sPBL.Count);
            DeactivateAll(sP.sPBL);
        }
        if (sP.sPQLCP)
        {
            sP.sPQL.sPQLBC = MoveLR(sP.sPQL.sPQLBC, iA.navigateLR, sP.sPQL.sPQLBL.Count);
            DeactivateAll(sP.sPQL.sPQLBL);
        }
        if (sP.sPSMCP)
        {
            sP.sPSM.sPSMBC = MoveLR(sP.sPSM.sPSMBC, iA.navigateLR, sP.sPSM.sPSMBL.Count);
            DeactivateAll(sP.sPSM.sPSMBL);
        }
        if (sP.sPRSCP)
        {
            sP.sPRS.sPRSBC = MoveLR(sP.sPRS.sPRSBC, iA.navigateLR, sP.sPRS.sPRSBL.Count);
            DeactivateAll(sP.sPRS.sPRSBL);
        }
        if (sP.sPVSCP)
        {
            sP.sPVS.sPVSBC = MoveLR(sP.sPVS.sPVSBC, iA.navigateLR, sP.sPVS.sPVSBL.Count);
            DeactivateAll(sP.sPVS.sPVSBL);
        }
        if (sP.sPFSCP)
        {
            sP.sPFS.sPFSBC = MoveLR(sP.sPFS.sPFSBC, iA.navigateLR, sP.sPFS.sPFSBL.Count);
            DeactivateAll(sP.sPFS.sPFSBL);
        }
    }

    // Start Screen Page To Main Menu Page
    public void SSPToMMP()
    {
        cP.sSPCP = false;
        pGO.sSPGO.SetActive(false);

        cP.mMPCP = true;
        pGO.mMPGO.SetActive(true);

        iA.selectPerformed = false;
        DeactivateAll(mMP.mMPBL);

        Debug.Log("GO TO MAIN MENU");
    }

    // Main Menu Page To Single Player Page
    public void MMPToSPP()
    {
        mMP.mMPBC = 0;

        cP.mMPCP = false;
        pGO.mMPGO.SetActive(false);

        cP.sPPCP = true;
        pGO.sPPGO.SetActive(true);
        
        iA.selectPerformed = false;
        DeactivateAll(sPP.sPPBL);
        DeactivateAll(mMP.mMPBL);
        Debug.Log("GO TO SINGLEPLAYER PAGE");
    }

    // Main Menu Page To Multiplayer Page
    public void MMPToMPP()
    {
        if (SteamManager.Initialized)
        {
            mMP.mMPBC = 1;

            cP.mMPCP = false;
            pGO.mMPGO.SetActive(false);

            cP.mPPCP = true;
            pGO.mPPGO.SetActive(true);
            DeactivateAll(mPP.mPPBL);
            Debug.Log("GO TO MULTIPLAYER PAGE");
        }
        else
        {
            Debug.Log("START STEAM");
        }
        iA.selectPerformed = false;
        DeactivateAll(mMP.mMPBL);
    }

    // Main Menu Page To Settings Page
    public void MMPToSP()
    {
        mMP.mMPBC = 2;

        cP.mMPCP = false;
        pGO.mMPGO.SetActive(false);
        
        cP.sPCP = true;
        pGO.sPGO.SetActive(true);

        iA.selectPerformed = false;
        DeactivateAll(sP.sPBL);
        DeactivateAll(mMP.mMPBL);
        Debug.Log("GO TO SETTINGS PAGE");
    }

    // Settings Page To Settings Page Quality Level
    public void SPToSPQL()
    {
        sP.sPBC = 0;

        cP.sPCP = false;

        sP.sPQLCP = true;

        iA.selectPerformed = false;
        DeactivateAll(sP.sPQL.sPQLBL);
        DeactivateAll(sP.sPBL);

        Debug.Log("CHANGE QUALITY LEVEL");
    }

    // Settings Page To Settings Page Screen Mode
    public void SPToSPSM()
    {
        sP.sPBC = 1;

        cP.sPCP = false;

        sP.sPSMCP = true;
        
        iA.selectPerformed = false;
        DeactivateAll(sP.sPSM.sPSMBL);
        DeactivateAll(sP.sPBL);
        
        Debug.Log("CHANGE FULL SCREEN MODE");
    }

    // Settings Page To Settings Page Resolution Set
    public void SPToSPRS()
    {
        sP.sPBC = 2;

        cP.sPCP = false;

        sP.sPRSCP = true;
        
        iA.selectPerformed = false;
        DeactivateAll(sP.sPRS.sPRSBL);
        DeactivateAll(sP.sPBL);
        
        Debug.Log("CHANGE SCREEN RESOLUTION");
    }

    // Settings Page To Settings Page VSync
    public void SPToSPVS()
    {
        sP.sPBC = 3;

        cP.sPCP = false;

        sP.sPVSCP = true;
        
        iA.selectPerformed = false;
        DeactivateAll(sP.sPVS.sPVSBL);
        DeactivateAll(sP.sPBL);
        
        Debug.Log("CHANGE VSYNC");
    }

    // Settings Page To Settings FPS Set
    public void SPToSPFS()
    {
        sP.sPBC = 4;

        cP.sPCP = false;

        sP.sPFSCP = true;

        iA.selectPerformed = false;
        DeactivateAll(sP.sPFS.sPFSBL);
        DeactivateAll(sP.sPBL);

        Debug.Log("CHANGE CAP FPS");
    }

    // Singleplayer Page To Continue Game
    public void ContinueGame()
    {
        if (SPLSS.hasSaveFile)
        {
            Debug.Log("CONTINUE GAME"); 
            SceneManager.LoadScene(1);
            SPLSS.continueGame = true;
        }
        else Debug.Log("NO SAVE FILE");

        iA.selectPerformed = false;
    }

    // Singleplayer Page To New Game
    public void StartNewGame()
    {
        Debug.Log("STARTED NEW GAME");
        SceneManager.LoadScene(1);
        SPLSS.startNewGame = true;
        iA.selectPerformed = false;
    }

    // Multiplayer Page To Host Lobby
    public void StartHosting()
    {
        Debug.Log("STARTED HOSTING");
        SLS.Instance.HostLobby();
        iA.selectPerformed = false;
    }

    // Multiplayer Page To Join Lobby
    public void ToJoinLobby()
    {
        Debug.Log("TO JOIN LOBBY");
        SLS.Instance.GetListOfLobbies();
        iA.selectPerformed = false;
    }

    public void QuitGame()
    {
        Application.Quit();
        Debug.Log("GO TO WINDOWS");
    }

    // Main Menu Page To Start Screen Page
    public void MMPToSSP()
    {
        cP.mMPCP = false;
        pGO.mMPGO.SetActive(false);

        cP.sSPCP = true;
        pGO.sSPGO.SetActive(true);
        
        iA.backPerformed = false;
        mMP.mMPBC = 0;
        
        Debug.Log("GO TO START SCREEN");
    }

    // Single Player Page To Main Menu Page
    public void SPPToMMP()
    {
        cP.sPPCP = false;
        pGO.sPPGO.SetActive(false);

        cP.mMPCP = true;
        pGO.mMPGO.SetActive(true);
        
        iA.backPerformed = false;
        sPP.sPPBC = -1;
        mMP.mMPBC = 0;
        Debug.Log("GO TO MAIN MENU");
    }

    // Multi Player Page To Main Menu Page
    public void MPPToMMP()
    {
        cP.mPPCP = false;
        pGO.mPPGO.SetActive(false);

        cP.mMPCP = true;
        pGO.mMPGO.SetActive(true);
        
        iA.backPerformed = false;
        mMP.mMPBC = 1;
        Debug.Log("GO TO MAIN MENU");        
    }

    // Settings Page To Main Menu Page
    public void SPToMMP()
    {
        cP.sPCP = false;
        pGO.sPGO.SetActive(false);

        cP.mMPCP = true;
        pGO.mMPGO.SetActive(true);
        
        iA.backPerformed = false;
        sP.sPBC = 0;
        mMP.mMPBC = 2;
        Debug.Log("GO TO MAIN MENU");
    }

    // Settings Page Quality Level To Settings Page
    public void SPQLToSP()
    {
        sP.sPQLCP = false;

        cP.sPCP = true;
        
        iA.backPerformed = false;
        sP.sPQL.sPQLBC = QualitySettings.GetQualityLevel(); 
        DeactivateAll(sP.sPQL.sPQLBL);
        Debug.Log("GO BACK SETTINGS");
    }

    // Settings Page Screen Mode To Settings Page
    public void SPSMToSP()
    {
        sP.sPSMCP = false;

        cP.sPCP = true;
        
        iA.backPerformed = false;
        sP.sPSM.sPSMBC = Screen.fullScreen == false ? 0 : 1;
        DeactivateAll(sP.sPSM.sPSMBL);
        Debug.Log("GO BACK SETTINGS");
    }

    // Settings Page Resolution Set To Settings Page
    public void SPRSToSP()
    {
        sP.sPRSCP = false;

        cP.sPCP = true;
        
        iA.backPerformed = false;
        sP.sPRS.sPRSBC = (Screen.width == 1920 ? 0 : (Screen.width == 1600 ? 1 : (Screen.width == 1366 ? 2 : 3)));
        DeactivateAll(sP.sPRS.sPRSBL);
        Debug.Log("GO BACK SETTINGS");
    }

    // Settings Page VSync To Settings Page
    public void SPVSToSP()
    {
        sP.sPVSCP = false;

        cP.sPCP = true;
        
        iA.backPerformed = false;
        sP.sPVS.sPVSBC = QualitySettings.vSyncCount;
        DeactivateAll(sP.sPVS.sPVSBL);
        Debug.Log("GO BACK SETTINGS");
    }

    // Settings Page FPS Set To Settings Page
    public void SPFSToSP()
    {
        sP.sPFSCP = false;

        cP.sPCP = true;

        iA.backPerformed = false;
        sP.sPFS.sPFSBC = (Application.targetFrameRate == 30 ? 0 : (Application.targetFrameRate == 60 ? 1 : (Application.targetFrameRate == 120 ? 2 : 3)));
        DeactivateAll(sP.sPFS.sPFSBL);
        Debug.Log("GO BACK SETTINGS");
    }

    private void Update()
    {
        sS.isFullScreen = Screen.fullScreen;
        sS.screenWidth = Screen.width;
        sS.screenHeight = Screen.height;
        sS.screenFPS = Application.targetFrameRate == -1 ? 3 : Application.targetFrameRate;

        if (iA.selectPerformed == true)
        {
            if (cP.sSPCP == true) SSPToMMP();
            else if (cP.mMPCP == true)
            {
                if (mMP.mMPBC == 0) MMPToSPP();
                else if (mMP.mMPBC == 1) MMPToMPP();
                else if (mMP.mMPBC == 2) MMPToSP();
                else if (mMP.mMPBC == 3) QuitGame();
            }
            else if (cP.sPPCP == true)
            {
                if (sPP.sPPBC == 0) ContinueGame();
                else if (sPP.sPPBC == 1) StartNewGame();
            }
            else if (cP.mPPCP == true)
            {
                if (mPP.mPPBC == 0) StartHosting();
                else if (mPP.mPPBC == 1) ToJoinLobby();
            }
            else if (cP.sPCP == true)
            {
                if (sP.sPBC == 0) SPToSPQL();
                else if (sP.sPBC == 1) SPToSPSM(); 
                else if (sP.sPBC == 2) SPToSPRS(); 
                else if (sP.sPBC == 3) SPToSPVS();
                else if (sP.sPBC == 4) SPToSPFS();
            }
            else if (sP.sPQLCP == true)
            {
                if (sP.sPQL.sPQLBC == 0)
                {
                    QualitySettings.SetQualityLevel(0);
                    Debug.Log("GRAPHIC SETTING LOW");
                }
                else if (sP.sPQL.sPQLBC == 1)
                {
                    QualitySettings.SetQualityLevel(1);
                    Debug.Log("GRAPHIC SETTING MEDIUM");
                }
                else if (sP.sPQL.sPQLBC == 2)
                {
                    QualitySettings.SetQualityLevel(2);
                    Debug.Log("GRAPHIC SETTING HIGH");
                }
            }
            else if (sP.sPSMCP == true)
            {
                if (sP.sPSM.sPSMBC == 0)
                {
                    Screen.fullScreen = false;
                    Debug.Log("SCREEN MODE WINDOWED");
                }
                else if (sP.sPSM.sPSMBC == 1)
                {
                    Screen.fullScreen = true;
                    Debug.Log("SCREEN MODE FULL SCREEN");
                }
            }
            else if (sP.sPRSCP == true)
            {
                if (sP.sPRS.sPRSBC == 0)
                {
                    Screen.SetResolution(1920, 1080, sS.isFullScreen);
                    Debug.Log("RESOLUTION SET 1920 * 1080");
                }
                else if (sP.sPRS.sPRSBC == 1)
                {
                    Screen.SetResolution(1600, 900, sS.isFullScreen);
                    Debug.Log("RESOLUTION SET 1600 * 900");
                }
                else if (sP.sPRS.sPRSBC == 2)
                {
                    Screen.SetResolution(1366, 768, sS.isFullScreen);
                    Debug.Log("RESOLUTION SET 1366 * 768");
                }
                else if (sP.sPRS.sPRSBC == 3)
                {
                    Screen.SetResolution(1280, 720, sS.isFullScreen);
                    Debug.Log("RESOLUTION SET 1280 * 720");
                }
            }
            else if (sP.sPVSCP == true)
            {
                if (sP.sPVS.sPVSBC == 0)
                {
                    QualitySettings.vSyncCount = 0;
                    Debug.Log("VSYNC OFF");
                }
                else if (sP.sPVS.sPVSBC == 1)
                {
                    QualitySettings.vSyncCount = 1;
                    Debug.Log("VSYNC ON");
                }
            }
            else if (sP.sPFSCP == true)
            {
                if (sP.sPFS.sPFSBC == 0)
                {
                    Application.targetFrameRate = 30;
                    Debug.Log("CAP FPS 30");
                }
                else if (sP.sPFS.sPFSBC == 1)
                {
                    Application.targetFrameRate = 60;
                    Debug.Log("CAP FPS 60");
                }
                else if (sP.sPFS.sPFSBC == 2)
                {
                    Application.targetFrameRate = 120;
                    Debug.Log("CAP FPS 120");
                }
                else if (sP.sPFS.sPFSBC == 3)
                {
                    Application.targetFrameRate = -1;
                    Debug.Log("CAP FPS UNLIMITED");
                }
            }
        }

        if (iA.backPerformed == true)
        {
            if (cP.mMPCP == true) MMPToSSP(); 
            else if (cP.sPPCP == true) SPPToMMP(); 
            else if (cP.mPPCP == true) MPPToMMP();
            else if (cP.sPCP == true) SPToMMP(); 
            else if (sP.sPQLCP == true) SPQLToSP(); 
            else if (sP.sPSMCP == true) SPSMToSP(); 
            else if (sP.sPRSCP == true) SPRSToSP(); 
            else if (sP.sPVSCP == true) SPVSToSP();
            else if (sP.sPFSCP == true) SPFSToSP(); 
        }

        mMP.mMPBL[mMP.mMPBC].SetActive(true);

        if (cP.sPPCP == true) sPP.sPPBL[sPP.sPPBC].SetActive(true);

        if (cP.mPPCP == true) mPP.mPPBL[mPP.mPPBC].SetActive(true);

        if (cP.sPCP == true || sP.sPQLCP == true || sP.sPSMCP == true || sP.sPRSCP == true|| sP.sPVSCP == true || sP.sPFSCP == true)
        {
            sP.sPBL[sP.sPBC].SetActive(true);
            sP.sPQL.sPQLBL[sP.sPQL.sPQLBC].SetActive(true);
            sP.sPSM.sPSMBL[sP.sPSM.sPSMBC].SetActive(true);
            sP.sPRS.sPRSBL[sP.sPRS.sPRSBC].SetActive(true);
            sP.sPVS.sPVSBL[sP.sPVS.sPVSBC].SetActive(true);
            sP.sPFS.sPFSBL[sP.sPFS.sPFSBC].SetActive(true);
        }
    }
}