using UnityEngine;

public class SceneBGMSetter : MonoBehaviour
{ 
    void Start()
    {
        if (SoundManager.Instance != null)
        {
            SoundManager.Instance.PlayBGM("maou_game_theme13");
        }
    }
}
