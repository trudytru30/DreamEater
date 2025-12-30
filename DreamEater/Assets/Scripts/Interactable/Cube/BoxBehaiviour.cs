using UnityEngine;

public class BoxBehaviour : MonoBehaviour, IGrabable
{
    
    private Rigidbody _rb;

    [Header("Pull and Push")] [SerializeField]
    private bool canBeGrabbed = false;
    [SerializeField] private Transform _grabberTransform = null;
    private bool _isHeld = false;
    [SerializeField] private float offsetX = 0.7f;
    [SerializeField] private float offsetY = 0;

    [Header("Moving(by itself)")]
    [SerializeField] protected float moveSpeed;
    [SerializeField] protected bool isMoving = false;
    protected bool moveRight = false;
    protected Vector3 startPosition;
    protected Vector3 moveDirection;

    protected Rigidbody rb {
        get { return _rb; }
        set { _rb = value; }
    }
    void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }
    void FixedUpdate()
    {
        if (!_isHeld || !canBeGrabbed || !_grabberTransform) return;
        UpdateHoldPosition(_grabberTransform.position + _grabberTransform.forward * offsetX + _grabberTransform.up * offsetY, _grabberTransform.rotation);
    }

    public bool CanBeGrabbed()
    {
        return canBeGrabbed;
    }

    public void Grab(Transform grabber)
    {
        _grabberTransform = grabber;
        _isHeld = true;
        _rb.isKinematic = true;
        _rb.useGravity = false;

        Debug.Log("HAVE BEEN GRABBED");
    }

    public void Release()
    {
        _grabberTransform = null;
        _isHeld = false;
        
        _rb.isKinematic = false;
        _rb.useGravity = true;
        
        Debug.Log("HAVE BEEN RELEASED");
    }

    public void UpdateHoldPosition(Vector3 targetPosition, Quaternion targetRotation)
    {
        _rb.MovePosition(targetPosition);
        _rb.MoveRotation(targetRotation);
    }
}

