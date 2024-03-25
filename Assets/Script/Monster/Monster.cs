using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster : MonoBehaviour
{
    [SerializeField] private float soundDetectionRadius = 5f;

    private void Update()
    {
        DetectSound();
    }

    private void DetectSound()
    {
        // Check if there are any sound objects within the detection radius
        Collider2D[] colliders = Physics2D.OverlapCircleAll(transform.position, soundDetectionRadius);

        foreach (Collider2D collider in colliders)
        {
            // Check if the sound object belongs to the player
            if (collider.CompareTag("PlayerSound"))
            {
                // You can add further checks here if needed, like line of sight check

                // Player sound detected, implement your monster's behavior here
                Debug.Log("Player sound detected!");

                // For example, you can make the monster move towards the player
                // or trigger an attack, etc.
            }
        }
    }

    private void OnDrawGizmosSelected()
    {
        // Visualize the detection radius in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, soundDetectionRadius);
    }
}
