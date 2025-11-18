using System.Collections;
using UnityEngine;


[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    
    //atributos
    [SerializeField] private bool  isAlive   = true;
    [SerializeField] private float jumpForce = 6.5f;
    [SerializeField] private float speed     = 3.5f;
    [SerializeField] private float timeStep  = 0.1f;

    //componentes
    private CharacterController cc;
    private Animator anim;

    //movimiento
    [SerializeField] private Movement movement = new Movement(); //composicion de Movement
    [SerializeField] private float gravity = 20f;
    [SerializeField] private float crouchSpeedFactor = 0.5f;
    private float verticalVelocity;

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
    private float edgeTimer = 0f;
    

    //clamp de profundidad
    [Header("Depth Clamp (W/S)")]
    [SerializeField] private bool  clampDepth = true;
    [SerializeField] private float minDepth   = -2f;
    [SerializeField] private float maxDepth   =  2f;

    //facing lateral (solo X)
    private float lastFacing = 1f;
    
    //jump request para sincronizar con animaciones
    private bool jumpRequested = false;


    
    private void Awake()
    {
        cc   = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        anim.applyRootMotion = false;

        cc.minMoveDistance = 0f;
        //lo ajustamos a mano para evitar bugs
        cc.stepOffset = Mathf.Clamp(cc.stepOffset, 0.25f, 0.6f);
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

        // movimiento input
        float h = InputManager.Instance.Horizontal; // A/D o stick X
        float z = InputManager.Instance.Depth;      // W/S o stick Y
        
        Vector3 input = new Vector3(h, 0f, z);
        if (input.sqrMagnitude > 1f)
        {
            input.Normalize();
        }

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

        //clamp profundidad Z ( PRUEBA)
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

            // corrección posicional por si acaso
            if (currZ <  minDepth) {
                transform.position = new Vector3(transform.position.x, transform.position.y, minDepth);
                
            }

            if (currZ > maxDepth)
            {
                transform.position = new Vector3(transform.position.x, transform.position.y, maxDepth);
            }
        }

        //grounding y gravedad
        bool groundedNow = IsGrounded();
        if (groundedNow)
        {
            edgeTimer = edgeTime;
        }
        else
        {
            edgeTimer -= Time.deltaTime;
        }

        if (jumpRequested && edgeTimer > 0.01f)
        {
            anim.SetTrigger(jumpTrig);
            jumpRequested = false;
        }

        
        if (groundedNow && verticalVelocity <= 0f)
        {
            verticalVelocity = -3f; // pequeño valor negativo para "pegarse" al suelo
        }
        verticalVelocity -= gravity * Time.deltaTime;

        //move
        Vector3 total = (input * currentSpeed) + Vector3.up * verticalVelocity;
        cc.Move(total * Time.deltaTime);

        //orientacion del personaje
        if (Mathf.Abs(input.x) > 0.01f)
        {
            lastFacing = Mathf.Sign(input.x);
        }
        transform.forward = new Vector3(lastFacing, 0f, 0f);

        //animator
        float blend01 = input.sqrMagnitude > 0.0001f ? (InputManager.Instance.RunHeld ? 1f : 0.5f) : 0f;
        anim.SetFloat(xParam, input.x);
        anim.SetFloat(zParam, input.z);
        anim.SetFloat(yParam, verticalVelocity);
        anim.SetBool (crouchBool, isCrouching);
        anim.SetFloat(blendParam, blend01);
        
      
    }

    //grounding con spherecast
    private bool IsGrounded()
    {
        Vector3 origin = transform.position + cc.center;
        float radius   = Mathf.Max(0.05f, cc.radius * 0.95f);
        float dist     = (cc.height * 0.5f) + 0.2f;

        return cc.isGrounded || Physics.SphereCast(origin, radius, Vector3.down, out _, dist, groundMask, QueryTriggerInteraction.Ignore);
    }

    //métodos de accion
    private void WalkPlayer() => movement.Walk();
    private void RunPlayer()  => movement.Run();
    private void Crouch()     { /* update */ }

    private void Jump()
    {
        if (!isAlive) return;
        jumpRequested = true;
    }

    private void Interact() { /* hook */ }

    public void Die()
    {
        if (!isAlive) return;
        isAlive = false;
        verticalVelocity = 0f;
    }

    public void OnJumpAnimEvent()
    {
        if (!isAlive) return;

        if (edgeTimer > 0.01f)
        {
            verticalVelocity = jumpForce;
            edgeTimer = 0f;
        }
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
