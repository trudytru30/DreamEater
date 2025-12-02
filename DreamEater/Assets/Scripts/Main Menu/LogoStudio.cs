using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections;

public class StudioLogo : MonoBehaviour
{
    public RectTransform logoTransform;
    public RectTransform textTransform;
    public Image logoImage;
    public TextMeshProUGUI logoText;
    public float animationDuration = 0.6f;
    public float waitTime = 1.5f;
    public float fadeOutDuration = 1f;
    public GameObject nextCanvas;

    private void Start()
    {
        nextCanvas.SetActive(false);
        StartCoroutine(LogoSequence());
    }

    IEnumerator LogoSequence()
    {
        Vector3 startScale = Vector3.zero;
        Vector3 endScale = Vector3.one;
        Vector3 overshootScale = endScale * 1.3f;

        logoTransform.localScale = startScale;
        textTransform.localScale = startScale;

        float t = 0f;

        while (t < animationDuration)
        {
            t += Time.deltaTime;
            float n = t / animationDuration;

            if (n < 0.7f)
            {
                float p = n / 0.7f;
                logoTransform.localScale = Vector3.Lerp(startScale, overshootScale, p);
                textTransform.localScale = Vector3.Lerp(startScale, overshootScale, p);
            }
            else
            {
                float p = (n - 0.7f) / 0.3f;
                logoTransform.localScale = Vector3.Lerp(overshootScale, endScale, p);
                textTransform.localScale = Vector3.Lerp(overshootScale, endScale, p);
            }

            yield return null;
        }

        yield return new WaitForSeconds(waitTime);

        float f = 0f;

        Color logoC = logoImage.color;
        Color textC = logoText.color;

        while (f < fadeOutDuration)
        {
            f += Time.deltaTime;
            float a = 1f - (f / fadeOutDuration);

            logoImage.color = new Color(logoC.r, logoC.g, logoC.b, a);
            logoText.color = new Color(textC.r, textC.g, textC.b, a);

            yield return null;
        }

        nextCanvas.SetActive(true);
        gameObject.SetActive(false);
    }
}

