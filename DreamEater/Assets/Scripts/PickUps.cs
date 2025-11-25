using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleItem : MonoBehaviour
{
    public enum ItemType { Bellota, Music }  // Tipo de ítem
    [SerializeField] private ItemType itemType;  // Tipo de ítem específico

    // Eliminar shieldValue para el diamante ya que no lo necesitamos aquí

    private void OnTriggerEnter(Collider collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            switch (itemType)
            {
                case ItemType.Bellota:
                    //show in UI
                    break;
                case ItemType.Music:
                    //show in UI
                    break;
            }
        }

        // Destruir el objeto después de que el jugador lo recoge
        Destroy(gameObject);
    }
}
