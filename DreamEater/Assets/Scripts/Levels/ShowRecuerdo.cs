using System.Collections;
using UnityEngine;

public class ShowRecuerdo : MonoBehaviour
{
    //Mostrar recuerdo del player al finalizar el nivel
    [SerializeField] private LevelManager levelManager;
    [SerializeField] private GameObject recuerdo;
    [SerializeField] private float memoryTime;
    
    private void Start()
    {
        recuerdo.SetActive(false);
        levelManager = LevelManager.Instance;
    }

    public void PlayMemory()
    {
        StartCoroutine(TimeOfMemory());
    }

    private IEnumerator TimeOfMemory()
    {
        yield return new WaitForSeconds(17);
        recuerdo.SetActive(true);
        yield return new WaitForSeconds(memoryTime);
        levelManager._canSwapScene = true;
    }
}
