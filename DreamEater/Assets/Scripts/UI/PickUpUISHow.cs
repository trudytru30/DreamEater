using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class PickUpUISHow : MonoBehaviour
{
    [SerializeField] private Text _bellotaCounter;
    [SerializeField] private GameObject _bellotaUI;
    public static PickUpUISHow Instance { get; private set; }
    public int _counter = 0;
    [SerializeField] public int _maxPickups;
    [SerializeField] private GameObject _mustPickUpText;

    private void Start()
    {
        _mustPickUpText.SetActive(false);
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
    }

    public void AddPickUp()
    {
        _bellotaUI.SetActive(true);
        _counter++;
        string text = _counter.ToString()+" / "+_maxPickups;
        _bellotaCounter.text = text;
    }
    
    public IEnumerator MustPickUpAll()
    {
        _mustPickUpText.SetActive(true);
        yield return new WaitForSeconds(5);
        _mustPickUpText.SetActive(false);
    }

}
