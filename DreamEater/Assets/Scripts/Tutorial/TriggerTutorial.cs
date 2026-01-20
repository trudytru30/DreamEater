using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class TriggerTutorial : MonoBehaviour
{
    private ControlsManager _controlsManager;

    [Header("Sprites")]
    [SerializeField] private Sprite keyboardControl;
    [SerializeField] private Sprite gamepadControl;
    [SerializeField] private string controlsMessage;

    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        _controlsManager = ControlsManager.Instance; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _controlsManager.keyboardControl.sprite = keyboardControl;
            _controlsManager.gamepadControl.sprite = gamepadControl;
            _controlsManager.ActivateControlsUI(controlsMessage);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _controlsManager.DeactivateControlsUI();
        }
        Debug.Log("Salido");
    }
}