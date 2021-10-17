using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.UI;

// Controls Hint Script
public class CHS : MonoBehaviour
{
    public bool isKMA;
    public bool isCA;

    public Vector2 cursorPos;
    public float cursorSpeed;

    [SerializeField]
    private List<GameObject> KMCH = new List<GameObject>();

    [SerializeField]
    private List<GameObject> CCH = new List<GameObject>();

    public RectTransform _curP;
    public Vector2 _halfS;
    public Vector2 _curPos;
    public bool isPressed;
    public ButtonControl bC;

    private void Awake() 
    {
        IM.Controls.ControlsHint.CheckKM.performed += ctx => SetKMA();    

        IM.Controls.ControlsHint.CheckJ.performed += ctx => SetCA();

        IM.Controls.UI.Move.performed += ctx => SetCCMove(ctx.ReadValue<Vector2>());
        // // IM.Controls.UI.Move.canceled += ctx => ResetCCMove();

        IM.Controls.UI.Click.performed += ctx => isPressed = true;
        IM.Controls.UI.Click.canceled += ctx => isPressed = false;

        _halfS = new Vector2(Screen.width, Screen.height) / 2;
        _curPos = _halfS;

        // Debug.Log(IM.Controls.ControlsHint.CheckJ.GetBindingDisplayString(0, out var deviceLayoutNameA, out var controlPathA));
        // Debug.Log(deviceLayoutNameA);
        // Debug.Log(controlPathA);
    }

    private void SetKMA()
    {
        if (!isKMA)
        {
            Debug.Log("KEYBOARD IS ACTIVE");
            SetState(true, false);
            _curP.gameObject.SetActive(false);
            // _curPos = _halfS;
        }
        Cursor.visible = true;
    }

    private void SetCA()
    {
        if (!isCA)
        {
            Debug.Log("CONTROLLER IS ACTIVE");
            SetState(false, true);
        }
        _curP.gameObject.SetActive(true);
        Cursor.visible = false;
    }

    private void SetState(bool k, bool c)
    {
        isKMA = k;
        isCA = c;
        foreach (GameObject kmch in KMCH)
        {
            kmch.SetActive(k);
        }
        foreach (GameObject cch in CCH)
        {
            cch.SetActive(c);
        }
    }

    private void SetCCMove(Vector2 pos)
    {
        cursorPos = pos;
    }

    private void Update()
    {
        _curPos += cursorPos * Time.deltaTime * cursorSpeed;
        _curPos.x = Mathf.Clamp(_curPos.x, 0, Screen.width);
        _curPos.y = Mathf.Clamp(_curPos.y, 0, Screen.height);
        InputState.Change(Mouse.current.position, _curPos);
        _curP.position = _curPos;

        if (isCA)
        {
            Mouse.current.CopyState<MouseState>(out var mouseState);
            mouseState = mouseState.WithButton(MouseButton.Left, isPressed);
            InputState.Change(Mouse.current, mouseState);
        }
    }
}
