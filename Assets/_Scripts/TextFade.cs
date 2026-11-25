using System.Collections;
using UnityEngine;
using TMPro;

public class TextFader : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI textMesh;
    [SerializeField] private float fadeDuration = 1.5f;
    [SerializeField] private bool StartEnabled = false;

    private Coroutine currentFade;

    private void Awake()
    {
        if (StartEnabled)
        {
            textMesh.enabled = true;
        }
        else
        {
            textMesh.enabled = false;
        }
        if (textMesh == null)
            textMesh = GetComponent<TextMeshProUGUI>();
    }

    public void StartFadeIn()
    {
        textMesh.enabled = true;
        StartFade(1f);
    }

    public void StartFadeOut()
    {
        StartFade(0f);
    }

    private void StartFade(float targetAlpha)
    {
        if (currentFade != null)
            StopCoroutine(currentFade);

        currentFade = StartCoroutine(FadeRoutine(targetAlpha));
    }

    private IEnumerator FadeRoutine(float targetAlpha)
    {
        Color startColor = textMesh.color;
        float elapsedTime = 0f;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            float newAlpha = Mathf.Lerp(startColor.a, targetAlpha, elapsedTime / fadeDuration);
            textMesh.color = new Color(startColor.r, startColor.g, startColor.b, newAlpha);
            yield return null;
        }

        textMesh.color = new Color(startColor.r, startColor.g, startColor.b, targetAlpha);
        currentFade = null;
    }
}