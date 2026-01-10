using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AbrePuerta : MonoBehaviour
{
    [SerializeField] Animator _animatorPuertaIzquierda;
    [SerializeField] Animator _animataorPuertaDerecha;
    [SerializeField] Animator _animatorPalanca;

    private bool estaCerca = false; // Variable para saber si el jugador está en el área

    private void Update()
    {
        // Revisamos el input en Update, que corre todo el tiempo
        if (estaCerca && Input.GetKeyDown(KeyCode.E))
        {
            activarPalanca();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            estaCerca = true;
            Debug.Log("Jugador en rango. Presiona E.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            estaCerca = false;
            Debug.Log("Jugador fuera de rango.");
        }
    }

    void activarPalanca()
    {
        _animatorPuertaIzquierda.SetTrigger("puerta");
        _animataorPuertaDerecha.SetTrigger("puerta");
        _animatorPalanca.SetTrigger("Activar2");
    }
}