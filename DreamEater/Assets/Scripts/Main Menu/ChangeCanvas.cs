using UnityEngine;

public class ChangeCanvas : MonoBehaviour
{
    public GameObject canvasToActivate;
    public float delay = 10f;

    void Start()
    {
        StartCoroutine(SwitchCanvasAfterDelay());
    }

    private System.Collections.IEnumerator SwitchCanvasAfterDelay()
    {
        yield return new WaitForSeconds(delay);
        gameObject.SetActive(false);
        if (canvasToActivate != null)
        {
            canvasToActivate.SetActive(true);
        }
    }
}