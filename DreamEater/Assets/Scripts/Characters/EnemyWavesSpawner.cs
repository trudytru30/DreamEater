using System;
using System.Collections;
using UnityEngine;

public class EnemyWavesSpawner : MonoBehaviour
{
    [SerializeField] float cooldown;
    private EnemyWaves instance;

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
