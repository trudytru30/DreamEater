using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuOptions : MonoBehaviour
{
    [SerializeField] private AudioMixer audioMixer;
    //[SerializeField] private AudioMixerGroup audioMixerGeneral;
    //[SerializeField] private AudioMixerGroup audioMixerEfectos;
    //[SerializeField] private AudioMixerGroup audioMixerMusica;

    /*private void Start()
    {
        slider.value =
    } */

    public void FullScreen(bool fullScreen)
    {
        Screen.fullScreen = fullScreen;
    }

    public void ChangeMusicVolume(float volume)
    {
        audioMixer.SetFloat("VolumeMusica", volume);
    }
    
    public void ChangeGeneralVolume(float volume)
    {
        audioMixer.SetFloat("VolumeGeneral", volume);
    }
    
    public void ChangeDialoguesVolume(float volume)
    {
        audioMixer.SetFloat("VolumeDialogos", volume);
    }
    
    public void ChangeEffectsVolume(float volume)
    {
        audioMixer.SetFloat("VolumeEfectos", volume);
    }
    
    public void ChangeQuality(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }
}