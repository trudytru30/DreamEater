using System.Collections;
using UnityEngine;

public class JumpingMushrooms : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private float delay;
    private Animator _animator;

    private void Start()
    {
        _animator = GetComponent<Animator>();   
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            StartCoroutine(Jump(other));
        }
    }

    private IEnumerator Jump(Collider otherl)
    {
        _animator.SetTrigger("Jump");
        yield return new WaitForSeconds(delay);
        otherl.GetComponent<Rigidbody>().AddForce(Vector3.up * force);
    }
}
