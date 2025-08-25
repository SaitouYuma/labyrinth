using UnityEngine;

public class VisionRange : MonoBehaviour
{
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            other.GetComponent<EnemyAI>().StartChase();
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.CompareTag("Enemy"))
            other.GetComponent<EnemyAI>().StopChase();
    }
}
