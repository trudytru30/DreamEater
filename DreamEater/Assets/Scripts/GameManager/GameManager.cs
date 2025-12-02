using UnityEngine;

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
}
