using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DoorController : MonoBehaviour
{
    public bool IsOpen;
    public GameObject doorCloseSprite;
    public GameObject doorOpenSprite;

    private List<string> requiredTags = new List<string>() { "BlindBoy", "Player", "Key" };
    private List<GameObject> objectsInRange = new List<GameObject>();

    public void OpenDoor()
    {
        Debug.Log("Attempting to open door. Objects in range: " + objectsInRange.Count);

        if (AreObjectsInRange())
        {
            IsOpen = true;
            doorCloseSprite.SetActive(false);
            doorOpenSprite.SetActive(true);
            Debug.Log("Door opened!");
        }
        else
        {
            Debug.Log("Cannot open door. Required objects are missing!");
        }
    }

    private bool AreObjectsInRange()
    {
        foreach (var tag in requiredTags)
        {
            bool tagFound = false;
            foreach (var obj in objectsInRange)
            {
                if (obj != null && obj.CompareTag(tag))
                {
                    tagFound = true;
                    break;
                }
            }
            if (!tagFound)
            {
                return false;
            }
        }
        return true;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (requiredTags.Contains(other.tag))
        {
            objectsInRange.Add(other.gameObject);
            Debug.Log("Object entered range: " + other.tag);
            Debug.Log("Total objects in range: " + objectsInRange.Count);
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (requiredTags.Contains(other.tag))
        {
            objectsInRange.Remove(other.gameObject);
            Debug.Log("Object exited range: " + other.tag);
            Debug.Log("Total objects in range: " + objectsInRange.Count);
        }
    }
}
