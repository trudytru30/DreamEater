/*Esta horientado a cada plataforma no funciona sobre un array CADA PLATAFORMA ES INDIVIUAL*/
using System.Collections;
using UnityEngine;

public class BlinkingPlatform : MonoBehaviour
{
    [SerializeField] private bool startsActive; //determina el estado INICIAL on/off
    [SerializeField] private float blinkingTime; //cooldown entre on/off
    private bool isActive; //indica si esta ON

    //setea configuracion inical
    private void Start()
    {
        isActive = startsActive;
        this.gameObject.SetActive(isActive);
        StartCoroutine(Blink()); //no va en update porque no se para ese hilo
    }

    //cambia el estado de la plataforma
    private IEnumerator Blink()
    {
        while (true) //para que se repita de forma constante
        {
            yield return new WaitForSeconds(blinkingTime);

            isActive = !isActive; // cambia el estado al contrario de on/off
            this.gameObject.SetActive(isActive); //realiza el cambio visual de estado
        }
    }
}