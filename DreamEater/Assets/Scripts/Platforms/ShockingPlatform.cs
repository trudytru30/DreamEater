using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ShockingPlatform : MonoBehaviour
{
    private bool canShock = false;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player") && canShock) //comprueba que sea el player y que la plataforma este activa
        {
            other.GetComponent<Player>().Die(); //mata al player
        }

        return;
    }

    public void setCanShock(bool _canShock)
    {
        canShock = _canShock;
    }

    public bool getCanShock()
    {
        return canShock;
    }
}
