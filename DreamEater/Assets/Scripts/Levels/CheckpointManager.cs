/*
 Controlla a que checkpoint tiene que volver el player a partir de los checkpoints individuales
 */

using UnityEngine;

public class CheckpointManager : MonoBehaviour
{
    public static CheckpointManager Instance; //para hacerlo singleton
    
    private GameObject _currentCheckpoint; //indica el checkpoint activo
    private Vector3 _checkpointPosition; //posicion a la que reestablece al player
    private int _checkpointId = -1; //inicializado asi para evitar errores
    
    private void Awake()
    {
        //creacion de la instacia como singleton
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
        //evita que se reaparezca en un checkpoint anterior
        if (id < _checkpointId)
        {
            return;
        }
        
        //desactivar el checkpoint anterior
        if (_currentCheckpoint != null)
        {
            //_currentCheckpoint.SetActive(false);
            Destroy(_currentCheckpoint);
        }
        
        //guardar checkpoint actual
        _checkpointId = id;
        _checkpointPosition = position;
        _currentCheckpoint = checkpoint;
        Debug.Log("Checkpoint " + id + " set");
    }
}