using System;
using System.Collections;
using System.Timers;
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
        StartCoroutine(Timer());
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

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(blinkingTime);
    }
}