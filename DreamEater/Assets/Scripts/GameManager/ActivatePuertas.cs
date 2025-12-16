using UnityEngine;

public class ActivatePuertas : MonoBehaviour
{
    [Header("Puertas")]
    private int _puertasDesbloqueadas;
    [SerializeField] private GameObject _puerta1;
    [SerializeField] private GameObject _puerta2;
    [SerializeField] private GameObject _puerta3;
    [SerializeField] private GameObject _puerta4;
    [SerializeField] private GameObject _puerta5;

    [Header("Triggers")]
    [SerializeField] private GameObject _trigger_1;
    [SerializeField] private GameObject _trigger_2;
    [SerializeField] private GameObject _trigger_3;
    [SerializeField] private GameObject _trigger_4;
    [SerializeField] private GameObject _trigger_5;


    private void Start()
    {
        _puerta1.SetActive(true);
        _puerta2.SetActive(false);
        _puerta3.SetActive(false);
        _puerta4.SetActive(false);


    }

    private void OnEnable()
    {
        _puertasDesbloqueadas = GameManager.Instance.puertasDesbloqueadas;

        _trigger_1.SetActive(false);
        _trigger_2.SetActive(false);
        _trigger_3.SetActive(false);
        _trigger_4.SetActive(false);
        _trigger_5.SetActive(false);         

        switch (_puertasDesbloqueadas)
        {
            case 1:
                _puerta1.SetActive(true);
                _trigger_1.SetActive(true);
                break;
            case 2:
                _puerta2.SetActive(true);
                _puerta1.SetActive(true);
                _trigger_2.SetActive(true);
                break;
            case 3:
                _puerta3.SetActive(true);
                _puerta2.SetActive(true);
                _puerta1.SetActive(true);
                _trigger_3.SetActive(true);
                break;
            case 4:
                _puerta4.SetActive(true);
                _puerta3.SetActive(true);
                _puerta2.SetActive(true);
                _puerta1.SetActive(true);
                _trigger_4.SetActive(true);
                break;
            case 5:
                _puerta5.SetActive(true);
                _puerta4.SetActive(true);
                _puerta3.SetActive(true);
                _puerta2.SetActive(true);
                _puerta1.SetActive(true);
                _trigger_5.SetActive(true);
                break;
            default:
                break;
        }
    }
}
