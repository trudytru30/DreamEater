using System.Collections;
using UnityEngine;

public class ShockingPlatformController : MonoBehaviour
{
    [SerializeField] private float changeTime;
    [SerializeField] private ShockingPlatform[] shockingPlatforms;
    private int indexShockPlatform;

    private void ChangeShock()
    {
        for (int i = 0; i < shockingPlatforms.Length; i++)
        {
            indexShockPlatform = i;
            shockingPlatforms[indexShockPlatform].setCanShock(true);
            if (i > 0)
            {
                shockingPlatforms[indexShockPlatform - 1].setCanShock(false);
            }
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
}
