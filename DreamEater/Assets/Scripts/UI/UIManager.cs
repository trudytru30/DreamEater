using System.Collections;
using UnityEngine;
using UnityEngine.Audio;
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
    [SerializeField] private Slider brightnessSlider;
    [SerializeField] private Toggle fullScreenToggle;
    [SerializeField] private Image brightPanel;
    [SerializeField] private Image darkPanel;
    [SerializeField] private GameObject brightnessCanvas;
    [SerializeField] private GameObject pauseCanvas;
    private bool _isGamePaused;
    #endregion
    [Header("Debuger(Luego se borra)")]
    [SerializeField] private KeyCode pauseControl;
    
    [Header("Die")]
    [SerializeField] private GameObject initDie;
    [SerializeField] private GameObject finishDie;
    
    [Header("AudioSources")]
    [SerializeField] private AudioMixer audioMixer;
    
    //Resolution Type
    //Type 0 = 1920 x 1080
    //Type 1 = 1280 x 720
    //Type 2 = 2560 x 1440
    
    private void Start()
    {
        ChangeResolution(0);
        ChangeBrightness(0.5f);
        FullScreen(true);
        _isGamePaused = false;
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
        if (Input.GetKeyDown(pauseControl)) //Abrir menu de pausa
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
        Debug.Log(Screen.currentResolution);
    }
    
    public void ChangeBrightness(float bright)
    {
        if (bright < 0.5f)
        {
            brightPanel.enabled = false;
            darkPanel.enabled = true;
            Color c = darkPanel.color;
            c.a = (1 - bright) * 0.9f;
            Debug.Log((1 - bright) * 0.9f);
            darkPanel.color = c;
        }
        else
        {
            darkPanel.enabled = false;
            brightPanel.enabled = true;
            Color c = brightPanel.color;
            c.a = (bright - 0.5f) * 0.039f;
            Debug.Log((bright - 0.5f) * 0.039f);
            brightPanel.color = c;
        }
    }
    
    public void ChangeQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
    
    public void FullScreen(bool fullScreen)
    {
        _fullScreen = fullScreen;
        Screen.fullScreen = fullScreen;
    }
    
    public void SaveBrightnessData()
    {
        var bp = brightPanel.color;
        GameManager.Instance.lightBrightnessFloat = bp.a;
        GameManager.Instance.lightBrightnessActive = brightPanel.enabled;

        var dp = darkPanel.color;
        GameManager.Instance.darkBrightnessFloat = dp.a;
        GameManager.Instance.darkBrightnessActive= darkPanel.enabled;
    }
    
    public void OpenClosePauseMenu()
    {
        pauseCanvas.SetActive(!_isGamePaused);
        _isGamePaused = !_isGamePaused;
        if (_isGamePaused)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale=1;
        }
        CursorManager.Instance.ShowCursor(_isGamePaused);
    }
    public void RestartLevel()
    {
        Time.timeScale = 1;
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
        brightPanel.enabled = GameManager.Instance.lightBrightnessActive;
        var bp = brightPanel.color;
        bp.a = GameManager.Instance.lightBrightnessFloat;
        brightPanel.color = bp;

        darkPanel.enabled = GameManager.Instance.darkBrightnessActive;
        var dp = darkPanel.color;
        dp.a = GameManager.Instance.darkBrightnessFloat;
        darkPanel.color = dp;
    }

    public void InitDieCharacter()
    {
        finishDie.SetActive(false);
        initDie.SetActive(true);
    }

    public void FinishDieCharacter()
    {
        initDie.SetActive(false);
        finishDie.SetActive(true);
    }

    public void ChangeSFXVolume(float volume)
    {
        audioMixer.SetFloat("VolumeEfectos", volume);
    }

    public void ChangeMusicVolume(float volume)
    {
        audioMixer.SetFloat("VolumeMusica", volume);
    }

    public void ChangeGeneralVolume(float volume)
    {
        audioMixer.SetFloat("VolumeGeneral", volume);
    }
}
