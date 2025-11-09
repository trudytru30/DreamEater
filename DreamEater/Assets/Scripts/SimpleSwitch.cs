using UnityEngine;

public class SimpleSwitch : Switch
{
    [SerializeField] private bool correctPosition;

    protected override void CheckPosition()
    {
        if (correctPosition == isActive)
        {
            Debug.Log("correcto");
        }
    }
}