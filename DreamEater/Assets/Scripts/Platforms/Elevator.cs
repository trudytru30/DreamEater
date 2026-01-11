using System.Collections.Generic;
using UnityEngine;





[RequireComponent(typeof(BoxCollider))]
public class Elevator : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] private float velocidad = 2.5f;
    [SerializeField] private List<float> pisosY;
    [SerializeField] private bool detenerSiJugadorSale = true;

    private Vector3 targetActual;
    private bool tieneOrden = false;
    private Transform originalParent;
    private Rigidbody rb; // Añadido para física suave

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
        
        if (rb) rb.isKinematic = true;

        if (GetComponent<BoxCollider>()) GetComponent<BoxCollider>().isTrigger = true;
    }

    private void Start()
    {
        targetActual = transform.position;
    }

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
            originalParent = other.transform.parent;
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(originalParent);
            if (detenerSiJugadorSale) tieneOrden = false;
        }
    }

    private void FixedUpdate()
    {
        if (!tieneOrden) return;

        
        Vector3 proximaPosicion = Vector3.MoveTowards(
            transform.position,
            targetActual,
            velocidad * Time.fixedDeltaTime
        );

        if (rb)
        {
            rb.MovePosition(proximaPosicion);
        }
        else
        {
            transform.position = proximaPosicion;
        }

        if (Vector3.Distance(transform.position, targetActual) < 0.01f)
        {
            transform.position = targetActual;
            tieneOrden = false;
        }
    }
}