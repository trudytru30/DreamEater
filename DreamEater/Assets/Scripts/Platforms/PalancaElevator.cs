using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider))]
public class PalancaElevator : MonoBehaviour
{
    [Header("Configuración Ascensor")]
    [SerializeField] private Elevator ascensor;
    [SerializeField] private int numeroDePisoAlQueLlamar;
    [SerializeField] private float retardoAccion = 0.5f;

    [Header("Animación")]
    [SerializeField] private Animator animadorPalanca;
    [SerializeField] private string nombreTriggerAnim = "Activar";

    [Header("Interfaz de Usuario")]
    [SerializeField] private GameObject mensajeUI;

    private bool jugadorEstaCerca = false;
    private bool procesando = false; // Solo bloquea mientras dura la animación actual

    private void Start()
    {
        if (mensajeUI != null) mensajeUI.SetActive(false);
    }

    private void Update()
    {
        // Ahora permitimos pulsar si el jugador está cerca y no hay una acción en curso
        if (jugadorEstaCerca && !procesando && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ProcesarLlamada());
        }
    }

    private IEnumerator ProcesarLlamada()
    {
        procesando = true; // Bloqueo temporal para no solapar animaciones

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
        // Esperamos un poco antes de permitir otra pulsación para que la palanca vuelva a su sitio
        yield return new WaitForSeconds(0.5f);

        procesando = false; // ¡Aquí liberamos el bloqueo!

        // Si el jugador sigue ahí, volvemos a mostrar el mensaje de "Pulsa E"
        if (jugadorEstaCerca)
        {
            if (mensajeUI != null) mensajeUI.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEstaCerca = true;
            if (!procesando && mensajeUI != null) mensajeUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            jugadorEstaCerca = false;
            if (mensajeUI != null) mensajeUI.SetActive(false);
        }
    }
}
