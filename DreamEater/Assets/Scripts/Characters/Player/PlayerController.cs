using System.Collections;
using UnityEngine;



[RequireComponent(typeof(CharacterController), typeof(Animator))]
public class PlayerController : MonoBehaviour
{
    //Cambiar lo del look para que mire con forward en esa dir  y el crouch a la izquierda 
    //atributos
    [SerializeField] private bool  isAlive   = true;
    [SerializeField] private float jumpForce = 6.5f;
    [SerializeField] private float speed     = 2.8f;
    [SerializeField] private float timeStep  = 0.1f;
    
    [SerializeField] private float turnSpeed = 12f; // giro suave al cambiar de izquierda/derecha
    [SerializeField] private float depthBias = 1.3f;// influencia del input Z sobre la orientación del personaje
    
    private Vector3 _lastLookDir = Vector3.right;


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
    
    
    //jump request para sincronizar con animaciones
    private bool jumpRequested;

    
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
        bool groundedNow = cc.isGrounded || IsGrounded();
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
            if (verticalVelocity < 0f){
                verticalVelocity = 0f; // limpia caída
            }
            verticalVelocity = jumpForce;//
            anim.ResetTrigger(jumpTrig);
            anim.SetTrigger(jumpTrig);
            edgeTimer = 0f;
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
        Vector3 desiredForward = _lastLookDir; // por defecto: última mirada

        // 
        if (input.sqrMagnitude > 0.0001f)
        {
            // Nueva dirección real (incluye diagonales)
            Vector3 desiredDir = new Vector3(input.x, 0f, input.z).normalized;

            // Guardamos última dirección válida
            _lastLookDir = desiredDir;
        }

        //aplica giro suave solo en Y
        float yaw = Mathf.Atan2(_lastLookDir.x, _lastLookDir.z) * Mathf.Rad2Deg;
        Quaternion targetRot = Quaternion.Euler(0f, yaw, 0f);
        transform.rotation = Quaternion.Slerp(transform.rotation, targetRot, Time.deltaTime * turnSpeed);

        //actualiza animaciones
        bool hasInput = input.sqrMagnitude > 0.0001f;
        float style = hasInput ? (InputManager.Instance.RunHeld ? 2f : 1f) : 0f;

        //valores de velocidad para el animador
        anim.SetFloat(xParam, input.x, 0.08f, Time.deltaTime);
        anim.SetFloat(zParam, input.z, 0.08f, Time.deltaTime);

        //actualiza parámetros del animador
        anim.SetFloat(yParam, style, 0.08f, Time.deltaTime);
        
        anim.SetBool ("Grounded", groundedNow);
        anim.SetBool (crouchBool, isCrouching);

        anim.SetFloat(blendParam, 0f);//para que interfiera en pruebas
        

        
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
        verticalVelocity = 0f;

        anim.SetTrigger("Die");//se quita es solopara probar death (en animator mientras se le da al play hacer click en die y se ve que si muere)
        StartCoroutine(RespawnSequence());  //Respawn del jugador en los checkpoints
    }

    private IEnumerator RespawnSequence()
    {
        //anim.SetTrigger("Die"); //Animacion de muerte
        yield return new WaitForSeconds(0.8f); 

        //Desactivar CharacterController (evita problemas)
        cc.enabled = false;

        //Mover al checkpoint
        transform.position = CheckpointManager.Instance.GetCheckpointPosition();
        
        yield return null;  //Espera de un frame 

        //Reactivar CharacterController
        cc.enabled = true;

        //Resetear valores
        verticalVelocity = 0f;
        anim.SetFloat(xParam, 0f); 
        anim.SetFloat(zParam, 0f);
        anim.SetFloat(blendParam, 0f);
        
        //Revivir al jugador
        isAlive = true;
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
