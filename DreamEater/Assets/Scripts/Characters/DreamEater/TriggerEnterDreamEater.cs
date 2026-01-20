using UnityEngine;

[RequireComponent (typeof(BoxCollider))]
public class TriggerEnterDreamEater : MonoBehaviour
{
    [SerializeField] GameObject dreamEater;
    
    private void OnTriggerEnter(Collider other)
    {
        dreamEater.SetActive(true); 
    }
    
    private void OnTriggerExit(Collider other)
    {
        dreamEater.SetActive(false);
    }
}
