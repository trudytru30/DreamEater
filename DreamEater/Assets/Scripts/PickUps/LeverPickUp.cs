using UnityEngine;

public class LeverPickUp : MonoBehaviour
{
    [SerializeField] private GameObject message;
    [SerializeField] private GameObject palanca;

    private void Start()
    {
        palanca.SetActive(false);
        message.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(message);
        palanca.SetActive(true);
        Destroy(gameObject);
    }
}
