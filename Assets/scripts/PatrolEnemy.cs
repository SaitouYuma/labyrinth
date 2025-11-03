using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class PatrolEnemy : MonoBehaviour
{
    public float speed = 2f;
    public float sidestepChance = 0.2f; // ���Ɉ���m��

    private Vector2 direction = Vector2.right;
    private Rigidbody2D rb;
    private bool isAlive = true;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();

        // GameMaster �ɓG�o�^
        if (DungeonManager.Instance != null)
            DungeonManager.Instance.RegisterEnemy();
    }

    void Update()
    {
        // �������̔��]
        if (direction.x > 0.01f)
            transform.localScale = new Vector3(1, 1, 1);
        else if (direction.x < -0.01f)
            transform.localScale = new Vector3(-1, 1, 1);
    }

    void FixedUpdate()
    {
        if (!isAlive) return;

        rb.linearVelocity = direction * speed;

        // �ǔ���
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, 0.6f, LayerMask.GetMask("Wall"));
        if (hit.collider != null)
        {
            // ��m���ŉ��Ɉ���
            if (Random.value < sidestepChance)
            {
                if (direction == Vector2.up || direction == Vector2.down)
                    direction = Random.value < 0.5f ? Vector2.left : Vector2.right;
                else
                    direction = Random.value < 0.5f ? Vector2.up : Vector2.down;
            }
            else
            {
                direction = -direction; // ���]
            }
        }

        // �㉺���E�݂̂ɐ���
        if (Mathf.Abs(direction.x) > 0.01f) direction.y = 0;
        if (Mathf.Abs(direction.y) > 0.01f) direction.x = 0;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!isAlive) return;

        if (other.CompareTag("Player"))
        {
            
            //PlayerLife playerLife = other.GetComponent<PlayerLife>();
            //if (playerLife != null && !playerLife.isDead)
            //{
            //    playerLife.ReduceTime(playerLife.deathPenalty);
            //    StartCoroutine(playerLife.DeathAndRespawn());
            //}

            
        }

        if (other.CompareTag("PlayerAttack"))
        {
            Die(); // �U�����ꂽ��G�͓|���
        }
    }

    public void Die()
    {
        if (!isAlive) return;

        isAlive = false;
        gameObject.SetActive(false);

        // GameMaster �ɒʒm
        if (DungeonManager.Instance != null)
            DungeonManager.Instance.EnemyDefeated();
    }
}
