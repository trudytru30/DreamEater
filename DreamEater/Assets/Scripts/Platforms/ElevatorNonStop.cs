using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class ElevatorNonStop : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidad = 2.5f;
    [SerializeField] private float maxY;
    [SerializeField] private float minY;
    
    [SerializeField] private float tiempoEspera = 1f;

    private Vector3 targetArriba;
    private Vector3 targetAbajo;
    private Vector3 targetActual;

    private void Awake()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void Start()
    {
        // Definimos los puntos de destino basándonos en la posición inicial X y Z
        targetArriba = new Vector3(transform.position.x, maxY, transform.position.z);
        targetAbajo = new Vector3(transform.position.x, minY, transform.position.z);
        
        // Empezamos yendo hacia arriba
        targetActual = targetArriba;

        // Iniciamos el ciclo de movimiento automático
        StartCoroutine(CicloMovimiento());
    }

    private IEnumerator CicloMovimiento()
    {
        while (true) // Bucle infinito para que no pare
        {
            // Mientras no hayamos llegado al destino
            while (Vector3.Distance(transform.position, targetActual) > 0.001f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    targetActual,
                    velocidad * Time.deltaTime
                );
                yield return null; // Espera al siguiente frame
            }

            // Ajuste fino de posición al llegar
            transform.position = targetActual;

            // --- PAUSA DE UN SEGUNDO ---
            yield return new WaitForSeconds(tiempoEspera);

            // Cambiamos el objetivo al punto opuesto
            targetActual = (targetActual == targetArriba) ? targetAbajo : targetArriba;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // El jugador se mueve con la plataforma
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // El jugador deja de ser hijo de la plataforma
            other.transform.SetParent(null);
        }
    }
}