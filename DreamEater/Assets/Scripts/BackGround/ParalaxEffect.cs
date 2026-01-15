using UnityEngine;

public class ParalaxEffect : MonoBehaviour
{
    [SerializeField] private float parallaxMultiplier;
    
    private Transform _cameraTransform;
    private Vector3 _previousCameraPosition;
    private float _spriteWidth, _startPosition;
    
    void Start()
    {
        _cameraTransform = Camera.main.transform;
        _previousCameraPosition = _cameraTransform.position;
        _spriteWidth = GetComponent<SpriteRenderer>().bounds.size.x;
        _startPosition = transform.position.x;
    }
    
    void LateUpdate()
    {
        float deltaX = (_cameraTransform.position.x - _previousCameraPosition.x) * parallaxMultiplier;
        float moveAmount = _cameraTransform.position.x * (1 - parallaxMultiplier);
        transform.Translate(new Vector3(deltaX, 0, 0));
        _previousCameraPosition = _cameraTransform.position;

        if (moveAmount > _startPosition + _spriteWidth)
        {
            transform.Translate(new Vector3(_spriteWidth, 0, 0));
            _startPosition += _spriteWidth;
        }
        else if (moveAmount < _startPosition - _spriteWidth)
        {
            transform.Translate(new Vector3(-_spriteWidth, 0, 0));
            _startPosition -= _spriteWidth;
        }
    }
}
