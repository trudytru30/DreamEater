using System.Collections;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager Instance {  get; private set; }

    #region PauseMenu
    [Header("PauseMenu")]
    //private int _qualityValue=2;
    private float _generalVolume = 1, _sfxVolume = 1, _musicVolume = 1, _dialogsVolume = 1;
    private bool _fullScreen = true;
    [SerializeField] private Slider _brightnessSlider;
    [SerializeField] private Slider _generalVolumeSlider;
    [SerializeField] private Slider _sfxVolumeSlider;
    [SerializeField] private Slider _musicVolumeSlider;
    [SerializeField] private Slider _dialogsVolumeSlider;
    [SerializeField] private Toggle _fullScreenToggle;
    [SerializeField] private Image _brightPanel;
    [SerializeField] private Image _darkPanel;
    [SerializeField] private GameObject _brightnessCanvas;
    [SerializeField] private GameObject _pauseCanvas;
    private bool isGamePaused;
    #endregion
    [Header("Debuger(Luego se borra)")]
    [SerializeField] private KeyCode _pauseControl;




    //Resolution Type
    //Type 0 = 1920 x 1080
    //Type 1 = 1280 x 720
    //Type 2 = 2560 x 1440


    private void Start()
    {
        ChangeResolution(0);
        ChangeBrightness(0.5f);
        FullScreen(true);
        isGamePaused = false;
        
    }

    private void OnEnable()
    {
        StartCoroutine(StartBrightness());
    }


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void Update()
    {
        if (Input.GetKeyDown(_pauseControl)) //Abrir menu de pausa
        {
            OpenClosePauseMenu();
        }
    }


    public void ChangeResolution(int resolutionType)
    {
        switch(resolutionType)
        {
            case 0:
                Screen.SetResolution(1920, 1080, _fullScreen);
                break;
            case 1:
                Screen.SetResolution(1280,720,_fullScreen);
                break;
            case 2:
                Screen.SetResolution(2560,1440,_fullScreen);
                break;
            default: return;
        }
    }
    public void ChangeBrightness(float bright)
    {
        if(bright < 0.5f)
        {
            _brightPanel.enabled = false;
            _darkPanel.enabled = true;
            Color c =_darkPanel.color;
            c.a = (1-bright)*0.9f;
            Debug.Log((1 - bright) * 0.9f);
            _darkPanel.color = c;
        }
        else
        {
            _darkPanel.enabled = false;
            _brightPanel.enabled = true;
            Color c = _brightPanel.color;
            c.a = (bright-0.5f)*0.039f;
            Debug.Log((bright - 0.5f) * 0.039f);
            _brightPanel.color = c;
        }
        
    }
    public void FullScreen(bool fullScreen)
    {
        _fullScreen = fullScreen;
        Screen.fullScreen = fullScreen;
    }
    public void SetSlidersValue()
    {
        _dialogsVolumeSlider.value = _dialogsVolume;
        _generalVolumeSlider.value = _generalVolume;
        _musicVolumeSlider.value = _musicVolume;
        _fullScreenToggle.isOn = _fullScreen;
        
    }
    public void SaveBrightnessData()
    {
        var bp = _brightPanel.color;
        GameManager.Instance.lightBrightnessFloat = bp.a;
        GameManager.Instance.lightBrightnessActive = _brightPanel.enabled;

        var dp = _darkPanel.color;
        GameManager.Instance.darkBrightnessFloat = dp.a;
        GameManager.Instance.darkBrightnessActive= _darkPanel.enabled;
    }
    private void OpenClosePauseMenu()
    {
        _pauseCanvas.SetActive(!isGamePaused);
        isGamePaused = !isGamePaused;
        if (isGamePaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale=1;
        }
    }
    public void RestartLevel()
    {
        var actualScene = SceneManager.GetActiveScene();
        var actualSceneName = actualScene.name;

        SceneManager.LoadScene(actualSceneName);

    }
    public void QuitGame()
    {
        Application.Quit();
    }
    public void QuitToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public IEnumerator StartBrightness()
    {
        yield return new WaitForSeconds(0.01f);
        _brightPanel.enabled = GameManager.Instance.lightBrightnessActive;
        var bp = _brightPanel.color;
        bp.a = GameManager.Instance.lightBrightnessFloat;
        _brightPanel.color = bp;

        _darkPanel.enabled = GameManager.Instance.darkBrightnessActive;
        var dp = _darkPanel.color;
        dp.a = GameManager.Instance.darkBrightnessFloat;
        _darkPanel.color = dp;
    }

    
}
