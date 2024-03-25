using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoyInteraction : InteractableBase
{
    [SerializeField] private Transform boyHoldingPosition;
    [SerializeField] private GameObject _interactionObject;

    public bool isHoldingDog;

    public override void Interact()
    {
        if (isHoldingDog)
        {
            BoyLetGoDog();
        }
        else
        {
            BoyHoldDog();
        }
    }

    private void BoyHoldDog()
    {
        // Ensure the player object is set as the parent of the Interaction object
        _interactionObject.transform.parent = _player.transform;

        // Ensure the Key object is set as the parent of the Interaction object
        gameObject.transform.parent = _interactionObject.transform;

        // Move the Interaction object (which now includes the Key) to the holding position
        _interactionObject.transform.position = boyHoldingPosition.position;

        //
        _interactionObject.GetComponent<Rigidbody2D>().isKinematic = true;

        isHoldingDog = true;

        Debug.Log("Boy is holding dog");
    }

    private void BoyLetGoDog()
    {
        // Remove the Interaction object from being a child of the player
        _interactionObject.transform.parent = null;

        _interactionObject.GetComponent<Rigidbody2D>().isKinematic = false;

        isHoldingDog = false;

        Debug.Log("Boy is not holding dog");
    }

}
