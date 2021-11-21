using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// Input Manager
public class IM : MonoBehaviour
{
    private static SampleControls controls;
    public static SampleControls Controls
    {
        get
        {
            if (controls != null) { return controls; }
            return controls = new SampleControls();
        }
    }

    private void Awake()
    {
        if (controls != null) { return; }
        controls = new SampleControls();
    }

    private void OnEnable() => Controls.Enable();
    private void OnDisable() => Controls.Disable();
    private void OnDestroy() => controls = null;
}
