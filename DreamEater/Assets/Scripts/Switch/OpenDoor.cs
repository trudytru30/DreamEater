using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class OpenDoor : MonoBehaviour
{
    [SerializeField] private Animator leftDoorAnimator;
    [SerializeField] private Animator rightDoorAnimator;
    [SerializeField] private Animator leverAnimator;

    private bool _isNear = false;

    private void Update()
    {
        if (_isNear && Input.GetKeyDown(KeyCode.E))
        {
            ActivateLever();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isNear = true;
            Debug.Log("Jugador en rango. Presiona E.");
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _isNear = false;
            Debug.Log("Jugador fuera de rango.");
        }
    }

    public void ActivateLever()
    {
        AudioManager.Instance.PlayLever();
        if (leftDoorAnimator != null)
        {
            leftDoorAnimator.SetTrigger("puerta");
        }
        if (rightDoorAnimator != null)
        {
            rightDoorAnimator.SetTrigger("puerta");
        }
        if (leverAnimator != null)
        {
            leverAnimator.SetTrigger("Activar2");
        }
    }
}