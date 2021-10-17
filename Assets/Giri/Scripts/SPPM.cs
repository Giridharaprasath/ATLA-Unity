using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

// Singleplayer Pause Menu
public class SPPM : MonoBehaviour
{
    public GameObject pauseMenu;

    public GameObject resumeButton;

    public bool isEscPressed;
    public bool isPaused;

    public void Awake()
    {
        Cursor.lockState = CursorLockMode.Locked;
        pauseMenu.SetActive(false);

        IM.Controls.Player.Pause.performed += ctx => isEscPressed = true;

        IM.Controls.Player.Pause.canceled += ctx => isEscPressed = false;
    }

    public void PMToGame()
    {
        Debug.Log("GAME RESUMED");
        isEscPressed = false;
        isPaused = false;
        pauseMenu.SetActive(false);
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void GameToPM()
    {
        Debug.Log("GAME PAUSED");
        isEscPressed = false;
        isPaused = true;
        pauseMenu.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        EventSystem.current.SetSelectedGameObject(resumeButton);
    }

    public void PMSaveGame()
    {
        SPLSS.Instance.SavePlayer();
    }

    public void PMToSP()
    {
        Debug.Log("GO TO SETTINGS PAGE");
        isEscPressed = false;
    }

    public void PMToMM()
    {
        Debug.Log("GO TO MAIN MENU");
        isEscPressed = false;
        isPaused = false;
        SceneManager.LoadScene(0);
        Time.timeScale = 1f;
    }

    public void Update()
    {
        if (isEscPressed == true)
        {
            if (isPaused == false)
            {
                GameToPM();
            }
            else if (isPaused == true)
            {
                PMToGame();
            }
        }
    }
}
