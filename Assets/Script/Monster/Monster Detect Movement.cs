using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MonsterDetectMovement : MonoBehaviour
{
    public Transform platform;
    public Transform startPoint;
    public Transform endPoint;
    public Transform windowThree;
    public float speed = 1.5f;
    int targetIndex = 0;
    Vector3[] targets;

    private void Start()
    {
        // Define the sequence of movement targets
        targets = new Vector3[] { startPoint.position, endPoint.position, windowThree.position, endPoint.position };
    }

    private void Update()
    {
        // Get the current movement target
        Vector3 target = targets[targetIndex];

        // Move the platform towards the target
        platform.position = Vector3.Lerp(platform.position, target, speed * Time.deltaTime);

        // Check if the platform has reached close enough to the target
        float distance = Vector3.Distance(platform.position, target);
        if (distance <= 0.1f)
        {
            // Move to the next target index
            targetIndex = (targetIndex + 1) % targets.Length;
        }
    }
}


