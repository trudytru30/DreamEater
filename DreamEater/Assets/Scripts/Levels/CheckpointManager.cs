using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance;
    
    private GameObject _currentCheckpoint;
    private Vector3 _checkpointPosition;
    private int _checkpointId = -1;
    
    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        
        else
        {
            Destroy(gameObject);
        }
    }

    public Vector3 GetCheckpointPosition()
    {
        return _checkpointPosition;
    }
    
    public void SetCheckpointPosition(int id, Vector3 position, GameObject checkpoint)
    {
        if (id < _checkpointId) return;
        
        //Desactivar el checkpoint anterior
        if (_currentCheckpoint != null)
        {
            //_currentCheckpoint.SetActive(false);
            Destroy(_currentCheckpoint);
        }
        
        //Guarder checkpoint actual
        _checkpointId = id;
        _checkpointPosition = position;
        _currentCheckpoint = checkpoint;
        Debug.Log("Checkpoint " + id + " set");
    }
}