using UnityEngine;
using UnityEngine.SceneManagement;

public class SoundManager : MonoBehaviour
{
    public static SoundManager Instance;

    [Header("Audio Sources")]
    public AudioSource _bgmSource; // BGM再生用
    public AudioSource _seSource;  // SE再生用

    [Header("Audio Clips")]
    [SerializeField] private AudioClip[] _bgmClips;  // 登録しておくBGM
    [SerializeField] private AudioClip[] _seClips;   // 登録しておくSE

    private void Awake()
    {
        // シングルトン化（重複回避 & シーンをまたいで保持）
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);

            SceneManager.sceneLoaded += OnSceneLoaded;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
    }

    private void OnDestroy()
    {
        SceneManager.sceneLoaded -= OnSceneLoaded;
    }

    private void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        StopBGM();
    }

    public void PlayBGM(string name)
    {
        AudioClip clip = FindClip(_bgmClips, name);
        if (clip == null) return;

        // 同じBGMなら再生し直さない
        if (_bgmSource.clip == clip) return;

        _bgmSource.clip = clip;
        _bgmSource.Play();
    }

    public void StopBGM()
    {
        _bgmSource.Stop();
    }

    public void PlaySE(string name)
    {
        AudioClip clip = FindClip(_seClips, name);
        if (clip != null)
        {
            _seSource.PlayOneShot(clip); // 前のSEを消さずに再生
        }
    }

    private AudioClip FindClip(AudioClip[] clips, string name)
    {
        foreach (var clip in clips)
        {
            if (clip != null && clip.name == name)
                return clip;
        }
        return null;
    }
}
