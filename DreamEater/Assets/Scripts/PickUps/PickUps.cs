/*
 Este script se encarga de recoger el pickUp y llamar al metodo que lo muestra en la UI
 Mostrarlo en la UI se hace desde el script de UIPickupsCounter
 Todos los tipos de items distintos se han de añadir en ItemType y luego el case en el switch
 */

using UnityEngine;

public class PickUps : MonoBehaviour
{
    public enum ItemType { Bellota }  // tipo de ítem
    [SerializeField] private ItemType itemType;  // tipo de ítem específico

    private void OnTriggerEnter(Collider collision)
    {
        // comprobacion de que es el player
        if (collision.GetComponent<PlayerController2>() != null)
        {
            switch (itemType) //se mantiene el switch para escalabilidad
            {
                case ItemType.Bellota:
                    AddToUI(ItemType.Bellota); //pasa el tipo para la UI
                    break;
            }
        }

        // destruye el objeto
        Destroy(gameObject);
    }

    private void AddToUI(ItemType itemType)
    {
        // mostrar contador en la UI y actualizar x/total
        UIPickupCounter[] counters = FindObjectsOfType<UIPickupCounter>();
        for (int i = 0; i < counters.Length; i++)
        {
            counters[i].RegisterPickup(itemType);
        }
    }
}

