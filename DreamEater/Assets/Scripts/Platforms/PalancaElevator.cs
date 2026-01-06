using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider))]
public class PalancaElevator : MonoBehaviour
{
    [SerializeField] private Elevator ascensor;
    [SerializeField] private float retardo = 0.5f;
    [SerializeField] private int numeroDePisoAlQueLlamar; // 0 para PB, 1 para Piso 1, etc.

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StopAllCoroutines(); // Evita errores si entras y sales rápido
            StartCoroutine(ActivarPalanca());
        }
    }

    private IEnumerator ActivarPalanca()
    {
        Debug.Log("Llamando al piso: " + numeroDePisoAlQueLlamar);
        yield return new WaitForSeconds(retardo);
        ascensor.IrAlPiso(numeroDePisoAlQueLlamar);
    }
}
