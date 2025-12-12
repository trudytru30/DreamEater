using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class PalancaElevator : MonoBehaviour
{
    [SerializeField] private GameObject elevador;
    [SerializeField] private float velocidad = 3f;
    [SerializeField] private float retardo = 3f;

    [Header("Configura si esta palanca hace SUBIR")]
    [SerializeField] private bool isPalancaDeSubir;

    [Header("Limites del elevador")]
    [SerializeField] private float minY; // Punto más bajo
    [SerializeField] private float maxY; // Punto más alto

    private Vector3 targetArriba;
    private Vector3 targetAbajo;

    private static bool moviendo = false;   // ambas palancas comparten estado
    private static Vector3 targetActual;     // destino actual del elevador

    private void Start()
    {
        // Creamos los targets SOLO una vez
        targetArriba = new Vector3(
            elevador.transform.position.x,
            maxY,
            elevador.transform.position.z
        );

        targetAbajo = new Vector3(
            elevador.transform.position.x,
            minY,
            elevador.transform.position.z
        );
    }

    private void OnTriggerEnter(Collider other)
    {
        if (!other.CompareTag("Player")) return;

        // Si esta palanca es la de subir…
        if (isPalancaDeSubir)
            StartCoroutine(OrdenSubir());
        else
            StartCoroutine(OrdenBajar());
    }

    private IEnumerator OrdenSubir()
    {
        yield return new WaitForSeconds(retardo);

        targetActual = targetArriba;
        moviendo = true;
    }

    private IEnumerator OrdenBajar()
    {
        yield return new WaitForSeconds(retardo);

        targetActual = targetAbajo;
        moviendo = true;
    }

    private void Update()
    {
        if (!moviendo) return;

        elevador.transform.position = Vector3.MoveTowards(
            elevador.transform.position,
            targetActual,
            velocidad * Time.deltaTime
        );

        // Cuando llegue, se detiene limpio
        if (Vector3.Distance(elevador.transform.position, targetActual) < 0.01f)
        {
            elevador.transform.position = targetActual;
            moviendo = false;
        }
    }
}
