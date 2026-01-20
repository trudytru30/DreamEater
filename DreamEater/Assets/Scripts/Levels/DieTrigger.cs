using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class DieTrigger : MonoBehaviour
{
    private void Start()
    {
        GetComponent<BoxCollider>().isTrigger=true;
    }
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            //StartCoroutine(KillPlayer(other));
            other.GetComponent<PlayerController2>().Die();
        }
    }

    /*private IEnumerator KillPlayer(Collider other)
    {
        other.GetComponent<PlayerController2>().Die();
        yield return new WaitForSeconds(1);
        UIManager.Instance.InitDieCharacter();


        yield return new WaitForSeconds(6);
        UIManager.Instance.FinishDieCharacter();

    }*/
}