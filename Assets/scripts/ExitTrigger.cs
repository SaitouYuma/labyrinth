using UnityEngine;

public class ExitTrigger : MonoBehaviour
{
    public ScreenFader screenFader;  // Inspector‚ÅƒZƒbƒg
    public string sceneToLoad = "TitleScene";

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("Player"))
        {
            screenFader.FadeOutAndLoad(sceneToLoad);
        }
    }
}
