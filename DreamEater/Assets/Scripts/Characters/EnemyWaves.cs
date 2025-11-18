using System;
using UnityEngine;

public class EnemyWaves : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float timeLimit = 5f;
    
    private float _time;
    private const int Direction = 1;

    private void Update()
    {
        Move();
    }
    
    //Mover la ola
    private void Move()
    {
        transform.position += new Vector3(Direction * speed * Time.deltaTime, 0f, 0f);
        
        //Desaparecer la ola despues de x segundos
        _time += Time.deltaTime;
        
        if (_time > timeLimit)
        {
            Destroy(gameObject);
        }
    }
    
    //Si la ola interactua con el player
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            //TODO: Aniadir fuerza para empujar al player
        }
    }
}