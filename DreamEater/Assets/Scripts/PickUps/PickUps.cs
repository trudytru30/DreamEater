using UnityEngine;

public class PickUps : MonoBehaviour
{
    public enum ItemType { Bellota }  // Tipo de ítem
    [SerializeField] private ItemType itemType;  // Tipo de ítem específico

    private void OnTriggerEnter(Collider collision)
    {
        PlayerController2 player = collision.GetComponent<PlayerController2>();

        if (player != null)
        {
            switch (itemType)
            {
                case ItemType.Bellota:
                    AddToUI(ItemType.Bellota);
                    break;
            }
        }

        // Destruir el objeto después de que el jugador lo recoge
        Destroy(gameObject);
    }

    private void AddToUI(ItemType itemType)
    {
        // Mostrar contador en la UI y actualizar x/total
        UIPickupCounter[] counters = FindObjectsOfType<UIPickupCounter>();
        for (int i = 0; i < counters.Length; i++)
        {
            counters[i].RegisterPickup(itemType);
        }
    }
}

