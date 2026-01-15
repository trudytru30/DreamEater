using UnityEngine;

[RequireComponent (typeof(BoxCollider))]
public class PickUpNecesary : MonoBehaviour
{
    [SerializeField] UIPickupCounter UIPickUp;
    [SerializeField] GameObject adviseUI;

    private void OnTriggerEnter(Collider other)
    {
        if(UIPickUp.current == UIPickUp.totalRequired)
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
