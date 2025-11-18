using UnityEngine;
using UnityEngine.UI;


[RequireComponent(typeof(BoxCollider))]
public class TriggerTutorial : MonoBehaviour
{
    [Header("UI GameObjects")]
    [SerializeField] GameObject i_keyboardControl;
    [SerializeField] GameObject i_gamepadControl;
    [SerializeField] GameObject i_slash;


    [Header("Sprites")]
    [SerializeField] Sprite _keyboardControl;
    [SerializeField] Sprite _gamepadControl;

    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger = true;
    }

    private void OnTriggerEnter(Collider other)
    {
        i_keyboardControl.GetComponent<Image>().sprite = _keyboardControl;
        i_gamepadControl.GetComponent<Image>().sprite= _gamepadControl;

        i_gamepadControl.SetActive(true);
        i_keyboardControl.SetActive(true);
        i_slash.SetActive(true);

    }
    private void OnTriggerExit(Collider other)
    {
        i_gamepadControl.SetActive(false);
        i_keyboardControl.SetActive(false);
        i_slash.SetActive(false);
    }
}
