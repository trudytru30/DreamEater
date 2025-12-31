/*
 Comprueba un conjunto de switches
 */

using UnityEngine;

public class PaternSwitch : Switch
{
    [SerializeField] private Switch[] totalSwitches; //numero del conjunto de interruptores
    [SerializeField] private bool[] correctPositions; //position correcta de cada switch del array TIENEN QUE TENER MISMO TSMSÑO

    protected override void CheckPosition()
    {
        //acceder a la funcion del padre
        //recorre el array comprobando posiciones
        for (int i = 0; i < totalSwitches.Length; i++)
        {
            //si una posicion no es correcta no sigue comprobando
            if (totalSwitches[i] != correctPositions[i])
            {
                return;
            }
        }
        
        Debug.Log("Correcto");
    }
}