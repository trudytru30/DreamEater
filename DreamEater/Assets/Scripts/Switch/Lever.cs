using Unity.VisualScripting;
using UnityEngine;

public class Lever : MonoBehaviour
{
    [SerializeField] private float force;
    [SerializeField] private Rigidbody rb;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            rb.AddForce(Vector3.down,ForceMode.Force);
        }
    }
}
