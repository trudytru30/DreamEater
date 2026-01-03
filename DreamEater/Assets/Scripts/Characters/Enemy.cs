using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class EnemyMovable : MonoBehaviour
{
    [SerializeField] private Animator anim;
    [SerializeField] private float speed = 3f;
    
    private Rigidbody _rb;
    
    private readonly int _walkAnimState = Animator.StringToHash("Enemy_Walk");
    
    //Inicializar enemy
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        if(anim == null) anim = GetComponent<Animator>();
    }
    
    //Movimiento del enemy
    private void Update()
    {
        _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, _rb.linearVelocity.y, speed );
        anim.SetBool(_walkAnimState, true);
    }
    
    //Cambiar direccion de movimiento si choca con un objeto
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            //Rotar enemy para cambiar direccion de movimiento
            speed *= -1;
            transform.Rotate(0, 0, 180);
        } else if(other.gameObject.layer == LayerMask.NameToLayer("Player")){
            //TODO: Llamar al metodo de muerte del player
            
        }
    }

    



}