using UnityEngine;

public class PickUps : MonoBehaviour
{
    public enum ItemType { Bellota, Music }  // Tipo de ítem
    [SerializeField] private ItemType itemType;  // Tipo de ítem específico

    private void OnTriggerEnter(Collider collision)
    {
        PlayerController player = collision.GetComponent<PlayerController>();

        if (player != null)
        {
            switch (itemType)
            {
                case ItemType.Bellota:
                    AddToUI(ItemType.Bellota);
                    break;
                case ItemType.Music:
                    AddToUI(ItemType.Bellota);
                    break;
            }
        }

        // Destruir el objeto después de que el jugador lo recoge
        Destroy(gameObject);
    }

    private void AddToUI(ItemType itemType)
    {
        //TODO: Mostrar items en la UI
    }
}
