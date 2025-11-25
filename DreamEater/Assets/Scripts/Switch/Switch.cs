using UnityEngine;

public class Switch : MonoBehaviour
{
    [SerializeField] protected bool isActive;

    public void ChangeActive()
    {
        if (isActive)
        {
            isActive = false;
            //cambiar posicion desactivado
        }
        else if (!isActive)
        {
            isActive = true;
            //cambiar posicion activo
        }
    }

    public void SetIsActive(bool  _isActive)
    {
        isActive = _isActive;
    }

    public bool GetIsActive()
    {
        return isActive;
    }

    protected virtual void CheckPosition()
    {
        if (!gameObject.GetComponent<Interactable>().GetCanInteract())
        {
            return;
        }
    }
}
