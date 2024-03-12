using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BlindBoyMovement : MonoBehaviour
{
    [SerializeField] private float movingSpeed = 1.5f;

    private GameObject followObject;
    
    private void Update()
    {
        if (followObject != null)
        {
            transform.position = Vector2.MoveTowards(transform.position, followObject.transform.position, movingSpeed * Time.deltaTime);
        }
    }

    public void SetFollowObject(GameObject newFollowObject)
    {
        followObject = newFollowObject;
    }
}
