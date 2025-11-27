using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerInteraction : MonoBehaviour
{
    private PlayerActions _playerActions;
    private Dialogue _npcInRange;

    private void Awake()
    {
        _playerActions = new PlayerActions();
    }

    private void OnEnable()
    {
        _playerActions.Enable();
        _playerActions.Actions.Interact.performed += OnInteract;
    }

    private void OnDisable()
    {
        _playerActions.Actions.Interact.performed -= OnInteract;
        _playerActions.Disable();
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            _npcInRange = other.GetComponent<Dialogue>();
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("NPC"))
        {
            _npcInRange = null;
        }
    }
    
    private void OnInteract(InputAction.CallbackContext context)
    {
        if (_npcInRange != null)
        {
            _npcInRange.Interact();
        }
    }
}