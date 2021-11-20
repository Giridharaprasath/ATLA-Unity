using UnityEngine;
using Mirror;

public class GamePlayerManager : NetworkBehaviour
{
    public static GamePlayerManager instance;

    private void Awake()
    {
        if (instance == null) instance = this;
    }
}
