/*
 Este es el Spawner de los enemies de tipo EnemyWaves
 Crea isntacias de ese objeto en la escena cualquier modificacion a la coniguracion de los EnemyWaves se ha de hacer en su clase propia
*/

using System.Collections;
using UnityEngine;

public class EnemyWavesSpawner : MonoBehaviour
{
    [SerializeField] private float cooldown; //tiempo entre ola y ola, nada que ver con el lifetime
    [SerializeField] private EnemyWaves instance; //poner el prefab de la ola para que cree la instancia
    private bool isWaiting = false; // Variable para controlar el flujo

    private void Update()
    {
        // Si no estamos esperando, disparamos la ola
        if (!isWaiting)
        {
            StartCoroutine(SpawnRoutine());
        }
    }

    IEnumerator SpawnRoutine()
    {
        isWaiting = true; // Bloqueamos el spawner
        
        Instantiate(instance, transform.position, Quaternion.identity);
        
        yield return new WaitForSeconds(cooldown); // Esperamos el tiempo definido
        
        isWaiting = false; // Liberamos el spawner
    }
}
