using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Vector2 movement;
    private string lastDirection = "Down"; // 最後の向きを記録

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    void Update()
    {
        // 入力取得
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        Vector2 normalizedMovement = movement.normalized;

        // 移動
        transform.position += new Vector3(normalizedMovement.x, normalizedMovement.y, 0) * moveSpeed * Time.deltaTime;

        if (movement != Vector2.zero)
        {
            animator.SetBool("isWalking", true);

            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {
                lastDirection = (movement.x > 0) ? "Right" : "Left";

                // 左向きスプライト基準でAnimatorに渡す
                animator.SetFloat("moveX", 1); // 左向きアニメを基準
                animator.SetFloat("moveY", 0);

                // 左向きスプライトを左右反転
                spriteRenderer.flipX = movement.x > 0; // 右移動なら反転
            }
            else
            {
                lastDirection = (movement.y > 0) ? "Up" : "Down";

                animator.SetFloat("moveX", 0);
                animator.SetFloat("moveY", movement.y > 0 ? 1 : -1);

                spriteRenderer.flipX = false; // 上下は反転不要
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
                    animator.SetFloat("moveX", 1);  // 左向きアニメ基準
                    animator.SetFloat("moveY", 0);
                    spriteRenderer.flipX = true;   // 左向きスプライトを右向きに
                    break;
                case "Left":
                    animator.SetFloat("moveX", 1);  // 左向きアニメ基準
                    animator.SetFloat("moveY", 0);
                    spriteRenderer.flipX = false;  // 左向きスプライト
                    break;
            }
        }
    }
}
