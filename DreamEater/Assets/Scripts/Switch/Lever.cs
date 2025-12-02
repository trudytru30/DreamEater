/*NO CONFUNDIR CON SITCHES/INTERRUPTORES
 Se necesita un pivote o algo sobre lo que hacer palanca no se puede hacer en el aire por fisica el rb se cae
 Fisicamente se aplica el principio de palanca sobre otro rigidbody*/

using Unity.VisualScripting;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private float force; //fuerza que aplica la palanca
    [SerializeField] private Rigidbody rb; //para las fisicas tiene que tener componente rigidboy PROPIO NO CON LO QUE SE VA A INTERWACTUAR
    
    private void OnTriggerEnter(Collider other)
    {
        //comprueba que el otro objeto sea player
        if (other.tag == "Player")
        {
            rb.AddForce(Vector3.down,ForceMode.Force); //aplica la fuerza hacia abajo, principio de palanca
        }
    }
}
