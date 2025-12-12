/*Este es el codigo de cada platforma inidivual
 No esta gestionado el cambio de plataforma para eso ver ShockingPlatformController*/
using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ShockingPlatform : MonoBehaviour
{
    private bool canShock = false; //inidica si es la plataforma de shock activa
    private PlayerController2 playerOnTop;

    //registra al player colisionando con esa plataforma
    private void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            playerOnTop = collision.gameObject.GetComponent<PlayerController2>();
    }

    //desregistra al player colisionando con esa plataforma
    private void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Player"))
            playerOnTop = null;
    }

    //si el player esta registrado y la plataforma activa lo mata
    private void FixedUpdate()
    {
        if (canShock && playerOnTop != null)
            playerOnTop.Die();
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
