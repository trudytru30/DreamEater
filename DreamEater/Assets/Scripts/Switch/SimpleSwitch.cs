/*
 Comprueba la posicion de un solo switch
 */

using UnityEditor;
using UnityEngine;

public class SimpleSwitch : MonoBehaviour
{
    [SerializeField] private bool correctPosition; //la posicion que el jugador debe poner
    [SerializeField] private Switch switchObject;

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
        if (changedSwitch == this)
        {
            CheckPosition();
        }
    }
    
    private void CheckPosition()
    { 
        //comprueba que son interactuables
        if (!gameObject.GetComponent<Interactable>().GetCanInteract())
        {
            return;
        }
        if (correctPosition == switchObject.GetIsActive())
        {
            Debug.Log("correcto");
        } 
    }
}