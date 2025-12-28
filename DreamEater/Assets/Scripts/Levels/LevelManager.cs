using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class LevelManager : MonoBehaviour
{
    //Carga escenas

    public static LevelManager Instance {  get; private set; }

    [SerializeField] private bool _hasMemoryToPlay;
    [SerializeField] private string _sceneName;
    [SerializeField] private ShowRecuerdo _showRecuerdo;
    [SerializeField] private int nivelDesbloqueado;
    [SerializeField] private int nextScene;
    public bool _canSwapScene;

    private void Start()
    {
        _canSwapScene = false;
    }

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
        Debug.Log(gameObject.name);

    }

    private void OnTriggerEnter(Collider other)
    {
        if (_hasMemoryToPlay)
        {
            _showRecuerdo.PlayMemory();
        }
        else
        {
            GameManager.Instance.ChangeNextLevelLoading(nextScene);
            GameManager.Instance.ChangePuertasDesbloqueadas(nivelDesbloqueado);
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
