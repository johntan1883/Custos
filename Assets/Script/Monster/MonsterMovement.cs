using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MonsterMovement : MonoBehaviour
{
    [SerializeField] private Transform playerTransform; // Reference to the player's transform
    [SerializeField] private float moveSpeed = 3f; // Speed at which the monster moves towards the player
    [SerializeField] private AudioClip breakGlassSound;

    private void Start()
    {
        StartCoroutine(StartDelay());
    }

    private IEnumerator StartDelay()
    {
        yield return new WaitForSeconds(1f);

        GameObject playerObject = GameObject.FindWithTag("Player");

        // Check if the player GameObject was found
        if (playerObject != null)
        {
            // Get the player's transform
            playerTransform = playerObject.transform;
        }
        else
        {
            Debug.LogError("Player GameObject not found");
        }
    }
    private void FixedUpdate()
    {
        if (playerTransform != null)
        {
            // Calculate the direction from the monster to the player
            Vector3 direction = (playerTransform.position - transform.position).normalized;

            // Move the monster towards the player
            transform.Translate(direction * moveSpeed * Time.fixedDeltaTime);
        }
    }

    public void SetPlayerTransform(Transform player)
    {
        playerTransform = player;
    }
}
