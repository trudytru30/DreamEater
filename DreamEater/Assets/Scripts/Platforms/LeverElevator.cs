using System.Collections;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class LeverElevator : MonoBehaviour
{
    [Header("Elevator Configuration")]
    [SerializeField] private Elevator elevator;
    [SerializeField] private int floorNumberToCall;
    [SerializeField] private float actionDelay = 0.5f;

    [Header("Animation")]
    [SerializeField] private Animator leverAnimator;
    [SerializeField] private string animationTriggerName = "Activar";

    [Header("User Interface")]
    [SerializeField] private GameObject messageUI;

    private bool _playerIsNear = false;
    private bool _processing = false;

    private void Start()
    {
        if (messageUI != null) messageUI.SetActive(false);
    }

    private void Update()
    {
        if (_playerIsNear && !_processing && Input.GetKeyDown(KeyCode.E))
        {
            StartCoroutine(ProcessCall());
        }
    }

    private IEnumerator ProcessCall()
    {
        _processing = true;

        if (messageUI != null) messageUI.SetActive(false);

        if (leverAnimator != null)
        {
            leverAnimator.SetTrigger(animationTriggerName);
        }

        yield return new WaitForSeconds(actionDelay);

        if (elevator != null)
        {
            elevator.GoToFloor(floorNumberToCall);
        }

        yield return new WaitForSeconds(0.5f);

        _processing = false;

        if (_playerIsNear)
        {
            if (messageUI != null) messageUI.SetActive(true);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsNear = true;
            if (!_processing && messageUI != null) messageUI.SetActive(true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _playerIsNear = false;
            if (messageUI != null) messageUI.SetActive(false);
        }
    }
}