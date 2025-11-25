using UnityEngine;

[ RequireComponent(typeof(Collider))]
public class Checkpoints : MonoBehaviour
{
    [SerializeField] private int checkpointId;
    
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            CheckpointManager.Instance.SetCheckpointPosition(checkpointId, transform.position, gameObject);
        }
    }
}