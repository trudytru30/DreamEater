using UnityEngine;

public class NPCDialogue : MonoBehaviour, IInteractableDialog
{
    [SerializeField] private DialogueController dialogueController;
    [SerializeField] private DialogueNode firstDialogue;
    [SerializeField] private DialogueNode repeatDialogue;

    private bool _hasTalked;
    
    public void Interact()
    {
        if (!_hasTalked)
        {
            dialogueController.StartDialogue(firstDialogue);
            _hasTalked = true;
        }
        else
        {
            dialogueController.StartDialogue(repeatDialogue);
        }
    }
}