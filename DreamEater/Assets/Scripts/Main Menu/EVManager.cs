using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class EVManager : MonoBehaviour
{
    [Header("Canvas References")]
    public GameObject mainMenuCanvas;
    public GameObject optionsMenuCanvas;
    public GameObject controlsMenuCanvas;

    [Header("Initial Buttons")]
    public Button mainMenuFirstButton;
    public Button optionsMenuFirstButton;
    public Button controlsMenuFirstButton;

    private void Start()
    {
        SelectButton(mainMenuFirstButton);
    }

    public void OpenOptions()
    {
        mainMenuCanvas.SetActive(false);
        optionsMenuCanvas.SetActive(true);

        SelectButton(optionsMenuFirstButton);
    }

    public void OpenControls()
    {
        optionsMenuCanvas.SetActive(false);
        controlsMenuCanvas.SetActive(true);

        SelectButton(controlsMenuFirstButton);
    }

    public void BackToMainMenu()
    {
        controlsMenuCanvas.SetActive(false);
        optionsMenuCanvas.SetActive(false);
        mainMenuCanvas.SetActive(true);

        SelectButton(mainMenuFirstButton);
    }

    public void BackToOptions()
    {
        controlsMenuCanvas.SetActive(false);
        optionsMenuCanvas.SetActive(true);

        SelectButton(optionsMenuFirstButton);
    }

    private void SelectButton(Button button)
    {
        EventSystem.current.SetSelectedGameObject(null);    
        EventSystem.current.SetSelectedGameObject(button.gameObject);
    }
}
