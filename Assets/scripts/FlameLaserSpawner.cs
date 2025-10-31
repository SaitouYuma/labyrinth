using System.Collections.Generic;
using UnityEngine;

public class FlameLaserSpawner : MonoBehaviour
{
    public GameObject bodyPrefab;
    public GameObject endPrefab;
    public LayerMask wallLayer;
    public LayerMask enemyLayer;
    public float unitLength = 1f;
    public float duration = 0.2f;
    public int damage = 10;
    public float maxDistance = 10f;

    public void Shoot(Vector2 startPos, Vector2 dir)
    {
        dir = dir.normalized;

        // �ǂ܂ł̋������v��
        RaycastHit2D hit = Physics2D.Raycast(startPos, dir, maxDistance, wallLayer);
        float distance = (hit.collider != null) ? hit.distance : maxDistance;

        // �������� Body �̐�
        int bodyCount = Mathf.FloorToInt(distance / unitLength);
        Vector2 currentPos = startPos;

        HashSet<GameObject> hitEnemies = new HashSet<GameObject>();

        // Body ����
        for (int i = 0; i < bodyCount; i++)
        {
            Vector2 spawnPos = currentPos + dir * (unitLength / 2f);
            GameObject body = Instantiate(bodyPrefab, spawnPos, Quaternion.identity);

            // �����ɉ����ĉ�]
            float angle = GetAngleFromDirection(dir);
            body.transform.rotation = Quaternion.Euler(0, 0, angle);

            // �X�P�[�������i�����X�v���C�g�p�j
            body.transform.localScale = new Vector3(
                dir.x != 0 ? unitLength : 1f,
                dir.y != 0 ? unitLength : 1f,
                1f
            );

            // �����蔻��X�N���v�g
            FlameLaserPiece piece = body.AddComponent<FlameLaserPiece>();
            piece.damage = damage;
            piece.enemyLayer = enemyLayer;
            piece.Init(hitEnemies);

            Destroy(body, duration);
            currentPos += dir * unitLength;
        }

        // End ����
        Vector2 endPos = currentPos;
        GameObject end = Instantiate(endPrefab, endPos, Quaternion.identity);
        float endAngle = GetAngleFromDirection(dir);
        end.transform.rotation = Quaternion.Euler(0, 0, endAngle);

        FlameLaserPiece endPiece = end.AddComponent<FlameLaserPiece>();
        endPiece.damage = damage;
        endPiece.enemyLayer = enemyLayer;
        endPiece.Init(hitEnemies);

        Destroy(end, duration);
    }

    private float GetAngleFromDirection(Vector2 dir)
    {
        if (dir == Vector2.right) return 0f;
        if (dir == Vector2.left) return 180f;
        if (dir == Vector2.up) return 90f;
        if (dir == Vector2.down) return -90f;
        return 0f;
    }
}
