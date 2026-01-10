/*
 La clase vinculada al objeto fisico de la palanca
*/

using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private bool isActive; //inidca si esta activado, es serializable para el estado incial

    //cambia estado al interactuar
    private void Update()
    {
        if (this.gameObject.GetComponent<Interactable>().GetIsInteracting())
        {
            ChangeActive();
            //lanzar llamada al observer para comprobar estado de palancas
            SwitchSubject.Notify(this);
        }
    }

    //cambia el estado
    private void ChangeActive()
    {
        if (isActive)
        {
            isActive = false;
            this.gameObject.transform.rotation = Quaternion.Euler(-180, 0, 0);
        }
        else if (!isActive)
        {
            isActive = true;
            this.gameObject.transform.rotation = Quaternion.Euler(180, 0, 0);
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
}
