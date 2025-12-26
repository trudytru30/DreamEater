using UnityEngine;
using UnityEngine.UI;

public class UIPickupCounter : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Text counterText;          // Texto que mostrará x/total
    [SerializeField] private GameObject rootToShow;     // Lo que se oculta/enseña (puede ser este mismo GO)

    [Header("Config")]
    [SerializeField] private PickUps.ItemType itemTypeToTrack; // Bellota o Music
    [SerializeField] private int totalRequired = 4;            // <- SERIALIZABLE (4 o 7)
    
    private int current = 0;
    private bool isVisible = false;

    private void Awake()
    {
        if (rootToShow == null) rootToShow = gameObject;

        // Oculto al empezar el nivel
        rootToShow.SetActive(false);
        isVisible = false;

        UpdateText();
    }

    public void RegisterPickup(PickUps.ItemType pickedType)
    {
        if (pickedType != itemTypeToTrack) return;

        if (!isVisible)
        {
            rootToShow.SetActive(true);
            isVisible = true;
        }

        current += 1;
        if (current < 0) current = 0;

        UpdateText();
    }

    private void UpdateText()
    {
        if (counterText == null) return;
        counterText.text = $"{current}/{totalRequired}";
    }
}