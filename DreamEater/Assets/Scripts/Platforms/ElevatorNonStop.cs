using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ElevatorNonStop : MonoBehaviour
{
    [Header("Configuración de Movimiento")]
    [SerializeField] private float velocidad = 2.5f;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    [SerializeField] private float tiempoEspera = 1f;

    private Vector3 _targetArriba;
    private Vector3 _targetAbajo;
    private Vector3 _targetActual;
    
    private float _cronometroPausa = 0f;
    private bool _estaEsperando = false;

    private void Awake()
    {
        if (GetComponent<BoxCollider>()) GetComponent<BoxCollider>().isTrigger = true;
    }

    private void Start()
    {
        _targetArriba = new Vector3(transform.position.x, maxY, transform.position.z);
        _targetAbajo = new Vector3(transform.position.x, minY, transform.position.z);
        
        // Empezamos yendo hacia el punto más lejano
        _targetActual = (Vector3.Distance(transform.position, _targetArriba) > 0.1f) ? _targetArriba : _targetAbajo;
    }

    private void FixedUpdate()
    {
        // Lógica de pausa
        if (_estaEsperando)
        {
            _cronometroPausa += Time.fixedDeltaTime;
            if (_cronometroPausa >= tiempoEspera)
            {
                _estaEsperando = false;
                _cronometroPausa = 0f;
                // Cambiamos el destino al llegar al final de la espera
                _targetActual = (_targetActual == _targetArriba) ? _targetAbajo : _targetArriba;
            }
            return; // No se mueve mientras espera
        }

        // Movimiento suave compatible con la física del Player
        transform.position = Vector3.MoveTowards(
            transform.position,
            _targetActual,
            velocidad * Time.fixedDeltaTime
        );

        // Si llega al destino, activa la pausa
        if (Vector3.Distance(transform.position, _targetActual) < 0.001f)
        {
            transform.position = _targetActual;
            _estaEsperando = true;
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