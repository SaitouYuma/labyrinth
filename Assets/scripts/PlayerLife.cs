using UnityEngine;
using UnityEngine.UI;
using System.Collections;
using UnityEngine.SceneManagement;

public class PlayerLife : MonoBehaviour
{
    [Header("Respawn & Invincible")]
    public Vector2 respawnPosition;
    public float respawnDelay = 3f;
    public float invincibleTime = 3f;
    public float blinkInterval = 0.2f;

    [Header("Circular Time Meter")]
    public Image fillImage;
    public float totalTime = 100f;
    public float timeDecreasePerSecond = 1f;
    public float deathPenalty = 10f;

    [Header("Sounds")]
    public AudioClip deathSE;   // やられ音
    public AudioClip respawnSE; // リスポーン音
    private AudioSource audioSource;

    [HideInInspector] public float currentTime;
    public bool isDead = false;
    private SpriteRenderer spriteRenderer;
    private Collider2D mainCollider;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        // AudioSourceを確保（なければ追加）
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }

        respawnPosition = transform.position;
        currentTime = totalTime;
        UpdateMeter();
    }

    

    private void UpdateMeter()
    {
        if (fillImage != null)
        {
            fillImage.fillAmount = currentTime / totalTime;
        }
    }

    

    

    public IEnumerator DeathAndRespawn()
    {
        isDead = true;

        // 🎵 死亡音を再生
        if (deathSE != null)
        {
            audioSource.PlayOneShot(deathSE);
        }

        mainCollider.enabled = false;
        rb.simulated = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(respawnDelay);

        transform.position = respawnPosition;
        mainCollider.enabled = true;
        rb.simulated = true;
        spriteRenderer.enabled = true;

        // 🎵 リスポーン音を再生
        if (respawnSE != null)
        {
            audioSource.PlayOneShot(respawnSE);
        }

        
        StartCoroutine(InvincibleBlink());

        yield return new WaitForSeconds(invincibleTime);

        
        spriteRenderer.enabled = true;
        isDead = false;
    }

    private IEnumerator InvincibleBlink()
    {
        float timer = 0f;
        while (timer < invincibleTime)
        {
            spriteRenderer.enabled = !spriteRenderer.enabled;
            yield return new WaitForSeconds(blinkInterval);
            timer += blinkInterval;
        }
        spriteRenderer.enabled = true;
    }
}
