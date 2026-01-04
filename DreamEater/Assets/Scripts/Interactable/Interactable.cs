/*
 Script base para todos los interactuables con el player
 */

using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private bool canInteract; //si el player PUEDE interactuar con ello
    private bool _isInteracting; //si el player ESTA interactuando con ello

    //Getters y setters
    public void SetCanInteract(bool _canInteract)
    {
        canInteract = _canInteract;
    }

    public bool GetCanInteract()
    {
        return canInteract;
    }

    public void SetIsInteracting(bool isInteracting)
    {
        _isInteracting = isInteracting;
    }

    public bool GetIsInteracting()
    {
        return _isInteracting;
    }
}
