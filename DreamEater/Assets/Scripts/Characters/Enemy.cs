/*
 Enemigos que se pueven y atacan por collision
 NO detectan al player de forma inteligente, se mueve de forma secuencial
*/

using UnityEngine;

[RequireComponent(typeof(Rigidbody), typeof(Collider))]
public class EnemyMovable : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    
    private Rigidbody _rb;

    private void Awake()
    {
        _rb = GetComponent<Rigidbody>();
    }


    // Movimiento del enemy
    private void Update()
    {
        _rb.linearVelocity = new Vector3(_rb.linearVelocity.x, _rb.linearVelocity.y,speed);
    }
    
    // Cambiar direccion de movimiento si choca con un objeto
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Obstacle"))
        {
            // Rotar enemy para cambiar direccion de movimiento
            speed *= -1;
            transform.Rotate(0, 0, 180);
        }
        // Mata al player
        else if(other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponent<PlayerController2>().Die();
        }
    }
}