using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class ElevatorNonStop : MonoBehaviour
{
    [Header("Movement Configuration")]
    [SerializeField] private float speed = 2.5f;
    [SerializeField] private float minY;
    [SerializeField] private float maxY;
    [SerializeField] private float waitTime = 1f;

    private Vector3 _targetUp;
    private Vector3 _targetDown;
    private Vector3 _currentTarget;
    
    private float _pauseTimer = 0f;
    private bool _isWaiting = false;

    private void Awake()
    {
        if (GetComponent<BoxCollider>()) GetComponent<BoxCollider>().isTrigger = true;
    }

    private void Start()
    {
        _targetUp = new Vector3(transform.position.x, maxY, transform.position.z);
        _targetDown = new Vector3(transform.position.x, minY, transform.position.z);
        
        _currentTarget = (Vector3.Distance(transform.position, _targetUp) > 0.1f) ? _targetUp : _targetDown;
    }

    private void FixedUpdate()
    {
        if (_isWaiting)
        {
            _pauseTimer += Time.fixedDeltaTime;
            if (_pauseTimer >= waitTime)
            {
                _isWaiting = false;
                _pauseTimer = 0f;
                _currentTarget = (_currentTarget == _targetUp) ? _targetDown : _targetUp;
            }
            return;
        }

        transform.position = Vector3.MoveTowards(
            transform.position,
            _currentTarget,
            speed * Time.fixedDeltaTime
        );

        if (Vector3.Distance(transform.position, _currentTarget) < 0.001f)
        {
            transform.position = _currentTarget;
            _isWaiting = true;
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(transform, true);
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            other.transform.SetParent(null);
        }
    }
}