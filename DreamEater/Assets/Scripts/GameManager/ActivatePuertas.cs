using UnityEngine;

public class ActivatePuertas : MonoBehaviour
{
    private int _puertasDesbloqueadas;
    [SerializeField] private GameObject _puerta1;
    [SerializeField] private GameObject _puerta2;
    [SerializeField] private GameObject _puerta3;
    [SerializeField] private GameObject _puerta4;

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

        switch (_puertasDesbloqueadas)
        {
            case 1:
                _puerta1.SetActive(true);
                break;
            case 2:
                _puerta2.SetActive(true);
                _puerta1.SetActive(true);
                break;
            case 3:
                _puerta3.SetActive(true);
                _puerta2.SetActive(true);
                _puerta1.SetActive(true);
                break;
            case 4:
                _puerta4.SetActive(true);
                _puerta3.SetActive(true);
                _puerta2.SetActive(true);
                _puerta1.SetActive(true);
                break;
            default:
                break;
        }
    }
}
