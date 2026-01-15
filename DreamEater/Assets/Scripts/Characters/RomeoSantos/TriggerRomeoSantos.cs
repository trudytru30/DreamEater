using UnityEngine;

public class TriggerRomeoSantos : MonoBehaviour
{
    [SerializeField] GameObject romeoSantos;
    [SerializeField] GameObject particleSystem;

    private void OnTriggerEnter(Collider other)
    {
        // 1. Verificamos si es el jugador
        if (other.CompareTag("Player"))
        {
            // 2. Obtenemos el Rigidbody del objeto _romeoSantos
            Rigidbody rb = romeoSantos.GetComponent<Rigidbody>();

            if (rb != null)
            {
                rb.isKinematic = false; // Desactivamos Kinematic
                rb.useGravity = true; // Activamos Gravedad
            }

            // 3. Activamos las part�culas
            if (particleSystem != null)
            {
                particleSystem.SetActive(true);
            }

            // Opcional: Desactivar el trigger para que no se repita
            // gameObject.SetActive(false); 
        }
    }
}
