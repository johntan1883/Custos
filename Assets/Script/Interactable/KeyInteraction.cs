using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class KeyInteraction : InteractableBase
{
    [SerializeField] private Transform holdingPosition;
    [SerializeField] private GameObject _interactionObject;

    private bool pickedUpKey;
    public override void Interact()
    {
        if (pickedUpKey)
        {
            DropKey();
        }
        else
        {
            GrabKey();
        }
    }

    private void GrabKey()
    {
        // Ensure the player object is set as the parent of the Interaction object
        _interactionObject.transform.parent = _player.transform;

        // Ensure the Key object is set as the parent of the Interaction object
        gameObject.transform.parent = _interactionObject.transform;

        // Move the Interaction object (which now includes the Key) to the holding position
        _interactionObject.transform.position = holdingPosition.position;

        //
        _interactionObject.GetComponent<Rigidbody2D>().isKinematic = true;

        pickedUpKey = true;

        Debug.Log("PICK UP KEY");
    }

    private void DropKey()
    {
        // Remove the Interaction object from being a child of the player
        _interactionObject.transform.parent = null;

        _interactionObject.GetComponent<Rigidbody2D>().isKinematic = false;

        pickedUpKey = false;

        Debug.Log("DROP KEY");
    }
}
