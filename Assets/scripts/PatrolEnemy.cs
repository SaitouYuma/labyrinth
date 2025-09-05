using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PatrolEnemy : MonoBehaviour
{
    public float speed = 2f;
    public float sidestepChance = 0.2f; // ‰¡‚Éˆí‚ê‚éŠm—¦

    private Vector2 direction = Vector2.right;
    private Rigidbody2D rb;
    private bool isAlive = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // GameMaster ‚É“G“o˜^
        if (GameMaster.Instance != null)
            GameMaster.Instance.RegisterEnemy();
    }

    void Update()
    {
        // ‰¡Œü‚«‚Ì”½“]
        if (direction.x > 0.01f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (direction.x < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void FixedUpdate()
    {
        if (!isAlive) return;

        rb.velocity = direction * speed;

        // •Ç”»’è
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.6f, LayerMask.GetMask("Wall"));
        if (hit.collider != null)
        {
            // ’áŠm—¦‚Å‰¡‚Éˆí‚ê‚é
            if (Random.value < sidestepChance)
            {
                if (direction == Vector2.up || direction == Vector2.down)
                    direction = Random.value < 0.5f ? Vector2.left : Vector2.right;
                else
                    direction = Random.value < 0.5f ? Vector2.up : Vector2.down;
            }
            else
            {
                direction = -direction; // ”½“]
            }
        }

        // ã‰º¶‰E‚Ì‚İ‚É§–ñ
        if (Mathf.Abs(direction.x) > 0.01f) direction.y = 0;
        if (Mathf.Abs(direction.y) > 0.01f) direction.x = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isAlive) return;

        if (other.CompareTag("Player"))
        {
            // ƒvƒŒƒCƒ„[‚ÌŠÔ‚ğŒ¸‚ç‚µA€–Sˆ—ŠJn
            PlayerLife playerLife = other.GetComponent<PlayerLife>();
            if (playerLife != null && !playerLife.isDead)
            {
                playerLife.ReduceTime(playerLife.deathPenalty);
                StartCoroutine(playerLife.DeathAndRespawn());
            }

            // “G‚Í€‚È‚È‚¢iG‚ê‚½‚ç“|‚·d—l‚È‚ç‚±‚±‚Íc‚·j
            // Die();
        }

        if (other.CompareTag("PlayerAttack"))
        {
            Die(); // UŒ‚‚³‚ê‚½‚ç“G‚Í“|‚ê‚é
        }
    }

    public void Die()
    {
        if (!isAlive) return;

        isAlive = false;
        gameObject.SetActive(false);

        // GameMaster ‚É’Ê’m
        if (GameMaster.Instance != null)
            GameMaster.Instance.EnemyDefeated();
    }
}
