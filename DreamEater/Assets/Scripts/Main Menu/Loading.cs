using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    private AsyncOperation _operation;
    [SerializeField] private Image progressBar;
    [SerializeField] private Text progressText;
    [SerializeField] private Text adviceText;
    
    private void OnEnable()
    {
        SelectAdvice();
        StartCoroutine(Delay());
    }

    private void Update()
    {
        progressBar.fillAmount = _operation.progress;
        progressText.text = (_operation.progress * 100f).ToString() + "%";
    }

    private IEnumerator Delay()
    {
        yield return new WaitForEndOfFrame();
        _operation = SceneManager.LoadSceneAsync(GameManager.Instance.nextScene, LoadSceneMode.Single);
        _operation.allowSceneActivation = false;

        while (!(_operation.progress >= 0.9f))
        {
            Debug.Log(_operation.progress.ToString("0.0000"));
            yield return null;
        }
        
        yield return new WaitForSeconds(5);
        FinishLoading();
    }

    private void FinishLoading()
    {
        _operation.allowSceneActivation = true;
    }

    private void SelectAdvice()
    {
        int selection = Random.Range(0, 5);
        switch (selection)
        {
            case 0:
                adviceText.text = "\"Never underestimate the power of dreams.\"";
                break;
            case 1:
                adviceText.text = "\"When a wave approaches, find a place to take shelter.\"";
                break;
            case 2:
                adviceText.text = "\"Friends are the family we choose\".";
                break;
            case 3:
                adviceText.text = "\"Watch your back, the DreamEater is stalking.\"";
                break;
            case 4:
                adviceText.text = "\"Only the DreamEater has a heart rotten enough to disturb a child's dream\".";
                break;
            default:
                return;
        }
    }
}