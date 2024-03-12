using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindBoyMovement : MonoBehaviour
{
    [SerializeField] private GameObject dog;
    [SerializeField] private float defaultMovingSpeed = 1.5f;
    [SerializeField] private float followingDogSpeed = 15f; // Speed when following dog
    [SerializeField] private float yOffset = 0.5f; // Offset in the y direction
    public float fixedZPosition = 5f;

    private PlayerMovement playerMovement;

    private void Start()
    {
        playerMovement = dog.GetComponent<PlayerMovement>();
    }

    private void Update()
    {
        if (playerMovement.IsFollowing)
        {
            Vector2 targetPosition = new Vector2(dog.transform.position.x, Mathf.Max(dog.transform.position.y + yOffset, transform.position.y));
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, defaultMovingSpeed * Time.deltaTime);
        }
        if (playerMovement.IsFollowingDog)
        {
            Vector2 targetPosition = new Vector2(dog.transform.position.x, Mathf.Max(dog.transform.position.y + yOffset, transform.position.y));
            transform.position = Vector2.MoveTowards(transform.position, targetPosition, followingDogSpeed * Time.deltaTime);
        }

        // Get the current position
        Vector3 currentPosition = transform.position;

        // Set the z position to the desired fixed z position
        currentPosition.z = fixedZPosition;

        // Apply the new position
        transform.position = currentPosition;
    }
}
