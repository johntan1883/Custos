using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DetectForBoyObject : MonoBehaviour
{
    [SerializeField] private GameObject Door;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("BlindBoy"))
        {
            // Get the component attached to the parent GameObject
            DoorInteractionForBoy doorInteraction = Door.GetComponent<DoorInteractionForBoy>();

            if (doorInteraction != null)
            {
                doorInteraction.BoyInteract();
                Door.GetComponent<Collider2D>().enabled = false;
            }
            else
            {
                Debug.LogError("DoorInteractionForBoy component not found on parent GameObject.");
            }
            Debug.Log("Boy Enter");
        }
    }
}
