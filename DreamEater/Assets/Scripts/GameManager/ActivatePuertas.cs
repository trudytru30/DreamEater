using UnityEngine;

public class ActivatePuertas : MonoBehaviour
{
    [Header("Puertas")]
    private int _puertasDesbloqueadas;
    [SerializeField] private GameObject puerta1;
    [SerializeField] private GameObject puerta2;
    [SerializeField] private GameObject puerta3;
    [SerializeField] private GameObject puerta4;
    [SerializeField] private GameObject puerta5;

    [Header("Bichos")]
    [SerializeField] private GameObject bicho1;
    [SerializeField] private GameObject bicho2;
    [SerializeField] private GameObject bicho3;
    [SerializeField] private GameObject bicho4;

    [Header("Triggers")]
    [SerializeField] private GameObject trigger1;
    [SerializeField] private GameObject trigger2;
    [SerializeField] private GameObject trigger3;
    [SerializeField] private GameObject trigger4;
    [SerializeField] private GameObject  trigger5;

    [Header("Animators")]
    [SerializeField] private Animator[] puertaanim;
    
    private void Start()
    {
        puerta1.SetActive(true);
        puerta2.SetActive(false);
        puerta3.SetActive(false);
        puerta4.SetActive(false);
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
                puerta1.SetActive(true);
                trigger1.SetActive(true);
                puertaanim[0].enabled = true;
                puertaanim[1].enabled = true;
                bicho1.SetActive(true);
                break;
            case 2:
                puerta2.SetActive(true);
                puerta1.SetActive(true);
                trigger2.SetActive(true);
                puertaanim[2].enabled = true;
                puertaanim[3].enabled = true;
                bicho1.SetActive(true);
                bicho2.SetActive(true);
                break;
            case 3:
                puerta3.SetActive(true);
                puerta2.SetActive(true);
                puerta1.SetActive(true);
                trigger3.SetActive(true);
                puertaanim[4].enabled = true;
                puertaanim[5].enabled = true;
                bicho1.SetActive(true);
                bicho2.SetActive(true);
                bicho3.SetActive(true);
                break;
            case 4:
                puerta4.SetActive(true);
                puerta3.SetActive(true);
                puerta2.SetActive(true);
                puerta1.SetActive(true);
                trigger4.SetActive(true);
                puertaanim[6].enabled = true;
                puertaanim[7].enabled = true;
                bicho1.SetActive(true);
                bicho2.SetActive(true);
                bicho3.SetActive(true);
                bicho4.SetActive(true);
                break;
            case 5:
                puerta5.SetActive(true);
                puerta4.SetActive(true);
                puerta3.SetActive(true);
                puerta2.SetActive(true);
                puerta1.SetActive(true);
                trigger5.SetActive(true);
                puertaanim[8].enabled = true;
                puertaanim[9].enabled = true;
                break;
            default:
                break;
        }
    }

    private void DeactivateTrigger()
    {
        trigger1.SetActive(false);
        trigger2.SetActive(false);
        trigger3.SetActive(false);
        trigger4.SetActive(false);
        trigger5.SetActive(false);
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
        bicho1.SetActive(false);
        bicho2.SetActive(false);
        bicho3.SetActive(false);
        bicho4.SetActive(false);
    }
}
