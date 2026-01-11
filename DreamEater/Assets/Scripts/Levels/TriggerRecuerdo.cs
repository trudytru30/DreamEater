using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TriggerRecuerdo : MonoBehaviour
{
    [SerializeField] GameObject _systemParticle;
    [SerializeField] Animator _animator;
    [SerializeField] Animator _animatorCamara;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _systemParticle.SetActive(true);
            StartCoroutine(MiDestello());
            other.GetComponent<Animator>().SetFloat("xSpeed", 0);
            other.GetComponent<Animator>().SetFloat("zSpeed", 0);
            other.GetComponent<PlayerController2>().speed = 0;
            
        }
        

    }

    IEnumerator MiDestello()
    {
        _animatorCamara.SetTrigger("Activar");
        yield return new WaitForSeconds(10f);
        _animator.enabled = true;
    }






}