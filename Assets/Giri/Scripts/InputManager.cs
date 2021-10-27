using UnityEngine;

public class InputManager : MonoBehaviour
{
    private static AllControls controls;
    public static AllControls Controls
    {
        get
        {
            if (controls != null) return controls;
            return controls = new AllControls();
        }
    }

    private void Awake()
    {
        if (controls != null) return;
        controls = new AllControls();
    }

    private void OnEnable() => Controls.Enable();
    private void OnDisable() => Controls.Disable();
    private void OnDestroy() => controls = null;

    public static void DisablePlayer()
    {
        Controls.asset.FindActionMap("Player").Disable();
        Controls.asset.FindActionMap("FireBending").Disable();
        Controls.asset.FindActionMap("EarthBending").Disable();
    }

    public static void EnablePlayer()
    {
        Controls.asset.FindActionMap("Player").Enable();
        Controls.asset.FindActionMap("FireBending").Enable();
        Controls.asset.FindActionMap("EarthBending").Enable();
    }
}
