using System.Collections;
using UnityEngine;

public class LampFunction : MonoBehaviour
{
    [Header("Lights")]
    public GameObject cyclicLight;
    public GameObject staticLight;

    [Header("Audio")]
    public AudioSource audioSource;
    public AudioClip flickerSound;

    [Header("Times")]
    public float onTime = 2f;
    public float flickerTime = 3f;
    public float offTime = 3f;

    [Header("Flicker")]
    public float flickerInterval = 0.1f;

    private void Start()
    {
        if (staticLight != null)
            staticLight.SetActive(true);

        if (cyclicLight != null)
            StartCoroutine(Cycle());
    }

    private IEnumerator Cycle()
    {
        while (true)
        {
            // 1. Encendido
            TurnOnLight();
            yield return new WaitForSeconds(onTime);

            // 2. Parpadeo
            float t = 0f;
            while (t < flickerTime)
            {
                ToggleLight();
                yield return new WaitForSeconds(flickerInterval);
                t += flickerInterval;
            }

            // Asegurar que queda apagada
            TurnOffLight();

            // 3. Tiempo apagada
            yield return new WaitForSeconds(offTime);
        }
    }

    // --- FUNCIONES AUXILIARES ---

    private void TurnOnLight()
    {
        cyclicLight.SetActive(true);
        PlaySound();
    }

    private void TurnOffLight()
    {
        cyclicLight.SetActive(false);
        PlaySound();
    }

    private void ToggleLight()
    {
        cyclicLight.SetActive(!cyclicLight.activeSelf);
        PlaySound();
    }

    private void PlaySound()
    {
        if (audioSource != null && flickerSound != null)
            audioSource.PlayOneShot(flickerSound);
    }
}