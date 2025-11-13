using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ShockingPlatform : MonoBehaviour
{
    [SerializeField] private float changeTime;
    [SerializeField] private ShockingPlatform[] shockingPlatform;
    private int indexShockPlatform;

    private void ChangeShock()
    {
        for (int i = 0; i < shockingPlatform.Length; i++)
        {
            OnTriggerEnter(shockingPlatform[i]);
        }

        if (indexShockPlatform == shockingPlatform.Length)
        {
            indexShockPlatform = 0;
        }
    }

    IEnumerator TimeTilChange()
    {
        yield return new WaitForSeconds(changeTime);
    }

    private void Update()
    {
        ChangeShock();
        StartCoroutine(TimeTilChange());
    }
}
