using System.Collections;
using UnityEngine;

public class TriggerMemory : MonoBehaviour
{
    [SerializeField] GameObject systemParticle;
    [SerializeField] Animator animator;
    [SerializeField] Animator animatorCamara;

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            systemParticle.SetActive(true);
            StartCoroutine(MiDestello());
            other.GetComponent<Animator>().SetFloat("xSpeed", 0);
            other.GetComponent<Animator>().SetFloat("zSpeed", 0);
            other.GetComponent<PlayerController2>().speed = 0;
        }
    }

    IEnumerator MiDestello()
    {
        animatorCamara.SetTrigger("Activar");
        yield return new WaitForSeconds(10f);
        animator.enabled = true;
    }
}