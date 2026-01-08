using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{

    private AsyncOperation m_operation;
    [SerializeField] private Image _progressbar;
    [SerializeField] private Text _progresstext;
    [SerializeField] private Text _consejosText;
    private void OnEnable ()
    {
        SelectConsejo();
        StartCoroutine(Delay());
    }

    private void Update()
    {
        _progressbar.fillAmount = m_operation.progress;
        _progresstext.text = (m_operation.progress*100f).ToString()+"%";
    }

    private IEnumerator Delay ()
    {
        yield return new WaitForEndOfFrame();
        m_operation = SceneManager.LoadSceneAsync(GameManager.Instance.nextScene, LoadSceneMode.Single);
        m_operation.allowSceneActivation = false;

        while (!(m_operation.progress >= 0.9f))
        {
            Debug.Log(m_operation.progress.ToString("0.0000"));
            yield return null;
        }
        
        yield return new WaitForSeconds(5);
        FinishLoading();

    }

    private void FinishLoading()
    {
        m_operation.allowSceneActivation = true;
    }

    private void SelectConsejo()
    {
        int selec = Random.Range(0, 5);
        switch (selec)
        {
            case 0:
                _consejosText.text = "\"Never underestimate the power of dreams.\"";
                break;
            case 1:
                _consejosText.text = "\"When a wave approaches, find a place to take shelter.\"";
                break;
            case 2:
                _consejosText.text = "\"Friends are the family we choose\".";
                break;
            case 3:
                _consejosText.text = "\"Watch your back, the DreamEater is stalking.\"";
                break;
            case 4:
                _consejosText.text = "\"Only the DreamEater has a heart rotten enough to disturb a child's dream\".";
                break;
            default:
                return;
        }

    }
}
