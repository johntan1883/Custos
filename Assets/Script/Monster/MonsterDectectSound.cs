using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterDectectSound : MonoBehaviour
{
    [SerializeField] private GameObject monsterPrefab;
    [SerializeField] private float soundDetectionRadius = 5f;
    [SerializeField] private AudioClip breakGlassSound;

    private bool isDetectingSound = true;

    private void Update()
    {
        if (isDetectingSound)
        {
            DetectSound();

            
        }
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
                // Player sound detected, spawn a new monster and disable this one
                SpawnMonster();
                SoundFXManager.Instance.PlaySoundFXClip(breakGlassSound, transform, 0.3f, "SFX");
                DisableMonster();
                break; // Exit the loop after spawning one monster
            }
        }
    }

    private void SpawnMonster()
    {
        // Instantiate the monster prefab at the same position as this monster
        GameObject newMonster = Instantiate(monsterPrefab, transform.position, Quaternion.identity);
    }

    private void DisableMonster()
    {
        // Disable this monster GameObject
        gameObject.SetActive(false);
    }

    #region Debug Function
    private void OnDrawGizmosSelected()
    {
        // Visualize the detection radius in the editor
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, soundDetectionRadius);
    }
    #endregion
}
