using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;

public class ScreenFader : MonoBehaviour
{
    public Image fadeImage;
    public float fadeDuration = 1f;

    private CanvasGroup canvasGroup;

    private void Awake()
    {
        canvasGroup = fadeImage.GetComponent<CanvasGroup>();
        if (canvasGroup == null) canvasGroup = fadeImage.gameObject.AddComponent<CanvasGroup>();

        canvasGroup.blocksRaycasts = false;
        canvasGroup.interactable = false;

        // 初期透明
        Color c = fadeImage.color;
        c.a = 0f;
        fadeImage.color = c;
    }

    public void FadeOutAndLoad(string sceneName)
    {
        StartCoroutine(FadeOutCoroutine(sceneName));
    }

    private IEnumerator FadeOutCoroutine(string sceneName)
    {
        // フェード中はゲームを止める
        Time.timeScale = 0f;
        float timer = 0f;
        Color color = fadeImage.color;

        canvasGroup.blocksRaycasts = true;
        canvasGroup.interactable = false;

        while (timer < fadeDuration)
        {
            timer += Time.unscaledDeltaTime;
            color.a = Mathf.Lerp(0, 1, timer / fadeDuration);
            fadeImage.color = color;
            yield return null;
        }

        Time.timeScale = 1f; // シーン切替前に戻す
        SceneManager.LoadScene(sceneName);
    }
}
