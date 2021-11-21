using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using Mirror;
using Steamworks;

// Multiplayer Network Manager Script
public class MNMS : NetworkManager
{
    [Header("Scenes")]
    [Scene] [SerializeField]
    private string mMPScene;
    [Scene] [SerializeField]
    private string mPCScene;

    [Header("Prefabs")]
    [SerializeField]
    private LLPS lLPSPrefab; 

    public List<LLPS> lLPSs = new List<LLPS>();

    public List<PCSS> pCSSs = new List<PCSS>();

    public override void OnServerAddPlayer(NetworkConnection conn)
    {
        Debug.Log("PLAYER SCENE NAME: " + SceneManager.GetActiveScene().name.ToString() + ". CORRECT SCENE NAME: TITLESCREEN");
        if (SceneManager.GetActiveScene().path == mMPScene || SceneManager.GetActiveScene().path == mPCScene)
        {
            bool isGameLeader = lLPSs.Count == 0;

            LLPS lLPSInstance = Instantiate(lLPSPrefab);

            lLPSInstance.isLeader = isGameLeader;
            lLPSInstance.lLPSConnID = conn.connectionId;
            lLPSInstance.lLPSPlayerNumber = lLPSs.Count + 1;

            NetworkServer.AddPlayerForConnection(conn, lLPSInstance.gameObject);
            Debug.Log("PLAYER ADDED. PLAYER NAME: " + lLPSInstance.lLPSName + ". PLAYER CONNECTION ID: " + lLPSInstance.lLPSConnID.ToString());
        }
    }

    public override void OnServerDisconnect(NetworkConnection conn)
    {
        if (conn.identity != null)
        {
            LLPS player = conn.identity.GetComponent<LLPS>();
            lLPSs.Remove(player);
            PCSS pl = conn.identity.GetComponent<PCSS>();
            pCSSs.Remove(pl);
        }
        base.OnServerDisconnect(conn);
    }

    public override void OnStopServer()
    {
        lLPSs.Clear();
        pCSSs.Clear();
    }

    public void HostShutDownServer()
    {
        GameObject MNMSObject = GameObject.Find("NetworkManager");
        Destroy(this.GetComponent<SLS>());
        Destroy(MNMSObject);
        Shutdown();
        SceneManager.LoadScene(0);
        Start();
    }
}
