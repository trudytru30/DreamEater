using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(CapsuleCollider), typeof(Animator))]
public class PlayerController2 : MonoBehaviour
{
    //Cambiar lo del look para que mire con forward en esa dir  y el crouch a la izquierda 
    //atributos
    [SerializeField] private bool  isAlive   = true;
    [SerializeField] private float jumpForce = 6.5f;
    [SerializeField] private float speed     = 4.0f;
    [SerializeField] private float timeStep  = 0.1f;
    
    [SerializeField] private float turnSpeed = 12f; // giro suave al cambiar de izquierda/derecha
    
    
    //movimiento
    [SerializeField] private Movement movement = new Movement(); //composicion de Movement
    [SerializeField] private float gravity = 20f;
    [SerializeField] private float crouchSpeedFactor = 0.5f;
    
    //parametros del animator
    [Header("Animator Params")]
    [SerializeField] private string blendParam = "Blend"; // 0=Idle, 0.5=Walk, 1=Run
    [SerializeField] private string xParam     = "xSpeed";
    [SerializeField] private string zParam     = "zSpeed";
    [SerializeField] private string yParam     = "ySpeed";
    [SerializeField] private string crouchBool = "IsCrouching";
    [SerializeField] private string jumpTrig   = "Jump";
    
    //sirve para saber si estamos en el suelo
    [Header("Grounding")]
    [SerializeField] private LayerMask groundMask = ~0;
    [SerializeField] private float edgeTime = 0.12f;
    
    
    //clamp de profundidad
    [Header("Depth Clamp (W/S)")]
    [SerializeField] private bool  clampDepth = true;
    [SerializeField] private float minDepth   = -2f;
    [SerializeField] private float maxDepth   =  2f;
    


    //componentes
    private Animator _anim;
    private Rigidbody _rb;
    private CapsuleCollider _col;
    
    private float _yVelocity;
    private float _edgeTimer = 0f;
    private bool _jumpRequested;//jump request para sincronizar con animaciones
    private Vector3 _input;
    private Vector3 _lastLookDir = Vector3.right;
    private void Awake()
    {
        _anim = GetComponent<Animator>();
        _rb = GetComponent<Rigidbody>();
        _col = GetComponent<CapsuleCollider>();
        
        _anim.applyRootMotion = false;

        _rb.isKinematic = false;                      
        _rb.useGravity = false;
        _rb.interpolation = RigidbodyInterpolation.Interpolate;
        _rb.collisionDetectionMode = CollisionDetectionMode.Continuous;
        _rb.constraints = RigidbodyConstraints.FreezeRotationX | RigidbodyConstraints.FreezeRotationZ;
    }

    private void OnEnable()
    {
        if (InputManager.Instance == null)
        {
            return;
        }
        InputManager.Instance.JumpPressed     += Jump;
        InputManager.Instance.InteractPressed += Interact;
    }
    private void OnDisable()
    {
        if (InputManager.Instance == null)
        {
            return;
        }
        InputManager.Instance.JumpPressed     -= Jump;
        InputManager.Instance.InteractPressed -= Interact;
    }

    private void Update()
    {
        if (!isAlive || InputManager.Instance == null)
        {
            return;
        }
        //Input
        // movimiento input
        float h = InputManager.Instance.Horizontal; // A/D o stick X
        float z = InputManager.Instance.Depth;      // W/S o stick Y
        
        _input = new Vector3(h, 0f, z);
        if (_input.sqrMagnitude > 1f)
        {
            _input.Normalize();
        }

        //Estados
        /*
        //walk, run, crouch
        float currentSpeed = speed;
        bool  isCrouching  = InputManager.Instance.CrouchHeld;
        if (isCrouching)
        {
            Crouch(); 
            currentSpeed *= crouchSpeedFactor;
        }

        if (InputManager.Instance.RunHeld)
        {
            RunPlayer();
        }
        else
        {
            WalkPlayer();
        }
        currentSpeed *= movement.speedMultiplier;
        
        */
        bool isCrouching = InputManager.Instance.CrouchHeld;
        if (InputManager.Instance.RunHeld) movement.Run(); else movement.Walk();
        float runFactor = InputManager.Instance.RunHeld ? 2f : 1f; // para el Animator

        //Orientacion
        //orientacion del personaje
        Vector3 desiredForward = _lastLookDir; // por defecto: última mirada

        // 
        if (_input.sqrMagnitude > 0.0001f)
        {
            // Nueva dirección real (incluye diagonales)
            Vector3 desiredDir = new Vector3(_input.x, 0f, _input.z).normalized;

            // Guardamos última dirección válida
            _lastLookDir = desiredDir;
        }

        //aplica giro suave solo en Y
        float yaw = Mathf.Atan2(_lastLookDir.x, _lastLookDir.z) * Mathf.Rad2Deg;
        Quaternion targetRot = Quaternion.Euler(0f, yaw, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * turnSpeed);

        
        //Animator
        //actualiza animaciones
        bool hasInput = _input.sqrMagnitude > 0.0001f;
        //float runFactor = hasInput ? (InputManager.Instance.RunHeld ? 2f : 1f) : 0f;

        //valores de velocidad para el animador
        _anim.SetFloat(xParam, _input.x, 0.08f, Time.deltaTime);
        _anim.SetFloat(zParam, _input.z, 0.08f, Time.deltaTime);
        _anim.SetFloat(yParam, _yVelocity);
        _anim.SetBool (crouchBool, isCrouching);

// Walk/Run solo con Blend:
        float blend = (_input.sqrMagnitude < 0.001f) ? 0f : (InputManager.Instance.RunHeld ? 1f : 0.5f);
        _anim.SetFloat(blendParam, blend, 0.08f, Time.deltaTime);
        
        float mag = _input.magnitude;
        if (mag < 0.1f) _input = Vector3.zero;         // dead-zone pequeña
        else _input /= mag;

        //Profundidad (clamp)
        if (clampDepth)
        {
            float zPosition = transform.position.z;
            if (zPosition < minDepth)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, minDepth);
            }

            if (zPosition > maxDepth)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, maxDepth);
            }
        }
        
    }

    //físicas
    private void FixedUpdate()
    {
        if (!isAlive)
        {
            _rb.linearVelocity = Vector3.zero; 
            return;
        }

        bool grounded = IsGrounded();

        // edgetimer
        if (grounded)
        {
            _edgeTimer = edgeTime;
        }
        else
        {
            _edgeTimer -= Time.fixedDeltaTime;
        }

        //confirmar salto
        if (_jumpRequested && _edgeTimer > 0.01f)
        {
            if (_yVelocity < 0f)
            {
                _yVelocity = 0f;
            }
            _yVelocity = jumpForce;
            _anim.ResetTrigger(jumpTrig);
            _anim.SetTrigger(jumpTrig);
            _edgeTimer = 0f;
            _jumpRequested = false;
        }

        //gravedad
        if (grounded && _yVelocity <= 0f)
        {
            _yVelocity = -3f;
        }
        _yVelocity -= gravity * Time.fixedDeltaTime;

        //velocidad en el plano
        float planarSpeed = speed;                // base
        if (InputManager.Instance.RunHeld)    planarSpeed *= 1.6f;           // corre ~60% más
        if (InputManager.Instance.CrouchHeld) planarSpeed *= crouchSpeedFactor; // ej. 0.5

        // normaliza input para diagonales sin turbo
        Vector3 dir = _input.sqrMagnitude > 1f ? _input.normalized : _input;

        // aplica clamp Z si procede
        if (clampDepth) {
            float zPos = _rb.position.z;
            if ((zPos <= minDepth && dir.z < 0f) || (zPos >= maxDepth && dir.z > 0f))
                dir.z = 0f;
        }

        // velocidad final en mundo (¡no dependas de la rotación!)
        Vector3 planarVel = dir * planarSpeed;
        Vector3 vel = new Vector3(planarVel.x, _yVelocity, planarVel.z);
        _rb.linearVelocity = vel; 

        // informar al animator de grounded (aquí es el valor real de física)
        _anim.SetBool("Grounded", grounded);
    }
   
    //ground check
    private bool IsGrounded()
    {Vector3 center = _col.bounds.center;
        float r = Mathf.Max(0.05f, _col.radius * 0.95f);
        Vector3 origin = new Vector3(center.x, _col.bounds.min.y + r + 0.01f, center.z);
        return Physics.CheckSphere(origin, r, groundMask, QueryTriggerInteraction.Ignore);
    }

    //métodos de accion
    private void WalkPlayer() => movement.Walk();
    private void RunPlayer()  => movement.Run();
    private void Crouch()     {  }// update

    private void Jump()
    {
        if (!isAlive) return;
        _jumpRequested = true;
        //anim.SetTrigger(jumpTrig);
    }

    private void Interact()
    {
        //TODO: implementar interaccion 
    }

    public void Die()
    {
        if (!isAlive) return;
        isAlive = false;
        _yVelocity = 0f;

        _anim.SetTrigger("Die");//se quita es solopara probar death (en animator mientras se le da al play hacer click en die y se ve que si muere)
        StartCoroutine(RespawnSequence());  //Respawn del jugador en los checkpoints
    }

    private IEnumerator RespawnSequence()
    {
        //anim.SetTrigger("Die"); //Animacion de muerte
        yield return new WaitForSeconds(0.8f); 

        _rb.isKinematic = true;
        transform.position = CheckpointManager.Instance.GetCheckpointPosition();
        yield return null;
        _rb.isKinematic = false;

        _yVelocity = 0f;
        _anim.SetFloat(xParam, 0f);
        _anim.SetFloat(zParam, 0f);
        _anim.SetFloat(blendParam, 0f);
        isAlive = true;
    }
    

    
    public IEnumerator PlaySound()
    {
        yield return new WaitForSeconds(timeStep);
    }
    
    //getters y setters
    public bool  GetIsAlive()          => isAlive;
    public void  SetIsAlive(float v)   => isAlive = v > 0f;
    public float GetJumpForce()        => jumpForce;
    public void  SetJumpForce(float v) => jumpForce = v;
    public float GetSpeed()            => speed;

    

}
