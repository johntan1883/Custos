using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyReactSound : MonoBehaviour
{
    [SerializeField] private float runSpeed = 5f;
    private bool isRunning = false;
    private Vector2 runDirection;

    void Update()
    {
        if (isRunning)
        {
            transform.Translate(runDirection * runSpeed * Time.deltaTime);
        }
    }

    public void ReactToSound(Vector3 soundPosition)
    {
        // Determine the direction to run away from the sound
        runDirection = (transform.position - soundPosition).normalized;
        isRunning = true;

        // Optionally, you can add a timer to stop the NPC after a while
        Invoke("StopRunning", 2f);
    }

    private void StopRunning()
    {
        isRunning = false;
    }
}
