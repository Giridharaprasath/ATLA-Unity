using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

// Single Player Load Save Script
public class SPLSS : MonoBehaviour
{
    public static SPLSS Instance { get; private set; }

    public GameObject player;
    public GameObject instantiatedPlayer;
    public bool playerInstantiated;

    public Vector3 playerPosition;
    public Vector3 playerRotation;

    public SPGD data;

    public Vector3 spawnPos = new Vector3(0f, 0.5f, 0f);
    public Vector3 spawnRot = new Vector3(0f, 0f, 0f);

    public static bool hasSaveFile { get; private set; }
    public static bool continueGame { get; set; }
    public static bool startNewGame { get; set; }

    public void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SavePlayer()
    {
        SSMS.SavePlayer(this);
        Debug.Log("GAME SAVED");
    }

    public void LoadPlayer()
    {
        data = SSMS.LoadPlayer();

        playerPosition.x = data.position[0];
        playerPosition.y = data.position[1];
        playerPosition.z = data.position[2];

        playerRotation.x = data.rotation[0];
        playerRotation.y = data.rotation[1];
        playerRotation.z = data.rotation[2];
    }

    public void CreatePlayer(GameObject _pl, Vector3 _plPos, Vector3 _plRot)
    {
        instantiatedPlayer = Instantiate(_pl, _plPos, Quaternion.Euler(_plRot));
        instantiatedPlayer.name = "Singleplayer - Aang";
        playerInstantiated = true;
    }

    public void AutoSave()
    {
        InvokeRepeating("SavePlayer", 0f , 10f);
    }

    public void Start()
    {
        LoadPlayer();
        if (data != null) hasSaveFile = true;
    }

    public void Update()
    {
        if (SceneManager.GetActiveScene().buildIndex == 1 && playerInstantiated == false)
        {
            if (continueGame == true)
            {
                Debug.Log("CONTINUE GAME");
                CreatePlayer(player, playerPosition, playerRotation);
                AutoSave();
                continueGame = false;
            }
            if (startNewGame == true)
            {
                Debug.Log("START NEW GAME");
                CreatePlayer(player, spawnPos, spawnRot);
                AutoSave();
                startNewGame = false;
            }
        }
        if (SceneManager.GetActiveScene().buildIndex == 0)
        {
            playerInstantiated = false;
            instantiatedPlayer = null;
        }
        if (playerInstantiated)
        {
            playerPosition = instantiatedPlayer.transform.position;
            playerRotation = instantiatedPlayer.transform.rotation.eulerAngles;
        }
    }
}
