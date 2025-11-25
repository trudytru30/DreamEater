using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(BoxCollider))]
public class TriggerTutorial : MonoBehaviour
{
    private ControlsManager cm;


    [Header("Sprites")]
    [SerializeField] private Sprite _keyboardControl;
    [SerializeField] private Sprite _gamepadControl;
    [SerializeField] private string _controlsMessage;

    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
        cm = ControlsManager.Instance; 
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cm.keyboardControl.sprite = _keyboardControl;
            cm.gamepadControl.sprite = _gamepadControl;
            cm.ActivateControlsUI(_controlsMessage);
        }
    }
    private void OnTriggerExit(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            cm.DeactivateControlsUI();
        }
    }
}
