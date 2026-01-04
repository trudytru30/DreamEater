using UnityEngine;


[RequireComponent(typeof(BoxCollider))]
public class FallObjects : MonoBehaviour
{
    [SerializeField] private Rigidbody[] objects;
    [SerializeField] private GameObject blackmask;
    

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("Player"))
        {
            blackmask.SetActive(false);
            for(int i = 0; i < objects.Length; i++)
            {
                objects[i].useGravity = true;
            }
        }
    }

}
