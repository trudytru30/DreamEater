using System.Collections.Generic;
using UnityEngine;





[RequireComponent(typeof(BoxCollider))]
public class Elevator : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] private float velocidad = 2.5f;
    [SerializeField] private List<float> pisosY; // Lista de alturas (Piso 0, Piso 1, etc.)
    [SerializeField] private bool detenerSiJugadorSale = true;

    private Vector3 targetActual;
    private bool tieneOrden = false;
    private bool jugadorEncima = false;

    // Para no perder la jerarquía original del player
    private Transform originalParent;

    private void Awake()
    {
        // Aseguramos que el collider sea trigger
        if (GetComponent<BoxCollider>()) GetComponent<BoxCollider>().isTrigger = true;
    }

    private void Start()
    {
        targetActual = transform.position;
    }

    // Método universal para llamar a cualquier piso
    public void IrAlPiso(int indicePiso)
    {
        if (indicePiso >= 0 && indicePiso < pisosY.Count)
        {
            targetActual = new Vector3(transform.position.x, pisosY[indicePiso], transform.position.z);
            tieneOrden = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEncima = true;
            // Guardamos el padre original antes de cambiarlo
            originalParent = other.transform.parent;
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEncima = false;
            // Devolvemos al jugador a su padre original (no a null)
            other.transform.SetParent(originalParent);

            // Si quieres que se pare al salir:
            if (detenerSiJugadorSale)
            {
                tieneOrden = false;
            }
        }
    }

    private void FixedUpdate()
    {
        if (!tieneOrden) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetActual,
            velocidad * Time.fixedDeltaTime
        );

        if (Vector3.Distance(transform.position, targetActual) < 0.001f)
        {
            transform.position = targetActual;
            tieneOrden = false;
        }
    }
}