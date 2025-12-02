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
    }

    //solo llama a funciones no hace nada mas
    private void Update()
    {
        Blink();
        StartCoroutine(TimeTilBlink());
    }

    //cambia el estado de la plataforma
    private void Blink()
    {
        if (isActive)
        {
            isActive = false;
            this.gameObject.SetActive(isActive);
        }
        else if (!isActive)
        {
            isActive = true;
            this.gameObject.SetActive(isActive);
        }
    }

    //corrutina para el cooldown
    IEnumerator TimeTilBlink()
    {
        yield return new WaitForSeconds(blinkingTime);
    }
}