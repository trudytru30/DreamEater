using UnityEngine;

[RequireComponent(typeof(BoxCollider))]
public class FallObjects : MonoBehaviour
{
    [SerializeField] private Rigidbody[] objects;
    [SerializeField] private GameObject blackMask;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            blackMask.SetActive(false);
            for(int i = 0; i < objects.Length; i++)
            {
                objects[i].useGravity = true;
            }
        }
    }
}