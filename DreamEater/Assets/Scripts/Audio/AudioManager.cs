using UnityEngine;
using UnityEngine.SceneManagement;

public class AudioManager : MonoBehaviour
{
    public static AudioManager Instance { get; private set; }

    [Header("AudioSources")]
    [SerializeField] private AudioSource sfxAudioSource;
    [SerializeField] private AudioSource musicAudioSource;

    [Header("UI")]
    [SerializeField] private AudioClip   buttonSound;

    [Header("SFX")]
    [SerializeField] private AudioClip   jumpSound;
    [SerializeField] private AudioClip   agacharseSound;
    [SerializeField] private AudioClip   morirSound;
    [SerializeField] private AudioClip   pickUpSound;
    [SerializeField] private AudioClip   palanca;
    [SerializeField] private AudioClip[] pasosNieve;
    [SerializeField] private AudioClip[] pasosArena;
    [SerializeField] private AudioClip[] pasosAgua;
    [SerializeField] private AudioClip[] pasosBosque;
    [SerializeField] private AudioClip[] pasos;
    
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

    public void PlayPickUp()
    {
        sfxAudioSource.clip = pickUpSound;
        sfxAudioSource.Play();
    }
    public void PlayPasos()
    {
        string _sceneName = SceneManager.GetActiveScene().name;

        switch (_sceneName)
        {
            case "Level_0":
                int selec = Random.Range(0, pasos.Length);
                sfxAudioSource.clip = pasosNieve[selec];
                sfxAudioSource.Play();
                break;
            case "Level_1":
                int selec1 = Random.Range(0, pasosArena.Length);
                sfxAudioSource.clip = pasosNieve[selec1];
                sfxAudioSource.Play();
                break;
            case "Level_2":
                int selec2 = Random.Range(0, pasosAgua.Length);
                sfxAudioSource.clip = pasosNieve[selec2];
                sfxAudioSource.Play();
                break;
            case "Level_3":
                int selec3 = Random.Range(0, pasosNieve.Length);
                sfxAudioSource.clip = pasosNieve[selec3];
                sfxAudioSource.Play();
                break;
            case "Level_4":
                int selec4 = Random.Range(0, pasosBosque.Length);
                sfxAudioSource.clip = pasosNieve[selec4];
                sfxAudioSource.Play();
                break;
            case "Level_5":
                int selec5 = Random.Range(0, pasos.Length);
                sfxAudioSource.clip = pasosNieve[selec5];
                sfxAudioSource.Play();
                break;
        }
    }
    
    public void PlayAgacharse()
    {
        sfxAudioSource.clip = agacharseSound;
        sfxAudioSource.Play();
    }
    
    public void PlayMorir()
    {
        sfxAudioSource.clip = morirSound;
        sfxAudioSource.Play();
    }
    
    public void PlayJump()
    {
        sfxAudioSource.clip = jumpSound;
        sfxAudioSource.Play();
    }
    
    public void PlayButton()
    {
        sfxAudioSource.clip = buttonSound;
        sfxAudioSource.Play();
    }
    
    public void PlayPalanca()
    {
        sfxAudioSource.clip = palanca;
        sfxAudioSource.Play();
    }
}
