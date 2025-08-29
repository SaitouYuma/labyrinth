using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class PlayerLife : MonoBehaviour
{
    [Header("Respawn & Invincible")]
    public Vector2 respawnPosition;
    public float respawnDelay = 3f;
    public float invincibleTime = 3f;
    public float blinkInterval = 0.2f;

    [Header("Circular Time Meter")]
    public Image fillImage;           // 円形 Fill Image
    public float totalTime = 100f;    // 最大値
    public float timeDecreasePerSecond = 1f; // 毎秒減る値
    public float deathPenalty = 10f;  // 死亡時に減る値

    private float currentTime;
    private bool isDead = false;
    private bool isInvincible = false;
    private SpriteRenderer spriteRenderer;
    private Collider2D mainCollider;
    private Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        mainCollider = GetComponent<Collider2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();

        respawnPosition = transform.position;
        currentTime = totalTime;
        UpdateMeter();
    }

    void Update()
    {
        if (!isDead)
        {
            currentTime -= timeDecreasePerSecond * Time.deltaTime;
            currentTime = Mathf.Max(0, currentTime);
            UpdateMeter();

            if (currentTime <= 0)
            {
                GameOver();
            }
        }
    }

    private void UpdateMeter()
    {
        if (fillImage != null)
        {
            fillImage.fillAmount = currentTime / totalTime;
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy") && !isInvincible && !isDead)
        {
            ReduceTime(deathPenalty);
            StartCoroutine(DeathAndRespawn());
        }
    }

    private void ReduceTime(float amount)
    {
        currentTime = Mathf.Max(0, currentTime - amount);
        UpdateMeter();

        if (currentTime <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        Debug.Log("Game Over!");
        // ここにゲームオーバー処理（UI表示やシーン切替）を追加
    }

    private IEnumerator DeathAndRespawn()
    {
        isDead = true;

        // 死亡演出
        mainCollider.enabled = false;
        rb.simulated = false;
        spriteRenderer.enabled = false;

        yield return new WaitForSeconds(respawnDelay);

        // リスポーン
        transform.position = respawnPosition;
        mainCollider.enabled = true;
        rb.simulated = true;
        spriteRenderer.enabled = true;

        // 無敵時間開始
        isInvincible = true;
        StartCoroutine(InvincibleBlink());

        yield return new WaitForSeconds(invincibleTime);

        isInvincible = false;
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
