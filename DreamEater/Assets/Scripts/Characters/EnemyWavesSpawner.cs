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

    //gestiona tiempo el cooldown
    IEnumerator Timer()
    {
        yield return new WaitForSeconds(cooldown);
    }

    //gestiona el proceso de instaciamiento
    private void Update()
    {
        Instantiate(instance, this.gameObject.transform.position, Quaternion.identity);
        StartCoroutine(Timer());
    }
}
