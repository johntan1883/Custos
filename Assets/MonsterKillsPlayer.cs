using UnityEngine;

public class MonsterKillsPlayer : MonoBehaviour
{

    private void OnTriggerEnter2D(Collider2D other)
    {
        // Check if the colliding object is the player
        if (other.CompareTag("Player"))
        {
            // Destroy the player object
            Destroy(other.gameObject);
        }
    }
}
