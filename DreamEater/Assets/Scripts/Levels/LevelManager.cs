using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(BoxCollider))]
public class LevelManager : MonoBehaviour
{
    //Carga escenas

    public static LevelManager Instance {  get; private set; }

    [SerializeField] private bool hasMemoryToPlay;
    [SerializeField] private string sceneName;
    [SerializeField] private ShowRecuerdo showRecuerdo;
    [SerializeField] private int unlockedLevel;
    [SerializeField] private int nextScene;
    public bool canSwapScene;

    private void Start()
    {
        canSwapScene = false;
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
        if (hasMemoryToPlay)
        {
            showRecuerdo.PlayMemory();
            GameManager.Instance.ChangeNextLevelLoading(nextScene);
            GameManager.Instance.ChangeUnlockedDoors(unlockedLevel);
        }
        else
        {
            GameManager.Instance.ChangeNextLevelLoading(nextScene);
            GameManager.Instance.ChangeUnlockedDoors(unlockedLevel);
            SceneManager.LoadScene(sceneName);
        }
    }

    private void Update()
    {
        if (canSwapScene)
        {
            SceneManager.LoadScene(sceneName);
        }
    }
}