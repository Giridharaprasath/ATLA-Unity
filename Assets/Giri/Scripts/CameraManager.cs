using UnityEngine;

public class CameraManager : MonoBehaviour
{
    public static CameraManager instance;

    public Animator animator;

    public bool canStartGame;

    private void Awake()
    {
        if (instance == null) instance = this;  
        animator = GetComponent<Animator>();
    }

    public void StartAnim() => animator.SetBool("StartGame", true);

    public void StartGame() => MainMenuManager.instance.HostGame();
}
