using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy : MonoBehaviour
{
    public bool IsHoldingKey = false;

    [SerializeField] private float movingSpeed = 5f;
    private Transform targetTransform; // The target transform for the boy to follow

    // Method to set the target transform for the boy to follow
    public void SetTargetTransform(Transform target)
    {
        targetTransform = target;
    }

    // Method to clear the target transform (stop following)
    public void ClearTargetTransform()
    {
        targetTransform = null;
    }

    // Method to move the boy towards the target transform
    public void MoveToTarget()
    {
        if (targetTransform != null)
        {
            // Calculate direction towards the target transform
            Vector3 direction = (targetTransform.position - transform.position).normalized;
            direction.y = 0f; // Make sure the boy moves only horizontally

            // Move the boy towards the target transform
            transform.position += direction * movingSpeed * Time.deltaTime;

            // Check if the boy has reached close to the target transform, then stop moving
            float distance = Vector3.Distance(transform.position, targetTransform.position);
            if (distance < 0.1f)
            {
                // Stop moving
                targetTransform = null;
            }
        }
    }
}
