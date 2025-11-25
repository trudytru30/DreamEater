using System.Collections;
using UnityEngine;

public class LampFunction : MonoBehaviour
{
    [Header("Luces")]
    public GameObject luzCiclica;     // Luz que hace el ciclo
    public GameObject luzEstatica;    // Luz fija encendida

    [Header("Tiempos")]
    public float tiempoEncendida = 2f;
    public float tiempoParpadeo = 3f;
    public float tiempoApagada = 3f;

    [Header("Parpadeo")]
    public float intervaloParpadeo = 0.1f; // Menor = más rápido

    private void Start()
    {
        // Activar la luz estática
        if (luzEstatica != null)
            luzEstatica.SetActive(true);

        // Empezar el ciclo
        if (luzCiclica != null)
            StartCoroutine(Ciclo());
    }

    IEnumerator Ciclo()
    {
        while (true)
        {
            // 1. Encendida
            luzCiclica.SetActive(true);
            yield return new WaitForSeconds(tiempoEncendida);

            // 2. Parpadeo
            float t = 0f;
            while (t < tiempoParpadeo)
            {
                luzCiclica.SetActive(!luzCiclica.activeSelf);
                yield return new WaitForSeconds(intervaloParpadeo);
                t += intervaloParpadeo;
            }

            // Dejar apagada
            luzCiclica.SetActive(false);

            // 3. Apagada
            yield return new WaitForSeconds(tiempoApagada);
        }
    }
}