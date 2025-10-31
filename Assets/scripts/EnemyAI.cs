//using UnityEngine;
//using Pathfinding;

//[RequireComponent(typeof(AIPath))]
//[RequireComponent(typeof(Seeker))]
//[RequireComponent(typeof(Rigidbody2D))]
//public class EnemyAI : MonoBehaviour
//{
//    public bool IsAlive = true;
//    public Transform player;

//    private AIPath aiPath;

//    void Start()
//    {
//        aiPath = GetComponent<AIPath>();
//        if (player == null)
//            player = GameObject.FindWithTag("Player")?.transform;

//        // 登録処理
//        if (GameMaster.Instance != null)
//            GameMaster.Instance.RegisterEnemy();
//    }


//    void Update()
//    {
//        if (!IsAlive || player == null) return;

//        aiPath.destination = player.position;

//        // 左右反転
//        if (aiPath.desiredVelocity.x >= 0.01f)
//            transform.localScale = new Vector3(1, 1, 1);
//        else if (aiPath.desiredVelocity.x <= -0.01f)
//            transform.localScale = new Vector3(-1, 1, 1);
//    }

//    public void Die()
//    {
//        if (!IsAlive) return;
//        IsAlive = false;
//        gameObject.SetActive(false);

//        if (GameMaster.Instance != null)
//            GameMaster.Instance.EnemyDefeated();
//    }
//}
