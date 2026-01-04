/*
 Comprueba un conjunto de switches
 */

using System;
using UnityEngine;
using UnityEngine.PlayerLoop;

public class PaternSwitch : Switch
{
    [SerializeField] private Switch[] totalSwitches; //numero del conjunto de interruptores
    [SerializeField] private bool[] correctPositions; //position correcta de cada switch del array TIENEN QUE TENER MISMO TAMAÑO

    //intentar cambiar por un observer
    private void Update()
    {
        // CheckPosition();
    }

    private void OnEnable()
    {
        SwitchSubject.OnSwitchStateChanged += OnSwitchStateChanged;
        CheckPosition();
    }

    private void OnDisable()
    {
        SwitchSubject.OnSwitchStateChanged -= OnSwitchStateChanged;
    }

    private void OnSwitchStateChanged(Switch _)
    {
        CheckPosition();
    }

    protected override void CheckPosition()
    {
        base.CheckPosition();
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