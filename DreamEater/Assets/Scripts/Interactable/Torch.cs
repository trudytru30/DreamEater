/*Para los objetos que pueden ser interactuables y hay que hacer interactuables, en este caso con la antorcha
 TODO objeto que lleve torch TIENE que tener tambien el COMPONENTE INTERACTABLE*/

using System;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] private bool isActive; //esto hace referencia a la ANTORCHA NO al OBJETO

    private void MakeInteractable()
    {
        gameObject.GetComponent<Interactable>().SetCanInteract(true); //activa la opcion de interactuar
    }

    /*gestiona que sea en el rango de accion de la antorcha donde los objetos se hacen interactuables*/
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<Interactable>().GetCanInteract() == false)
        {
            MakeInteractable();
        }
    }
}
