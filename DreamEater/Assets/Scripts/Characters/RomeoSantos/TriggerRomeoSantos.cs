using UnityEngine;

public class TriggerRomeoSantos : MonoBehaviour
{
    [SerializeField] GameObject _romeoSantos;
    [SerializeField] GameObject _particleSystem;

    private void OnTriggerEnter(Collider other)
    {
        // 1. Verificamos si es el jugador
        if (other.CompareTag("Player"))
        {
            // 2. Obtenemos el Rigidbody del objeto _romeoSantos
            Rigidbody rb = _romeoSantos.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.isKinematic = false;  // Desactivamos Kinematic
                rb.useGravity = true;    // Activamos Gravedad
            }

            // 3. Activamos las partículas
            if (_particleSystem != null)
            {
               _particleSystem.SetActive(true);
            }

            // Opcional: Desactivar el trigger para que no se repita
            // gameObject.SetActive(false); 
        }








}   }
