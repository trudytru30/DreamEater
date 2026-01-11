/*
 La clase vinculada al objeto fisico de la palanca
*/

using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] private bool isActive; //inidca si esta activado, es serializable para el estado incial

    private float _lastInteractTime = -1f; 
    private const float _cooldown = 0.5f; // Tiempo de espera entre usos
    
    //cambia estado al interactuar
    private void Update()
    {
        // Cacheamos la referencia
        var interactable = this.gameObject.GetComponent<Interactable>();

        if (interactable != null && interactable.GetIsInteracting())
        {
            if (Time.time >= _lastInteractTime + _cooldown)
            {
                ChangeActive();
                
                _lastInteractTime = Time.time; 
                
                SwitchSubject.Notify(this);
            }
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
