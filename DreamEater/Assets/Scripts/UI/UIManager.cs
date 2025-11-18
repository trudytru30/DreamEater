using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;
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
    #endregion




    //Resolution Type
    //Type 0 = 1920 x 1080
    //Type 1 = 1280 x 720
    //Type 2 = 2560 x 1440


    private void Start()
    {
        ChangeResolution(0);
        ChangeBrightness(0.5f);
        FullScreen(true);
        
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
            _brightPanel.gameObject.SetActive(false);
            _darkPanel.gameObject.SetActive(true);
            Color c =_darkPanel.color;
            c.a = bright * 10f;
            _darkPanel.color = c;
        }
        else
        {
            _darkPanel.gameObject.SetActive(false);
            _brightPanel.gameObject.SetActive(true);
            Color c = _brightPanel.color;
            c.a = bright * 10f;
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

    
}
