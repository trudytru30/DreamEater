using UnityEngine;

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
    
    private float cronometroPausa = 0f;
    private bool estaEsperando = false;

    private void Awake()
    {
        if (GetComponent<BoxCollider>()) GetComponent<BoxCollider>().isTrigger = true;
    }

    private void Start()
    {
        targetArriba = new Vector3(transform.position.x, maxY, transform.position.z);
        targetAbajo = new Vector3(transform.position.x, minY, transform.position.z);
        
        // Empezamos yendo hacia el punto más lejano
        targetActual = (Vector3.Distance(transform.position, targetArriba) > 0.1f) ? targetArriba : targetAbajo;
    }

    private void FixedUpdate()
    {
        // Lógica de pausa
        if (estaEsperando)
        {
            cronometroPausa += Time.fixedDeltaTime;
            if (cronometroPausa >= tiempoEspera)
            {
                estaEsperando = false;
                cronometroPausa = 0f;
                // Cambiamos el destino al llegar al final de la espera
                targetActual = (targetActual == targetArriba) ? targetAbajo : targetArriba;
            }
            return; // No se mueve mientras espera
        }

        // Movimiento suave compatible con la física del Player
        transform.position = Vector3.MoveTowards(
            transform.position,
            targetActual,
            velocidad * Time.fixedDeltaTime
        );

        // Si llega al destino, activa la pausa
        if (Vector3.Distance(transform.position, targetActual) < 0.001f)
        {
            transform.position = targetActual;
            estaEsperando = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            // Mantenemos la jerarquía global para libertad de movimiento
            other.transform.SetParent(transform, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}