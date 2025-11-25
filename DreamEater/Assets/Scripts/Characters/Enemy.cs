using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class EnemyMovable : MonoBehaviour
{
    //[SerializeField] private Animator anim;
    [SerializeField] private float speed = 3f;
    
    private Rigidbody _rb;
    
    //private readonly int _walkAnimState = Animator.StringToHash("EnemyMelee_Walk");
    
    //Inicializar enemy
    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
        //if(anim == null) anim = GetComponent<Animator>();
    }
    
    private void Update()
    {
        Move();
    }
    
    //Movimiento del enemy
    private void Move()
    {
        _rb.linearVelocity = new Vector2(speed, _rb.linearVelocity.y);
    }
    
    //Cambiar direccion de movimiento si choca con un objeto
    private void OnCollisionEnter(Collision other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            speed *= -1;
        } else if(other.gameObject.layer == LayerMask.NameToLayer("Player")){
            //TODO: Llamar al metodo de muerte del player
            /*
             COPIAR Y PEGAR EN EL PLAYER PARA PROBAR
             private void Respawn(){
                transform.position = CheckpointManager.Instance.GetCheckpointPosition();
                
                //Quitarle velocidad para que no aparezca moviendose
                rb.velocity = Vector3.zero;
                rb.angularVelocity = Vector3.zero;
             }
             */
        }
    }
}