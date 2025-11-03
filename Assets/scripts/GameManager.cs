using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;

public class GameManager : MonoBehaviour
{
    public void SceneChange(int sceneIndex)
    {
        StartCoroutine(ChangeSceneRoutine(sceneIndex));
    }

    public void EndGame()
    {
#if UNITY_EDITOR
        UnityEditor.EditorApplication.isPlaying = false;
#else
        Application.Quit();
#endif
    }

    public void GameOver()
    {
        SceneManager.LoadScene(2);
    }

    private IEnumerator ChangeSceneRoutine(int sceneIndex)
    {
        // Singleton経由でFadeManager呼び出し
        var fade = FadeManager.Instance;

        // フェードアウト開始
        fade.StartFadeOut();
        yield return new WaitUntil(() => !fade.IsFading);

        // シーン切り替え
        SceneManager.LoadScene(sceneIndex);

        // フェードイン開始
        fade.StartFadeIn();
    }
}
