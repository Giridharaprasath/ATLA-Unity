using System.Collections.Generic;
using UnityEngine;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.LowLevel;
using UnityEngine.InputSystem.Controls;

public class ControlHints : MonoBehaviour
{
    public bool isKM;
    public bool isXC;

    public Vector2 cursorPosition;
    public float cursorSpeed = 200f;

    public List<GameObject> kmCH = new List<GameObject>();
    public List<GameObject> xcCH = new List<GameObject>();

    public RectTransform curRect;
    public Vector2 halfScreen;
    public Vector2 curPos;
    public bool isPressed;
    public ButtonControl bC;

    private void Awake()
    {
        halfScreen = new Vector2(Screen.width, Screen.height) / 2;
        curPos = halfScreen;

        InputManager.Controls.ControlHints.CheckKM.performed += ctx => SetKM();
        InputManager.Controls.ControlHints.CheckXC.performed += ctx => SetXC();

        InputManager.Controls.UI.Move.performed += ctx => SetXCMove(ctx.ReadValue<Vector2>());

        InputManager.Controls.UI.Click.performed += ctx => isPressed = true;
        InputManager.Controls.UI.Click.canceled += ctx => isPressed = false;
    }

    private void SetKM()
    {
        if (!isKM)
        {
            Debug.Log("KEYBOARD IS ACTIVE");
            SetState(true, false);
            curRect.gameObject.SetActive(false);
        }
        Cursor.visible = true;
    }

    private void SetXC()
    {
        if (!isXC)
        {
            Debug.Log("CONTROLLER IS ACTIVE");
            SetState(false, true);
        }
        curRect.gameObject.SetActive(true);
        Cursor.visible = false;
    }

    private void SetState(bool km, bool xc)
    {
        isKM = km;
        isXC = xc;
        foreach (GameObject gO in kmCH) gO.SetActive(km);
        foreach (GameObject gO in xcCH) gO.SetActive(xc);
    }

    private void SetXCMove(Vector2 pos) => cursorPosition = pos;

    private void Update()
    {
        curPos += cursorPosition * Time.deltaTime * cursorSpeed;
        curPos.x = Mathf.Clamp(curPos.x, 0, Screen.width);
        curPos.y = Mathf.Clamp(curPos.y, 0, Screen.height);
        InputState.Change(Mouse.current.position, curPos);
        curRect.position = curPos;

        if (isXC)
        {
            Mouse.current.CopyState<MouseState>(out var mouseState);
            mouseState = mouseState.WithButton(MouseButton.Left, isPressed);
            InputState.Change(Mouse.current, mouseState);
        }
    }
}
