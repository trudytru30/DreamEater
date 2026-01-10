using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class TriggerRecuerdo : MonoBehaviour
{
    [SerializeField] GameObject _systemParticle;
    [SerializeField] Animator _animator;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            _systemParticle.SetActive(true);
            StartCoroutine(MiDestello());
            other.GetComponent<Animator>().SetFloat("xSpeed", 0);
            other.GetComponent<Animator>().SetFloat("zSpeed", 0);
            other.GetComponent<PlayerController2>().enabled = false;
            
        }
        

    }

    IEnumerator MiDestello()
    {
        yield return new WaitForSeconds(10f);
        _animator.enabled = true;
    }






}