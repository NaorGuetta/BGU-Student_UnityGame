using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections;

public class FadeControllerMenu : MonoBehaviour
{
    public Image fadeImage;  
    public float fadeDuration = 0.5f; 

    private void Start()
    {
        fadeImage.color = new Color(fadeImage.color.r, fadeImage.color.g, fadeImage.color.b, 1);
        StartCoroutine(FadeOut());
    }

    public void FadeToScene(string sceneName)
    {
        StartCoroutine(FadeIn(sceneName));
    }

    private IEnumerator FadeOut()
    {
        float elapsedTime = 0.0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = 1.0f - Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 0.0f;
        fadeImage.color = color;
        fadeImage.raycastTarget = false;  // Disable raycast target after fade-out
    }

    private IEnumerator FadeIn(string sceneName)
    {
        fadeImage.raycastTarget = true;  // Enable raycast target before fade-in
        float elapsedTime = 0.0f;
        Color color = fadeImage.color;

        while (elapsedTime < fadeDuration)
        {
            elapsedTime += Time.deltaTime;
            color.a = Mathf.Clamp01(elapsedTime / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        color.a = 1.0f;
        fadeImage.color = color;

        SceneManager.LoadScene(sceneName);
    }
}
