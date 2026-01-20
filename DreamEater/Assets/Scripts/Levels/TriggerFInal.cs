using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerFinal : MonoBehaviour
{
    [SerializeField] private Animator animatorTurnOn;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(ActivateSceneChange());
    }

    private IEnumerator ActivateSceneChange()
    {
        animatorTurnOn.SetTrigger("Activar");
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu");
    }
}