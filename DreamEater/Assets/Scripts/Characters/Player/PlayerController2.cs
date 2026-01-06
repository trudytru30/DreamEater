/*
 Controlador del player por fisicas y rigidbody
 Las funciones de movimiento se encuentran en la clase movement (Run y Walk)
*/

using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Animator), typeof(CapsuleCollider))]
public class PlayerController2 : MonoBehaviour
{
    [SerializeField] private bool isAlive = true;
    [SerializeField] private float jumpForce = 6.5f;
    [SerializeField] private float speed = 2.8f;
    [SerializeField] private float timeStep = 0.1f;
    
    [SerializeField] private float turnSpeed = 12f;
    [SerializeField] private float crouchSpeedFactor = 0.5f;
    [SerializeField] private float edgeTime = 0.12f;
    [SerializeField] private LayerMask groundMask = ~0;

    [Header("Depth Clamp (W/S)")]//Clamp de profundidad
    [SerializeField] private bool clampDepth = true;
    [SerializeField] private float minDepth = -3f;
    [SerializeField] private float maxDepth = 3f;

    [Header("Ground Check")]
    [SerializeField] private Transform groundCheck;
    [SerializeField] private float groundRadius = 0.2f;

    [Header("Animator Params")]
    [SerializeField] private string blendParam = "Blend";
    [SerializeField] private string xParam = "xSpeed";
    [SerializeField] private string zParam = "zSpeed";
    [SerializeField] private string yParam = "ySpeed";
    [SerializeField] private string crouchBool = "IsCrouching";
    [SerializeField] private string jumpTrig = "Jump";

    private Rigidbody _rb;
    private Animator _anim;
    private Movement _movement = new Movement();

    private float _verticalVelocity;
    private float _edgeTimer = 0f;
    private bool _isGrounded;

    private Vector3 _lastLookDir = Vector3.right;
    private Interactable _currentInteractable;
    
    private CapsuleCollider _collider;
    private Vector3 _originalCenter;
    private float _originalHeight;
    private bool _isCrouching ;
    
    private IGrabable _currentGrabbedObject;

    [SerializeField] private Transform grabOrigin; // punto delante del jugador
    [SerializeField] private float grabRange = 1.5f;
    
    //[SerializeField] private LayerMask movableLayer;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        _anim = GetComponent<Animator>();
        _collider = GetComponent<CapsuleCollider>();

        _originalCenter = _collider.center;
        _originalHeight = _collider.height;

        _rb.constraints = RigidbodyConstraints.FreezeRotation;
        _rb.interpolation = RigidbodyInterpolation.Interpolate;
        _anim.applyRootMotion = false;
    }

    private void OnEnable()
    {
        if (InputManager.Instance == null) return;
        InputManager.Instance.JumpPressed += Jump;
        InputManager.Instance.InteractPressed += Interact;
        //InputManager.Instance.InteractPressed += TryGrabOrRelease;
    }

    private void OnDisable()
    {
        if (InputManager.Instance == null) return;
        InputManager.Instance.JumpPressed -= Jump;
        InputManager.Instance.InteractPressed -= Interact;
        //InputManager.Instance.InteractPressed -= TryGrabOrRelease;
    }

    private void Update()
    {
        if (!isAlive || InputManager.Instance == null) return;
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask, QueryTriggerInteraction.Ignore);//comprobar suelo
        
        float h = InputManager.Instance.Horizontal;
        float z = InputManager.Instance.Depth;

        Vector3 input = new Vector3(h, 0f, z);

        //normalizar input
        if (input.sqrMagnitude > 1f)
        {
            input.Normalize();
        }

        //al correr multiplica el vector de input por 2
        if (InputManager.Instance.RunHeld && input.sqrMagnitude > 0.01f)
        {
            input *= 2.0f;
        }
        // Clamp de profundidad
        if (clampDepth)
        {
            float currZ = transform.position.z;
            if (currZ <= minDepth && input.z < 0f)
            {
                input.z = 0f;
            }

            if (currZ >= maxDepth && input.z > 0f)
            {
                input.z = 0f;
            }

            if (currZ < minDepth)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, minDepth);
            }

            if (currZ > maxDepth)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, maxDepth);
            }
        }

        //movimiento base
        bool wantCrouch = InputManager.Instance.CrouchHeld;

        if (_isCrouching)
        {
            //si agachado se mantiene agachado si se pide o no hay espacio para levantarse
            if (!wantCrouch && CanStandUp())
            {
                _isCrouching = false;
                // restaurar cápsula
                _collider.height = _originalHeight;
                _collider.center = _originalCenter;
            }
            else
            {
                //mantiene agachado
                float crouchHeight   = _originalHeight * 0.5f; //0.7f
                float originalBottom = _originalCenter.y - (_originalHeight * 0.5f);
                float newCenterY     = originalBottom + (crouchHeight * 0.5f);

                _collider.height = crouchHeight;
                _collider.center = new Vector3(_originalCenter.x, newCenterY, _originalCenter.z);
            }
        }
        else
        {
            //si esta de pie y no techo,si se quiere agachar se agacha
            if (wantCrouch)
            {
                _isCrouching = true;

                float crouchHeight   = _originalHeight * 0.5f;
                float originalBottom = _originalCenter.y - (_originalHeight * 0.5f);
                float newCenterY     = originalBottom + (crouchHeight * 0.5f);

                _collider.height = crouchHeight;
                _collider.center = new Vector3(_originalCenter.x, newCenterY, _originalCenter.z);
            }
            else
            {
               
                if (_collider.height != _originalHeight)
                {
                    _collider.height = _originalHeight;
                    _collider.center = _originalCenter;
                }
            }
            _anim.SetBool(crouchBool, _isCrouching);
            if (_isCrouching) _anim.ResetTrigger(jumpTrig);
        }

        if (InputManager.Instance.RunHeld)
        {
            _movement.Run();
        }
        else
        {
            _movement.Walk();
        }

        float currentSpeed = speed * _movement.speedMultiplier;
        if (_isCrouching)
        {
            currentSpeed *= crouchSpeedFactor;
        }

        //mov físico
        Vector3 move = input * currentSpeed;
        move.y = _rb.linearVelocity.y;
        _rb.linearVelocity = move;

        //ground check
        _isGrounded = Physics.CheckSphere(groundCheck.position, groundRadius, groundMask, QueryTriggerInteraction.Ignore);
        _edgeTimer = _isGrounded ? edgeTime : _edgeTimer - Time.deltaTime;


        // Orientación
        if (input.sqrMagnitude > 0.001f)
        {
            _lastLookDir = input;
        }

        float yaw = Mathf.Atan2(_lastLookDir.x, _lastLookDir.z) * Mathf.Rad2Deg;
        Quaternion targetRot = Quaternion.Euler(0f, yaw, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * turnSpeed);

        // Animaciones
        _anim.SetFloat(xParam, input.x, 0.08f, Time.deltaTime);
        _anim.SetFloat(zParam, input.z, 0.08f, Time.deltaTime);
        //_anim.SetBool(crouchBool, _isCrouching);
        _anim.SetBool("Grounded", _isGrounded);

        /*float ySpeed = 0f;
        if (input.sqrMagnitude > 0.01f)
        {
            ySpeed = InputManager.Instance.RunHeld ? 2f : 1f;
        }

        _anim.SetFloat(yParam, ySpeed, 0.08f, Time.deltaTime);*/
        _anim.SetFloat(yParam, _rb.linearVelocity.y, 0.08f, Time.deltaTime);


    }

    private void Jump()
    {
        if (!isAlive) return;

        if (!_isGrounded) return;//solo salta si esta en el suelo
        if (_isCrouching || !CanStandUp())
        {
            _anim.ResetTrigger(jumpTrig);//cancela salto en crouch po ej
            return;
        }

        //salto
        _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        _anim.ResetTrigger(jumpTrig);
        _anim.SetTrigger(jumpTrig);
    }
    private void Crouch()
    {
        float crouchHeight   = _originalHeight * 0.5f;
        float originalBottom = _originalCenter.y - (_originalHeight * 0.5f);
        float newCenterY     = originalBottom + (crouchHeight * 0.5f);

        _collider.height = crouchHeight;
        _collider.center = new Vector3(_originalCenter.x, newCenterY, _originalCenter.z);
    }

    private bool CanStandUp()
    {
        var bounds      = _collider.bounds; 
        float radius    = _collider.radius * 0.95f;
        float feetY     = bounds.min.y;//pos pies
        float headYFull = feetY + _originalHeight;

        //para que casula no toque justo el techo
        float epsilon   = 0.02f;

        Vector3 worldBottom = new Vector3(bounds.center.x, feetY + radius + epsilon, bounds.center.z);
        Vector3 worldTop    = new Vector3(bounds.center.x, headYFull - radius,       bounds.center.z);
        
        var hits = Physics.OverlapCapsule(worldBottom, worldTop, radius, groundMask, QueryTriggerInteraction.Ignore);

        //si hay algun collider encima no puede levantarse
        foreach (var col in hits)
        {
            //ignora el propio collider
            if (col.transform.root == transform.root) continue;
            return false; //hay techo
        }
        return true;
    }

    private void WalkPlayer() => _movement.Walk();
    private void RunPlayer() => _movement.Run();

    private void Interact()
    {
        if (_currentInteractable != null && _currentInteractable.GetCanInteract())
        {
            _currentInteractable.SetIsInteracting(true);
            //añadir anim interactuar
            Debug.Log("Interactuando con " + _currentInteractable.gameObject.name);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.TryGetComponent(out Interactable interactable) && interactable.GetCanInteract())
        {
            _currentInteractable = interactable;
        }
    }

    private void OnTriggerExit(Collider other)
    {
        if (other.TryGetComponent(out Interactable interactable) && _currentInteractable == interactable)
        {
            _currentInteractable = null;
        }
    }

    public void Die()
    {
        if (!isAlive) return;
        isAlive = false;
        _rb.linearVelocity = Vector3.zero;

        _anim.SetTrigger("Die");
        StartCoroutine(RespawnSequence());
    }

    private IEnumerator RespawnSequence()
    {
        Debug.Log("Inicia respawn...");
        yield return new WaitForSeconds(6f);
        _rb.isKinematic = true;
        
        Debug.Log("Checkpoint pos: " + CheckpointManager.Instance?.GetCheckpointPosition());
        transform.position = CheckpointManager.Instance.GetCheckpointPosition();
        yield return null;
        _rb.isKinematic = false;
        _anim.SetFloat(xParam, 0f);
        _anim.SetFloat(zParam, 0f);
        _anim.SetFloat(blendParam, 0f);
        
        //Revivir al jugador
        isAlive = true;
    }
/*
    private void TryGrabOrRelease()
    {
        if (_currentGrabbedObject != null)
        {
            _currentGrabbedObject.Release();
            _currentGrabbedObject = null;
            _anim.SetBool("IsPushing", false);
            return;
        }

        Collider[] hits = Physics.OverlapSphere(transform.position, grabRange, movableLayer);
        float closestDistance = Mathf.Infinity;
        IGrabable closestGrabable = null;

        foreach (var hit in hits)
        {
            IGrabable grabTarget = hit.GetComponent<IGrabable>();
            if (grabTarget != null)
            {
                float distance = Vector3.Distance(transform.position, hit.transform.position);
                if (distance < closestDistance)
                {
                    closestDistance = distance;
                    closestGrabable = grabTarget;
                }
            }
        }

        if (closestGrabable != null)
        {
            _currentGrabbedObject = closestGrabable;
            _currentGrabbedObject.Grab(grabOrigin);
            _anim.SetBool("IsPushing", true);
        }
    }
    
    //PRUEBA RADIO PARA GRAB
    private void OnDrawGizmosSelected()
    {
        if (grabOrigin == null) return;

        Gizmos.color = Color.cyan;
        Gizmos.DrawWireSphere(grabOrigin.position, grabRange);
    }
    */

    public void OnJumpAnimEvent()
    {
        if (!isAlive) return;
        if (_edgeTimer > 0.01f)
        {
            _rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
            _edgeTimer = 0f;
        }
    }

    public IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(timeStep);
    }

    // Getters y Setters
    public bool GetIsAlive() => isAlive;
    public void SetIsAlive(float v) => isAlive = v > 0f;
    public float GetJumpForce() => jumpForce;
    public void SetJumpForce(float v) => jumpForce = v;
    public float GetSpeed() => speed;
}

