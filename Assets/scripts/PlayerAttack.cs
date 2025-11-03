using UnityEngine;
using System.Collections;

public class PlayerAttack : MonoBehaviour
{
    public FlameLaserSpawner laserSpawner;
    public float attackDuration = 0.2f;
    public PlayerLife playerLife;         // ← PlayerLifeをInspectorでセット
    public float timeCostPerAttack = 10f; // ← 攻撃で減らす時間

    [Header("Attack Sound")]
    public AudioClip flameSE;             // ← 炎の効果音
    private AudioSource audioSource;

    private PlayerMovement movement;

    private void Start()
    {
        movement = GetComponent<PlayerMovement>();

        // AudioSourceを取得（なければ自動追加）
        audioSource = GetComponent<AudioSource>();
        if (audioSource == null)
        {
            audioSource = gameObject.AddComponent<AudioSource>();
        }
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

        //// 時間ゲージを減らす
        //if (playerLife != null)
        //{
        //    playerLife.ReduceTime(timeCostPerAttack);
        //}

        // 炎を出す
        Vector2 dir = GetLastMoveDirection();
        Vector3 spawnPos = transform.position + (Vector3)(dir * 0.5f);
        laserSpawner.Shoot(spawnPos, dir);

        // 🔊 効果音を鳴らす
        if (flameSE != null)
        {
            audioSource.PlayOneShot(flameSE);
        }

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
