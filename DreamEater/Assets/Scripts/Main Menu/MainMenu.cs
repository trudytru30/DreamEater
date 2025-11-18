using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Mainmenu : MonoBehaviour
{
    public AudioSource musicSource;  
    public float fadeDuration = 8f;  

    public void Play()
    {
        StartCoroutine(FadeOutAndLoadScene());
    }

    private IEnumerator FadeOutAndLoadScene()
    {
        float startVolume = musicSource.volume;

        
        for (float t = 0; t < fadeDuration; t += Time.deltaTime)
        {
            musicSource.volume = Mathf.Lerp(startVolume, 0f, t / fadeDuration);
            yield return null;
        }

        
        musicSource.volume = 0f;

        SceneManager.LoadScene(AppScenes.LOADING_SCENE);
    }

    public void Exit()
    {
        Debug.Log("Exiting...");
        Application.Quit();
    }
}