using Unity.VisualScripting;
using UnityEngine;

public class SimpleSwitch : Switch
{
    [SerializeField] private bool correctPosition;

    protected override void CheckPosition()
    {
        //acceder a la funcion del padre
        if (correctPosition == isActive)
        {
            Debug.Log("correcto");
        }
    }
}