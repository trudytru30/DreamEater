using Unity.Cinemachine;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AbrePuerta : MonoBehaviour
{
    [SerializeField] Animator _animatorPuertaIzquierda;
    [SerializeField] Animator _animatorPuertaDerecha;
    [SerializeField] Animator _animatorPalanca;

    private bool estaCerca = false; // Variable para saber si el jugador est� en el �rea

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

    public void activarPalanca()
    {
        AudioManager.Instance.PlayPalanca();
        if (_animatorPuertaIzquierda != null)
        {
            _animatorPuertaIzquierda.SetTrigger("puerta");
        }
        if (_animatorPuertaDerecha != null)
        {
            _animatorPuertaDerecha.SetTrigger("puerta");
        }
        if (_animatorPalanca != null)
        {
            _animatorPalanca.SetTrigger("Activar2");
        }
    }
}