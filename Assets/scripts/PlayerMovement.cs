using UnityEngine;

[RequireComponent(typeof(Animator), typeof(SpriteRenderer), typeof(Rigidbody2D))]
public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 5f;

    private Animator animator;
    private SpriteRenderer spriteRenderer;
    private Rigidbody2D rb;
    private Vector2 movement;
    private string lastDirection = "Down"; // ÅŒã‚ÌŒü‚«‚ğ‹L˜^

    [HideInInspector] public Vector2 LastMoveDirection = Vector2.right;

    [HideInInspector] public bool canMove = true; // UŒ‚’†‚ÉˆÚ“®’â~—p

    void Start()
    {
        animator = GetComponent<Animator>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        rb = GetComponent<Rigidbody2D>();
        rb.gravityScale = 0;
        rb.freezeRotation = true;
    }

    void Update()
    {
        if (!canMove)
        {
            animator.SetBool("isWalking", false);
            ApplyLastDirection(); // Œü‚«‚¾‚¯ˆÛ
            return;
        }

        // “ü—Íæ“¾
        movement.x = Input.GetAxisRaw("Horizontal");
        movement.y = Input.GetAxisRaw("Vertical");

        if (movement != Vector2.zero)
        {
            animator.SetBool("isWalking", true);

            // ---------------------------
            // Œü‚«‚Ì”»’è
            // ---------------------------
            if (Mathf.Abs(movement.x) > Mathf.Abs(movement.y))
            {
                lastDirection = (movement.x > 0) ? "Right" : "Left";

                animator.SetFloat("moveX", 1);
                animator.SetFloat("moveY", 0);

                spriteRenderer.flipX = movement.x > 0;
            }
            else
            {
                lastDirection = (movement.y > 0) ? "Up" : "Down";

                animator.SetFloat("moveX", 0);
                animator.SetFloat("moveY", movement.y > 0 ? 1 : -1);

                spriteRenderer.flipX = false;
            }

            // ---------------------------
            // ÅŒã‚É“®‚¢‚½•ûŒü‚ğƒxƒNƒgƒ‹‚Å•Û‘¶
            // ---------------------------
            LastMoveDirection = movement.normalized;
        }
        else
        {
            animator.SetBool("isWalking", false);
            ApplyLastDirection(); // ’â~’†‚àŒü‚«ˆÛ
        }
    }


    void FixedUpdate()
    {
        if (!canMove) return; // UŒ‚’†‚ÍˆÚ“®’â~

        Vector2 normalizedMovement = movement.normalized;
        rb.MovePosition(rb.position + normalizedMovement * moveSpeed * Time.fixedDeltaTime);
    }

    /// <summary>
    /// lastDirection ‚É‰‚¶‚Ä Animator ‚Æ flipX ‚ğXV
    /// </summary>
    private void ApplyLastDirection()
    {
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
                animator.SetFloat("moveX", 1);
                animator.SetFloat("moveY", 0);
                spriteRenderer.flipX = true;
                break;
            case "Left":
                animator.SetFloat("moveX", 1);
                animator.SetFloat("moveY", 0);
                spriteRenderer.flipX = false;
                break;
        }
    }
}
