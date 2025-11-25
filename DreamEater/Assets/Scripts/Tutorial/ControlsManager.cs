using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class ControlsManager : MonoBehaviour
{
    public static ControlsManager Instance { get; private set; }
    [SerializeField] public Text controlsMessage;
    [SerializeField] public Image keyboardControl;
    [SerializeField] public Image gamepadControl;
    [SerializeField] private Image slash;

    private void Start()
    {
        DeactivateControlsUI();
    }

    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(this);
        }
    }

    public void DeactivateControlsUI()
    {
        keyboardControl.enabled = false;
        gamepadControl.enabled = false;
        controlsMessage.enabled = false;
        slash.enabled = false;
    }

    public void ActivateControlsUI(string message)
    {
        controlsMessage.text = message;
        keyboardControl.enabled = true;
        gamepadControl.enabled = true;
        slash.enabled = true;


    }


}
