using UnityEngine;
using UnityEngine.UI;

[RequireComponent(typeof(BoxCollider))]
public class TriggerTutorial : MonoBehaviour
{
    private ControlsManager _cm;

    [Header("Sprites")]
    [SerializeField] private Sprite keyboardControl;
    [SerializeField] private Sprite gamepadControl;
    [SerializeField] private string controlsMessage;

    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        _cm = ControlsManager.Instance; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _cm.keyboardControl.sprite = keyboardControl;
            _cm.gamepadControl.sprite = gamepadControl;
            _cm.ActivateControlsUI(controlsMessage);
        }
    }
    
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _cm.DeactivateControlsUI();
        }
        Debug.Log("Salido");
    }
}