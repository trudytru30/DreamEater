using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    private AsyncOperation _mOperation;
    [SerializeField] private Image progressbar;
    [SerializeField] private Text progresstext;
    [SerializeField] private Text consejosText;
    private void OnEnable ()
    {
        SelectConsejo();
        StartCoroutine(Delay());
    }

    private void Update()
    {
        progressbar.fillAmount = _mOperation.progress;
        progresstext.text = (_mOperation.progress*100f).ToString()+"%";
    }

    private IEnumerator Delay ()
    {
        yield return new WaitForEndOfFrame();
        _mOperation = SceneManager.LoadSceneAsync(GameManager.Instance.nextScene, LoadSceneMode.Single);
        _mOperation.allowSceneActivation = false;

        while (!(_mOperation.progress >= 0.9f))
        {
            Debug.Log(_mOperation.progress.ToString("0.0000"));
            yield return null;
        }
        
        yield return new WaitForSeconds(5);
        FinishLoading();
    }

    private void FinishLoading()
    {
        _mOperation.allowSceneActivation = true;
    }

    private void SelectConsejo()
    {
        int selec = Random.Range(0, 5);
        switch (selec)
        {
            case 0:
                consejosText.text = "\"Never underestimate the power of dreams.\"";
                break;
            case 1:
                consejosText.text = "\"When a wave approaches, find a place to take shelter.\"";
                break;
            case 2:
                consejosText.text = "\"Friends are the family we choose\".";
                break;
            case 3:
                consejosText.text = "\"Watch your back, the DreamEater is stalking.\"";
                break;
            case 4:
                consejosText.text = "\"Only the DreamEater has a heart rotten enough to disturb a child's dream\".";
                break;
            default:
                return;
        }

    }
}
