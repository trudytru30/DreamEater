/*
 Para los objetos que pueden ser interactuables y hay que hacer interactuables, en este caso con la antorcha
 TODO objeto que lleve torch TIENE que tener tambien el COMPONENTE INTERACTABLE
*/

using System.Collections;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] private bool isActive; //esto hace referencia a la ANTORCHA NO al OBJETO
    [SerializeField] private Material material; //material para mostrar el cambio de estado

    private void MakeInteractable(GameObject obj)
    {
        var interactable = obj.GetComponent<Interactable>();
        if (interactable != null)
        {
            interactable.SetCanInteract(true); //activa la opcion de interactuar
            Debug.Log("Se activó interactable en: " + obj.name);
        }
    }

    /*gestiona que sea en el rango de accion de la antorcha donde los objetos se hacen interactuables*/
    private void OnTriggerEnter(Collider other)
    {
        var interactable = other.GetComponent<Interactable>();
        if (interactable != null && !interactable.GetCanInteract())
        {
            MakeInteractable(other.gameObject);//hacer interactuable
            StartCoroutine(Unfreeze(other)); //activa el cambio de material
        }
    }

    private IEnumerator Unfreeze(Collider other)
    {
        yield return new WaitForSeconds(3);
        other.GetComponent<Rigidbody>().isKinematic = false;//quita al rb de kinematc para que se pueda mover
        other.GetComponent<Renderer>().material = material; //render del material nuevo en lugar del viejo
        other.transform.GetChild(0).gameObject.SetActive(false);
    }

}
