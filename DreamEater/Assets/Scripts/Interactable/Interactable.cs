using UnityEngine;

public class Interactable : MonoBehaviour
{
    [SerializeField] private bool canInteract;
    private bool isInteracting;

    public void SetCanInteract(bool _canInteract)
    {
        canInteract = _canInteract;
    }

    public bool GetCanInteract()
    {
        return canInteract;
    }

    public void SetIsInteracting(bool _isInteracting)
    {
        isInteracting = _isInteracting;
    }

    public bool GetIsInteracting()
    {
        return isInteracting;
    }
}
