using System.Collections.Generic;
using UnityEngine;

public class FlameLaserPiece : MonoBehaviour
{
    public int damage = 10;
    public LayerMask enemyLayer;

    private HashSet<GameObject> hitEnemies;

    // 外部からセットする
    public void Init(HashSet<GameObject> hitSet)
    {
        hitEnemies = hitSet;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (((1 << other.gameObject.layer) & enemyLayer) != 0)
        {
            if (!hitEnemies.Contains(other.gameObject))
            {
                var enemy = other.GetComponent<EnemyHealth>();
                if (enemy != null)
                {
                    enemy.TakeDamage(damage);
                    hitEnemies.Add(other.gameObject);
                }
            }
        }
    }
}
