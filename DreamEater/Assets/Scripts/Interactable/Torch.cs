using System;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] private bool isActive;

    private void MakeInteractable()
    {
        gameObject.GetComponent<Interactable>().SetCanInteract(true);
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Interactable>().GetCanInteract() == false)
        {
            MakeInteractable();
        }
    }
}
