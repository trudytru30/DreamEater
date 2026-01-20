using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class Elevator : MonoBehaviour
{
    [Header("Movement Configuration")]
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private List<float> floorsY;
    [SerializeField] private bool stopIfPlayerLeaves = true;

    private Vector3 _currentTarget;
    private bool _hasOrder = false;
    private Transform _originalParent;
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        
        if (_rb) _rb.isKinematic = true;

        if (GetComponent<BoxCollider>()) GetComponent<BoxCollider>().isTrigger = true;
    }

    private void Start()
    {
        _currentTarget = transform.position;
    }

    public void GoToFloor(int floorIndex)
    {
        if (floorIndex >= 0 && floorIndex < floorsY.Count)
        {
            _currentTarget = new Vector3(transform.position.x, floorsY[floorIndex], transform.position.z);
            _hasOrder = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _originalParent = other.transform.parent;
            other.transform.SetParent(transform);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(_originalParent);
            if (stopIfPlayerLeaves) _hasOrder = false;
        }
    }

    private void FixedUpdate()
    {
        if (!_hasOrder) return;
        
        Vector3 nextPosition = Vector3.MoveTowards(
            transform.position,
            _currentTarget,
            speed * Time.fixedDeltaTime
        );

        if (_rb)
        {
            _rb.MovePosition(nextPosition);
        }
        else
        {
            transform.position = nextPosition;
        }

        if (Vector3.Distance(transform.position, _currentTarget) < 0.01f)
        {
            transform.position = _currentTarget;
            _hasOrder = false;
        }
    }
}