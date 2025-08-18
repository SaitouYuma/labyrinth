using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Vector2 movement;
    private string lastDirection = "Down"; // 最後の向きを記録

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0; // 重力不要
        rb.freezeRotation = true;
    }

    void Update()
    {
        // 入力取得
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        // Animatorと向き判定
        if (movement != Vector2.zero)
        {
            animator.SetBool("isWalking", true);

            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {
                lastDirection = (movement.x > 0) ? "Right" : "Left";

                animator.SetFloat("moveX", 1); // 左向きアニメ基準
                animator.SetFloat("moveY", 0);

                spriteRenderer.flipX = movement.x > 0; // 右なら反転
            }
            else
            {
                lastDirection = (movement.y > 0) ? "Up" : "Down";

                animator.SetFloat("moveX", 0);
                animator.SetFloat("moveY", movement.y > 0 ? 1 : -1);

                spriteRenderer.flipX = false;
            }
        }
        else
        {
            animator.SetBool("isWalking", false);

            switch (lastDirection)
            {
                case "Up":
                    animator.SetFloat("moveX", 0);
                    animator.SetFloat("moveY", 1);
                    spriteRenderer.flipX = false;
                    break;
                case "Down":
                    animator.SetFloat("moveX", 0);
                    animator.SetFloat("moveY", -1);
                    spriteRenderer.flipX = false;
                    break;
                case "Right":
                    animator.SetFloat("moveX", 1); // 左向きアニメ基準
                    animator.SetFloat("moveY", 0);
                    spriteRenderer.flipX = true;
                    break;
                case "Left":
                    animator.SetFloat("moveX", 1); // 左向きアニメ基準
                    animator.SetFloat("moveY", 0);
                    spriteRenderer.flipX = false;
                    break;
            }
        }
    }

    void FixedUpdate()
    {
        // Rigidbodyで移動（衝突を考慮）
        Vector2 normalizedMovement = movement.normalized;
        Vector2 newPos = rb.position + normalizedMovement * moveSpeed * Time.fixedDeltaTime;
        rb.MovePosition(newPos);
    }
}
