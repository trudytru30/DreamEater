using UnityEngine;

public class LeverPickUp : MonoBehaviour
{
    [SerializeField] private GameObject _message;
    [SerializeField] private GameObject _Palanca;

    private void Start()
    {
        _Palanca.SetActive(false);
        _message.SetActive(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        Destroy(_message);
        _Palanca.SetActive(true);
        Destroy(gameObject);
    }
}
