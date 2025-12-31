/*
 Comprueba la posicion de un solo switch
 */

using Unity.VisualScripting;
using UnityEngine;

public class SimpleSwitch : Switch
{
    [SerializeField] private bool correctPosition; //la posicion que el jugador debe poner

    protected override void CheckPosition()
    { ;
        //acceder a la funcion del padre
        if (correctPosition == isActive)
        {
            Debug.Log("correcto");
        }
    }
}