using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public FlameLaserSpawner laserSpawner;
    public float attackDuration = 0.2f;
    public PlayerLife playerLife;         // Inspectorでセット
    public float timeCostPerAttack = 10f; // 攻撃で減らす時間

    [Header("Attack Sound")]
    public AudioClip flameSE;             // 炎の効果音（SoundManagerのSEリストに登録しておく）

    private PlayerMovement movement;
    private bool isAttacking = false;     // 攻撃中フラグ

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            StartCoroutine(Attack());
        }
    }

    private IEnumerator Attack()
    {
        if (isAttacking) yield break; // 二重攻撃防止
        isAttacking = true;

        movement.canMove = false;

        // 時間ゲージを減らす
        //if (playerLife != null)
        //    playerLife.ReduceTime(timeCostPerAttack);

        // 攻撃方向
        Vector2 dir = movement.LastMoveDirection;
        Vector3 spawnPos = transform.position + (Vector3)(dir * 0.5f);
        laserSpawner.Shoot(spawnPos, dir);

        // SoundManager経由でSE再生
        if (SoundManager.Instance != null && flameSE != null)
        {
            SoundManager.Instance.PlaySE("火炎魔法1 (1)");
        }

        yield return new WaitForSeconds(attackDuration);

        movement.canMove = true;
        isAttacking = false;
    }
}
