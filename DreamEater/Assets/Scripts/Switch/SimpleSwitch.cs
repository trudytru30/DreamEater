/*
 Comprueba la posicion de un solo switch
 */

using UnityEngine;

public class SimpleSwitch : Switch
{
    [SerializeField] private bool correctPosition; //la posicion que el jugador debe poner

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
    
    protected override void CheckPosition()
    { 
        base.CheckPosition();
        if (correctPosition == isActive)
        {
            Debug.Log("correcto");
        }
    }
}