using UnityEngine;

public class PaternSwitch : Switch
{
    [SerializeField] private Switch[] totalSwitches;
    [SerializeField] private bool[] correctPositions;

    protected override void CheckPosition()
    {
        for (int i = 0; i < totalSwitches.Length; i++)
        {
            if (totalSwitches[i] != correctPositions[i])
            {
                return;
            }
        }
        
        Debug.Log("Correcto");
    }
}