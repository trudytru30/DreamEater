using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Elevator : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] private float velocidad = 2.5f;
    [SerializeField] private List<float> pisosY;
    [SerializeField] private bool detenerSiJugadorSale = true;

    private Vector3 _targetActual;
    private bool _tieneOrden = false;
    private Transform _originalParent;
    private Rigidbody _rb; // Añadido para física suave

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        
        if (_rb) _rb.isKinematic = true;

        if (GetComponent<BoxCollider>()) GetComponent<BoxCollider>().isTrigger = true;
    }

    private void Start()
    {
        _targetActual = transform.position;
    }

    public void IrAlPiso(int indicePiso)
    {
        if (indicePiso >= 0 && indicePiso < pisosY.Count)
        {
            _targetActual = new Vector3(transform.position.x, pisosY[indicePiso], transform.position.z);
            _tieneOrden = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _originalParent = other.transform.parent;
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(_originalParent);
            if (detenerSiJugadorSale) _tieneOrden = false;
        }
    }

    private void FixedUpdate()
    {
        if (!_tieneOrden) return;
        
        Vector3 proximaPosicion = Vector3.MoveTowards(
            transform.position,
            _targetActual,
            velocidad * Time.fixedDeltaTime
        );

        if (_rb)
        {
            _rb.MovePosition(proximaPosicion);
        }
        else
        {
            transform.position = proximaPosicion;
        }

        if (Vector3.Distance(transform.position, _targetActual) < 0.01f)
        {
            transform.position = _targetActual;
            _tieneOrden = false;
        }
    }
}