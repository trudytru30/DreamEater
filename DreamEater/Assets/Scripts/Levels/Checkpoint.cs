/*
 Este hace referencia a un checkpoint unico
 NO se gestiona el checkpoint al que vuelve eso se hace en CheckpointManager
 */

using UnityEngine;

[ RequireComponent(typeof(Collider))]
public class Checkpoints : MonoBehaviour
{
    [SerializeField] private int checkpointId; //indica el valor del checkpoint para el manager
    
    //detecta al player y setea el nuevo punto de respawn
    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.layer == LayerMask.NameToLayer("Player"))
        {
            CheckpointManager.Instance.SetCheckpointPosition(checkpointId, transform.position, gameObject);
        }
    }
}