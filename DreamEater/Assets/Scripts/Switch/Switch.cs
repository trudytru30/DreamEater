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
        // Cacheamos la referencia
        var interactable = this.gameObject.GetComponent<Interactable>();

        if (interactable != null && interactable.GetIsInteracting())
        {
            ChangeActive();
            SwitchSubject.Notify(this);
            
            // CORRECCIÓN VITAL: "Consumimos" el input para que no parpadee
            interactable.SetIsInteracting(false);
        }
    }

    //cambia el estado
    private void ChangeActive()
    {
        if (isActive)
        {
            Debug.Log("isNOTActive");
            isActive = false;
            this.gameObject.transform.rotation = Quaternion.Euler(-45, 0, 0);
        }
        else if (!isActive)
        {
            Debug.Log("isActive");
            isActive = true;
            this.gameObject.transform.rotation = Quaternion.Euler(45, 0, 0);
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
