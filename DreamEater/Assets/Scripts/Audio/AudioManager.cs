using UnityEditor.PackageManager;
using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("AudioSources")]
    [SerializeField] private AudioSource _sfxAudioSource;
    [SerializeField] private AudioSource _musicAudioSource;
    [SerializeField] private AudioSource _dialogsAudioSource;

    [Header("UI")]
    [SerializeField] private AudioClip   _buttonSound;

    [Header("SFX")]
    [SerializeField] private AudioClip   _jumpSound;
    [SerializeField] private AudioClip   _agacharseSound;
    [SerializeField] private AudioClip[] _pasosNieve;
    [SerializeField] private AudioClip[] _pasosArena;
    [SerializeField] private AudioClip[] _pasosAgua;
    [SerializeField] private AudioClip[] _pasosBosque;
    [SerializeField] private AudioClip[] _pasos;
    [SerializeField] private AudioClip   _morirSound;


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


    public void PlayPasos()
    {
        string _sceneName = SceneManager.GetActiveScene().name;

        switch (_sceneName)
        {
            case "Level_0":
                int selec = Random.Range(0, _pasos.Length);
                _sfxAudioSource.clip = _pasosNieve[selec];
                _sfxAudioSource.Play();
                break;
            case "Level_1":
                int selec1 = Random.Range(0, _pasosArena.Length);
                _sfxAudioSource.clip = _pasosNieve[selec1];
                _sfxAudioSource.Play();
                break;
            case "Level_2":
                int selec2 = Random.Range(0, _pasosAgua.Length);
                _sfxAudioSource.clip = _pasosNieve[selec2];
                _sfxAudioSource.Play();
                break;
            case "Level_3":
                int selec3 = Random.Range(0, _pasosNieve.Length);
                _sfxAudioSource.clip = _pasosNieve[selec3];
                _sfxAudioSource.Play();
                break;
            case "Level_4":
                int selec4 = Random.Range(0, _pasosBosque.Length);
                _sfxAudioSource.clip = _pasosNieve[selec4];
                _sfxAudioSource.Play();
                break;
            case "Level_5":
                int selec5 = Random.Range(0, _pasos.Length);
                _sfxAudioSource.clip = _pasosNieve[selec5];
                _sfxAudioSource.Play();
                break;

        }
    }
    public void PlayAgacharse()
    {
        _sfxAudioSource.clip = _agacharseSound;
        _sfxAudioSource.Play();
    }
    public void PlayMorir()
    {
        _sfxAudioSource.clip = _morirSound;
        _sfxAudioSource.Play();
    }
}
