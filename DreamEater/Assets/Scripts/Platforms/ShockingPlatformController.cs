/*Este script gestiona el cambio de plataforma de shock dentro de un array de ShockingPlatforms
 El funcionamiento de cada plataforma individual se gestion desde ShockingPlatform*/
using System.Collections;
using UnityEngine;

public class ShockingPlatformController : MonoBehaviour
{
    [SerializeField] private float changeTime; //tiempo de cooldown entre cambios de shock
    [SerializeField] private ShockingPlatform[] shockingPlatforms; //array de plataformas a gestionar

    //llama de forma continua a la funcion de cambio no hace mas
    private void Start()
    {
        ChangeShock();
    }

    //cambiar plataforma activa
    private IEnumerator ChangeShock()
    {
        while (true)
        {
            //recorre array
            for (int i = 0; i < shockingPlatforms.Length; i++)
            {
                shockingPlatforms[i].setCanShock(true); //activa la plataforma que toca
                if (i > 0)
                {
                    shockingPlatforms[i - 1].setCanShock(false); //desactiva la plataforma anterior
                }
                yield return new WaitForSeconds(changeTime);
            }
        }
    }
}
