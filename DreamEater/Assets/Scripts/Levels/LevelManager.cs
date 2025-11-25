using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    //Carga escenas

    public static LevelManager Instance {  get; private set; }

    [SerializeField] private bool _hasMemoryToPlay;
    [SerializeField] private string _sceneName;
    [SerializeField] private ShowRecuerdo _showRecuerdo;
    public bool _canSwapScene;


    private void Awake()
    {
        if(Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hasMemoryToPlay)
        {
            _showRecuerdo.PlayMemory();
        }
        else
        {
            SceneManager.LoadScene(_sceneName);
        }
    }

    private void Update()
    {
        if (_canSwapScene)
        {
            SceneManager.LoadScene(_sceneName);
        }
    }


}
