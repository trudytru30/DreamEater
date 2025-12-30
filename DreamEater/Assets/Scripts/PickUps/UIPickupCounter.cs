using UnityEngine;
using UnityEngine.UI;

public class UIPickupCounter : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Text counterText;          // Texto que mostrará x/total
    [SerializeField] private GameObject rootToShow;     // Lo que se oculta/enseña

    [Header("Config")]
    [SerializeField] private PickUps.ItemType itemTypeToTrack; // Bellota o Music
    [SerializeField] private int totalRequired; 
    
    private int _current;
    private bool _isVisible;

    private void Awake()
    {
        _current = 0;
        _isVisible = false;
        if (rootToShow == null)
        {
            rootToShow = gameObject;
        }

        // Oculto al empezar el nivel
        rootToShow.SetActive(false);
        _isVisible = false;

        UpdateText();
    }

    public void RegisterPickup(PickUps.ItemType pickedType)
    {
        if (pickedType != itemTypeToTrack)
        {
            return;
        }

        if (!_isVisible)
        {
            rootToShow.SetActive(true);
            _isVisible = true;
        }

        _current += 1;
        if (_current < 0)
        {
            _current = 0;
        }

        UpdateText();
    }

    private void UpdateText()
    {
        if (counterText == null) return;
        counterText.text = $"{_current}/{totalRequired}";
    }
}