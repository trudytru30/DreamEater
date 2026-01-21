/*
 Comprueba un conjunto de switches
 */

using UnityEngine;

public class PaternSwitch : MonoBehaviour
{
    [Header("Configuración")]
    [SerializeField] private Switch[] totalSwitches; //numero del conjunto de interruptores
    [SerializeField] private bool[] correctPositions; //position correcta de cada switch del array TIENEN QUE TENER MISMO TAMAÑO

    [Header("Puerta")]
    [SerializeField] private OpenDoor doorToSolve; //referencia al script de la puerta

    private void OnEnable()
    {
        SwitchSubject.OnSwitchStateChanged += OnSwitchStateChanged;
    }

    private void OnDisable()
    {
        SwitchSubject.OnSwitchStateChanged -= OnSwitchStateChanged;
    }

    private void OnSwitchStateChanged(Switch changedSwitch)
    {
        CheckPosition();
    }

    private void CheckPosition()
    { 
        if (totalSwitches.Length != correctPositions.Length) return;
        //recorre el array comprobando posiciones
        for (int i = 0; i < totalSwitches.Length; i++)
        {
            if (totalSwitches[i] == null) return;
            //si una posicion no es correcta no sigue comprobando
            if (totalSwitches[i].GetIsActive() != correctPositions[i])
            {
                return;
            }
        }
        
        if (doorToSolve != null)
        {
            Debug.Log("ABRIENDO PUERTA");
            doorToSolve.ActivateLever();
        }
    }
}