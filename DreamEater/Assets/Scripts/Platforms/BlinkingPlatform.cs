using System.Collections;
using UnityEngine;

public class BlinkingPlatform : MonoBehaviour
{
    [SerializeField] private bool startsActive;
    [SerializeField] private float blinkingTime;
    private bool isActive;

    private void Start()
    {
        isActive = startsActive;
        this.gameObject.SetActive(isActive);
    }

    private void Update()
    {
        Blink();
        StartCoroutine(TimeTilBlink());
    }

    private void Blink()
    {
        if (isActive)
        {
            isActive = false;
            this.gameObject.SetActive(isActive);
        }
        else if (!isActive)
        {
            isActive = true;
            this.gameObject.SetActive(isActive);
        }
    }

    IEnumerator TimeTilBlink()
    {
        yield return new WaitForSeconds(blinkingTime);
    }
}