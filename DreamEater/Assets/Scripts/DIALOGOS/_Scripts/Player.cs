using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private Transform interactionSource;
    [SerializeField] private float interactRange = 5f;

    private void Awake()
    {
        dialogueController = Object.FindFirstObjectByType<DialogueController>();
    }

    private void OnInteract()
    {
        // Si hay diálogo, avanzar en el
        if (dialogueController != null && dialogueController.IsDialogueActive)
        {
            dialogueController.AdvanceDialogue();
            return;
        }
        
        // Interacción normal si no hay diálogo
        Ray ray = new Ray(interactionSource.position, interactionSource.forward);
        if(Physics.Raycast(ray, out RaycastHit hit, interactRange))
            if(hit.collider.TryGetComponent(out IInteractable interactable))
                interactable.Interact();
    }
    
    private void OnDrawGizmosSelected()
    {
        if (interactionSource == null)
            return;

        Gizmos.color = Color.green;

        Vector3 start = interactionSource.position;
        Vector3 end = start + interactionSource.forward * interactRange;

        Gizmos.DrawLine(start, end);
        Gizmos.DrawWireSphere(end, 0.1f);
    }
}