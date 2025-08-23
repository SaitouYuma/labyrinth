using UnityEngine;

public class EnemyPatrol : MonoBehaviour
{
    public float speed = 2f;
    public float chaseRange = 5f;

    private Transform player;
    private SpriteRenderer spriteRenderer;
    private Vector2 currentDirection;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player").transform;
        spriteRenderer = GetComponent<SpriteRenderer>();

        // 初期の巡回方向（右向き）
        currentDirection = Vector2.right;
    }

    void Update()
    {
        Vector2 toPlayer = player.position - transform.position;

        // プレイヤーを感知したら追尾
        if (toPlayer.magnitude < chaseRange)
        {
            Vector2 moveDir;
            if (Mathf.Abs(toPlayer.x) > Mathf.Abs(toPlayer.y))
            {
                moveDir = new Vector2(Mathf.Sign(toPlayer.x), 0);
                spriteRenderer.flipX = moveDir.x < 0;
            }
            else
            {
                moveDir = new Vector2(0, Mathf.Sign(toPlayer.y));
            }

            if (!IsBlocked(moveDir))
                currentDirection = moveDir;
        }
        else
        {
            // 壁にぶつかったら反対方向に
            if (IsBlocked(currentDirection))
                currentDirection = -currentDirection;

            // 左右向き調整
            if (currentDirection.x != 0)
                spriteRenderer.flipX = currentDirection.x < 0;
        }

        // 移動
        transform.position += (Vector3)(currentDirection * speed * Time.deltaTime);
    }

    bool IsBlocked(Vector2 direction)
    {
        float distance = 0.6f; // タイル1マスより少し大きめ
        RaycastHit2D hit = Physics2D.Raycast(transform.position, direction, distance, LayerMask.GetMask("Wall"));
        return hit.collider != null;
    }
}
