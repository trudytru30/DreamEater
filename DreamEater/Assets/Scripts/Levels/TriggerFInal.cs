using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

public class TriggerFInal : MonoBehaviour
{
    [SerializeField] Animator _animatorEncender;

    private void OnTriggerEnter(Collider other)
    {
        StartCoroutine(ActivarCambioEscena());
    }



    IEnumerator ActivarCambioEscena()
    {
        _animatorEncender.SetTrigger("Activar");
        yield return new WaitForSeconds(5f);
        SceneManager.LoadScene("MainMenu");
    }



}
