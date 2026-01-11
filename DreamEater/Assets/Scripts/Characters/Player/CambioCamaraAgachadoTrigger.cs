using Unity.Cinemachine;
using UnityEngine;

public class CambioCamaraAgachadoTrigger : MonoBehaviour
{
    [Header("Cámara a Activar")]
    // Arrastraremos aquí tu cámara "Camera_Agachado"
    public CinemachineCamera camaraAgachado;

    [Header("Prioridades")]
    // Prioridad alta para que gane el control
    public int prioridadAlta = 20;
    // Prioridad baja para que lo pierda (debe ser menor que la de tu cámara principal)
    public int prioridadBaja = 0;

    private void OnTriggerEnter(Collider other)
    {
        // Verifica que sea el jugador quien entra (asegúrate de que tu Player tenga el tag "Player")
        if (other.CompareTag("Player"))
        {
            if (camaraAgachado != null)
            {
                // Subimos la prioridad para que esta cámara tome el control
                camaraAgachado.Priority = prioridadAlta;
            }
            Debug.Log("Metido");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        // Al salir el jugador...
        if (other.CompareTag("Player"))
        {
            if (camaraAgachado != null)
            {
                // Bajamos la prioridad para que el control vuelva a la cámara principal
                camaraAgachado.Priority = prioridadBaja;
            }
            Debug.Log("Salido");
        }
    }
}