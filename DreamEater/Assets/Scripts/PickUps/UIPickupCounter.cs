using UnityEngine;
using UnityEngine.UI;

public class UIPickupCounter : MonoBehaviour
{
    [Header("UI")]
    [SerializeField] private Text counterText; //el texto de x/total
    [SerializeField] private GameObject rootToShow; //lo que se oculta/enseña

    [Header("Config")]
    [SerializeField] private PickUps.ItemType itemTypeToTrack; //el tipo de item en este caso bellota
    [SerializeField] private int totalRequired; //total de elementos que conseguir
    
    private int _current; //numero actual de elementos
    private bool _isVisible; //estado de visibilidad

    private void Awake()
    {
        //inicializacion de variables
        _current = 0;
        _isVisible = false;
        if (rootToShow == null)
        {
            rootToShow = gameObject;
        }

        //oculto al empezar el nivel
        rootToShow.SetActive(false);
        _isVisible = false;

        UpdateText();
    }

    //registra en la UI los pickUps recogidos
    public void RegisterPickup(PickUps.ItemType pickedType)
    {
        //comprueba que sea el tipo correcto para evitar errores
        if (pickedType != itemTypeToTrack)
        {
            return;
        }
        
        //actva la UI si no estaba visible
        if (!_isVisible)
        {
            rootToShow.SetActive(true);
            _isVisible = true;
        }

        _current += 1; //controla el indica mostrado
        
        //control de errores
        if (_current < 0)
        {
            _current = 0;
        }

        UpdateText(); 
    }

    //actualiza con cada cambio el texto mostrado
    private void UpdateText()
    {
        if (counterText == null) return;
        counterText.text = $"{_current}/{totalRequired}"; //concatena current con el total en el texto de la UI
    }
}