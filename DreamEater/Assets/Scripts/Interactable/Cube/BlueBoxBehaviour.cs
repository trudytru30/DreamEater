using UnityEngine;

public class BlueBoxBehaviour : BoxBehaviour
{
    [SerializeField] private float moveDistance = 1f;
    private Vector3 _moveVector; // This will store the direction vector (e.g., Vector3.forward)

    [SerializeField]
    enum Directions
    {
        FORWARD,
        BACKWARDS,
        LEFT,
        RIGHT
    }

    [SerializeField] private Directions currentDirection;
    private Vector3 _targetPosition; // Renamed for clarity: this is the final destination point
    private float _moveDuration;     // Renamed for clarity: this is the total time the movement should take
    private float _startTime;        // Time when the current movement started
    
    // Ensure moveSpeed is defined in BoxBehaviour or BlueBoxBehaviour
    // For example: [SerializeField] private float moveSpeed = 5f; 

    private void Start()
    {
        // Set the base direction vector based on the enum selection
        switch (currentDirection)
        {
            case Directions.FORWARD:
                _moveVector = Vector3.forward;
                break;
            case Directions.BACKWARDS:
                _moveVector = Vector3.back;
                break;
            case Directions.LEFT:
                _moveVector = Vector3.left;
                break;
            case Directions.RIGHT:
                _moveVector = Vector3.right;
                break;
            default:
                _moveVector = Vector3.forward; // Default to forward if somehow unset
                break;
        }
        isMoving = false;
        
        // Removed newPosition and moveTime calculation from Start()
        // They need to be calculated when the movement actually starts in MoveOneUnitDistance()
    }

    public void MoveOneUnitDistance()
    {
        if (!isMoving)
        {
            Debug.Log("MoveOneUnit");
            isMoving = true;   
            startPosition = transform.position; // Capture the object's current position as the starting point
            _startTime = Time.time;              // Record the time this movement began

            // CALCULATE THE TARGET POSITION RELATIVE TO THE STARTING POSITION
            _targetPosition = startPosition + (_moveVector * moveDistance);

            // Calculate the total duration for this specific movement
            if (moveSpeed <= 0) // Prevent division by zero
            {
                Debug.LogError("moveSpeed must be greater than 0!");
                isMoving = false;
                return;
            }
            _moveDuration = moveDistance / moveSpeed;
        }
    }

    private void FixedUpdate()
    {
        if (isMoving)
        {
            // Calculate the interpolation factor (progress from 0 to 1)
            float lerpFactor = (Time.time - _startTime) / _moveDuration;
            
            // Apply the interpolation using the correct lerpFactor
            transform.position = Vector3.Lerp(startPosition, _targetPosition, lerpFactor);

            // Stop condition: if we've reached or passed the target duration, or are very close
            if (lerpFactor >= 1f || Vector3.Distance(transform.position, _targetPosition) < 0.01f) // Added a small epsilon for distance check robustness
            {
                // Snap to the exact target position to avoid slight overshooting due to floating point precision
                transform.position = _targetPosition; 
                isMoving = false;
                Debug.Log("stop moving");
            }
        }
    }
}
