using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using UnityEngine.UI;

public class MenuOpciones : MonoBehaviour
{
   [SerializeField] private AudioMixer  audioMixer;
    //[SerializeField] private AudioMixerGroup audioMixerGeneral;
   // [SerializeField] private AudioMixerGroup audioMixerEfectos;
   // [SerializeField] private AudioMixerGroup audioMixerMusica;

   



    /*private void Start()
    {
        slider.value = 
    } */

    public void PantallaCompleta(bool pantallaCompleta)
    {
        Screen.fullScreen = pantallaCompleta;
    }


    public void CambiarVolumenMusica(float volumen)
    {
        audioMixer.SetFloat("VolumeMusica", volumen);
        
    }
    public void CambiarVolumenGeneral(float volumen)
    {
        audioMixer.SetFloat("VolumeGeneral", volumen);

    }
    public void CambiarVolumenDialogos(float volumen)
    {
        audioMixer.SetFloat("VolumeDialogos", volumen);

    }
    public void CambiarVolumenEfectos(float volumen)
    {
        audioMixer.SetFloat("VolumeEfectos", volumen);

    }




    public void CambiarCalidad(int index)
    {
        QualitySettings.SetQualityLevel(index);
    }


    









}
