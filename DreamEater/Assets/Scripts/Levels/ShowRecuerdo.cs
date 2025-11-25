using System;
using System.Collections;
using UnityEngine;

public class ShowRecuerdo : MonoBehaviour
{
    //Mostrar recuerdo del player al finalizar el nivel


    [SerializeField] private LevelManager _levelManager;
    [SerializeField] private GameObject _recuerdo;
    [SerializeField] private float _memoryTime;


    private void Start()
    {
        _recuerdo.SetActive(false);
        _levelManager = LevelManager.Instance;
    }

    public void PlayMemory()
    {
        StartCoroutine(TimeOfMemory());
    }

    private IEnumerator TimeOfMemory()
    {

        yield return new WaitForSeconds(_memoryTime);
        _levelManager._canSwapScene = true;

    }
}
