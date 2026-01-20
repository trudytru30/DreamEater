/*
 
 */

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
    [SerializeField] private AudioClip   crouchSound;
    [SerializeField] private AudioClip   dieSound;
    [SerializeField] private AudioClip   pickUpSound;
    [SerializeField] private AudioClip   lever;
    [SerializeField] private AudioClip[] stepsSnow;
    [SerializeField] private AudioClip[] stepsSand;
    [SerializeField] private AudioClip[] stepsWater;
    [SerializeField] private AudioClip[] stepsForest;
    [SerializeField] private AudioClip[] steps;
    
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
    public void PlaySteps()
    {
        string sceneName = SceneManager.GetActiveScene().name;

        switch (sceneName)
        {
            case "Level_0":
                int selec = Random.Range(0, steps.Length);
                sfxAudioSource.clip = stepsSnow[selec];
                sfxAudioSource.Play();
                break;
            case "Level_1":
                int selec1 = Random.Range(0, stepsSand.Length);
                sfxAudioSource.clip = stepsSnow[selec1];
                sfxAudioSource.Play();
                break;
            case "Level_2":
                int selec2 = Random.Range(0, stepsWater.Length);
                sfxAudioSource.clip = stepsSnow[selec2];
                sfxAudioSource.Play();
                break;
            case "Level_3":
                int selec3 = Random.Range(0, stepsSnow.Length);
                sfxAudioSource.clip = stepsSnow[selec3];
                sfxAudioSource.Play();
                break;
            case "Level_4":
                int selec4 = Random.Range(0, stepsForest.Length);
                sfxAudioSource.clip = stepsSnow[selec4];
                sfxAudioSource.Play();
                break;
            case "Level_5":
                int selec5 = Random.Range(0, steps.Length);
                sfxAudioSource.clip = stepsSnow[selec5];
                sfxAudioSource.Play();
                break;
        }
    }
    
    public void PlayCrouch()
    {
        sfxAudioSource.clip = crouchSound;
        sfxAudioSource.Play();
    }
    
    public void PlayDie()
    {
        sfxAudioSource.clip = dieSound;
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
    
    public void PlayLever()
    {
        sfxAudioSource.clip = lever;
        sfxAudioSource.Play();
    }
}