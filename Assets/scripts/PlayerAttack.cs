using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public FlameLaserSpawner laserSpawner;
    public float attackDuration = 0.2f;
    public PlayerLife playerLife;         // ← PlayerLifeをInspectorでセット
    public float timeCostPerAttack = 10f; // ← 攻撃で減らす時間

    private PlayerMovement movement;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        movement.canMove = false;

        // 時間ゲージを減らす
        if (playerLife != null)
        {
            playerLife.ReduceTime(timeCostPerAttack);
        }

        // 炎を出す
        Vector2 dir = GetLastMoveDirection();
        Vector3 spawnPos = transform.position + (Vector3)(dir * 0.5f);
        laserSpawner.Shoot(spawnPos, dir);

        yield return new WaitForSeconds(attackDuration);

        movement.canMove = true;
    }

    private Vector2 GetLastMoveDirection()
    {
        float h = Input.GetAxisRaw("Horizontal");
        float v = Input.GetAxisRaw("Vertical");

        if (h > 0) return Vector2.right;
        if (h < 0) return Vector2.left;
        if (v > 0) return Vector2.up;
        if (v < 0) return Vector2.down;

        // 入力なしなら右を向く
        return Vector2.right;
    }
}
