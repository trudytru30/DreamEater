using System;
using System.Collections;
using UnityEngine;

public class EnemyWavesSpawner : MonoBehaviour
{
    [SerializeField] private float cooldown;
    [SerializeField] private EnemyWaves instance;

    IEnumerator Timer()
    {
        yield return new WaitForSeconds(cooldown);
    }

    private void Update()
    {
        Instantiate(instance, this.gameObject.transform.position, Quaternion.identity);
        StartCoroutine(Timer());
    }
}
