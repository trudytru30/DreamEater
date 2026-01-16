using UnityEngine;
using UnityEngine.Events;

public class DialogueController : MonoBehaviour
{
    public DialogueNode currentNode; // Nodo actual
    public DialogueUI ui; // Referencia a la UI 
    public UnityEvent onDialogueStart;
    public UnityEvent onDialogueEnd;
    public bool IsDialogueActive => _isDialogueActive;
    private bool _isDialogueActive;
    

    // Iniciar un diálogo desde un nodo inicial 
    public void StartDialogue(DialogueNode startNode) 
    {
        CursorManager.Instance.ShowCursor(true);
        if (_isDialogueActive) return; 
        _isDialogueActive = true; 
        TogglePlayerMovement(false); 

        // Resetear UI para evitar restos de diálogos anteriores 
        if (ui != null) ui.ResetUI(); 
        currentNode = startNode; 
        onDialogueStart?.Invoke(); 
        ShowNode(); 
    } 

    // Mostrar el nodo actual en la UI 
    private void ShowNode() 
    { 
        if (!currentNode) return; 
        
        if (ui) 
        { 
            ui.ShowLine(currentNode.speaker, currentNode.line, currentNode.choices, OnChoiceSelected); 
        } 
    }

    // Avanzar diálogo
    public void AdvanceDialogue()
    {
        if (!_isDialogueActive || !ui) return;
        ui.AdvanceDialogue();
    }
    
    // Elección del player 
    private void OnChoiceSelected(DialogueNode nextNode) 
    { 
        if (!nextNode) 
        { 
            EndDialogue();
            CursorManager.Instance.ShowCursor(false);
            return; 
        } 
        currentNode = nextNode; 
        ShowNode(); 
    } 

    // Terminar diálogo
    private void EndDialogue() 
    { 
        // Limpieza de la UI 
        if (ui) 
        { 
            ui.ResetUI(); 
            ui.Hide(); 
        } 
        _isDialogueActive = false; 
        TogglePlayerMovement(true); 

        // Limpiar estado del controlador 
        currentNode = null; 
        onDialogueEnd?.Invoke(); 
    } 

    // Función que permite al jugador moverse o no al estar en diálogo 
    private static void TogglePlayerMovement(bool canMove) 
    { 
        PlayerController2 player = FindFirstObjectByType<PlayerController2>(); 
        if (player) 
        { 
            player.enabled = canMove; 
        } 
    }
}