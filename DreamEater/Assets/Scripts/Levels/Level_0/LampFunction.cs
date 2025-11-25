using System.Collections;
using UnityEngine;

public class LampFunction : MonoBehaviour
{
    [Header("Luces")]
    public GameObject luzCiclica;     // Luz que hace el ciclo
    public GameObject luzEstatica;    // Luz fija encendida

    [Header("Audio")]
    public AudioSource audioSource;   // Componente de audio
    public AudioClip sonidoParpadeo;  // Sonido al encender/apagar

    [Header("Tiempos")]
    public float tiempoEncendida = 2f;
    public float tiempoParpadeo = 3f;
    public float tiempoApagada = 3f;

    [Header("Parpadeo")]
    public float intervaloParpadeo = 0.1f; // menor = más rápido

    private void Start()
    {
        // Luz estática encendida
        if (luzEstatica != null)
            luzEstatica.SetActive(true);

        // Iniciar ciclo
        if (luzCiclica != null)
            StartCoroutine(Ciclo());
    }

    IEnumerator Ciclo()
    {
        while (true)
        {
            // 1. Encendido
            EncenderLuz();
            yield return new WaitForSeconds(tiempoEncendida);

            // 2. Parpadeo
            float t = 0f;
            while (t < tiempoParpadeo)
            {
                AlternarLuz();
                yield return new WaitForSeconds(intervaloParpadeo);
                t += intervaloParpadeo;
            }

            // Asegurar que queda apagada
            ApagarLuz();

            // 3. Tiempo apagada
            yield return new WaitForSeconds(tiempoApagada);
        }
    }

    // --- FUNCIONES AUXILIARES ---

    void EncenderLuz()
    {
        luzCiclica.SetActive(true);
        ReproducirSonido();
    }

    void ApagarLuz()
    {
        luzCiclica.SetActive(false);
        ReproducirSonido();
    }

    void AlternarLuz()
    {
        luzCiclica.SetActive(!luzCiclica.activeSelf);
        ReproducirSonido();
    }

    void ReproducirSonido()
    {
        if (audioSource != null && sonidoParpadeo != null)
            audioSource.PlayOneShot(sonidoParpadeo);
    }
}

