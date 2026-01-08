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

    [Header("Bichos")]
    [SerializeField] private GameObject _bicho1;
    [SerializeField] private GameObject _bicho2;
    [SerializeField] private GameObject _bicho3;
    [SerializeField] private GameObject _bicho4;

    [Header("Triggers")]
    [SerializeField] private GameObject _trigger_1;
    [SerializeField] private GameObject _trigger_2;
    [SerializeField] private GameObject _trigger_3;
    [SerializeField] private GameObject _trigger_4;
    [SerializeField] private GameObject _trigger_5;

    [Header("Animators")]
    [SerializeField] private Animator[] puertaanim;


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

        DeactivateTrigger();
        DeactivateAnimators();
        DeactivateBichos();

        switch (_puertasDesbloqueadas)
        {
            case 1:
                _puerta1.SetActive(true);
                _trigger_1.SetActive(true);
                puertaanim[0].enabled = true;
                puertaanim[1].enabled = true;
                _bicho1.SetActive(true);
                break;
            case 2:
                _puerta2.SetActive(true);
                _puerta1.SetActive(true);
                _trigger_2.SetActive(true);
                puertaanim[2].enabled = true;
                puertaanim[3].enabled = true;
                _bicho1.SetActive(true);
                _bicho2.SetActive(true);
                break;
            case 3:
                _puerta3.SetActive(true);
                _puerta2.SetActive(true);
                _puerta1.SetActive(true);
                _trigger_3.SetActive(true);
                puertaanim[4].enabled = true;
                puertaanim[5].enabled = true;
                _bicho1.SetActive(true);
                _bicho2.SetActive(true);
                _bicho3.SetActive(true);
                break;
            case 4:
                _puerta4.SetActive(true);
                _puerta3.SetActive(true);
                _puerta2.SetActive(true);
                _puerta1.SetActive(true);
                _trigger_4.SetActive(true);
                puertaanim[6].enabled = true;
                puertaanim[7].enabled = true;
                _bicho1.SetActive(true);
                _bicho2.SetActive(true);
                _bicho3.SetActive(true);
                _bicho4.SetActive(true);
                break;
            case 5:
                _puerta5.SetActive(true);
                _puerta4.SetActive(true);
                _puerta3.SetActive(true);
                _puerta2.SetActive(true);
                _puerta1.SetActive(true);
                _trigger_5.SetActive(true);
                puertaanim[8].enabled = true;
                puertaanim[9].enabled = true;
                break;
            default:
                break;
        }
    }

    private void DeactivateTrigger()
    {
        _trigger_1.SetActive(false);
        _trigger_2.SetActive(false);
        _trigger_3.SetActive(false);
        _trigger_4.SetActive(false);
        _trigger_5.SetActive(false);
    }
    private void DeactivateAnimators()
    {
        for(int i = 0; i<puertaanim.Length; i++)
        {
            puertaanim[i].enabled = false;
        }

    }
    private void DeactivateBichos()
    {
        _bicho1.SetActive(false);
        _bicho2.SetActive(false);
        _bicho3.SetActive(false);
        _bicho4.SetActive(false);
    }
}
