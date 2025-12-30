/*Esta horientado a cada plataforma no funciona sobre un array CADA PLATAFORMA ES INDIVIUAL
 NO se puede hacer setActive porque detiene la corrutina, se juega con la variacion de visibilidad del MeshRenderer y del collider
 */

using System.Collections;
using UnityEngine;

public class BlinkingPlatform : MonoBehaviour
{
    [SerializeField] private bool startsActive; //determina el estado INICIAL on/off
    [SerializeField] private float blinkingTime; //cooldown entre on/off
    private bool _isActive; //indica si esta ON

    //setea configuracion inical
    private void Start()
    {
        _isActive = startsActive;
        this.gameObject.GetComponent<MeshRenderer>().enabled = _isActive; //setea visibilidad
        this.gameObject.GetComponent<Collider>().enabled = _isActive; //setea collider
        StartCoroutine(Blink()); //no va en update porque no se para ese hilo
    }

    //cambia el estado de la plataforma
    private IEnumerator Blink()
    {
        while (true) //para que se repita de forma constante
        {
            yield return new WaitForSeconds(blinkingTime);

            _isActive = !_isActive; // cambia el estado al contrario de on/off
            
            //setea el cambio de estado
            this.gameObject.GetComponent<MeshRenderer>().enabled = _isActive;
            this.gameObject.GetComponent<Collider>().enabled = _isActive;
        }
    }
    
}