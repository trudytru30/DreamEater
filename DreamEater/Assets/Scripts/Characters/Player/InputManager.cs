using UnityEngine;
using UnityEngine.InputSystem;

public class InputManager : MonoBehaviour, InputSystem_Actions.IPlayerActions
{
    public static InputManager Instance { get; private set; }
    private InputSystem_Actions _actions;
    
    //movimiento principal
    public Vector2 Move { get; private set; }//vector 2d
    public float Horizontal => Move.x;          
    public float Depth      => Move.y;   
    
    //look
    public Vector2 Look { get; private set; }
    
    //acciones(mantener el boton)
    public bool RunHeld     { get; private set; }
    public bool CrouchHeld  { get; private set; }// Crouch

    //eventos
    public event System.Action JumpPressed;
    public event System.Action InteractPressed;
    public event System.Action PausePressed;

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject); 
            return;
        }
        Instance = this;
        _actions = new InputSystem_Actions();
        _actions.Player.SetCallbacks(this);
        //_actions.UI.SetCallbacks(this);
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        _actions.Player.Enable();
       // _actions.UI.Enable();
    }

    private void OnDisable()
    {
        _actions.Player.Disable();
        //_actions.UI.Disable();
    }

    private void OnDestroy() => _actions?.Dispose();

    //callbacks del palyer
    public void OnMove(InputAction.CallbackContext ctx)
    {
        Move = ctx.ReadValue<Vector2>();
    }

    public void OnPause(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) PausePressed?.Invoke();
    }

    public void OnLook(InputAction.CallbackContext ctx)
    {
        Look= ctx.ReadValue<Vector2>();
    }

    public void OnRun(InputAction.CallbackContext ctx)
    {
        RunHeld = ctx.ReadValueAsButton();
    }

    public void OnCrouch(InputAction.CallbackContext ctx)
    {
        CrouchHeld = ctx.ReadValueAsButton();
    }

    public void OnJump(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) JumpPressed?.Invoke();
    }

    public void OnInteract(InputAction.CallbackContext ctx)
    {
        if (ctx.performed) InteractPressed?.Invoke();
    }


    //callbacks del UI


    
}
