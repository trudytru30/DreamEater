using System;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine;

public class ShockingPlatform : MonoBehaviour
{
    [SerializeField] private float changeTime;
    [SerializeField] private ShockingPlatform[] shockingPlatforms;
    private ShockingPlatform currentShockingPlatform;
    private int indexShockPlatform;

    private void ChangeShock()
    {
        for (int i = 0; i < shockingPlatforms.Length; i++)
        {
            indexShockPlatform = i;
            currentShockingPlatform = shockingPlatforms[indexShockPlatform];
        }

        if (indexShockPlatform == shockingPlatforms.Length)
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

    private void OnTriggerEnter(Collider other)
    {
        if (this.gameObject == currentShockingPlatform.gameObject) //si la plataforma es la que electrocuta entra al if
        {
            if (other.CompareTag("Player"))
            {
                other.GetComponent<Player>().Die(); //mata al player
            }
        }
    }
}
