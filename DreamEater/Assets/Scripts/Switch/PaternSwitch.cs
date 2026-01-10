/*
 Comprueba un conjunto de switches
 */

using UnityEngine;

public class PaternSwitch : MonoBehaviour
{
    [SerializeField] private Switch[] totalSwitches; //numero del conjunto de interruptores
    [SerializeField] private bool[] correctPositions; //position correcta de cada switch del array TIENEN QUE TENER MISMO TAMAÑO

    private void OnEnable()
    {
        SwitchSubject.OnSwitchStateChanged += OnSwitchStateChanged;
        CheckPosition();
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
        //comprueba que son interactuables
        if (!gameObject.GetComponent<Interactable>().GetCanInteract())
        {
            return;
        }
        //recorre el array comprobando posiciones
        for (int i = 0; i < totalSwitches.Length; i++)
        {
            //si una posicion no es correcta no sigue comprobando
            if (totalSwitches[i].GetIsActive() != correctPositions[i])
            {
                return;
            }
        }
        
        Debug.Log("Correcto");
    }
}