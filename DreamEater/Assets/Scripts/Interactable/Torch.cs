/*
 Para los objetos que pueden ser interactuables y hay que hacer interactuables, en este caso con la antorcha
 TODO objeto que lleve torch TIENE que tener tambien el COMPONENTE INTERACTABLE
*/

using System.Collections;
using UnityEngine;

public class Torch : MonoBehaviour
{
    [SerializeField] private bool isActive; //esto hace referencia a la ANTORCHA NO al OBJETO
    [SerializeField] private Material material; //BORRAR UNA VEZ QUE YA ESTE ENTREGADO

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
            StartCoroutine(Dehcongelar(other));//BORRAR UNA VEZ QUE YA ESTE ENTREGADO
        }
    }

    private IEnumerator Dehcongelar(Collider other)//BORRAR UNA VEZ QUE YA ESTE ENTREGADO
    {
        yield return new WaitForSeconds(3);//BORRAR UNA VEZ QUE YA ESTE ENTREGADO
        other.GetComponent<Rigidbody>().isKinematic = false;//BORRAR UNA VEZ QUE YA ESTE ENTREGADO
        other.GetComponent<Renderer>().material = material; //BORRAR UNA VEZ QUE YA ESTE ENTREGADO
        other.transform.GetChild(0).gameObject.SetActive(false); //BORRAR UNA VEZ QUE YA ESTE ENTREGADO
    }

}
