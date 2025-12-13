using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(BoxCollider))]
public class PalancaElevator : MonoBehaviour
{
    [SerializeField] private Elevator ascensor;
    [SerializeField] private float retardo = 1.5f;
    [SerializeField] private bool isPalancaDeSubir;

    private void Awake()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        Debug.Log("Palanca activada");

        if (isPalancaDeSubir)
            StartCoroutine(Subir());
        else
            StartCoroutine(Bajar());
    }

    private IEnumerator Subir()
    {
        yield return new WaitForSeconds(retardo);
        ascensor.LlamarArriba();
    }

    private IEnumerator Bajar()
    {
        yield return new WaitForSeconds(retardo);
        ascensor.LlamarAbajo();
    }
}

