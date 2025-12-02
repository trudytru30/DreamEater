using UnityEngine;
using TMPro;

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

        if (Input.anyKey)
        {
            if (mainMenuCanvas != null)
                mainMenuCanvas.SetActive(true);
            gameObject.SetActive(false);
        }
    }
}
