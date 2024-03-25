using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Interactable : MonoBehaviour
{
    //public bool IsInRange;
    //public KeyCode InteractKey;
    //public UnityEvent InteractAction;

    //void Update()
    //{
    //    if (IsInRange)
    //    {
    //        if (Input.GetKeyDown(InteractKey))
    //        {
    //            InteractAction.Invoke();//Begin Event
    //        }
    //    }
    //}

    //public void BoyInteract()
    //{
    //    Debug.Log("Interacting with " + gameObject.name);
    //}
    //private void OnTriggerEnter2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        IsInRange = true;
    //        Debug.Log("Player now in range");
    //    }
    //}
    //private void OnTriggerExit2D(Collider2D collision)
    //{
    //    if (collision.gameObject.CompareTag("Player"))
    //    {
    //        IsInRange = false;
    //        Debug.Log("Player now not in range");
    //    }
    //}

    public bool CanInteract = true;

    public virtual void Interact()
    {
        // This method will be overridden by specific interactable objects
        Debug.Log("Interacting with " + gameObject.name);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (CanInteract && collision.CompareTag("Player"))
        {
            Interact();
        }
    }
}
