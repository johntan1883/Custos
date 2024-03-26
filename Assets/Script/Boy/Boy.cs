using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boy : MonoBehaviour
{
    public bool IsHoldingKey = false;

    [SerializeField] private GameObject CheckForInteractObject;
    [SerializeField] private Animator anim;
    [SerializeField] private GameObject Sprite;

    [SerializeField] private float movingSpeed = 5f;
    private Transform targetTransform; // The target transform for the boy to follow

    private void Start()
    {
        anim = GetComponent<Animator>();
    }
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
            anim.SetBool("isWalkingB", true);

            // Check if the boy has reached close to the target transform, then stop moving
            float distance = Vector3.Distance(transform.position, targetTransform.position);
            if (distance < 0.1f)
            {
                // Stop moving
                targetTransform = null;
            }
        }
    }

    public void CheckForInteract()
    {
        CheckForInteractObject.SetActive(true);

        StartCoroutine(FlipSpriteTwice());
    }

    IEnumerator FlipSpriteTwice()
    {
        SpriteRenderer spriteRenderer = Sprite.GetComponent<SpriteRenderer>();
        if (spriteRenderer != null)
        {
            // Flip sprite horizontally
            spriteRenderer.flipX = !spriteRenderer.flipX;
            yield return new WaitForSeconds(0.5f); // Wait for a brief moment

            // Flip sprite back to original orientation
            spriteRenderer.flipX = !spriteRenderer.flipX;
        }
        else
        {
            Debug.LogError("SpriteRenderer component not found on spriteObject.");
        }
    }
}