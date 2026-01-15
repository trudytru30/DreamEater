using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PalancaElevator : MonoBehaviour
{
    [Header("Configuraci�n Ascensor")]
    [SerializeField] private Elevator ascensor;
    [SerializeField] private int numeroDePisoAlQueLlamar;
    [SerializeField] private float retardoAccion = 0.5f;

    [Header("Animaci�n")]
    [SerializeField] private Animator animadorPalanca;
    [SerializeField] private string nombreTriggerAnim = "Activar";

    [Header("Interfaz de Usuario")]
    [SerializeField] private GameObject mensajeUI;

    private bool _jugadorEstaCerca = false;
    private bool _procesando = false; // Solo bloquea mientras dura la animaci�n actual

    private void Start()
    {
        if (mensajeUI != null) mensajeUI.SetActive(false);
    }

    private void Update()
    {
        // Ahora permitimos pulsar si el jugador est� cerca y no hay una acci�n en curso
        if (_jugadorEstaCerca && !_procesando && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ProcesarLlamada());
        }
    }

    private IEnumerator ProcesarLlamada()
    {
        _procesando = true; // Bloqueo temporal para no solapar animaciones

        if (mensajeUI != null) mensajeUI.SetActive(false);

        if (animadorPalanca != null)
        {
            animadorPalanca.SetTrigger(nombreTriggerAnim);
        }

        yield return new WaitForSeconds(retardoAccion);

        if (ascensor != null)
        {
            ascensor.IrAlPiso(numeroDePisoAlQueLlamar);
        }

        // ESPERA DE SEGURIDAD
        // Esperamos un poco antes de permitir otra pulsaci�n para que la palanca vuelva a su sitio
        yield return new WaitForSeconds(0.5f);

        _procesando = false; // �Aqu� liberamos el bloqueo!

        // Si el jugador sigue ah�, volvemos a mostrar el mensaje de "Pulsa E"
        if (_jugadorEstaCerca)
        {
            if (mensajeUI != null) mensajeUI.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _jugadorEstaCerca = true;
            if (!_procesando && mensajeUI != null) mensajeUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _jugadorEstaCerca = false;
            if (mensajeUI != null) mensajeUI.SetActive(false);
        }
    }
}
