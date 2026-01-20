using UnityEngine;
using System.Collections;
using TMPro;
using UnityEngine;
using UnityEngine.UI;
<<<<<<< HEAD
=======
using System.Collections;
using TMPro;
>>>>>>> e08950077953c3ef511f2d821997e82ba5b851ee

public class LogoStudio : MonoBehaviour
{
    public RectTransform logoTransform;
    public RectTransform textTransform;
    public Image logoImage;
    public TextMeshProUGUI logoText;
    public float fadeDuration = 0.6f;
    public float waitTime = 1.5f;
    public float fadeOutDuration = 1f;
    public GameObject nextCanvas;

    private void Start()
    {
        nextCanvas.SetActive(false);
        StartCoroutine(LogoSequence());
    }

    private IEnumerator LogoSequence()
    {
        Vector3 logoEndScale = logoTransform.localScale;
        Vector3 logoOvershoot = logoEndScale * 1.2f;
        Vector3 textEndScale = textTransform.localScale;
        Vector3 textOvershoot = textEndScale * 1.2f;

        logoTransform.localScale = logoEndScale;
        textTransform.localScale = textEndScale;

        logoImage.color = new Color(logoImage.color.r, logoImage.color.g, logoImage.color.b, 0f);
        logoText.color = new Color(logoText.color.r, logoText.color.g, logoText.color.b, 0f);

        float t = 0f;

        while (t < fadeDuration)
        {
            t += Time.deltaTime;
            float n = t / fadeDuration;

            float alpha = Mathf.Lerp(0f, 1f, n);
            logoImage.color = new Color(logoImage.color.r, logoImage.color.g, logoImage.color.b, alpha);
            logoText.color = new Color(logoText.color.r, logoText.color.g, logoText.color.b, alpha);

            float scaleN = Mathf.Sin(n * Mathf.PI * 0.5f);
            logoTransform.localScale = Vector3.Lerp(logoEndScale, logoOvershoot, scaleN);
            textTransform.localScale = Vector3.Lerp(textEndScale, textOvershoot, scaleN);

            yield return null;
        }

        logoTransform.localScale = logoEndScale;
        textTransform.localScale = textEndScale;

        yield return new WaitForSeconds(waitTime);

        t = 0f;
        Color logoC = logoImage.color;
        Color textC = logoText.color;

        while (t < fadeOutDuration)
        {
            t += Time.deltaTime;
            float a = 1f - (t / fadeOutDuration);

            logoImage.color = new Color(logoC.r, logoC.g, logoC.b, a);
            logoText.color = new Color(textC.r, textC.g, textC.b, a);

            yield return null;
        }

        nextCanvas.SetActive(true);
        gameObject.SetActive(false);
    }
}