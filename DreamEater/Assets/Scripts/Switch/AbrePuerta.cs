using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class AbrePuerta : MonoBehaviour
{
    [SerializeField] Animator animatorPuertaIzquierda;
    [SerializeField] Animator animatorPuertaDerecha;
    [SerializeField] Animator animatorPalanca;

    private bool _estaCerca = false; // Variable para saber si el jugador est� en el �rea

    private void Update()
    {
        // Revisamos el input en Update, que corre todo el tiempo
        if (_estaCerca && Input.GetKeyDown(KeyCode.E))
        {
            activarPalanca();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _estaCerca = true;
            Debug.Log("Jugador en rango. Presiona E.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _estaCerca = false;
            Debug.Log("Jugador fuera de rango.");
        }
    }

    public void activarPalanca()
    {
        AudioManager.Instance.PlayPalanca();
        if (animatorPuertaIzquierda != null)
        {
            animatorPuertaIzquierda.SetTrigger("puerta");
        }
        if (animatorPuertaDerecha != null)
        {
            animatorPuertaDerecha.SetTrigger("puerta");
        }
        if (animatorPalanca != null)
        {
            animatorPalanca.SetTrigger("Activar2");
        }
    }
}