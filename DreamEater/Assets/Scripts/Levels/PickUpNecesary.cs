using UnityEngine;

[RequireComponent (typeof(BoxCollider))]
public class PickUpNecesary : MonoBehaviour
{
    [SerializeField] UIPickupCounter uipickUp;
    [SerializeField] GameObject _adviseUI;



    private void OnTriggerEnter(Collider other)
    {
        if(uipickUp._current == uipickUp.totalRequired)
        {
            Destroy(gameObject);
        }
        else
        {
            _adviseUI.SetActive(true);
        }

    }
    private void OnTriggerExit(Collider other)
    {
        _adviseUI.SetActive(false);
    }
}
