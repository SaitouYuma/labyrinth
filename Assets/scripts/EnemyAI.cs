using UnityEngine;

public class EnemyAI : MonoBehaviour
{
    public float speed = 2f;
    public LayerMask wallLayer;

    private Transform player;
    private bool isChasing = false;
    private Rigidbody2D rb;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        if (!isChasing || player == null) return;

        Vector2 direction = (player.position - transform.position).normalized;

        // 壁チェック
        RaycastHit2D hit = Physics2D.Raycast(rb.position, direction, 0.5f, wallLayer);
        if (hit.collider != null)
        {
            // 壁回避（左右どちらかにずらす）
            Vector2 perp = Vector2.Perpendicular(direction);
            direction = (Random.value > 0.5f) ? perp : -perp;
        }

        // 移動
        rb.MovePosition(rb.position + direction * speed * Time.fixedDeltaTime);

        // 左右反転
        if (direction.x > 0) transform.localScale = new Vector3(1, 1, 1);
        else if (direction.x < 0) transform.localScale = new Vector3(-1, 1, 1);
    }

    public void StartChase() { isChasing = true; }
    public void StopChase() { isChasing = false; }
}
