/*
 NO CONFUNDIR CON SITCHES/INTERRUPTORES
 Se pone una animacion para hacer la simulacion para conseguir un resultado realista
 No se hace por fisicas el obejto no se lanza como tal
*/

using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private Animator anim; //animacion de lanzamiiento
    [SerializeField] private Transform socket;
    
    private void OnTriggerEnter(Collider other)
    {
        //comprueba que el otro objeto sea player
        if (other.tag == "Player")
        {
            anim.enabled = true;
            Destroy(gameObject);
        }
    }
}
