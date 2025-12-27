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
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void Start()
    {
        targetArriba = new Vector3(transform.position.x, maxY, transform.position.z);
        targetAbajo = new Vector3(transform.position.x, minY, transform.position.z);
        targetActual = transform.position;
    }

    public void LlamarArriba()
    {
        targetActual = targetArriba;
        tieneOrden = true;
    }

    public void LlamarAbajo()
    {
        targetActual = targetAbajo;
        tieneOrden = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEncima = true;
            // Usamos true para que mantenga su posición en el mundo al hacerse hijo
            other.transform.SetParent(transform, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEncima = false;
            other.transform.SetParent(null);
            // IMPORTANTE: Al salir, asegúrate de que el objeto no herede escalas raras
            DontDestroyOnLoad(other.gameObject); // Opcional, dependiendo de tu setup
        }
    }

    private void FixedUpdate() // Cambiamos Update por FixedUpdate para físicas
    {
        if (!tieneOrden) return;

        // Movemos el ascensor
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
