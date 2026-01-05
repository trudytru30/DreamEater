using UnityEngine;
using System.Collections;

[RequireComponent(typeof(BoxCollider))]
public class ElevatorNonStop : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] private float velocidad = 2.5f;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    [SerializeField] private float tiempoEspera = 1f;

    private Vector3 targetArriba;
    private Vector3 targetAbajo;
    private Vector3 targetActual;

    private void Awake()
    {
        // El trigger es necesario para detectar al jugador sin bloquear su paso
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void Start()
    {
        // Inicializamos los vectores de destino
        targetArriba = new Vector3(transform.position.x, maxY, transform.position.z);
        targetAbajo = new Vector3(transform.position.x, minY, transform.position.z);
        
        // Determinar el objetivo inicial (el más alejado de la posición actual)
        targetActual = (Vector3.Distance(transform.position, targetArriba) > 0.1f) ? targetArriba : targetAbajo;

        StartCoroutine(CicloMovimiento());
    }

    private IEnumerator CicloMovimiento()
    {
        while (true)
        {
            // Movimiento hacia el objetivo actual
            while (Vector3.Distance(transform.position, targetActual) > 0.001f)
            {
                transform.position = Vector3.MoveTowards(
                    transform.position,
                    targetActual,
                    velocidad * Time.deltaTime
                );
                yield return null; 
            }

            // Ajuste exacto de posición y pausa
            transform.position = targetActual;
            yield return new WaitForSeconds(tiempoEspera);

            // Alternar destino
            targetActual = (targetActual == targetArriba) ? targetAbajo : targetArriba;
        }
    }

    // --- Lógica de Player Parent ---

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Hacemos al jugador hijo de la plataforma para que se mueva con ella
            // 'true' mantiene la posición, rotación y escala global actual
            other.transform.SetParent(transform, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Quitamos el parentesco al salir
            other.transform.SetParent(null);
        }
    }
}