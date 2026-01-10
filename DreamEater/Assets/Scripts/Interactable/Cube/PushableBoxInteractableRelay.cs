/*va en el HIJO con Trigger + Interactable*/

using UnityEngine;

[RequireComponent(typeof(Interactable), typeof(Collider))]
public class PushableBoxInteractableRelay : MonoBehaviour
{
    [Header("References")]
    [SerializeField] private PushableBox box; // Arrastra aquí el PushableBox del padre (o se auto-detecta)

    private Interactable _interactable;
    private Transform _currentPlayer; // player dentro del trigger
    private bool _isGrabbing;

    private void Awake()
    {
        _interactable = GetComponent<Interactable>();

        // Necesario para que PlayerController2 detecte este Interactable
        Collider col = GetComponent<Collider>();
        col.isTrigger = true;
    }

    private void Start()
    {
        // PlayerController2 solo asigna _currentInteractable si canInteract == true
        _interactable.SetCanInteract(true);

        if (box == null)
            box = GetComponentInParent<PushableBox>();
    }

    private void Update()
    {
        if (box == null) return;

        // Pulsación de interact del player => el player hace SetIsInteracting(true)
        if (_interactable.GetIsInteracting())
        {
            // Consumimos la pulsación para permitir otra pulsación después
            _interactable.SetIsInteracting(false);

            // Toggle: una pulsación inicia, otra termina
            if (!_isGrabbing)
            {
                if (_currentPlayer != null)
                {
                    box.Grab(_currentPlayer);
                    _isGrabbing = true;
                }
            }
            else
            {
                box.Release();
                _isGrabbing = false;
            }
        }

        // Si la caja se soltó por seguridad (breakDistance), sincroniza
        if (_isGrabbing && !box.IsHeld())
        {
            _isGrabbing = false;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        PlayerController2 pc = other.GetComponentInParent<PlayerController2>();
        if (pc != null)
        {
            _currentPlayer = pc.transform;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        PlayerController2 pc = other.GetComponentInParent<PlayerController2>();
        if (pc == null) return;

        // OJO: si estás agarrando, NO cortamos la interacción aquí.
        // (Se termina solo con otra pulsación, como has pedido.)
        if (_isGrabbing) return;

        if (_currentPlayer == pc.transform)
        {
            _currentPlayer = null;
        }
    }
}

