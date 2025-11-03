using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class FadeManager : MonoBehaviour
{
    [SerializeField] float fadeSpeed = 1.5f;
    [SerializeField] Image fadeImage;
    private float alpha;
    private bool isFading = false;
    public static FadeManager Instance;

    void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Canvas‚²‚ÆŽc‚·
        }
        else
        {
            Destroy(gameObject);
            return;
        }

        if (fadeImage == null)
            fadeImage = GetComponentInChildren<Image>();

        alpha = fadeImage.color.a;
    }

    public void StartFadeIn()
    {
        if (!isFading)
            StartCoroutine(FadeIn());
    }

    IEnumerator FadeIn()
    {
        isFading = true;
        fadeImage.enabled = true;

        while (alpha > 0)
        {
            alpha -= fadeSpeed * Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        alpha = 0;
        fadeImage.color = new Color(0, 0, 0, 0);
        fadeImage.enabled = false;
        isFading = false;
    }

    public void StartFadeOut()
    {
        if (!isFading)
            StartCoroutine(FadeOut());
    }

    IEnumerator FadeOut()
    {
        isFading = true;
        fadeImage.enabled = true;

        while (alpha < 1)
        {
            alpha += fadeSpeed * Time.deltaTime;
            fadeImage.color = new Color(0, 0, 0, alpha);
            yield return null;
        }

        alpha = 1;
        fadeImage.color = new Color(0, 0, 0, 1);
        isFading = false;
    }

    public bool IsFading => isFading;
}
