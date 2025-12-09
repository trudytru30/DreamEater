using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    //Este va a ser un pedazo de script que persiste entre escenas para manejar todo el cotarro que 
    //se necesite y pase datos

    public static GameManager Instance;


    //Lista de datos para pasar entre escenas

    #region brillo
    public bool darkBrightnessActive;
    public bool lightBrightnessActive;
    public float darkBrightnessFloat;
    public float lightBrightnessFloat;

    #endregion

    #region puertas
    public int puertasDesbloqueadas;
    public string nombreNivelPuertas;
    [SerializeField] private GameObject _puerta1;
    [SerializeField] private GameObject _puerta2;
    [SerializeField] private GameObject _puerta3;
    [SerializeField] private GameObject _puerta4;
    #endregion

    private void Start()
    {
        puertasDesbloqueadas = 1;
    }

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
        DontDestroyOnLoad(gameObject);
    }

    private void OnEnable()
    {
        ActivatePuertas();
    }

    public void ActivatePuertas()
    {
        var actualScene = SceneManager.GetActiveScene();
        if (actualScene.name != nombreNivelPuertas)
        return;

        _puerta1.SetActive(false);
        _puerta2.SetActive(false);
        _puerta3.SetActive(false);
        _puerta4.SetActive(false);

        switch (puertasDesbloqueadas)
        {
            case 1:
                _puerta1.SetActive(true);
                break;
            case 2:
                _puerta1.SetActive(true);
                _puerta2.SetActive(true);
                break;
            case 3:
                _puerta1.SetActive(true);
                _puerta2.SetActive(true);
                _puerta3.SetActive(true);
                break;
            case 4:
                _puerta1.SetActive(true);
                _puerta2.SetActive(true);
                _puerta3.SetActive(true);
                _puerta4.SetActive(true);
                break;
            default:
                break;

        }
    }
}
