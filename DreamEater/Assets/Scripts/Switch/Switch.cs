/*Es una clase abstracta NO SE PUEDE INSTANCIAR/PONER COMO COMPONENTE
 Es la clase padre de los interruptores tanto en patron como simples
 Gestiona los factores comunes de todos los switches es la clase padre*/

using UnityEngine;

public abstract class Switch : MonoBehaviour
{
    [SerializeField] protected bool isActive; //inidca si esta activado, es serializable para el estado incial
    private bool isButton;

    //cmabia el estado
    public void ChangeActive()
    {
        if (isActive)
        {
            isActive = false;
            if (!isButton)
            {
                this.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
            }
            else
            {
                //cambiar posicion si es boton
            }
        }
        else if (!isActive)
        {
            isActive = true;
            if (!isButton)
            {
                this.gameObject.transform.rotation = Quaternion.Euler(180, 0, 0);
            }
            else
            {
                //cambiar posicion si es boton
            }
        }
    }
    //getters y setters
    public void SetIsActive(bool  _isActive)
    {
        isActive = _isActive;
    }

    public bool GetIsActive()
    {
        return isActive;
    }

    //se completa en los hijos y se usa para comprobar la posicion
    protected virtual void CheckPosition()
    {
        //comprueba que son interactuables
        if (!gameObject.GetComponent<Interactable>().GetCanInteract())
        {
            return;
        }
    }
}
