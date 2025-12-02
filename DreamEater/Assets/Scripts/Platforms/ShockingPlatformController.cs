/*Este script gestiona el cambio de plataforma de shock dentro de un array de ShockingPlatforms
 El funcionamiento de cada plataforma individual se gestion desde ShockingPlatform*/
using System.Collections;
using UnityEngine;

public class ShockingPlatformController : MonoBehaviour
{
    [SerializeField] private float changeTime; //tiempo de cooldown entre cambios de shock
    [SerializeField] private ShockingPlatform[] shockingPlatforms; //array de plataformas a gestionar

    //cambiar plataforma activa
    private void ChangeShock()
    {
        //recorre array
        for (int i = 0; i < shockingPlatforms.Length; i++)
        {
            shockingPlatforms[i].setCanShock(true); //activa la plataforma que toca
            if (i > 0)
            {
                shockingPlatforms[i - 1].setCanShock(false); //desactiva la plataforma anterior
            }
            StartCoroutine(TimeTilChange()); //llama a la corrutina entre vuelta y vuelta del for
        }
    }

    //corrutina para el cooldown
    IEnumerator TimeTilChange()
    {
        yield return new WaitForSeconds(changeTime);
    }

    //llama de forma continua a la funcion de cambio no hace mas
    private void Update()
    {
        ChangeShock();
    }
}
