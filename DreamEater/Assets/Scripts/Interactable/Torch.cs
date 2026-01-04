/*
 Para los objetos que pueden ser interactuables y hay que hacer interactuables, en este caso con la antorcha
 TODO objeto que lleve torch TIENE que tener tambien el COMPONENTE INTERACTABLE
*/

using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] private bool isActive; //esto hace referencia a la ANTORCHA NO al OBJETO

    private void MakeInteractable(GameObject obj)
    {
        //gameObject.GetComponent<Interactable>().SetCanInteract(true); //activa la opcion de interactuar
        var interactable = obj.GetComponent<Interactable>();
        if (interactable != null)
        {
            interactable.SetCanInteract(true);
            Debug.Log("Se activó interactable en: " + obj.name);
        }
    }

    /*gestiona que sea en el rango de accion de la antorcha donde los objetos se hacen interactuables*/
    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<Interactable>();
        if (interactable != null && !interactable.GetCanInteract())
        {
            MakeInteractable(other.gameObject);//MakeInteractable();
        }
    }

}
