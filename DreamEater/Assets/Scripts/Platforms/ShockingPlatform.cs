/*Este es el codigo de cada platforma inidivual
 No esta gestionado el cambio de plataforma para eso ver ShockingPlatformController*/
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ShockingPlatform : MonoBehaviour
{
    private bool canShock = false; //inidica si es la plataforma de shock activa
    
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player") && canShock) //comprueba que sea el player y que la plataforma este activa
        {
            collision.gameObject.GetComponent<PlayerController2>().Die(); //mata al player//llama al player controller 2 
        }
        return;
    }
 
    //getters y setters
    public void setCanShock(bool _canShock)
    {
        canShock = _canShock;
    }

    public bool getCanShock()
    {
        return canShock;
    }
}
