using UnityEngine;


[RequireComponent (typeof(BoxCollider))]
public class TriggerEntradaDreamEater : MonoBehaviour
{
    [SerializeField] GameObject _dreamEater;


    private void OnTriggerEnter(Collider other)
    {
        _dreamEater.SetActive(true); 
    }
    private void OnTriggerExit(Collider other)
    {
        _dreamEater.SetActive(false);
    }

}
