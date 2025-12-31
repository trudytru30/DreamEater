/*
 Aplica la fuerza al player, es lo que se configura como enemigo
 Es lo que se instancia en EnemyWavesSpawner, en el spawner no se pueden modificar sus porpiedades, solo las de spawn
 NO deberia ponerse un EnemyWave en la escena si se quieren poner waves se pone un Spawner
*/

using UnityEngine;

[RequireComponent(typeof(Collider))]
public class EnemyWaves : MonoBehaviour
{
    [SerializeField] private float speed = 3f;
    [SerializeField] private float force;
    [SerializeField] private float timeLimit = 5f;
    
    private float _time;
    private const int Direction = 1;

    private void Update()
    {
        Move();
    }
    
    //mover la ola
    private void Move()
    {
        transform.position += new Vector3(Direction * speed * Time.deltaTime, 0f, 0f);
        
        //desaparecer la ola despues de x segundos
        _time += Time.deltaTime;
        
        if (_time > timeLimit)
        {
            Destroy(gameObject);
        }
    }
    
    //si la ola interactua con el player
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            other.gameObject.GetComponent<Rigidbody>().AddForce(Vector3.forward, ForceMode.Force); //revisar que .fordward hace lo que debe sino probar a cambiar por .right
        }
    }
}