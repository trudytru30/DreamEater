using System.Collections;
using UnityEngine;

public class ShowMemory : MonoBehaviour
{
    //Mostrar recuerdo del player al finalizar el nivel
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject memory;
    [SerializeField] private float memoryTime;
    
    private void Start()
    {
        memory.SetActive(false);
        levelManager = LevelManager.Instance;
    }

    public void PlayMemory()
    {
        StartCoroutine(TimeOfMemory());
    }

    private IEnumerator TimeOfMemory()
    {
        yield return new WaitForSeconds(17);
        memory.SetActive(true);
        yield return new WaitForSeconds(memoryTime);
        levelManager.canSwapScene = true;
    }
}