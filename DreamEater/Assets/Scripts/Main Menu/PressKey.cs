using UnityEngine;
using TMPro;
using UnityEngine.InputSystem;
using UnityEngine.InputSystem.Controls;

public class PressKey : MonoBehaviour
{
    public TextMeshProUGUI text;
    public float blinkSpeed = 1f;
    public GameObject mainMenuCanvas;

    private void OnEnable()
    {
        if (mainMenuCanvas != null)
            mainMenuCanvas.SetActive(false);
    }

    private void Update()
    {
        if (text != null)
        {
            Color c = text.color;
            c.a = 0.5f + 0.5f * Mathf.Sin(Time.time * blinkSpeed * Mathf.PI);
            text.color = c;
        }

        bool pressed = Keyboard.current.anyKey.wasPressedThisFrame;
        pressed |= Mouse.current.leftButton.wasPressedThisFrame;

        Gamepad pad = Gamepad.current;
        if (pad != null)
            pressed |= AnyGamepadButtonPressed(pad);

        if (pressed)
        {
            if (mainMenuCanvas != null)
            {
                mainMenuCanvas.SetActive(true);
                CursorManager.Instance.ShowCursor(true);
            }
                
            gameObject.SetActive(false);
        }
    }

    private bool AnyGamepadButtonPressed(Gamepad pad)
    {
        foreach (var control in pad.allControls)
        {
            if (control is ButtonControl button && button.wasPressedThisFrame)
                return true;
        }
        return false;
    }
}

