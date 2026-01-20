/*
 Este es el Spawner de los enemies de tipo EnemyWaves
 Crea isntacias de ese objeto en la escena cualquier modificacion a la coniguracion de los EnemyWaves se ha de hacer en su clase propia
*/

using System.Collections;
using UnityEngine;

public class EnemyWavesSpawner : MonoBehaviour
{
    [SerializeField] private float cooldown; // Tiempo entre ola y ola, nada que ver con el lifetime
    [SerializeField] private EnemyWaves instance; // Poner el prefab de la ola para que cree la instancia

    // Gestiona el proceso de instaciamiento
    private void Start()
    {
        StartCoroutine(InstanceWaves());
    }

    // Gestiona tiempo el cooldown
    IEnumerator InstanceWaves()
    {
        while (true)
        {
            Instantiate(instance, this.gameObject.transform.position, Quaternion.identity);
            yield return new WaitForSeconds(cooldown);
        }
    }
}