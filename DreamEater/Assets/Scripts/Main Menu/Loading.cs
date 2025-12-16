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
    private void OnEnable ()
    {
        StartCoroutine(Delay());
    }

    private void Update()
    {
        _progressbar.fillAmount = m_operation.progress;
        _progresstext.text = m_operation.progress.ToString()+"%";
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
}
