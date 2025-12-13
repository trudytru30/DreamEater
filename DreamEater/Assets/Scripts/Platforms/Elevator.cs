using UnityEngine;





[RequireComponent(typeof(BoxCollider))]
public class Elevator : MonoBehaviour
{
    [Header("Movimiento")]
    [SerializeField] private float velocidad = 2.5f;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;

    private Vector3 targetArriba;
    private Vector3 targetAbajo;
    private Vector3 targetActual;

    private bool jugadorEncima = false;
    private bool tieneOrden = false;

    private void Awake()
    {
        // Aseguramos trigger
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void Start()
    {
        targetArriba = new Vector3(transform.position.x, maxY, transform.position.z);
        targetAbajo = new Vector3(transform.position.x, minY, transform.position.z);

        targetActual = transform.position;
    }

    // 🔹 Llamadas desde palancas
    public void LlamarArriba()
    {
        targetActual = targetArriba;
        tieneOrden = true;
        Debug.Log("Ascensor: orden SUBIR");
    }

    public void LlamarAbajo()
    {
        targetActual = targetAbajo;
        tieneOrden = true;
        Debug.Log("Ascensor: orden BAJAR");
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        jugadorEncima = true;
        Debug.Log("Jugador encima del ascensor");

        // Para que el jugador suba con el ascensor
        other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        jugadorEncima = false;
        other.transform.SetParent(null);
    }

    private void Update()
    {
        if (!tieneOrden || !jugadorEncima) return;

        transform.position = Vector3.MoveTowards(
            transform.position,
            targetActual,
            velocidad * Time.deltaTime
        );

        if (Vector3.Distance(transform.position, targetActual) < 0.01f)
        {
            transform.position = targetActual;
            tieneOrden = false;
            Debug.Log("Ascensor: destino alcanzado");
        }
    }
}
