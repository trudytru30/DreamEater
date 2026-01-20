using UnityEngine;

[RequireComponent (typeof(BoxCollider))]
public class PickUpNecesary : MonoBehaviour
{
    [SerializeField] private UIPickupCounter uiPickUp;
    [SerializeField] private GameObject adviseUI;

    private void OnTriggerEnter(Collider other)
    {
        if(uiPickUp.current == uiPickUp.totalRequired)
        {
            Destroy(gameObject);
        }
        else
        {
            adviseUI.SetActive(true);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        adviseUI.SetActive(false);
    }
}