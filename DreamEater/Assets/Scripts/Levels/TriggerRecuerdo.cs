using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TriggerRecuerdo : MonoBehaviour
{
    [Header("1. Partículas")]
    public GameObject particulasPrefab;
    public Transform puntoAparicion;

    [Header("2. Animaciones")]
    public float tiempoEsperaAnim = 2.0f;
    public Animator[] animadores;
    public string nombreDelTrigger = "Abrir"; // Mira el paso 2 abajo

    [Header("3. Autodestrucción")]
    public float tiempoParaAutodestruirse = 1.0f;

    private bool activado = false;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && !activado)
        {
            activado = true;

            // CONGELAR AL JUGADOR
            // Buscamos cualquier componente de movimiento. Cambia "MonoBehaviour" 
            // por el nombre de tu script de movimiento si quieres ser más específico.
            var movimiento = other.GetComponent<MonoBehaviour>();
            if (movimiento != null) movimiento.enabled = false;

            StartCoroutine(SecuenciaEvento());
        }
    }

    IEnumerator SecuenciaEvento()
    {
        // PASO 1: Partículas
        if (particulasPrefab != null)
        {
            Vector3 pos = puntoAparicion != null ? puntoAparicion.position : transform.position;
            Instantiate(particulasPrefab, pos, Quaternion.identity);
        }

        // PASO 2: Espera
        yield return new WaitForSeconds(tiempoEsperaAnim);

        // PASO 3: Activar Animadores
        foreach (Animator anim in animadores)
        {
            if (anim != null) anim.SetTrigger(nombreDelTrigger);
        }

        // PASO 4: Espera final y destrucción del Trigger
        yield return new WaitForSeconds(tiempoParaAutodestruirse);
        Destroy(gameObject);
    }
}